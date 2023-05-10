using Microsoft.EntityFrameworkCore;
using SolickManagerV3_4.DTO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SolickManagerV3_4.Windows;

namespace SolickManagerV3_4.Pages
{
    /// <summary>
    /// Логика взаимодействия для SalesPage.xaml
    /// </summary>
    public partial class SalesPage : Page, INotifyPropertyChanged
    {
        private DTO.Application selectedSale;

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // Работник за приложением
        public Worker Worker { get; set; }

        // Основные данные
        public List<DTO.Application> Sales { get; set; }
        public DTO.Application SelectedSale { get => selectedSale; 
            set 
            { 
                selectedSale = value;
                
                SelectedSaleGroupsProducts.Clear();
                SelectedSaleProducts.Clear();

                if(SelectedSale != null)
                {
                    SelectedSaleProducts = SelectedSale.Products;
                    SelectedSaleGroupsProducts = SelectedSale.Assemblies;
                }
                else
                {
                    SelectedSaleGroupsProducts = new List<Assembly>();
                    SelectedSaleProducts = new List<Product>();
                }
                Signal();
                Signal(nameof(SelectedSaleProducts));
                Signal(nameof(SelectedSaleGroupsProducts));
            } }

        // Поиск
        public string DataStart { get; set; }
        public string DataEnd { get; set; }
        public string SearchText { get; set; }
        public List<string> StatusesList { get; set; } = new List<string>();
        public int StatusIndex { get; set; } = 0;

        // Быстрое редактирование
        public List<string> EditStatusesList { get; set; } = new List<string>() { "Создана", "Принята", "Собрана", "Оплачена", "Завершена" };
        public int EditStatusIndex { get; set; }
        public List<Product> SelectedSaleProducts { get; set; } = new List<Product>();
        public Product SelectedSaleSelectedProduct { get; set; }
        public List<Assembly> SelectedSaleGroupsProducts { get; set; } = new List<Assembly>();
        public Assembly SelectedSaleSelectedGroupProducts { get; set; }

        public SalesPage(Worker worker)
        {
            InitializeComponent();

            Worker = worker;
            Search();
            FillStatusesList();

            DataContext = this;
        }

        private void FillStatusesList()
        {
            List<string> statuses = new List<string>();

            if (Sales.Count > 0)
                statuses = Sales.Select(s => s.Status).Distinct().ToList();

            StatusesList.Add("Все статусы");
            StatusesList.Add("Незавершённые");
            if (statuses.Count() > 0)
                StatusesList.AddRange(statuses);

            Signal(nameof(StatusesList));
        }
        private void Search()
        {
            var result = DB.Instance.Applications.Include(s => s.Applicationproducts).Include(s => s.Applicationassemblies).Where(s => (s.Applicationassemblies.Count() > 0 || s.Applicationproducts.Count() > 0) &&
                                                                                                                                       s.Deleted == false);

            if (StatusIndex == 0)
                Sales = result.OrderBy(s => s.Id).ToList();
            else if (StatusIndex == 1)
                Sales = result.Where(s => s.Status != "Завершена").OrderBy(s => s.Id).ToList();
            else
                Sales = result.Where(s => s.Status == StatusesList[StatusIndex]).OrderBy(s => s.Id).ToList();

            DateOnly startData, endData;
            if (DateOnly.TryParse(DataStart, out startData) && !DateOnly.TryParse(DataEnd, out endData))
                Sales = Sales.Where(s => s.Data > startData).OrderBy(s => s.Id).ToList();
            else if (!DateOnly.TryParse(DataStart, out startData) && DateOnly.TryParse(DataEnd, out endData))
                Sales = Sales.Where(s => s.Data < endData).OrderBy(s => s.Id).ToList();
            else if (DateOnly.TryParse(DataStart, out startData) && DateOnly.TryParse(DataEnd, out endData))
                Sales = Sales.Where(s => s.Data > startData && s.Data < endData).OrderBy(s => s.Id).ToList();

            if (SelectedSale != null)
                SelectedSale = Sales.FirstOrDefault(s => s.Id == SelectedSale.Id);

            Signal(nameof(Sales));
            Signal(nameof(SelectedSale));
        }


        private void AddNewSale(object sender, RoutedEventArgs e)
        {
            new AddOrEditSaleWindow(Worker).ShowDialog();

            Search();
        }
        private void EditSelectedSale(object sender, RoutedEventArgs e)
        {
            if (SelectedSale != null)
            {

            }
        }
        private void DeleteSelectedSale(object sender, RoutedEventArgs e)
        {
            if(SelectedSale != null)
            {
                SelectedSale.Deleted = true;

                DB.Instance.Applications.Update(SelectedSale);

                if (SelectedSale.Assemblies.Count() > 0)
                {
                    foreach (Assembly assembly in SelectedSale.Assemblies)
                    {
                        DB.Instance.Applicationassemblies.Remove(DB.Instance.Applicationassemblies.FirstOrDefault(s => s.Idapplication == SelectedSale.Id && s.Idassemby == assembly.Id));

                        assembly.Deleted = false;
                        DB.Instance.Assemblies.Update(assembly);
                    }
                }
                if (SelectedSale.Products.Count() > 0)
                {
                    foreach (Product product in SelectedSale.Products)
                    {
                        DB.Instance.Applicationproducts.Remove(DB.Instance.Applicationproducts.FirstOrDefault(s => s.Idapplication == SelectedSale.Id && s.Idproduct == product.Id));

                        product.Amount += 1;
                        DB.Instance.Products.Update(product);
                    }
                }

                DB.Instance.SaveChanges();

                MessageBox.Show("Продажа успешно удалена!");

                Search();
            }
        }

        private void SaveEditSelectedSale(object sender, RoutedEventArgs e)
        {
            if(SelectedSale != null)
            {
                List<Assembly> OldAssembliesList = DB.Instance.Applicationassemblies.Include(s => s.IdassembyNavigation).Where(s => s.Idapplication == SelectedSale.Id).Select(s => s.IdassembyNavigation).ToList();
                List<Product> OldProductsList = DB.Instance.Applicationproducts.Include(s => s.IdproductNavigation).Where(s => s.Idapplication == SelectedSale.Id).Select(s => s.IdproductNavigation).ToList();

                List<Assembly> RemoveAssemblies = OldAssembliesList.Except(SelectedSaleGroupsProducts).ToList();
                List<Product> RemoveProducts = OldProductsList.Except(SelectedSaleProducts).ToList();

                List<Assembly> AddAssemblies = SelectedSaleGroupsProducts.Except(OldAssembliesList).ToList();
                List<Product> AddProducts = SelectedSaleProducts.Except(OldProductsList).ToList();


                if(RemoveAssemblies.Count() > 0)
                {
                    foreach (Assembly assembly in RemoveAssemblies) 
                    {
                        DB.Instance.Applicationassemblies.Remove(DB.Instance.Applicationassemblies.FirstOrDefault(s => s.Idapplication == SelectedSale.Id && s.Idassemby == assembly.Id));
                        
                        assembly.Deleted = false;
                        DB.Instance.Assemblies.Update(assembly);
                    }
                }
                if (RemoveProducts.Count() > 0)
                {
                    foreach (Product product in RemoveProducts)
                    {
                        DB.Instance.Applicationproducts.Remove(DB.Instance.Applicationproducts.FirstOrDefault(s => s.Idapplication == SelectedSale.Id && s.Idproduct == product.Id));

                        product.Amount += 1;
                        DB.Instance.Products.Update(product);
                    }
                }
                if(AddAssemblies.Count() > 0) 
                {
                    foreach(Assembly assembly in AddAssemblies)
                    {
                        DB.Instance.Applicationassemblies.Add(new Applicationassembly() { Idapplication = SelectedSale.Id, Idassemby = assembly.Id });

                        assembly.Deleted = false;
                        DB.Instance.Assemblies.Update(assembly);
                    }
                }
                if (AddProducts.Count() > 0)
                {
                    foreach (Product product in AddProducts)
                    {
                        DB.Instance.Applicationproducts.Add(new Applicationproduct() { Idapplication = SelectedSale.Id, Idproduct = product.Id });

                        product.Amount -= 1;
                        DB.Instance.Products.Update(product);
                    }
                }

                DB.Instance.SaveChanges();

                MessageBox.Show("Продажа успешно изменена!");

                Search();
            }
        }

        private void AddCrossSaleProduct(object sender, RoutedEventArgs e)
        {
            if (SelectedSale != null)
            {
                OtherFunctons.Products.Clear();

                new AddCrossSaleProductWindow(SelectedSaleProducts).ShowDialog();

                if(OtherFunctons.Products.Count() > 0) 
                    SelectedSaleProducts.Add(OtherFunctons.Products.Last());

                Signal(nameof(SelectedSaleProducts));
                SaleProductsListView.Items.Refresh();
            }
        }
        private void DeleteCrossSaleProduct(object sender, RoutedEventArgs e)
        {
            if (SelectedSale != null && SelectedSaleSelectedProduct != null && (SelectedSaleProducts.Count() > 1 || SelectedSaleGroupsProducts.Count() > 1))
            {
                SelectedSaleProducts.Remove(SelectedSaleSelectedProduct);
                Signal(nameof(SelectedSaleProducts));
                SaleProductsListView.Items.Refresh();
            }
            else
                MessageBox.Show("Невозможно удалить товар из списка!");     
        }
        private void AddCrossSaleGroupProducts(object sender, RoutedEventArgs e)
        {
            if (SelectedSale != null)
            {
                OtherFunctons.Assemblies.Clear();

                new AddCrossSaleAssemblyWindow(SelectedSaleGroupsProducts).ShowDialog();

                if (OtherFunctons.Assemblies.Count() > 0)
                    SelectedSaleGroupsProducts.Add(OtherFunctons.Assemblies.Last());

                Signal(nameof(SelectedSaleGroupsProducts));
                SaleAssembliesListView.Items.Refresh();
            }
        }
        private void DeleteCrossSaleGroupProducts(object sender, RoutedEventArgs e)
        {
            if (SelectedSale != null && SelectedSaleSelectedGroupProducts != null && (SelectedSaleProducts.Count() > 1 || SelectedSaleGroupsProducts.Count() > 1))
            {
                SelectedSaleGroupsProducts.Remove(SelectedSaleSelectedGroupProducts);
                Signal(nameof(SelectedSaleGroupsProducts));
                SaleAssembliesListView.Items.Refresh();
            }
            else
                MessageBox.Show("Невозможно сборку товаров товар из списка!");
        }
    }
}
