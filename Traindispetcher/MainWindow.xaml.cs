using System;
using System.Windows;
using static Traindispetcher.GlobalClass;

namespace Traindispetcher
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DataAccess DataConnection;

        public static Trainride editedTrainride;

        EditDB editedRow = new EditDB();

        public static Authorization logedUser = new Authorization();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InfoTrainrideForm_Loaded(object sender, RoutedEventArgs e)
        {

            //selTrainrideGroupBox.Visibility = Visibility.Hidden;

            DataConnection = new DataAccess();

            TrainrideListDG.ItemsSource = DataConnection.fList;

            TrainrideGroupBox.Visibility = Visibility.Hidden;

            this.SizeToContent = SizeToContent.Manual;
            TrainrideListDG.Height = 260;
            this.Width = TrainrideListDG.Margin.Left + TrainrideListDG.RenderSize.Width + 30;
            this.Height = TrainrideListDG.Margin.Top + TrainrideListDG.RenderSize.Height + 100;

            TrainrideMenuItem.Visibility = Visibility.Hidden;
            TrainrideMenuItem.Width = 0;
        }
        private void SaveDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            /*
            if ((selTrainrideGroupBox.Visibility == Visibility.Visible) && (selXY.selectedCityList.Count > 0))
            {
                selXY.WriteData(selXY.selectedCityList, selXY.selectedCityTimeList)
            }

            if (TrainrideGroupBox.Visibility == Visibility.Visible)
            {
                if ((TrainrideListDG.SelectedIndex < 0) && (!editedRow.flightAdd))
                {
                    MessageBox.Show("Оберіть у списку потяг для редагування подвійним кліком", "Увага!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    ChangeTrainrideListData(editedRow.trainrideNum);

                    editedRow.ChangeDBRow();
                }
            }
            */
        }
        private void LoadDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TrainrideListDG.ItemsSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + char.ConvertFromUtf32(13) + char.ConvertFromUtf32(13) + "Для завантаження файлу " + "виконайте команду Файл-Завантажити", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            DataAccess DataConnection = new DataAccess();

            TrainrideListDG.ItemsSource = DataConnection.fList;
        }
        private void AuthMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LogInForm logWnd = new LogInForm();
            logWnd.Show();
            this.Visibility = Visibility.Collapsed;
        }
        private void InfoTrainrideForm_Activated(object sender, System.EventArgs e)
        {
            if (Authorization.logUser == 2)
            {
                TrainrideMenuItem.Visibility = Visibility.Visible;
                TrainrideMenuItem.Width = 70;
            }
            else 
            {
                TrainrideMenuItem.Visibility = Visibility.Hidden;
                TrainrideMenuItem.Width = 0;
            }
        }
        private void EditDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            TrainrideGroupBox.Visibility = Visibility.Visible;

            this.Width += TrainrideGroupBox.Margin.Left + TrainrideGroupBox.RenderSize.Width + 40;
            this.Height = TrainrideListDG.Margin.Top + TrainrideListDG.RenderSize.Height + 100;

            MessageBox.Show("Оберіть у списку рейс для редагування подвійним кліком", "Увага!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        private void TrainrideListDG_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Trainride editedTrainride = TrainrideListDG.SelectedItem as Trainride;
            try
            {
                numTrainrideTextBox.Text = editedTrainride.number;
                cityTrainrideTextBox.Text = editedTrainride.city;
                timeTrainrideTextBox.Text = editedTrainride.departure_time.ToString(@"hh\:mm");
                freeSeatsTextBox.Text = editedTrainride.free_seats.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + char.ConvertFromUtf32(13),"",MessageBoxButton.OK,MessageBoxImage.Error);
            }

            editedRow = new EditDB();
            editedRow.TrainrideNum = TrainrideListDG.SelectedIndex;
            editedRow.TrainrideAdd = false;
        }
        private void ChangeTrainrideListData(int num) 
        {
            TimeSpan depTime;
            int seats;

            editedTrainride.number = numTrainrideTextBox.Text;
            editedTrainride.city = cityTrainrideTextBox.Text;
            if (TimeSpan.TryParse(timeTrainrideTextBox.Text, out depTime)) 
            {
                editedTrainride.departure_time = depTime;
            }
            if (int.TryParse(freeSeatsTextBox.Text.Trim('_'), out seats)) 
            {
                editedTrainride.free_seats = seats;
            }

            DataConnection.fList[num] = editedTrainride;

            TrainrideListDG.ItemsSource = null;
            TrainrideListDG.ItemsSource = DataConnection.fList;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TrainrideListDG.SelectedIndex < 0) 
            {
                MessageBox.Show("Оберіть у списку рейс для редагування подвійним кліком", "Увага!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else 
            {
                ChangeTrainrideListData(TrainrideListDG.SelectedIndex);
            }
        }
    }
}
