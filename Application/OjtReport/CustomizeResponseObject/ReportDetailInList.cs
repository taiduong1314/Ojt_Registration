using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OjtReport.CustomizeResponseObject
{
    public class ReportDetailInList
    {
        public DateTime? PublicDate { get; set; }
        public string WorkSortDescription { get; set; }
        public string StudentCode { get; set; }
        public double? Mark { get; set; }
        public string StudentName { get; set; }
    }
}
