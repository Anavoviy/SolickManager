using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для ListApplicationPage.xaml
    /// </summary>
    public partial class ListApplicationPage : Page
    {
        public Worker Worker { get; set; }

        //Фильтры
        public string SearchText { get; set; } = "";
        public List<string> StatusesList { get; set; }
        public int SelectedStatusIndex { get; set; } = 0;

        //Вывод сбоку:
        public List<Service> SelectedApplicationServices { get; set; }
        public DTO.Application SelectedApplication { get; set; }

        //Главный ListView
        public List<DTO.Application> Applications { get; set; }

        public ListApplicationPage(Worker worker)
        {
            InitializeComponent();
            Worker = worker;

            Applications = DB.Instance.Applications.Include(s => s.IdclientNavigation).Include(s => s.IddeviceNavigation).ToList();

            DataContext = this;
        }
    }
}
