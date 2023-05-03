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
using SolickManagerV3_4.Pages;
using System.Windows.Media.Animation;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using SolickManagerV3_4.Windows;

namespace SolickManagerV3_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void Signal([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //I Notify Property Changed


        public Worker Worker { get; set; } //Вошедший сотрудник
        public bool CloseIt = false;

        // MenuStackPanel - имя StackPanel в котором находится меню

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(Worker? user)
        {
            InitializeComponent();
            Worker = user;
            Navigation.GetInstance().CurrentPage = new ListApplicationPage(Worker);

            DataContext = Navigation.GetInstance();
        }

        //метод сохранения сессии
        private void SaveWorkingShift(bool saveIt)
        {
            if (saveIt)
            {
                Workingshift ws = DB.Instance.Workingshifts.FirstOrDefault(s => s.Data == OtherFunctons.Instance.DateOnlyNow() && s.Idworker == Worker.Id);
                if (Worker != null && Worker.Idpost != 1 && ws == null)
                {
                    Workingshift wShift = new Workingshift();

                    wShift.Data = OtherFunctons.Instance.DateOnlyNow();
                    wShift.Idworker = Worker.Id;
                    wShift.Spendunits = 1;

                    DB.Instance.Workingshifts.Add(wShift);
                    DB.Instance.SaveChanges();
                }
            }
            else { }
        }



        //Анимация бокового меню
        private void AnimationMenu(object sender, RoutedEventArgs e)
        {
            OpenMenu();
        }
        private void AnimationCloseMenu(object sender, RoutedEventArgs e)
        {
            CloseMenu();
        }

        // Методы боковго окна
        private void OpenMenu()
        {
            MenuPanel.Visibility = Visibility.Visible;

            DoubleAnimation da = new DoubleAnimation();
            da.From = 30;
            da.To = 300;
            da.Duration = TimeSpan.FromMilliseconds(300);

            MenuPanel.BeginAnimation(DockPanel.WidthProperty, da);
        }
        private void CloseMenu()
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = MenuPanel.ActualWidth;
            da.To = 30;
            da.Duration = TimeSpan.FromMilliseconds(300);
            da.Completed += CloseEnd;
            MenuPanel.BeginAnimation(DockPanel.WidthProperty, da);

            Signal();
        }
        private void CloseEnd(object? sender, EventArgs e)
        {
            MenuPanel.Visibility = Visibility.Collapsed;
        }


        //Выход из окна / закрытие окна с сохранением сессии (смены) работника
        private void Exit(object sender, MouseButtonEventArgs e)
        {
            CloseIt = true;
            this.Close();
        }
        private void CloseWindow(object sender, CancelEventArgs e)
        {
            SaveWorkingShift((bool)new ConfirmationWindow("Сохранить сессию работника?").ShowDialog());
            
            if(CloseIt)
                new AuthorizationWindow().Show();
        }
        private void ExitWindow(object sender, RoutedEventArgs e)
        {
            CloseIt = true;
            this.Close();
        }

        // Переходы по вкладкам бокового меню
        private void Applications(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ListApplicationPage(Worker);

            CloseMenu();

            Signal();
        }
        private void ClientsAndDevices(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ListClientAndDevicePage(Worker);
            
            CloseMenu();
            
            Signal();
        }
        private void Services(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ListServicesPage(Worker);

            CloseMenu();

            Signal();
        }
        private void Assemblies(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ListAssembliesPage(Worker);

            CloseMenu();

            Signal();
        }
        private void Products(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ListProductsPage(Worker);

            CloseMenu();

            Signal();
        }
        private void WareHouse(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new WareHousePage(Worker);

            CloseMenu();

            Signal();
        }
        private void Accounting(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new AccountingPage(Worker);

            CloseMenu();

            Signal();
        }
        private void Management(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ManagementPage(Worker);

            CloseMenu();

            Signal();
        }
        private void Reports(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new ReportsPage(Worker);

            CloseMenu();

            Signal();
        }
        private void Settings(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new SettingsPage(Worker);

            CloseMenu();

            Signal();
        }

        private void Sales(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new SalesPage(Worker);

            CloseMenu();

            Signal();
        }
    }
}
