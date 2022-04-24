using LawyerUpBackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerUpBackend.DataAccess.Model
{
    public class CaseCountResult
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public int? Year { get; set; }
        public string? Word { get; set; }
        public int? Number { get; set; }
        public string? Classification { get; set; }
        [Column("main_content")]
        public string? MainContent { get; set; }
        [Column("judge_date")]

        public DateTime? JudgeDate { get; set; }

        public int Result { get; set; }
    }
}
