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
using System.Windows.Shapes;

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditPostWindow.xaml
    /// </summary>
    public partial class AddOrEditPostWindow : Window
    {
        public Post EditPost { get; set; }

        public AddOrEditPostWindow()
        {
            InitializeComponent();

            EditPost = new Post();

            DataContext = this;
        }

        private void SavePost(object sender, RoutedEventArgs e)
        {
            if(EditPost != null && (EditPost.Title != "" || EditPost.Title != " ")) 
            {
                DB.Instance.Posts.Add(EditPost);
                DB.Instance.SaveChanges();

                MessageBox.Show("Успешно добавлена новая должность!");

                this.Close();
            }
        }
    }
}
