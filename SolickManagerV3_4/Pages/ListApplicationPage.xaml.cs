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
using SolickManagerV3_4.Windows;

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
        private string dataStart = "";
        private string dataEnd = "";

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        //Работник за приложением
        public Worker Worker { get; set; }

        //Фильтры
        public string SearchText { get => searchText; set { searchText = value; Search(); } }

        public string DataStart { get => dataStart; set { dataStart = value; Search(); } }
        public string DataEnd { get => dataEnd; set { dataEnd = value; Search(); } }

        public List<string> StatusesList { get; set; } = new List<string>();
        public int StatusIndex { get; set; } = 0;

        //Вывод сбоку:
        public List<Service> SelectedApplicationServices { get => selectedApplicationServices; set { selectedApplicationServices = value; Signal(); } }
        public DTO.Application SelectedApplication { get => selectedApplication; 
            set 
            { 
                selectedApplication = value;
                SelectedStatusIndex = StatusesList.IndexOf(SelectedApplication.Status);
                Signal(nameof(SelectedStatusIndex));
                Signal(); 
            } }

        public List<string> EditStatusesList { get; set; } = new List<string>();
        public int SelectedStatusIndex { get; set; } = 0;

        //Главный ListView
        public List<DTO.Application> Applications { get; set; }

        public ListApplicationPage(Worker worker)
        {
            InitializeComponent();
            Worker = worker;

            Applications = DB.Instance.Applications.Include(s => s.IdclientNavigation).Include(s => s.IddeviceNavigation).ToList();
            StatusesList = FillStatusesList();

            DataContext = this;
        }

        private List<string> FillStatusesList()
        {
            List<string> statuses = new List<string>() { "Все статусы" };

            var result = DB.Instance.Applications.ToList();
            foreach (var item in result)
            {
                if (statuses.Count == 0)
                    statuses.Add(item.Status.ToString());
                else
                {
                    int statusYesInList = 0;
                    foreach (string status in statuses)
                    {
                        if (status == item.Status.ToString())
                            statusYesInList++;
                    }

                    if (statusYesInList == 0)
                        statuses.Add(item.Status.ToString());
                }
            }

            return statuses;
        }

        private void SaveEditSelectedApplication(object sender, RoutedEventArgs e)
        {
            DB.Instance.Applications.Update(SelectedApplication);
            DB.Instance.SaveChanges();

            Signal(nameof(Applications));
        }

        private void Search()
        {
            var result = DB.Instance.Applications
                .Include(s => s.IdclientNavigation)
                .Include(s => s.IddeviceNavigation)
                .Where(s =>
                    s.Status.ToLower().Contains(searchText.ToLower()) ||
                    s.Problem.ToLower().Contains(searchText.ToLower()) ||
                    s.IdclientNavigation.Firstname.ToLower().Contains(searchText.ToLower()) ||
                    s.IdclientNavigation.Secondname.ToLower().Contains(searchText.ToLower()) ||
                    s.IdclientNavigation.Patronymic.ToLower().Contains(searchText.ToLower()) ||
                    s.IddeviceNavigation.Model.ToLower().Contains(searchText.ToLower())
                    );
            if (StatusIndex == 0)
            {
                Applications = result.ToList();
            }
            else
            {
                Applications = result.Where(s => s.Status == StatusesList[StatusIndex]).ToList();
            }

            if (DataStart != "" && DataEnd == "")
            {
                DateOnly StartData;
                if (DateOnly.TryParse(DataStart, out StartData))
                    Applications = Applications.Where(s => s.Data >= StartData).ToList();
            } else if(DataEnd != "" && DataStart == "")
            {
                DateOnly EndData;
                if (DateOnly.TryParse(DataEnd, out EndData))
                    Applications = Applications.Where(s => s.Data <= EndData).ToList();
            }
            else
            {
                DateOnly StartData;
                DateOnly EndData;
                if(DateOnly.TryParse(DataEnd, out EndData) && DateOnly.TryParse(DataStart, out StartData))
                    Applications = Applications.Where(s => s.Data <= EndData && s.Data >= StartData).ToList();
            }



            Signal(nameof(Applications));
        }

        private void AddNewApplication(object sender, RoutedEventArgs e)
        {
            new EditOrAddApplication().ShowDialog();
        }
    }
}
