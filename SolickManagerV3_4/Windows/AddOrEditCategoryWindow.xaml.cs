using Microsoft.EntityFrameworkCore;
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
        private string searchText = "";

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        // Строка поиска
        public string SearchText { get => searchText; set { searchText = value; Search(); } }

        // Данные из которых необходимо выбирать характеристики
        public List<Characteristic> Characteristics { get; set; } = new List<Characteristic>();
        public Characteristic SelectedCharacteristic { get; set; }

        // Данные категории
        public Category Category { get; set; } = new Category();
        public List<Characteristic> CategoryCharacteristics { get; set; } = new List<Characteristic>();
        public Characteristic SelectedCategoryCharacteristic { get; set; }

        public AddOrEditCategoryWindow()
        {
            InitializeComponent();

            CategoryCharacteristics = new List<Characteristic>();

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
            var result = DB.Instance.Characteristics.Where(s => (this.SearchText == "" || s.Title.ToLower().Contains(SearchText.ToLower())) && s.Deleted == false).OrderBy(s => s.Id);

            Characteristics = result.ToList();

            for (int i = 0; i < CategoryCharacteristics.Count; i++)
            {
                if (Characteristics.FirstOrDefault(s => s.Id == CategoryCharacteristics[i].Id) != null)
                {
                    Characteristics.Remove(Characteristics.FirstOrDefault(s => s.Id == CategoryCharacteristics[i].Id));
                }
            }

            Signal(nameof(Characteristics));
        }

        private void DeleteCharacteristic(object sender, RoutedEventArgs e)
        {
            if(SelectedCategoryCharacteristic != null)
                CategoryCharacteristics.Remove(SelectedCategoryCharacteristic);

            CategoryCharacteristicsListView.Items.Refresh();

            Search();
        }
        private void AddCharacteristic(object sender, RoutedEventArgs e)
        {
            if (SelectedCharacteristic != null)
                CategoryCharacteristics.Add(SelectedCharacteristic);

            CategoryCharacteristicsListView.Items.Refresh();

            Search();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if(Category != null && Category.Title != "" && CategoryCharacteristics != null && CategoryCharacteristics.Count > 0)
            {
                if (Category.Id == 0)
                {
                    DB.Instance.Categories.Add(Category);
                    DB.Instance.SaveChanges();

                    int categoryId = DB.Instance.Categories.OrderBy(s => s.Id).Last().Id;

                    for (int i = 0; i < CategoryCharacteristics.Count; i++)
                    {
                        Categorycharacteristic cc = new Categorycharacteristic();

                        cc.Idcharacteristic = CategoryCharacteristics[i].Id;
                        cc.Idcategory = categoryId;

                        DB.Instance.Add(cc);
                    }

                    DB.Instance.SaveChanges();

                    MessageBox.Show("Добавлена новая категория!");
                }
                else
                {
                    DB.Instance.Categories.Update(Category);
                    DB.Instance.SaveChanges();

                    
                    List<Categorycharacteristic> chars = DB.Instance.Categorycharacteristics.Where(s => s.Idcategory == Category.Id && s.Deleted == true).ToList();
                    if(chars.Count > 0)
                    {
                        DB.Instance.Categorycharacteristics.RemoveRange(chars);
                    }

                    chars = DB.Instance.Categorycharacteristics.Include(s => s.IdcharacteristicNavigation).Where(s => s.Idcategory == this.Category.Id && s.Deleted == false).ToList();
                    if(chars!= null && chars.Count > 0)
                    {
                        for(int i = 0; i < chars.Count(); i++) 
                        { 
                            if(CategoryCharacteristics.FirstOrDefault(s => s.Id == chars[i].IdcharacteristicNavigation.Id) == null)
                            {
                                DB.Instance.Categorycharacteristics.Remove(chars[i]);
                            }
                        }
                    }

                    if (CategoryCharacteristics != null && CategoryCharacteristics.Count > 0)
                    {
                        for (int i = 0; i < CategoryCharacteristics.Count(); i++)
                        {
                            if (DB.Instance.Categorycharacteristics.FirstOrDefault(s => s.Idcharacteristic == CategoryCharacteristics[i].Id && s.Idcategory == Category.Id) == null)
                            {
                                DB.Instance.Categorycharacteristics.Add(new Categorycharacteristic() { Idcharacteristic = CategoryCharacteristics[i].Id, Idcategory = Category.Id });
                            }
                        }
                    }

                    DB.Instance.SaveChanges();

                    MessageBox.Show("Категория успешно изменена!");
                }
                this.Close();
            }
        }
    }
}
