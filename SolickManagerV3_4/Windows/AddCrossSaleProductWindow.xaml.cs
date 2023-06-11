using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using SolickManagerV3_4.DTO;

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddCrossSaleProductWindow.xaml
    /// </summary>
    public partial class AddCrossSaleProductWindow : Window
    {
        public List<Product> Products { get; set; }
        public Product SelectedProduct { get; set; }

        public AddCrossSaleProductWindow(List<Product> SaleProducts)
        {
            InitializeComponent();

            Products = DB.Instance.Products.Include(s => s.IdshipmentNavigation).Include(s => s.IdcategoryNavigation).Where(s => s.Amount > 0).ToList();

            foreach(var product in SaleProducts)
                Products.Remove(Products.FirstOrDefault(s => s.Id == product.Id));

            DataContext = this;
        }

        private void AddProductInCross(object sender, RoutedEventArgs e)
        {
            if(SelectedProduct != null)
            {
                OtherFunctons.Products.Add(SelectedProduct);

                this.Close();
            }
        }
    }
}
