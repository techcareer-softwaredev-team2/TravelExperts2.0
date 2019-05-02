using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class Packages
    {
        public int PackageId { get; set; }
        public string PkgName { get; set; }
        public DateTime? PkgStartDate { get; set; }
        public DateTime? PkgEndDate { get; set; }
        public string PkgDesc { get; set; }
        public decimal PkgBasePrice { get; set; }
        public decimal? PkgAgencyCommission { get; set; }
        public override string ToString()
        {
            return PkgName + ": " + PkgStartDate + ", " + PkgEndDate + ", " + PkgDesc + ", " + PkgBasePrice.ToString("c") + ", " + Convert.ToDecimal(PkgAgencyCommission).ToString("c");
        }
    }
}
