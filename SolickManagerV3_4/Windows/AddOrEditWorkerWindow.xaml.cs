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
    public partial class AddOrEditWorkerWindow : Window
    {
        private string dataBirthday = "";
        private string login = "";

        public Worker EditWorker { get; set; } = new Worker() 
        { 
            Firstname = "",
            Surname = "",
            Phone = "",
            Email = "",
            Passport = ""
        };

        public string DataBirthday { get => dataBirthday; set { dataBirthday = value; ValidData(); } }
        public string Login { get => login; set { login = value; ValidLogin(); } }

        public List<Post> Posts { get; set; }
        public Post SelectedPost { get; set; }
        public List<Plan> Plans { get; set; }
        public Plan SelectedPlan { get; set; }

        public AddOrEditWorkerWindow()
        {
            InitializeComponent();

            Posts = DB.Instance.Posts.Where(s => s.Deleted == false).ToList();
            Plans = DB.Instance.Plans.Where(s => s.Deleted == false).ToList();

            DataContext = this;
        }
        public AddOrEditWorkerWindow(Worker OldWorker)
        {
            InitializeComponent();

            EditWorker = OldWorker;

            DataBirthday = EditWorker.Birthday.ToString("d");
            Login = EditWorker.Login;
            SelectedPost = DB.Instance.Posts.FirstOrDefault(s => s.Id == EditWorker.Idpost);
            SelectedPlan = DB.Instance.Plans.FirstOrDefault(s => s.Id == EditWorker.Idplan);

            Posts = DB.Instance.Posts.Where(s => s.Deleted == false).ToList();
            Plans = DB.Instance.Plans.Where(s => s.Deleted == false).ToList();

            DataContext = this;
        }

        private void ValidData()
        {
            DateOnly data;
            if (DateOnly.TryParse(DataBirthday, out data))
            {
                BirthdayTextBox.Foreground = new SolidColorBrush(Colors.Black);
                SaveButton.IsEnabled = true;
            }
            else
            {
                BirthdayTextBox.Foreground = new SolidColorBrush(Colors.Red);
                SaveButton.IsEnabled = false;
            }
        }
        private void ValidLogin()
        {
            if (DB.Instance.Workers.FirstOrDefault(s => s.Login == Login) == null || (Login == EditWorker.Login))
            {
                LoginTextBox.Foreground = new SolidColorBrush(Colors.Black);
                SaveButton.IsEnabled = true;
            }
            else
            {
                LoginTextBox.Foreground = new SolidColorBrush(Colors.Red);
                SaveButton.IsEnabled = false;
            }
        }

        private void SaveEditWorker(object sender, RoutedEventArgs e)
        {
            if (EditWorker != null && DataBirthday != "" && Login != "" && SelectedPlan != null && SelectedPost != null && EditWorker.Passport != "")
            {
                EditWorker.Birthday = DateOnly.Parse(DataBirthday);
                EditWorker.Login = Login;
                EditWorker.Idplan = SelectedPlan.Id;
                EditWorker.Idpost = SelectedPost.Id;

                if (EditWorker.Id == 0)
                    DB.Instance.Workers.Add(EditWorker);
                else
                    DB.Instance.Workers.Update(EditWorker);

                DB.Instance.SaveChanges();

                MessageBox.Show("Успешно добавлен новый сотрудник!");

                this.Close();
            }
            else
                MessageBox.Show("Не заполнены необходимые данные!");
        }
    }
}
