using Microsoft.EntityFrameworkCore;
using SolickManagerV3_4.DTO;
using SolickManagerV3_4.Windows;
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

namespace SolickManagerV3_4.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListServicesPage.xaml
    /// </summary>
    public partial class ListServicesPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private string description = "";
        private decimal cost = 0;
        private string searchTitle = "";
        private Service selectedService;

        //Работник за приложением
        public Worker Worker { get; set; }

        // Данные поиска 
        public string Description { get => description; set { description = value; Search(); } }
        public decimal Cost { get => cost; set { cost = value; Search(); } }
        public string SearchTitle { get => searchTitle; set { searchTitle = value; Search(); } }

        // Основные данные страницы
        public List<Service> Services { get; set; } = new List<Service>();
        public Service SelectedService { get => selectedService; set { selectedService = value; Signal(); } }

        // Данные изменения услуги
        public string EditTitle { get; set; }
        public decimal EditCost { get; set; }
        public string EditDescription { get; set; }

        public ListServicesPage(DTO.Worker worker)
        {
            InitializeComponent();

            Worker = worker;

            Search();

            DataContext = this;
        }

        private void Search()
        {
            var result = DB.Instance.Services.Where(s => ((this.SearchTitle == "" || s.Title.ToLower().Contains(this.SearchTitle.ToLower()))
                                                         && (this.Description == "" || s.Description.ToLower().Contains(this.Description.ToLower()))
                                                         && (this.Cost == 0 || s.Cost == this.Cost))
                                                         && s.Deleted == false);

            this.Services = result.OrderBy(s => s.Id).ToList();

            Signal(nameof(Services));
        }

        private void SaveEditSelectedService(object sender, RoutedEventArgs e)
        {
            if(SelectedService != null)
            {
                DB.Instance.Services.Update(SelectedService);
                DB.Instance.SaveChanges();

                MessageBox.Show("Услуга успешно изменена!");

                Search();
            }
        }

        private void AddNewService(object sender, RoutedEventArgs e)
        {
            new AddOrEditServiceWindow().ShowDialog();

            Search();
        }

        private void EditSelectedService(object sender, RoutedEventArgs e)
        {
            if(SelectedService != null)
            {
                new AddOrEditServiceWindow(SelectedService).ShowDialog();

                Search();
            }
        }

        private void DeleteSelectedService(object sender, RoutedEventArgs e)
        {
            if(SelectedService != null )
            {
                SelectedService.Deleted = true;

                DB.Instance.Update(SelectedService);
                DB.Instance.SaveChanges();

                MessageBox.Show("Услуга успешно удалена!");

                Search();
            }
        }
    }
}
