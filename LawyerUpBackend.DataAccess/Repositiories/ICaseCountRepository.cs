using LawyerUpBackend.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerUpBackend.DataAccess.Repositiories
{
    public interface ICaseCountRepository
    {
        IQueryable<CaseCountResult> GetAll(string query);
    }
}
