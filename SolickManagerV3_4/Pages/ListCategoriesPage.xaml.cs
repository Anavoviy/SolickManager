using SolickManagerV3_4.DTO;
using SolickManagerV3_4.Windows;
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
    /// Логика взаимодействия для ListCategoriesPage.xaml
    /// </summary>
    public partial class ListCategoriesPage : Page, INotifyPropertyChanged
    {
        private string searchText = "";
        private Category selectedCategory;

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
        public List<Category> Categories { get; set; } = new List<Category>();
        public Category SelectedCategory { get => selectedCategory; 
            set 
            { 
                selectedCategory = value; 
            
                if(SelectedCategory != null)
                {
                    CategoryCharacteristics = SelectedCategory.Characteristics;
                    Signal(nameof(SelectedCategory));
                    Signal(nameof(CategoryCharacteristics));
                }
            } }

        // Боковое окошко
        public List<Characteristic> CategoryCharacteristics { get; set; } = new List<Characteristic>();
        public Characteristic SelectedCategoryCharacteristic { get; set; }



        public ListCategoriesPage(DTO.Worker worker)
        {
            InitializeComponent();

            Worker = worker;

            Search();

            DataContext = this;
        }

        public void Search()
        {
            var result = DB.Instance.Categories.Where(s => (SearchText == "" || s.Title.ToLower().Contains(SearchText.ToLower())) && s.Deleted == false);

            Categories = result.OrderBy(s => s.Id).ToList();

            Signal(nameof(Categories));
        }

        private void AddCharacteristicCategory(object sender, RoutedEventArgs e)
        {
            if (SelectedCategory != null)
            {
                new AddCrossCategoryCharacteristicWindow(SelectedCategory).ShowDialog();

                Search();

                SelectedCategory = Categories.FirstOrDefault(s => s.Id == SelectedCategory.Id);

                Signal(nameof(SelectedCategory));
            }
        }

        private void DeleteCharacteristicCategory(object sender, RoutedEventArgs e)
        {
            if (SelectedCategory != null && CategoryCharacteristics.Count > 0 && SelectedCategoryCharacteristic != null)
            {

                Categorycharacteristic cc = DB.Instance.Categorycharacteristics.FirstOrDefault(s => s.Idcharacteristic == SelectedCategoryCharacteristic.Id
                                                                   && s.Idcategory == SelectedCategory.Id);

                cc.Deleted = true;

                DB.Instance.Categorycharacteristics.Update(cc);

                DB.Instance.SaveChanges();

                Search();

                if (Categories.Count > 0)
                    SelectedCategory = Categories.FirstOrDefault(s => s.Id == SelectedCategory.Id);
            }
        }

        private void AddNewCategory(object sender, RoutedEventArgs e)
        {
            new AddOrEditCategoryWindow().ShowDialog();

            Search();
        }

        private void EditSelectedCategory(object sender, RoutedEventArgs e)
        {
            if (SelectedCategory != null)
            {
                new AddOrEditCategoryWindow(SelectedCategory).ShowDialog();

                Search();
            }
        }

        private void DeleteSelectedCategory(object sender, RoutedEventArgs e)
        {
            if (SelectedCategory != null)
            {
                SelectedCategory.Deleted = true;

                DB.Instance.Categories.Update(SelectedCategory);

                DB.Instance.SaveChanges();

                MessageBox.Show("Успешно удалена категория товаров!");

                Search();
            }
        }

        private void SaveEditSelectedCategory(object sender, RoutedEventArgs e)
        {
            if (SelectedCategory != null)
            {
                DB.Instance.Update(SelectedCategory);

                DB.Instance.SaveChanges();

                Search();
            }
        }
    }
}
