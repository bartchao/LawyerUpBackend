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

        IQueryable<Case> ICaseRepository.GetAll(string query)
        {
            var result = Context.Cases.FromSqlInterpolated($"select top(100) percent * from Cases where TRY_CONVERT( INT ,LEN(main_content) - LEN(REPLACE(main_content,{query}, SPACE(LEN({query})-1)))) > 0 order by TRY_CONVERT( INT ,LEN(main_content) - LEN(REPLACE(main_content, {query}, SPACE(LEN({query})-1)))) desc");
            return result;
        }
    }
    public class CaseCountRepository : ICaseCountRepository
    {
        private readonly AppDbContext Context;

        public CaseCountRepository(AppDbContext context)
        {
            Context = context;
        }

        IQueryable<CaseCountResult> ICaseCountRepository.GetAll(string query)
        {
            throw new NotImplementedException();
            //var result = Context.Cases.FromSqlInterpolated($"select top(100) percent * from Cases where TRY_CONVERT( INT ,LEN(main_content) - LEN(REPLACE(main_content,{query}, SPACE(LEN({query})-1)))) > 0 order by TRY_CONVERT( INT ,LEN(main_content) - LEN(REPLACE(main_content, {query}, SPACE(LEN({query})-1)))) desc");
            //return result;
        }
    }
}
