using AutoMapper;
using LawyerUpBackend.Application.Dtos;
using LawyerUpBackend.Application.Exceptions;
using LawyerUpBackend.Application.Models.Case;
using LawyerUpBackend.Application.Models.Lawyer;
using LawyerUpBackend.Core.Entities;
using LawyerUpBackend.DataAccess.Repositiories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerUpBackend.Application.Services.Impl
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository repository;
        private readonly IMapper mapper;
        private readonly ILawyerCaseMatchRepository lawyerCaseMatchRepository;

        public CaseService(ICaseRepository _repository,IMapper _mapper, ILawyerCaseMatchRepository _lawyerCaseMatchRepository)
        {
            this.repository = _repository;
            this.mapper = _mapper;
            lawyerCaseMatchRepository = _lawyerCaseMatchRepository;

        }
        public async Task<PagedResultDto<CaseListResponseModel>> SearchCaseListAsync(CaseSearchQueryModel input)
        {
            List<Case> query;
            if (input.Classification != null)
            {
                query = repository.GetAllWithClassification(input.SearchQuery, input.Classification);
            }
            else
            {
                query = repository.GetAll(input.SearchQuery);

            }
            if(query.Count == 0)
            {
                throw new SearchNotFoundException();
            }
            //var count = query.Count();
            //if (count == 0) throw new SearchNotFoundException();
            //query = query.Skip((input.CurrentPage - 1) * input.MaxResultCount).Take(input.MaxResultCount) ;
            //var result = await query.AsNoTracking().ToListAsync();
            var data = mapper.Map<List<CaseListResponseModel>>(query);
            foreach (var item in data)
            {
                var lawyers = await lawyerCaseMatchRepository.GetAllAsync(@case => @case.CaseId == item.Id);
                if (lawyers.Count() > 0)
                {
                    item.Lawyers = mapper.Map<List<CaseListResponseModel.Lawyer>>(lawyers);
                }
            }
            data = data.OrderByDescending(x => x.Lawyers?.Count).ToList();
            var returnValue = new PagedResultDto<CaseListResponseModel>()
            {
                CurrentPage = input.CurrentPage,
                TotalCount = data.Count,
                MaxResultCount = input.MaxResultCount,
                Data = data,
                Sort = input.Sort,
            };
            return returnValue;

        }
        public async Task<CaseResponseModel> GetByIdAsync(int id)
        {
            var @case = await repository.GetFirstAsync(@case => @case.Id == id);
            
            CaseResponseModel response = mapper.Map<CaseResponseModel>(@case);
            if (@case.LawyerCaseMatches.Count > 0)
            {
                response.Lawyer = new List<LawyerResponseModel>();
                @case.LawyerCaseMatches.ToList().ForEach(matches =>
                {
                    response.Lawyer.Add(mapper.Map<LawyerResponseModel>(matches.Lawyer));
                });
            }
            return response;
        }

       
    }
}
