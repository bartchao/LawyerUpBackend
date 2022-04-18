using System;
using System.Collections.Generic;

namespace LawyerUpBackend.Core.Models
{
    public partial class Lawyer
    {
        public Lawyer()
        {
            LawyerCaseMatches = new HashSet<LawyerCaseMatch>();
        }

        public int Id { get; set; }
        public Guid? UniqueId { get; set; }
        public string? Name { get; set; }
        public string? NowLicNo { get; set; }
        public string? Sex { get; set; }
        public int? Birthyear { get; set; }
        public string? GuildName { get; set; }
        public string? Office { get; set; }
        public string? Email { get; set; }
        public string? Tel { get; set; }
        public string? Addr { get; set; }

        public virtual ICollection<LawyerCaseMatch> LawyerCaseMatches { get; set; }
    }
}
