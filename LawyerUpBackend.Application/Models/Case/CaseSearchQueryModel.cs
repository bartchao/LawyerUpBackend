using LawyerUpBackend.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerUpBackend.Application.Models.Case
{
    public class CaseSearchQueryModel: PagedSortedAndFilterInput
    {
        public string SearchQuery { get; set; }
        public string? Classification { get; set; }
    }
}
