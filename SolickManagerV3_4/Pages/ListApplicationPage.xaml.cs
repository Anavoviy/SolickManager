using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using SolickManagerV3_4.DTO;
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
    /// Логика взаимодействия для ListApplicationPage.xaml
    /// </summary>
    public partial class ListApplicationPage : Page, INotifyPropertyChanged
    {
        private List<Service> selectedApplicationServices;
        private DTO.Application selectedApplication;
        private string searchText = "";

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        //Работник за приложением
        public Worker Worker { get; set; }

        //Фильтры
        public string SearchText { get => searchText; set { searchText = value; Search(); } }

        

        public List<string> StatusesList { get; set; }
        public int SelectedStatusIndex { get; set; } = 0;

        //Вывод сбоку:
        public List<Service> SelectedApplicationServices { get => selectedApplicationServices; set { selectedApplicationServices = value; Signal(); } }
        public DTO.Application SelectedApplication { get => selectedApplication; set { selectedApplication = value; Signal(); } }

        //Главный ListView
        public List<DTO.Application> Applications { get; set; }

        public ListApplicationPage(Worker worker)
        {
            InitializeComponent();
            Worker = worker;

            Applications = DB.Instance.Applications.Include(s => s.IdclientNavigation).Include(s => s.IddeviceNavigation).ToList();

            DataContext = this;
        }

        private void SaveEditSelectedApplication(object sender, RoutedEventArgs e)
        {

        }


        private void Search()
        {
            var result = DB.Instance.Applications
                .Include(s => s.IdclientNavigation)
                .Include(s => s.IddeviceNavigation)
                .Where(s =>
                    s.Status.Contains(searchText) ||
                    s.Problem.Contains(searchText) ||
                    s.IdclientNavigation.Firstname.Contains(searchText) ||
                    s.IdclientNavigation.Secondname.Contains(searchText) ||
                    s.IdclientNavigation.Patronymic.Contains(searchText) ||
                    s.IddeviceNavigation.Model.Contains(searchText) 
                    );


            Applications = result.ToList();

            Signal(nameof(Applications));
        }
    }
}
