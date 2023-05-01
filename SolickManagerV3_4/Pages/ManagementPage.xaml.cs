using SolickManagerV3_4.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для ManagementPage.xaml
    /// </summary>
    public partial class ManagementPage : Page
    {
        //Работник за приложением
        public Worker Worker { get; set; }

        public ManagementPage(DTO.Worker worker)
        {
            InitializeComponent();

            Worker = worker;

            DataContext = this;
        }

        private void ListWorkersButton(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ListWorkersPage(Worker);
        }

        private void ListPostsButton(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ListPostsPage(Worker);
        }

        private void ListPlansButton(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ListPlansPage(Worker);
        }
    }
}
