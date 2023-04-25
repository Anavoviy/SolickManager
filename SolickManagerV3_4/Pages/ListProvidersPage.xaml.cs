using SolickManagerV3_4.DTO;
using SolickManagerV3_4.Windows.Images;
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
    /// Логика взаимодействия для ListProvidersPage.xaml
    /// </summary>
    public partial class ListProvidersPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private Provider selectedProvider;
        private string searchDirectorManager = "";
        private string searchTitle = "";

        // Работник за приложением
        public Worker Worker { get; set; }

        // Основные данные
        public List<Provider> Providers { get; set; }
        public Provider SelectedProvider { get => selectedProvider; set { selectedProvider = value; Signal(); } }

        // Поиск среди поставщиков
        public string SearchDirectorManager { get => searchDirectorManager; set { searchDirectorManager = value; Search(); } }
        public string SearchTitle { get => searchTitle; set { searchTitle = value; Search(); } }


        public ListProvidersPage(Worker worker)
        {
            InitializeComponent();

            Worker = worker;

            Search();

            DataContext = this;
        }

        public void Search()
        {
            var result = DB.Instance.Providers.Where(s => (SearchDirectorManager == "" || s.DirectorManager.ToLower().Contains(SearchDirectorManager.ToLower())) &&
                                                          (SearchTitle == "" || s.Title.ToLower().Contains(SearchTitle.ToLower())) &&
                                                          s.Deleted == false);

            Providers = result.OrderBy(s => s.Id).ToList();

            Signal(nameof(Providers));
        }

        private void AddNewProvider(object sender, RoutedEventArgs e)
        {
            new AddOrEditProviderWindow().ShowDialog();

            Search();
        }

        private void EditSelectedProvider(object sender, RoutedEventArgs e)
        {
            if (SelectedProvider != null)
            {
                new AddOrEditProviderWindow(SelectedProvider).ShowDialog();

                Search();

                SelectedProvider = Providers.FirstOrDefault(s => s.Id == SelectedProvider.Id);
            }
        }

        private void DeleteSelectedProvider(object sender, RoutedEventArgs e)
        {
            if(SelectedProvider != null)
            {
                SelectedProvider.Deleted = true;

                DB.Instance.Providers.Update(SelectedProvider);
                DB.Instance.SaveChanges();

                Search();

                SelectedProvider = new Provider();
            }
        }

        private void SaveEditSelectedprovider(object sender, RoutedEventArgs e)
        {
            if (SelectedProvider != null && SelectedProvider.Title != null && SelectedProvider.DirectorManager != null)
            {
                DB.Instance.Providers.Update(SelectedProvider);
                DB.Instance.SaveChanges();

                Search();

                SelectedProvider = Providers.FirstOrDefault(s => s.Id == SelectedProvider.Id);
            }
        }
    }
}
