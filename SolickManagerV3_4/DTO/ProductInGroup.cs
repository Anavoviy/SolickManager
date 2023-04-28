using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolickManagerV3_4.DTO
{
    public class ProductInGroup : Product
    {
        public int CountInAssembly { get; set; }

        public ProductInGroup() { }
    }
}
