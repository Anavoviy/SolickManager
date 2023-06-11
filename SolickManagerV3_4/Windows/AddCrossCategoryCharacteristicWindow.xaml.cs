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
using System.Windows.Shapes;
using SolickManagerV3_4.DTO;

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddCrossCategoryCharacteristicWindow.xaml
    /// </summary>
    public partial class AddCrossCategoryCharacteristicWindow : Window
    {
        private string editTitle = "";

        // Строка поиска
        public string EditTitle { get => editTitle; set { editTitle = value; Search(); } }

        // Основные данные
        public List<Characteristic> CharacteristicsList { get; set; } = new List<Characteristic>();
        public Characteristic SelectedCharacteristic { get; set; }

        // Данные, нужные при добавлении новой записи в кросс-таблицу
        private Category Category { get; set; }


        public AddCrossCategoryCharacteristicWindow(DTO.Category selectedCategory)
        {
            InitializeComponent();

            Category = selectedCategory;
            Search();

            DataContext = this;
        }

        private void Search()
        {
            var result = DB.Instance.Characteristics.Where(s => (EditTitle == "" || s.Title.ToLower().Contains(EditTitle.ToLower()))
                                                           && s.Deleted == false);

            CharacteristicsList = result.OrderBy(s => s.Id).ToList();
        }

        private void VisibillityCharacteristic(object sender, RoutedEventArgs e)
        {
            if(CharacteristicBorder.Visibility == Visibility.Visible)
                CharacteristicBorder.Visibility = Visibility.Collapsed;
            else
                CharacteristicBorder.Visibility = Visibility.Visible;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if(Category != null && (SelectedCharacteristic != null || EditTitle != ""))
            {
                Categorycharacteristic cc = new Categorycharacteristic();
                cc.Idcategory = Category.Id;

                if (EditTitle != "")
                {
                    DB.Instance.Characteristics.Add(new Characteristic() { Title = EditTitle });
                    DB.Instance.SaveChanges();

                    cc.Idcharacteristic = DB.Instance.Characteristics.Where(s => s.Deleted == false).OrderBy(s => s.Id).Last().Id;
                }
                else
                    cc.Idcharacteristic = SelectedCharacteristic.Id;

                if (DB.Instance.Categorycharacteristics.FirstOrDefault(s => s.Idcharacteristic == cc.Idcharacteristic && s.Idcategory == cc.Idcategory) == null)
                {
                    DB.Instance.Categorycharacteristics.Add(cc);
                    DB.Instance.SaveChanges();

                    MessageBox.Show("Успешно добавлена харкетристика!");

                    this.Close();
                }
                else if(DB.Instance.Categorycharacteristics.FirstOrDefault(s => s.Idcharacteristic == cc.Idcharacteristic && s.Idcategory == cc.Idcategory).Deleted)
                {
                    cc = DB.Instance.Categorycharacteristics.FirstOrDefault(s => s.Idcharacteristic == cc.Idcharacteristic && s.Idcategory == cc.Idcategory);

                    cc.Deleted = false;

                    DB.Instance.Categorycharacteristics.Update(cc);
                    DB.Instance.SaveChanges();

                    MessageBox.Show("Успешно добавлена харкетристика!");

                    this.Close();
                }
            }
            else 
                MessageBox.Show("Характеристкиа не добавлена!");
        }
    }
}
