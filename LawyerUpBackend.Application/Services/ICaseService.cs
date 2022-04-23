using LawyerUpBackend.Application.Dtos;
using LawyerUpBackend.Application.Models.Case;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerUpBackend.Application.Services
{
    public interface ICaseService
    {
        Task<PagedResultDto<CaseListResponseModel>> SearchCaseListAsync(CaseSearchQueryModel input);
        Task<CaseResponseModel> GetByIdAsync(int id);
    }
}
