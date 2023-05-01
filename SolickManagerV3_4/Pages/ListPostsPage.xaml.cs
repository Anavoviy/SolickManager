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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SolickManagerV3_4.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListPostsPage.xaml
    /// </summary>
    public partial class ListPostsPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private string searchTitle = "";

        // Работник за приложением
        public Worker Worker { get; set; }

        // Основные данные
        public List<Post> Posts { get; set; }
        public Post SelectedPost { get; set; }

        // Поиск
        public string SearchTitle { get => searchTitle; set { searchTitle = value; Search(); } }
        public ListPostsPage(Worker worker)
        {
            InitializeComponent();

            Worker = worker;

            Search();

            DataContext = this;
        }

        private void Search()
        {
            var result = DB.Instance.Posts.Where(s => (SearchTitle == "" || s.Title.ToLower().Contains(SearchTitle.ToLower())) && s.Deleted == false);

            Posts = result.OrderBy(s => s.Title).ToList();

            Signal(nameof(Posts));

            if (SelectedPost != null)
                SelectedPost = Posts.FirstOrDefault(s => s.Id == SelectedPost.Id);

            Signal(nameof(SelectedPost));
        }

        private void AddNewPost(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteSelectedPost(object sender, RoutedEventArgs e)
        {
            if(SelectedPost != null)
            {
                SelectedPost.Deleted = true;

                DB.Instance.Posts.Update(SelectedPost);
                DB.Instance.SaveChanges();

                Search();
            }
        }

        private void SaveEditSelectedPost(object sender, RoutedEventArgs e)
        {
            if(SelectedPost != null)
            {
                DB.Instance.Posts.Update(SelectedPost);
                DB.Instance.SaveChanges();

                Search();
            }
        }
    }
}
