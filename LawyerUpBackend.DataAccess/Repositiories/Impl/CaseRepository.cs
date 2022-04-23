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
            var result = Context.CaseCountResults.FromSqlInterpolated($"select id,type,year,word,number,classification,main_content,judge_date,Result from (SELECT id,type,year,word,number,classification,main_content,judge_date, TRY_CONVERT( INT ,LEN(main_content) - LEN(REPLACE(main_content,{query}, SPACE(LEN({query})-1)))) as Result FROM Cases) p where Result > 0 order by Result desc");
            return result;
        }
    }
}
