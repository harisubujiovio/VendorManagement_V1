using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.Contracts
{
    public class CardSummaryResponse
    {
        public int data { get; set; }

        public string label { get; set; }

        public string name { get; set; }

        public string filterKey { get; set; }

        public string icon { get; set; }

        public CardSummaryResponse(int data, string label, string name, string filterKey, string icon)
        {
            this.data = data;
            this.label = label;
            this.name = name;
            this.filterKey = filterKey;
            this.icon = icon;
        }
    }
}
