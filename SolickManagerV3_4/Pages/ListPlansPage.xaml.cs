using Microsoft.EntityFrameworkCore;
using SolickManagerV3_4.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;
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
    /// Логика взаимодействия для ListPlansPage.xaml
    /// </summary>
    public partial class ListPlansPage : Page, INotifyPropertyChanged
    {
        private Plan selectedPlan;
        private Howpay editHowPay;

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        //Работник за приложением
        public Worker Worker { get; set; }

        // Основные данные
        public List<Plan> Plans { get; set; }
        public Plan SelectedPlan
        {
            get => selectedPlan;
            set
            {
                selectedPlan = value;
                if (SelectedPlan != null)
                {
                    EditHowPay = SelectedPlan.IdhowpayNavigation;
                    Signal();
                }
            }
        }

        // Поиск
        public string SearchText { get; set; } = "";

        // Данные для быстрого редактирования
        public List<Howpay> HowPays { get; set; }
        public Howpay EditHowPay { get => editHowPay; set { editHowPay = value; Signal(); } }

        public ListPlansPage(Worker worker)
        {
            InitializeComponent();

            Worker = worker;

            HowPays = DB.Instance.Howpays.Where(s => s.Deleted == false).ToList();
            Search();

            DataContext = this;
        }

        private void Search()
        {
            var result = DB.Instance.Plans.Include(s => s.IdhowpayNavigation).Where(s => s.Deleted == false);

            Plans = result.Where(s => SearchText == "" || (s.Costofone == decimal.Parse(SearchText) || s.HowPayTitle.ToLower().Contains(SearchText.ToLower()))).ToList();

            Signal(nameof(Plans));

            if (SelectedPlan != null)
                SelectedPlan = Plans.FirstOrDefault(s => s.Id == SelectedPlan.Id);

            Signal(nameof(SelectedPlan));
        }

        private void AddNewPlan(object sender, RoutedEventArgs e)
        {
            new AddOrEditPlanWindow().ShowDialog();

            Search();
        }
        private void DeleteSelectedPlan(object sender, RoutedEventArgs e)
        {
            if (SelectedPlan != null)
            {
                SelectedPlan.Deleted = true;

                DB.Instance.Plans.Update(SelectedPlan);
                DB.Instance.SaveChanges();

                Search();
            }
        }

        private void SaveEditSelectedPlan(object sender, RoutedEventArgs e)
        {
            if (SelectedPlan != null)
            {
                SelectedPlan.Idhowpay = EditHowPay.Id;
                DB.Instance.Plans.Update(SelectedPlan);
                DB.Instance.SaveChanges();

                Search();
            }
        }
    }
}
