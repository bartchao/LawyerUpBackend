﻿using LawyerUpBackend.Core.Common;
using System;
using System.Collections.Generic;

namespace LawyerUpBackend.Core.Entities
{
    public partial class LawyerCaseMatch :BaseEntity
    {
        public int CaseId { get; set; }
        public int? LawyerId { get; set; }

        public virtual Case Case { get; set; } = null!;
        public virtual Lawyer? Lawyer { get; set; }
    }
}
