using LawyerUpBackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerUpBackend.DataAccess.Repositiories.Impl
{
    public class LawyerRepository : BaseRepository<Lawyer>, ILawyerRepostiory
    {
        public LawyerRepository(AppDbContext context) : base(context)
        {
        }

        public Dictionary<string,int> GetAllGuild()
        {
            var count = Context.Lawyers.GroupBy(x => x.Guild_name)
                .Select(n => new
                {
                    GuildName = n.Key,
                    GuildCount = n.Count()
                }).OrderByDescending(y => y.GuildCount);
            Dictionary<string, int> result = new Dictionary<string, int>();
            foreach(var item in count)
            {
                result.Add(item.GuildName, item.GuildCount);
            }
            return result;
                        
        }
    }
}
