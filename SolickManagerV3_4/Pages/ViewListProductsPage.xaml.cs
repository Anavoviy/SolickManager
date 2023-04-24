using Microsoft.EntityFrameworkCore;
using SolickManagerV3_4.DTO;
using SolickManagerV3_4.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SolickManagerV3_4.Pages
{
    /// <summary>
    /// Логика взаимодействия для ViewListProductsPage.xaml
    /// </summary>
    public partial class ViewListProductsPage : Page, INotifyPropertyChanged
    {
        private string searchModel = "";
        private decimal searchCost = 0;
        private Category searchCategory = new Category() { Title = "Все категории" };
        private string searchDescription = "";
        private Product selectedProduct = new Product();

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // Работник за приложением
        public Worker Worker { get; set; }

        // Данные для поиска
        public string SearchModel { get => searchModel; set { searchModel = value; Search(); } }
        public decimal SearchCost { get => searchCost; set { searchCost = value; Search(); } }
        public Category SearchCategory { get => searchCategory; 
            set 
            { 
                searchCategory = value; 
                Search();
                if (SelectedProduct == null || (SelectedProduct.Idcategory != SearchCategory.Id && SearchCategory.Title != "Все категории")) 
                {
                    SelectedProduct = new Product();
                }
            } }
        public string SearchDescription { get => searchDescription; set { searchDescription = value; Search(); } }

        public List<Category> SearchCategoriesList { get; set; }

        // Основные данные
        public List<Product> Products { get; set; } = new List<Product>();
        public Product SelectedProduct { get => selectedProduct; 
            set 
            { 
                selectedProduct = value;
                if (SelectedProduct != null)
                    OldCost = decimal.Parse(SelectedProduct.CostView);
                else
                    OldCost = 0;
                
                NewCost = OldCost;
                Signal(nameof(NewCost));
                Signal();

            } }

        // Дополнительные данные
        private decimal OldCost = 0;
        private decimal newCost = 0;
        public decimal NewCost { get => newCost; set { newCost = value; ValidCost(); } }
        
        
        public ViewListProductsPage(DTO.Worker worker)
        {
            InitializeComponent();

            Worker = worker;

            FillCategories();
            Search();

            DataContext = this;
        }

        private void ValidCost()
        {
            if (NewCost >= 0)
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
        private void FillCategories()
        {
            SearchCategoriesList = new List<Category>();

            SearchCategoriesList.Add(new Category() { Title = "Все категории" });
            SearchCategoriesList.AddRange(DB.Instance.Categories.ToList());

            SearchCategory = SearchCategoriesList[0];

            Signal(nameof(SearchCategory));
        }

        private void Search()
        {
            var result = DB.Instance.Products.Include(s => s.IdcategoryNavigation)
                                             .Include(s => s.IdshipmentNavigation)
                                             .Where(s => (SearchModel == "" || s.Model.ToLower().Contains(SearchModel.ToLower())) &&
                                                   (SearchCost == 0 || SearchCost == s.Cost) &&
                                                   (SearchCategory.Title == "Все категории" || s.Idcategory == SearchCategory.Id) &&
                                                   (SearchDescription == "" || (s.Description != null && s.Description.ToLower().Contains(SearchDescription.ToLower()))
                                                   ));

            Products = result.OrderBy(s => s.Id).ToList();

            Signal(nameof(Products));
        }

        private void AddNewProduct(object sender, RoutedEventArgs e)
        {
            new AddOrEditProductWindow().ShowDialog();

            Search();

            SelectedProduct = Products.FirstOrDefault(s => s.Id == SelectedProduct.Id);

            Signal(nameof(SelectedProduct));
        }
        private void DeleteSelectedProduct(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct != null)
            {
                if (DB.Instance.Productpricechanges.FirstOrDefault(s => s.Idproduct == SelectedProduct.Id) != null)
                {
                    List<Productpricechange> PPC = DB.Instance.Productpricechanges.Where(s => s.Idproduct != SelectedProduct.Id).ToList();

                    DB.Instance.Productpricechanges.RemoveRange(PPC);
                }

                if (DB.Instance.Productcharacteristics.FirstOrDefault(s => s.Idproduct == SelectedProduct.Id) != null)
                {
                    List<Productcharacteristic> PC = DB.Instance.Productcharacteristics.Where(s => s.Idproduct == SelectedProduct.Id).ToList();

                    DB.Instance.Productcharacteristics.RemoveRange(PC);
                }

                DB.Instance.Products.Remove(SelectedProduct);
                DB.Instance.SaveChanges();

                MessageBox.Show("Товар успешно удалён!");

                Search();
            }

        }

        private void SaveEditSelectedProduct(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct != null)
            {
                if (NewCost != OldCost)
                {
                    DB.Instance.Productpricechanges.Add(new Productpricechange()
                    {
                        Idproduct = SelectedProduct.Id,
                        Newcost = NewCost,
                        Ratio = NewCost / OldCost
                    });
                    DB.Instance.SaveChanges();
                }

                DB.Instance.Products.Update(SelectedProduct);
                DB.Instance.SaveChanges();

                Search();

                SelectedProduct = Products.FirstOrDefault(s => s.Id == SelectedProduct.Id);
                Signal(nameof(SelectedProduct));

                MessageBox.Show("Продукт успешно изменён!");
            }
            else
                MessageBox.Show("Не выбран ни один продукт!");
        }
    }
}
