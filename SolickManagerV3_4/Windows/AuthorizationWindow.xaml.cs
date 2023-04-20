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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
      public partial class AuthorizationWindow : Window, INotifyPropertyChanged
      {
            private Visibility notVerifyVisibility { get; set; } = Visibility.Hidden;
            private Visibility loading { get; set; } = Visibility.Collapsed;
            private Visibility authorization { get; set; } = Visibility.Visible;

            public string CaptchaVerify { get; set; }
            public string Login { get; set; }
            public int ProgressLoading { get; set; } = 0;

            public Visibility Loading { get => loading; set { loading = value; Signal(); } }
            public Visibility Authorization { get => authorization; set { authorization = value; Signal(); } }
            public Visibility NotVerifyVisibility { get => notVerifyVisibility; set { notVerifyVisibility = value; Signal(); } }

            //captchCanvas - название Канваса
            //passbox - название ПассвордБокса

            public event PropertyChangedEventHandler? PropertyChanged;
            private void Signal([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public AuthorizationWindow()
            {
                InitializeComponent();



                DataContext = this;

            }


            //Событие при клике на кнопку, которое вызывает проверку на вход пользователя 
            //Событие на кнопку войти!
            private void VerifyClick(object sender, RoutedEventArgs e)
            {


                if (CaptchaValid())
                    CheckAuth(Login, passbox.Password);
                else
                {
                    GenerateCaptcha();
                }
            }

            //Метод вызывающий 2,5секундную анимацию крутящегося вентилятора с прогресс баром
            private void LoadingAnimation()
            {
                DoubleAnimation animation = new DoubleAnimation();
                animation.From = 0;
                animation.To = 100;
                animation.Duration = new Duration(TimeSpan.FromSeconds(1));
                animation.Completed += LoadingCompleted;
                progressbar.BeginAnimation(System.Windows.Controls.Primitives.RangeBase.ValueProperty, animation);
            }

            //Метод, выполняющейся при завершении анимации загрузки
            private void LoadingCompleted(object? sender, EventArgs e)
            {
                var user = DB.Instance.Workers.FirstOrDefault(s => s.Login == Login && s.Password == passbox.Password);
                new MainWindow(user).Show();
                Close();
            }

            //Code by DyaDya Pushkin
            //Проверка правильности пароля и логина
            private void CheckAuth(string login, string pass)
            {

                var user = DB.Instance.Workers.FirstOrDefault(s => s.Login == login && s.Password == pass);
                if (user == null)
                { // неудачная авторизация
                    GenerateCaptcha();
                }
                else
                {
                    Authorization = Visibility.Collapsed;
                    Loading = Visibility.Visible;

                    LoadingAnimation();
                }
            }

            //Код появляения капчи
            Random random = new Random();
            string captchaText = null;
            private void GenerateCaptcha()
            {
                captchaText = GenerateText();
                captchCanvas.Children.Clear();
                NotVerifyVisibility = Visibility.Visible;
                int x, y;
                x = y = 10;
                foreach (var s in captchaText)
                {
                    TextBlock textBlock = new TextBlock();
                    textBlock.FontSize = 25;
                    textBlock.Text = s.ToString();
                    captchCanvas.Children.Add(textBlock);
                    Canvas.SetLeft(textBlock, x);
                    Canvas.SetTop(textBlock, y);
                    x += 8 + random.Next(-5, 10);
                    y = 13 + random.Next(-5, 5);
                }
                var line = new Line();
                line.X1 = random.Next(5, 10);
                line.X2 = 50 + random.Next(5, 15);
                line.Y1 = random.Next(15, 20);
                line.Y2 = random.Next(10, 15);
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 2;

                captchCanvas.Children.Add(line);
                Canvas.SetLeft(line, random.Next(5, 15));
                Canvas.SetTop(line, random.Next(5, 15));
            }

            //Проверка капчи на правильность ввода
            private bool CaptchaValid()
            {
                if (captchaText == null)
                    return true;
                return captchaText == CaptchaVerify;
            }

            (int, int) digits = (48, 58);
            (int, int) charsUpper = (65, 91);
            (int, int) charsLower = (97, 123);

            //Создание текста дл капчи
            private string GenerateText()
            {
                string result = null;
                for (int i = 0; i < 4; i++)
                {
                    char s;
                    switch (random.Next(3))
                    {
                        case 0: s = (char)random.Next(digits.Item1, digits.Item2); break;
                        case 1: s = (char)random.Next(charsUpper.Item1, charsUpper.Item2); break;
                        default: s = (char)random.Next(charsLower.Item1, charsLower.Item2); break;
                    }
                    result += s;
                }
                return result;
            }


       }
}

