using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using SolickManagerV3_4.DTO;

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для ListAssembliesWindow.xaml
    /// </summary>
    public partial class ListAssembliesWindow : Window
    {
        public List<Product> Products { get; set; }
        public Product SelectedProduct { get; set; }

        public ListAssembliesWindow()
        {
            InitializeComponent();

            this.Products = DB.Instance.Products.ToList();

            foreach (var assProd in OtherFunctons.AssemblyProducts)
                Products.Remove(assProd.IdproductNavigation);
            
            
            DataContext = this;
        }

        private void AddProductInGroup(object sender, RoutedEventArgs e)
        {
            if(SelectedProduct!= null)
            {
                OtherFunctons.Instance.AddProductInGroup(SelectedProduct);

                this.Close();
            }
        }
    }
}
