using SolickManagerV3_4.DTO;
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
using System.Windows.Shapes;

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditCategoryWindow.xaml
    /// </summary>
    public partial class AddOrEditCategoryWindow : Window, INotifyPropertyChanged
    {
        private List<Characteristic> categoryCharacteristics = new List<Characteristic>();

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        // Строка поиска
        public string SearchText { get; set; } = "";

        // Данные из которых необходимо выбирать характеристики
        public List<Characteristic> Characteristics { get; set; } = new List<Characteristic>();
        public Characteristic SelectedCharacteristic { get; set; }

        // Данные категории
        public Category Category { get; set; } = new Category();
        public List<Characteristic> CategoryCharacteristics { get => categoryCharacteristics; set { categoryCharacteristics = value; Signal(); } }
        public Characteristic SelectedCategoryCharacteristic { get; set; }

        public AddOrEditCategoryWindow()
        {
            InitializeComponent();

            Search();

            DataContext = this;
        }

        public AddOrEditCategoryWindow(Category selectedCategory)
        {
            InitializeComponent();

            Category = selectedCategory;
            CategoryCharacteristics = Category.Characteristics;

            Search();

            DataContext = this;
        }

        private void Search()
        {
            var result = DB.Instance.Characteristics.Where(s => (SearchText == "" || s.Title.ToLower().Contains(SearchText.ToLower())) && s.Deleted == false);

            Characteristics = result.OrderBy(s => s.Id).ToList();
            if (CategoryCharacteristics != null)
            {
                foreach (Characteristic character in CategoryCharacteristics)
                    Characteristics.Remove(character);
            }

            Signal(nameof(CategoryCharacteristics));
            Signal(nameof(Characteristics));
        }

        private void DeleteCharacteristic(object sender, RoutedEventArgs e)
        {
            if (SelectedCategoryCharacteristic != null)
            {
                CategoryCharacteristics.Remove(SelectedCategoryCharacteristic);

                Search();
            }
        }

        private void AddCharacteristic(object sender, RoutedEventArgs e)
        {
            if (SelectedCharacteristic != null)
            {
                CategoryCharacteristics.Add(SelectedCharacteristic);

                Search();
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {

        }
    }
}
