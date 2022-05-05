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
            var lawyer_result = (from @case in Context.Cases
                                join matches in Context.LawyerCaseMatches
                                on @case.Id equals matches.CaseId
                                join lawyer in Context.Lawyers on matches.LawyerId equals lawyer.Id
                                where lawyer.Name.Contains(query)
                                select @case).ToList();
            var classification_result = (from @case in Context.Cases
                                        where @case.Classification.Contains(query)
                                        select @case).ToList();
            //var case_result = from @case in Context.Cases where @case.MainContent.Contains(query) select @case;
            //var lawyer_result = Context.Cases.FromSqlInterpolated($"select Cases.* from Cases inner join Lawyer_Case_match on Cases.id = Lawyer_Case_match.case_id inner join Lawyers on Lawyer_Case_match.lawyer_id = Lawyers.id where Lawyers.name like '{query}'");
            var case_result = Context.Cases.FromSqlInterpolated($"select distinct top(100) percent Cases.* from Cases right join Lawyer_Case_match on Cases.id = Lawyer_Case_match.case_id    where TRY_CONVERT( INT ,LEN(main_content) - LEN(REPLACE(main_content,{query}, SPACE(LEN({query})-1)))) > 0 ").ToList();
            List<Case> returnList = new List<Case>(lawyer_result);
            returnList.AddRange(classification_result);
            returnList.AddRange(@case_result);
            returnList = returnList.GroupBy(x => x.Id).Select(x => x.First()).ToList();
            return returnList;

        }
        List<Case> ICaseRepository.GetAllWithClassification(string query, string classification)
        {
            var result = Context.Cases.FromSqlInterpolated($"select top(100) percent * from Cases where TRY_CONVERT( INT ,LEN(main_content) - LEN(REPLACE(main_content,{query}, SPACE(LEN({query})-1)))) > 0 order by TRY_CONVERT( INT ,LEN(main_content) - LEN(REPLACE(main_content, {query}, SPACE(LEN({query})-1)))) desc");
            result = result.Where(@case => @case.Classification == classification);
            if (result.Count() == 0)
            {
                result = from @case in Context.Cases
                         where @case.Classification.Contains(classification)
                         select @case;
            }
            List<Case> result_list = result.ToList();
            return result_list;
        }
    }

}
