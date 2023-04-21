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
        private List<Applicationservice> selectedApplicationServices;
        private DTO.Application selectedApplication;
        private string searchText = "";
        private string dataStart = "";
        private string dataEnd = "";
        private int statusIndex = 0;

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
        public int StatusIndex { get => statusIndex; set { statusIndex = value; Search(); } }

        //Вывод сбоку:
        public Applicationservice SelectedServiceInSelectedApplication { get; set; }
        public List<Applicationservice> SelectedApplicationServices { get => selectedApplicationServices; set { selectedApplicationServices = value; Signal(); } }
        public DTO.Application SelectedApplication
        {
            get => selectedApplication;
            set
            {
                selectedApplication = value;
                if (selectedApplication != null) {
                    SelectedStatusIndex = EditStatusesList.IndexOf(SelectedApplication.Status);
                    //SelectedApplicationServices = this.SelectedApplication.ListService.Where(s => s.Deleted == false).ToList();
                        }
                Signal(nameof(SelectedStatusIndex));
                Signal(nameof(SelectedApplicationServices));
                Signal();
            }
        }
        public List<string> EditStatusesList { get; set; } = new List<string>() 
        {
            "Принята",
            "У мастера",
            "На выдаче",
            "Завершена",
        };
        public int SelectedStatusIndex { get; set; } = 0;

        //Главный DataGrid
        public List<DTO.Application> Applications { get; set; }

        public ListApplicationPage(Worker worker)
        {
            InitializeComponent();
            Worker = worker;

            Search();
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
            SelectedApplication.Status = EditStatusesList[SelectedStatusIndex];

            DB.Instance.Applications.Update(SelectedApplication);
            DB.Instance.SaveChanges();

            StatusesList = FillStatusesList();

            Signal(nameof(SelectedApplication));
            Signal(nameof(StatusesList));

            Search();
        }

        private void Search()
        {
            var result = DB.Instance.Applications
                .Include(s => s.IdclientNavigation)
                .Include(s => s.IddeviceNavigation)
                .Where(s => (
                    s.Status.ToLower().Contains(searchText.ToLower()) ||
                    s.Problem.ToLower().Contains(searchText.ToLower()) ||
                    s.IdclientNavigation.Firstname.ToLower().Contains(searchText.ToLower()) ||
                    s.IdclientNavigation.Secondname.ToLower().Contains(searchText.ToLower()) ||
                    s.IdclientNavigation.Patronymic.ToLower().Contains(searchText.ToLower()) ||
                    s.IddeviceNavigation.Model.ToLower().Contains(searchText.ToLower())
                    ) && s.Deleted == false
                    );
            if (StatusIndex == 0)
            {
                Applications = result.OrderBy(s => s.Id).ToList();
            }
            else
            {
                Applications = result.Where(s => s.Status == StatusesList[StatusIndex]).OrderBy(s => s.Id).ToList();
            }

            if (DataStart != "" && DataEnd == "")
            {
                DateOnly StartData;
                if (DateOnly.TryParse(DataStart, out StartData))
                    Applications = Applications.Where(s => s.Data >= StartData).OrderBy(s => s.Id).ToList();
            }
            else if (DataEnd != "" && DataStart == "")
            {
                DateOnly EndData;
                if (DateOnly.TryParse(DataEnd, out EndData))
                    Applications = Applications.Where(s => s.Data <= EndData).OrderBy(s => s.Id).ToList();
            }
            else
            {
                DateOnly StartData;
                DateOnly EndData;
                if (DateOnly.TryParse(DataEnd, out EndData) && DateOnly.TryParse(DataStart, out StartData))
                    Applications = Applications.Where(s => s.Data <= EndData && s.Data >= StartData).OrderBy(s => s.Id).ToList();
            }

            Signal(nameof(Applications));
        }

        private void AddNewApplication(object sender, RoutedEventArgs e)
        {
            new EditOrAddApplication(this.Worker).ShowDialog();

            Search();

            StatusesList = FillStatusesList();
            Signal(nameof(StatusesList));
        }
        private void EditSelectedApplication(object sender, RoutedEventArgs e)
        {
            if (SelectedApplication != null)
            {
                new EditOrAddApplication(this.Worker, this.SelectedApplication).ShowDialog();
                Search();

                StatusesList = FillStatusesList();
                Signal(nameof(StatusesList));
            }
            else
                MessageBox.Show("Не выбрана ни одна заявка!");
        }
        private void DeleteSelectedApplication(object sender, RoutedEventArgs e)
        {
            if(SelectedApplication != null)
            {
                if ((bool)new ConfirmationWindow("Вы хотите удалить заявку и все связанные с ней данные?").ShowDialog())
                {
                    if (DB.Instance.Applicationservices.FirstOrDefault(s => s.Idapplication == this.SelectedApplication.Id) != null)
                    {
                        var list = DB.Instance.Applicationservices.Where(s => s.Idapplication == this.SelectedApplication.Id).ToList();
                        foreach (var appServ in list)
                        {
                            appServ.Deleted = true;
                            DB.Instance.Applicationservices.Update(appServ);
                        }
                    }
                    if (DB.Instance.Applicationproducts.FirstOrDefault(s => s.Idapplication == this.SelectedApplication.Id) != null)
                    {
                        var list = DB.Instance.Applicationproducts.Where(s => s.Idapplication == this.SelectedApplication.Id).ToList();
                        foreach (var appProd in list)
                        {
                            appProd.Deleted = true;
                            DB.Instance.Applicationproducts.Update(appProd);
                        }
                    }
                    if (DB.Instance.Applicationassemblies.FirstOrDefault(s => s.Idapplication == this.SelectedApplication.Id) != null)
                    {
                        var list = DB.Instance.Applicationassemblies.Where(s => s.Idapplication == this.SelectedApplication.Id).ToList();
                        foreach (var appAssem in list)
                        {
                            appAssem.Deleted = true;
                            DB.Instance.Applicationassemblies.Update(appAssem);
                        }
                    }

                    SelectedApplication.Deleted = true;
                    DB.Instance.Applications.Update(SelectedApplication);

                    DB.Instance.SaveChanges();

                    MessageBox.Show("Выбранная заявка удалена!");

                    StatusesList = FillStatusesList();
                    Signal(nameof(StatusesList));
                }
            }
            else
                MessageBox.Show("Не выбрана ни одна заявка!");

            Search();
        }



        private void DeleteCrossApplicationService(object sender, RoutedEventArgs e)
        {
            if (SelectedServiceInSelectedApplication != null)
            {
                SelectedServiceInSelectedApplication.Deleted = true;
                DB.Instance.Applicationservices.Update(SelectedServiceInSelectedApplication);
                DB.Instance.SaveChanges();

                
            }
            else
                MessageBox.Show("Не выбрана ни одна услуга!");

            Search();

            this.SelectedApplicationServices = Applications.FirstOrDefault(s => s.Id == this.SelectedApplication.Id).ListService.Where(s => s.Deleted == false).ToList();
            Signal(nameof(SelectedApplicationServices));
        }
        private void AddCrossApplicationService(object sender, RoutedEventArgs e)
        {
            if (SelectedApplication != null)
            {
                new AddCrossApplicationServiceWindow(this.SelectedApplication).ShowDialog();

                Signal(nameof(SelectedApplication));


                this.SelectedApplicationServices = this.SelectedApplication.ListService.Where(s => s.Deleted == false).ToList();
            }
        }
    }
}
