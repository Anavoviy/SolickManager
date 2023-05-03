using Microsoft.EntityFrameworkCore;
using SolickManagerV3_4.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
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

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditProductWindow.xaml
    /// </summary>
    public partial class AddOrEditProductWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        private Category selectedCategory;

        // Основные данные
        public Product Product { get; set; } = new Product();
        public decimal Cost { get => cost; set { cost = value; ValidCost(); } }
        private bool IsEdit = false;

        // Данные для заполнения
        public List<Category> Categories { get; set; }
        public Category SelectedCategory { get => selectedCategory; set { selectedCategory = value; FillCharacteristics(); } }
        public List<Provider> Providers { get; set; }
        public Provider SelectedProvider { get; set; }
        public List<Productcharacteristic> Characteristics { get; set; }

        // Полезные данные
        private decimal oldCost;
        private decimal cost = 0;
        private bool IsEditable = false;
        public OtherFunctons OtherFunctons { get; set; } = OtherFunctons.Instance;

        public AddOrEditProductWindow()
        {
            InitializeComponent();

            FillProviders();

            FillCategories();

            DataContext = this;
        }
        public AddOrEditProductWindow(Provider provider)
        {
            InitializeComponent();

            FillProviders();
            SelectedProvider = Providers.FirstOrDefault(s => s.Id == provider.Id);
            IsEditable = false;

            FillCategories();

            DataContext = this;
        }

        private void ValidCost()
        {
            if (Cost >= 0)
            {
                CostTextBox.Foreground = new SolidColorBrush(Colors.Black);
                SaveButton.IsEnabled = true;
            }
            else
            {
                CostTextBox.Foreground = new SolidColorBrush(Colors.Red);
                SaveButton.IsEnabled = false;
            }
        }

        private void FillProviders()
        {
            Providers = DB.Instance.Providers.Where(s => s.Deleted == false).OrderBy(s => s.Title).ToList();

            Signal(nameof(Providers));
        }
        private void FillCategories()
        {
            Categories = DB.Instance.Categories.Where(s => s.Deleted == false).OrderBy(s => s.Title).ToList();

            Signal(nameof(Categories));

        }
        private void FillCharacteristics()
        {
            if (SelectedCategory != null && !IsEdit)
            {
                Characteristics = new List<Productcharacteristic>();

                List<Characteristic> characteristics = DB.Instance.Categorycharacteristics.Include(s => s.IdcharacteristicNavigation)
                                                                                          .Where(s => s.Idcategory == SelectedCategory.Id && s.Deleted == false)
                                                                                          .Select(s => s.IdcharacteristicNavigation)
                                                                                          .OrderBy(s => s.Title).ToList();

                foreach (Characteristic characteristic in characteristics)
                    Characteristics.Add(new Productcharacteristic() { IdcharacteristicNavigation = characteristic });

                Signal(nameof(Characteristics));
            }
            else if (SelectedCategory != null && IsEdit)
            {
                Characteristics = DB.Instance.Productcharacteristics.Include(s => s.IdcharacteristicNavigation)
                                                                    .Where(s => s.Idproduct == Product.Id && s.Deleted == false)
                                                                    .OrderBy(s => s.IdcharacteristicNavigation.Title).ToList();

                Signal(nameof(Characteristics));
            }
        }

        private class ProdCharacteristics
        {
            public Characteristic Characteristic { get; set; }
            public string Meaning { get; set; }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (Product != null && (Product.Model != "" && Product.Model != null) && SelectedCategory != null && SelectedProvider != null)
            {
                Product.Idcategory = SelectedCategory.Id;
                Product.Cost = this.Cost;
                Product.Amount = 1;

                if (IsEditable)
                {
                    OtherFunctons.Instance.AddProduct(Product);
                    
                    int idProd = 1;
                    if (OtherFunctons.Products.Count > 0)
                        idProd = OtherFunctons.Products.OrderBy(s => s.Id).Last().Id;
                    else
                        idProd = DB.Instance.Products.OrderBy(s => s.Id).Last().Id;

                    foreach (Productcharacteristic c in Characteristics)
                        c.Idproduct = idProd;

                    OtherFunctons.Productcharacteristics.AddRange(Characteristics);
                }
                else
                {
                    DB.Instance.Shipments.Add(new Shipment()
                    {
                        Data = OtherFunctons.Instance.DateOnlyNow(),
                        Idprovider = SelectedProvider.Id,
                        Amount = Product.Cost,
                        Numberproducts = 1
                    });
                    DB.Instance.SaveChanges();

                    Product.Idshipment = DB.Instance.Shipments.OrderBy(s => s.Id).Last().Id;

                    DB.Instance.Products.Add(Product);
                    DB.Instance.SaveChanges();

                    int idProd = DB.Instance.Products.OrderBy(s => s.Id).Last().Id;
                    foreach (Productcharacteristic c in Characteristics)
                        c.Idproduct = idProd;

                    DB.Instance.Productcharacteristics.AddRange(Characteristics);
                    DB.Instance.SaveChanges();
                }

                MessageBox.Show("Товар успешно добавлен!");

                this.Close();

            }
        }
    }
}




