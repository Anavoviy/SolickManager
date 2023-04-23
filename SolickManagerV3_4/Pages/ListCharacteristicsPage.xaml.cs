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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SolickManagerV3_4.Windows;

namespace SolickManagerV3_4.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListCharacteristicsPage.xaml
    /// </summary>
    public partial class ListCharacteristicsPage : Page, INotifyPropertyChanged
    {
        private Characteristic selectedCharacteristic;
        private string searchText = "";

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        //Работник за приложением
        public Worker Worker { get; set; }

        // Поиск
        public string SearchText { get => searchText; set { searchText = value; Search(); } }
        // Основные данные
        public List<Characteristic> Characteristics { get; set; } = new List<Characteristic>();
        public Characteristic SelectedCharacteristic { get => selectedCharacteristic; set { selectedCharacteristic = value; Signal(); } }


        public ListCharacteristicsPage(DTO.Worker worker)
        {
            InitializeComponent();

            Worker = worker;
            Search();

            DataContext = this;
        }

        public void Search()
        {
            var result = DB.Instance.Characteristics.Where(s => (s.Title.ToLower().Contains(SearchText.ToLower()) || this.SearchText == "") && s.Deleted == false);

            Characteristics = result.OrderBy(s => s.Id).ToList();

            Signal(nameof(Characteristics));
        }

        private void AddNewCharacteristic(object sender, RoutedEventArgs e)
        {
            new AddOrEditCharacteristicWindow().ShowDialog();

            Search();
        }

        private void EditSelectedCharacteristic(object sender, RoutedEventArgs e)
        {
            if (SelectedCharacteristic != null)
            {
                new AddOrEditCharacteristicWindow(SelectedCharacteristic).ShowDialog();

                Search();
            }
        }

        private void DeleteSelectedCharacteristic(object sender, RoutedEventArgs e)
        {
            if (SelectedCharacteristic != null)
            {
                SelectedCharacteristic.Deleted = true;

                DB.Instance.Update(SelectedCharacteristic);

                DB.Instance.SaveChanges();

                Search();
            }
        }

        private void SaveEditSelectedCharacteristic(object sender, RoutedEventArgs e)
        {
            if (SelectedCharacteristic != null)
            {
                DB.Instance.Characteristics.Update(SelectedCharacteristic);

                DB.Instance.SaveChanges();

                Search();
            }
        }
    }
}
