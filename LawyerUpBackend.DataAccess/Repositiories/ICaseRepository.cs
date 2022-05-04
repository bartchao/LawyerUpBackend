using LawyerUpBackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerUpBackend.DataAccess.Repositiories
{
    public interface ICaseRepository : IBaseRepository<Case>
    {
        List<Case> GetAll(string query);
        List<Case> GetAllWithClassification(string query, string classification);
    }

}
