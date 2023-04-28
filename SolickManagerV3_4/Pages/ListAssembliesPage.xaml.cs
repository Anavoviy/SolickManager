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

        //Работник за приложением
        public Worker Worker { get; set; }

        // Основные данные
        public List<Assembly> Assemblies { get; set; }
        public Assembly SelectedAssembly { get; set; }

        // Поиск и фильтрация
        public string SearchTitle { get => searchTitle; set { searchTitle = value; Search(); } }
        public decimal SearchCost { get => searchCost; set { searchCost = value; Search(); } }
        public List<Worker> Masters { get; set; }
        public Worker SelectedMaster { get => selectedMaster; set { selectedMaster = value; Search(); } }

        public ListAssembliesPage(DTO.Worker worker)
        {
            InitializeComponent();

            Search();

            Masters = DB.Instance.Workers.Where(s => s.Deleted == false).OrderBy(s => s.Surname).ThenBy(s => s.Firstname).ThenBy(s => s.Patronymic).ToList();

            Worker = worker;

            DataContext = this;
        }

        public void Search()
        {
            var result = DB.Instance.Assemblies.Where(s => (SearchTitle == "" || s.Title.ToLower().Contains(SearchTitle.ToLower())) &&
                                                           (SearchCost == 0 || s.Cost == SearchCost) &&
                                                           (SelectedMaster == null || s.Idmasterconfiguration == SelectedMaster.Id || s.Idmasterassembler == SelectedMaster.Id) &&
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

                Search();
            }
        }
    }
}
