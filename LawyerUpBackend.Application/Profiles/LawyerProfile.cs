using AutoMapper;
using LawyerUpBackend.Application.Models.Case;
using LawyerUpBackend.Application.Models.Lawyer;
using LawyerUpBackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerUpBackend.Application.Profiles
{
    public class LawyerProfile:Profile
    {
        public LawyerProfile()
        {
            CreateMap<Lawyer, LawyerResponseModel>()
                .ForMember(target => target.Age, option => option.MapFrom(source => source.Birthyear))
                .AfterMap((src,dest)=> dest.Age = DateTime.Now.Year-1911-dest.Age);
            CreateMap<Lawyer, LawyerListResponseModel>().ForMember(target => target.Id,option => option.MapFrom(source =>source.UniqueId));
            CreateMap<LawyerCaseMatch, CaseListResponseModel.Lawyer>().ForMember(target => target.Name, option => option.MapFrom(source => source.Lawyer.Name)).ForMember(target => target.Id,option=>option.MapFrom(source =>source.Lawyer.UniqueId));
            CreateMap<Dictionary<string, int>, LawyerGuildCountResponseModel>().ForMember(target => target.GuildName, option => option.MapFrom(source => source.Keys)).ForMember(target => target.GuildCount, option => option.MapFrom(source => source.Values));
        }
    }
    public class LawyerCaseMatchProfile : Profile
    {
        public LawyerCaseMatchProfile()
        {
            //CreateMap<Lawyer, CaseListResponseModel.Lawyer>().ForMember(target => target.Name, option => option.MapFrom(source => source.Lawyer.Name));
        }
    }
}
