using Microsoft.EntityFrameworkCore;
using SolickManagerV3_4.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
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
using SolickManagerV3_4.Windows;

namespace SolickManagerV3_4.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListWorkersPage.xaml
    /// </summary>
    public partial class ListWorkersPage : Page, INotifyPropertyChanged
    {
        private string searchDataStart = "";
        private string searchDataEnd = "";
        private string searchFIO = "";
        private Post searchPost;
        private Worker selectedWorker;
        private Post editPost;

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        //Работник за приложением
        public Worker Worker { get; set; }

        // Основные данные 
        public List<Worker> Workers { get; set; }
        public Worker SelectedWorker
        {
            get => selectedWorker;
            set
            {
                selectedWorker = value;
                if (SelectedWorker != null)
                    EditPost = SelectedWorker.IdpostNavigation;

                Signal();
            }
        }
        public List<Post> Posts { get; set; } = new List<Post>();

        // Данные поиска
        public Post SearchPost { get => searchPost; set { searchPost = value; Search(); } }
        public string SearchDataStart { get => searchDataStart; set { searchDataStart = value; Search(); } }
        public string SearchDataEnd { get => searchDataEnd; set { searchDataEnd = value; Search(); } }
        public string SearchFIO { get => searchFIO; set { searchFIO = value; Search(); } }

        // Данные для быстрого редактирования
        public List<Post> EditListPosts { get; set; }
        public Post EditPost { get => editPost; set { editPost = value; Signal(); } }

        public ListWorkersPage(Worker worker)
        {
            InitializeComponent();

            Worker = worker;

            FillPostList();
            Search();

            DataContext = this;
        }

        public void Search()
        {
            var result = DB.Instance.Workers.Include(s => s.IdpostNavigation).Where(s => s.Deleted == false);


            DateOnly dataStart;
            DateOnly dataEnd;
            if (DateOnly.TryParse(SearchDataStart, out dataStart) && !DateOnly.TryParse(SearchDataEnd, out dataEnd))
                Workers = result.Where(s => s.Birthday >= dataStart).OrderBy(s => s.Surname).ThenBy(s => s.Firstname).ThenBy(s => s.Patronymic).ToList();
            else if (!DateOnly.TryParse(SearchDataStart, out dataStart) && DateOnly.TryParse(SearchDataEnd, out dataEnd))
                Workers = result.Where(s => s.Birthday <= dataEnd).OrderBy(s => s.Surname).ThenBy(s => s.Firstname).ThenBy(s => s.Patronymic).ToList();
            else if (DateOnly.TryParse(SearchDataStart, out dataStart) && DateOnly.TryParse(SearchDataEnd, out dataEnd))
                Workers = result.Where(s => s.Birthday >= dataStart && s.Birthday <= dataEnd).OrderBy(s => s.Surname).ThenBy(s => s.Firstname).ThenBy(s => s.Patronymic).ToList();
            else
                Workers = result.OrderBy(s => s.Surname).ThenBy(s => s.Firstname).ThenBy(s => s.Patronymic).ToList();

            if (SearchPost.Id == 0) { }
            else
                Workers = Workers.Where(s => s.Idpost == SearchPost.Id).ToList();

            if (SearchFIO != "")
                Workers = Workers.Where(s => s.FIO.ToLower().Contains(SearchFIO.ToLower())).ToList();

            Signal(nameof(Workers));

            if (SelectedWorker != null)
            {
                SelectedWorker = Workers.FirstOrDefault(s => s.Id == SelectedWorker.Id);
                Signal(nameof(SelectedWorker));
            }
        }

        public void FillPostList()
        {
            List<Post> posts = DB.Instance.Posts.Where(s => s.Deleted == false).ToList();

            Posts.Clear();
            Posts.Add(new Post { Id = 0, Title = "Все должности" });
            Posts.AddRange(posts);

            EditListPosts = posts;

            SearchPost = Posts[0];

            Signal(nameof(EditListPosts));
            Signal(nameof(SearchPost));
            Signal(nameof(Posts));
        }

        private void AddNewWorker(object sender, RoutedEventArgs e)
        {
            new AddOrEditWorkerWindow().ShowDialog();

            Search();
        }
        private void EditSelectedWorker(object sender, RoutedEventArgs e)
        {
            if(SelectedWorker != null)
            {
                new AddOrEditWorkerWindow(SelectedWorker).ShowDialog();

                Search();
            }
        }
        private void DeleteSelectedWorker(object sender, RoutedEventArgs e)
        {
            if(SelectedWorker!= null)
            {
                SelectedWorker.Deleted = true;

                DB.Instance.Workers.Update(SelectedWorker);
                DB.Instance.SaveChanges();

                Search();
            }
        }

        private void SaveEditWorker(object sender, RoutedEventArgs e)
        {
            if(SelectedWorker != null && EditPost != null)
            {
                DB.Instance.Workers.Update(SelectedWorker);
                DB.Instance.SaveChanges();

                Search();
            }
        }


    }
}
