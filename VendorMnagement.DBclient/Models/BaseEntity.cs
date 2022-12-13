using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.DBclient.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Guid { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
