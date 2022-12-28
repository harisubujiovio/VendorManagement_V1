using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.Contracts
{
    public record ResourceDictionary
    {
        public Guid Value { get; set; }

        public string Text { get; set; }

        public ResourceDictionary(Guid value, string text) { 
           this.Value = value;
           this.Text = text;
        }
    }
}
