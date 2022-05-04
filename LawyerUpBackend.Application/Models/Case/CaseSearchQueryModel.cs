using LawyerUpBackend.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerUpBackend.Application.Models.Case
{
    public class CaseSearchQueryModel : PagedSortedAndFilterInput
    {
        public string SearchQuery { get; set; }
        private string _classification;
        public string? Classification
        {
            get
            {
                return _classification;
            }
            set
            {
                //ex:加重詐欺等 = 加重詐欺
                if (value.EndsWith("等"))
                {
                    _classification = value.Remove(value.Length - 1);
                }
                else
                {
                    _classification = value;
                }
            }
        }
    }
}
