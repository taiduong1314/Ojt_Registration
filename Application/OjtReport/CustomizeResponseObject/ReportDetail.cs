using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OjtReport.CustomizeResponseObject
{
    public class ReportDetail
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public double? Mark { get; set; }
    }
}
