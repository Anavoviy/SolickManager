using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Microsoft.EntityFrameworkCore;
using SolickManagerV3_4.DTO;

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для ListAssembliesWindow.xaml
    /// </summary>
    public partial class ListAssembliesWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void Signal([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<Product> Products { get; set; }
        public Product SelectedProduct { get; set; }
        public bool IsAssembly = true;
        private bool IsCounting = true;
        private string searchText = "";

        public string SearchText { get => searchText; set { searchText = value; Search(); } }
        public ListAssembliesWindow(bool? isAssembly, bool isCounting)
        {
            InitializeComponent();

            if (isAssembly == false)
                IsAssembly = false;
            if (isCounting == false)
                IsCounting = false;

            Search();

            DataContext = this;
        }

        private void Search()
        {
            this.Products = DB.Instance.Products.Include(s => s.IdcategoryNavigation).Include(s => s.IdshipmentNavigation).Where(s => (SearchText == "" || s.Model.ToLower().Contains(SearchText.ToLower())) && s.Amount > 0).ToList();

            if (IsCounting && OtherFunctons.AssemblyProducts.Count() > 0)
                foreach (var assProd in OtherFunctons.AssemblyProducts)
                    Products.Remove(assProd.IdproductNavigation);
            else
            {
                if (OtherFunctons.Products.Count() > 0)
                    foreach (var product in OtherFunctons.Products)
                    {
                        if (OtherFunctons.Products.Where(s => s.Id == product.Id).Count() == DB.Instance.Products.FirstOrDefault(s => s.Id == product.Id).Amount)
                            Products.Remove(product);
                        else
                            Products.FirstOrDefault(s => s.Id == product.Id).Amount -= OtherFunctons.Products.Where(s => s.Id == product.Id).Count();
                    }
            }
            if (SelectedProduct != null)
                SelectedProduct = Products.FirstOrDefault(s => s.Id == SelectedProduct.Id);

            Signal(nameof(Products));
            Signal(nameof(SelectedProduct));
        }

        private void AddProductInGroup(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct != null)
            {
                if (IsAssembly)
                    OtherFunctons.Instance.AddProductInGroup(SelectedProduct);
                else
                    OtherFunctons.Products.Add(SelectedProduct);

                this.Close();
            }
        }
    }
}
