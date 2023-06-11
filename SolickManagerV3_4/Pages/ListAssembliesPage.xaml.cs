using SolickManagerV3_4.DTO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SolickManagerV3_4.Windows;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SolickManagerV3_4.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListAssembliesPage.xaml
    /// </summary>
    public partial class ListAssembliesPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void Signal([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string searchTitle = "";
        private decimal searchCost = 0;
        private Worker selectedMaster = null;
        private Assembly selectedAssembly;

        //Работник за приложением
        public Worker Worker { get; set; }

        // Основные данные
        public List<Assembly> Assemblies { get; set; }
        public Assembly SelectedAssembly { get => selectedAssembly; set { selectedAssembly = value; Signal(); } }

        // Поиск и фильтрация
        public string SearchTitle { get => searchTitle; set { searchTitle = value; Search(); } }
        public decimal SearchCost { get => searchCost; set { searchCost = value; Search(); } }
        public List<Worker> Masters { get; set; }
        public Worker SelectedMaster { get => selectedMaster; set { selectedMaster = value; Search(); } }

        public ListAssembliesPage(DTO.Worker worker)
        {
            InitializeComponent();

            Search();

            FillMasters();

            Worker = worker;

            DataContext = this;
        }

        private void FillMasters()
        {
            Masters = new List<Worker>() { new Worker { Firstname = "Все мастера" ,Surname = " ", Id = 0} };
            Masters.AddRange(DB.Instance.Workers.Where(s => s.Deleted == false).OrderBy(s => s.Surname).ThenBy(s => s.Firstname).ThenBy(s => s.Patronymic).ToList());

            Signal(nameof(Masters));
        }
        public void Search()
        {
            var result = DB.Instance.Assemblies.Where(s => (SearchTitle == "" || s.Title.ToLower().Contains(SearchTitle.ToLower())) &&
                                                           (SearchCost == 0 || s.Cost == SearchCost) &&
                                                           (SelectedMaster == null || SelectedMaster.Id == 0 || s.Idmasterconfiguration == SelectedMaster.Id || s.Idmasterassembler == SelectedMaster.Id) &&
                                                           s.Deleted == false);

            Assemblies = result.OrderBy(s => s.Id).ToList();

            if (SelectedAssembly != null)
                SelectedAssembly = Assemblies.FirstOrDefault(s => s.Id == SelectedAssembly.Id);

            Signal(nameof(Assemblies));
            Signal(nameof(SelectedAssembly));
        }

        private void AddNewAssembly(object sender, RoutedEventArgs e)
        {
            new AddOrEditAssemblyWindow().ShowDialog();

            Search();
        }

        private void DeleteSelectedAssembly(object sender, RoutedEventArgs e)
        {
            if (SelectedAssembly != null)
            {
                SelectedAssembly.Deleted = true;

                DB.Instance.Assemblies.Update(SelectedAssembly);

                List<Assemblyproduct> assemblyproducts = DB.Instance.Assemblyproducts.Include(s => s.IdproductNavigation).Where(s => s.Idassembly == SelectedAssembly.Id).ToList();
                foreach(Assemblyproduct assemblyproduct in assemblyproducts)
                {
                    Product product = assemblyproduct.IdproductNavigation;
                    product.Amount += assemblyproduct.Count;
                    DB.Instance.Products.Update(product);
                }

                DB.Instance.SaveChanges();

                Search();
            }
        }

        private void SaveEditSelectedAssembly(object sender, RoutedEventArgs e)
        {
            if (SelectedAssembly != null)
            {
                DB.Instance.Assemblies.Update(SelectedAssembly);
                DB.Instance.SaveChanges();

                MessageBox.Show("Успешно изменена выбранная группа товаров (сборка)!");

                Search();
            }
        }
    }
}
