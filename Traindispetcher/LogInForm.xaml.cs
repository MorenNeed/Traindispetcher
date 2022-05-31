using System.Windows;
using System.Windows.Input;

namespace Traindispetcher
{
    /// <summary>
    /// Логика взаимодействия для LogInForm.xaml
    /// </summary>
    public partial class LogInForm : Window
    {
        public LogInForm()
        {
            InitializeComponent();
        }
        private void AuthCheck()
        {
            if (MainWindow.logedUser.LogCheck(logTextBox.Text, passwordTextBox.Text) == 2)
            {
                Application.Current.MainWindow.Show();
                this.Close();
            }
            else 
            {
                MessageBox.Show("Введіть правильні дані авторизації.", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            AuthCheck();
        }

        private void LogInForm_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                AuthCheck();
            }
        }

        private void LogInForm_Closed(object sender, System.EventArgs e)
        {
            Application.Current.MainWindow.Show();
        }
    }
}
