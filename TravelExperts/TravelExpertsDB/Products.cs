using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class Products
    {
        public int ProductId { get; set; }
        public string ProdName { get; set; }

        public override string ToString()
        {
            return ProductId.ToString() + ": " + ProdName;
        }
    }
}

