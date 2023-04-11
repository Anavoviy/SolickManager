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
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using SolickManagerV3_4.DTO;

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddCrossApplicationServiceWindow.xaml
    /// </summary>
    public partial class AddCrossApplicationServiceWindow : Window, INotifyPropertyChanged
    {
        private string title = "";
        private string descripton = "";
        private decimal cost = 0;

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // Данные новой услуги
        public string Title { get => title; set { title = value; Search(); } }
        public string Descripton { get => descripton; set { descripton = value; Search(); } }
        public decimal Cost { get => cost; set { cost = value; Search(); } }

        // Данные списка
        List<Service> Services { get; set; } = new List<Service>();
        Service SelectedService { get; set; }

        public DTO.Application Application { get; set; }

        public AddCrossApplicationServiceWindow(DTO.Application application)
        {
            InitializeComponent();

            Application = application;

            Search();
        }

        private void Search()
        {
            var result = DB.Instance.Services.Where(s => (s.Title.ToLower().Contains(Title.ToLower()) || Title == "") &&
                                                         (Descripton == "" || s.Description.ToLower().Contains(Descripton.ToLower())));

            Services = result.ToList();

            Signal(nameof(Services));
        }

        private void VisibillityService(object sender, RoutedEventArgs e)
        {
            if (ServiceBorder.Visibility == Visibility.Collapsed)
                ServiceBorder.Visibility = Visibility.Visible;
            else
            {
                ServiceBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (this.SelectedService == null)
            {
                if (Cost > 0)
                {
                    Service newService = new Service();
                    newService.Title = this.Title;
                    newService.Description = this.Descripton;
                    newService.Cost = this.Cost;

                    DB.Instance.Services.Add(newService);

                    Applicationservice newApplicationService = new Applicationservice();
                    newApplicationService.Idapplication = this.Application.Id;
                    newApplicationService.Idservice = DB.Instance.Services.OrderBy(s => s.Id).Last().Id;

                    DB.Instance.Applicationservices.Add(newApplicationService);
                    
                    DB.Instance.SaveChanges();
                }
                else
                    MessageBox.Show("Неккоректная стоимость услуги!");
            }
        }
    }
}
