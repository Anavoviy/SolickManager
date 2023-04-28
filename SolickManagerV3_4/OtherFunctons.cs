using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using SolickManagerV3_4.DTO;

namespace SolickManagerV3_4
{
    public class OtherFunctons
    {
        private static OtherFunctons other { get; set; }

        public static List<Product> Products { get; set; } = new List<Product>();
        public static List<Productcharacteristic> Productcharacteristics { get; set; } = new List<Productcharacteristic>();
        public static List<Assemblyproduct> AssemblyProducts { get; set; } = new List<Assemblyproduct>();

        public static OtherFunctons Instance
        {
            get
            {
                if (other == null)
                    other = new OtherFunctons();
                return other;
            }
            set => other = value;
        }

        public DateOnly DateOnlyNow() {
            string dataTime = DateTime.Now.ToString();

            string[] dataAndTime = dataTime.Split(' ');

            DateOnly data = DateOnly.Parse(dataAndTime[0]);

            return data;
        }

        public void AddProduct(Product product)
        {
            if (Products.Count == 0)
                product.Id = DB.Instance.Products.OrderBy(s => s.Id).Last().Id + 1;
            else
                product.Id = DB.Instance.Products.OrderBy(s => s.Id).Last().Id + Products.Count;

            Products.Add(product);
        }
        public void RemoveProduct(Product product)
        {
            if (Products.Count > 0)
            {
                foreach (Product prod in Products)
                {
                    if (prod.Id > product.Id)
                    {
                        List<Productcharacteristic> pchars = Productcharacteristics.Where(s => s.Idproduct == prod.Id).ToList();

                        foreach (Productcharacteristic pchar in pchars)
                            pchar.Idproduct -= 1;

                        prod.Id -= 1;
                    }
                }

                Products.Remove(product);
            }
        }
        public void AddProductInGroup(Product product)
        {
            if(product != null)
            {
                AssemblyProducts.Add(new Assemblyproduct()
                {
                    IdproductNavigation = product,
                    Idproduct = product.Id,
                    Count = 1
                });
            }
        }
    }
}
