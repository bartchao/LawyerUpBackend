using LawyerUpBackend.Core.Entities;
using LawyerUpBackend.DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LawyerUpBackend.DataAccess.Repositiories.Impl
{
    public class CaseRepository : BaseRepository<Case>, ICaseRepository
    {
        public CaseRepository(AppDbContext context) : base(context)
        {
        }

        List<Case> ICaseRepository.GetAll(string query)
        {
            var lawyer_result = from @case in Context.Cases
                                join matches in Context.LawyerCaseMatches
                                on @case.Id equals matches.CaseId
                                join lawyer in Context.Lawyers on matches.LawyerId equals lawyer.Id
                                where lawyer.Name.Contains(query)
                                select @case;
            var classification_result = from @case in Context.Cases
                                        where @case.Classification.Contains(query)
                                        select @case;
            List<Case> lawyer_reuslt_list = lawyer_result.ToList();
            List<Case> classification_result_list = classification_result.ToList();
            //var case_result = from @case in Context.Cases where @case.MainContent.Contains(query) select @case;
            //var lawyer_result = Context.Cases.FromSqlInterpolated($"select Cases.* from Cases inner join Lawyer_Case_match on Cases.id = Lawyer_Case_match.case_id inner join Lawyers on Lawyer_Case_match.lawyer_id = Lawyers.id where Lawyers.name like '{query}'");
            var result = Context.Cases.FromSqlInterpolated($"select top(100) percent * from Cases where TRY_CONVERT( INT ,LEN(main_content) - LEN(REPLACE(main_content,{query}, SPACE(LEN({query})-1)))) > 0 order by TRY_CONVERT( INT ,LEN(main_content) - LEN(REPLACE(main_content, {query}, SPACE(LEN({query})-1)))) desc");
            List<Case> result_list = result.ToList();
            List<Case> return_list = new List<Case>(result_list);
            return_list.AddRange(lawyer_reuslt_list);
            return_list.AddRange(classification_result_list);
            return return_list;

        }
        List<Case> ICaseRepository.GetAllWithClassification(string query,string classification)
        {
            var result = Context.Cases.FromSqlInterpolated($"select top(100) percent * from Cases where TRY_CONVERT( INT ,LEN(main_content) - LEN(REPLACE(main_content,{query}, SPACE(LEN({query})-1)))) > 0 order by TRY_CONVERT( INT ,LEN(main_content) - LEN(REPLACE(main_content, {query}, SPACE(LEN({query})-1)))) desc");
            result = result.Where(@case => @case.Classification == classification);
            List<Case> result_list = result.ToList();
            return result_list;
        }
    }

}
