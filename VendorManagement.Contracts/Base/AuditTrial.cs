﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.Contracts.Base
{
    public record AuditTrialRequest
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }

    public record AuditTrialResponse : AuditTrialRequest
    {
        public Guid Id { get; set; }

    }
}
