using LawyerUpBackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerUpBackend.Application.Models.Case
{
    public class CaseListResponseModel
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public int? Year { get; set; }
        public string? Word { get; set; }
        public int? Number { get; set; }
        public string? Classification { get; set; }
        public string? MainContent { get; set; }
        public DateTime? JudgeDate { get; set; }
        public ICollection<Lawyer>? Lawyers { get; set; }
        public class Lawyer
        {
            public string Name { get; set; }
            public Guid Id { get; set; }
        }
    }
}
