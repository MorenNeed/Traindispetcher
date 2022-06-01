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
        public static EditDB editedRow = new EditDB();
        public static Authorization logedUser = new Authorization();
        public static int TrainrideCount;
        public static SelectData selXY;
        public static string selectedCity;
        public static TimeSpan timeTrainride;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InfoTrainrideForm_Loaded(object sender, RoutedEventArgs e)
        {

            selTrainrideGroupBox.Visibility = Visibility.Hidden;

            DataConnection = new DataAccess();

            TrainrideListDG.ItemsSource = DataConnection.fList;

            TrainrideGroupBox.Visibility = Visibility.Hidden;

            this.SizeToContent = SizeToContent.Manual;
            TrainrideListDG.Height = 420;
            this.Width = TrainrideListDG.Margin.Left + TrainrideListDG.RenderSize.Width + 90;
            this.Height = TrainrideListDG.Margin.Top + TrainrideListDG.RenderSize.Height + 90;

            TrainrideMenuItem.Visibility = Visibility.Hidden;
            TrainrideMenuItem.Width = 0;
        }
        private void SaveDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            
            if ((selTrainrideGroupBox.Visibility == Visibility.Visible) && (selXY.selectedCityList.Count > 0))
            {
                selXY.WriteData(selXY.selectedCityList, selXY.selectedCityTimeList);
            }

            if (TrainrideGroupBox.Visibility == Visibility.Visible)
            {
                if ((TrainrideListDG.SelectedIndex < 0) && (!editedRow.TrainrideAdd))
                {
                    MessageBox.Show("Оберіть у списку потяг для редагування подвійним кліком", "Увага!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    ChangeTrainrideListData(editedRow.TrainrideNum);

                    editedRow.ChangeDBRow();
                }
            }
            
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
                TrainrideMenuItem.Width = 50;
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

            this.Width = TrainrideGroupBox.Margin.Left + TrainrideGroupBox.RenderSize.Width + 90;
            this.Height = TrainrideListDG.Margin.Top + TrainrideListDG.RenderSize.Height + 90;

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

            if (editedRow.TrainrideAdd) 
            {
                editedTrainride = new Trainride(DataConnection.fList.Count + 1, "", "", TimeSpan.Zero, 0);
            }

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

            if (editedRow.TrainrideAdd)
            {
                DataConnection.fList.Add(editedTrainride);
            }
            else
            {
                DataConnection.fList[num] = editedTrainride;
            }
            TrainrideListDG.ItemsSource = null;
            TrainrideListDG.ItemsSource = DataConnection.fList;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((TrainrideListDG.SelectedIndex < 0)&&(!editedRow.TrainrideAdd)) 
            {
                MessageBox.Show("Оберіть у списку рейс для редагування подвійним кліком", "Увага!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else 
            {
                ChangeTrainrideListData(editedRow.TrainrideNum);

                editedRow.ChangeDBRow();
            }
        }
        private void AddDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            TrainrideGroupBox.Visibility = Visibility.Visible;

            this.Width = TrainrideGroupBox.Margin.Left + TrainrideGroupBox.RenderSize.Width + 90;
            this.Height = TrainrideListDG.Margin.Top + TrainrideListDG.RenderSize.Height + 90;

            numTrainrideTextBox.Text = "";
            cityTrainrideTextBox.Text = "";
            timeTrainrideTextBox.Text = "";
            freeSeatsTextBox.Text = "";

            editedRow.TrainrideAdd = true;

            editedRow.TrainrideNum = DataConnection.fList.Count;
            if (editedRow.TrainrideNum >= 85) 
            {
                editedRow.TrainrideAdd = false;
                MessageBox.Show("Кількість записів перевищує ліміт. Подвійним кліком миші оберіть запис, " + "який потрібно видалити", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void SelectXMenuItem_Click(object sender, RoutedEventArgs e)
        {
            selTrainrideGroupBox.Visibility = Visibility.Visible;
            timeTrainrideLabelY.Visibility = Visibility.Hidden;
            sTime.Visibility = Visibility.Hidden;

            this.Width = 660;
            this.Height = 480;

            cityList.Items.Clear();

            FillCityList();
        }
        private void FillCityList() 
        {
            TrainrideCount = 0;

            for (int i = 0; i < DataConnection.fList.Count; i++)
            {
                if (DataConnection.fList[i].city != null)
                {
                    if (DataConnection.fList[i].city != "") 
                    {
                        TrainrideCount++;
                    }
                }
            }
            bool nameExist = false;
            cityList.Items.Add(DataConnection.fList[0].city);

            for (int i = 1; i < TrainrideCount; i++) 
            {
                for (int j = 0; j < cityList.Items.Count; j++) 
                {
                    if (cityList.Items[j].ToString() == DataConnection.fList[i].city) 
                    {
                        nameExist = true;
                    }
                }
                if (!nameExist) 
                {
                    cityList.Items.Add(DataConnection.fList[i].city);
                }
                nameExist = false;
            
            }
        }
        private void selBtn_Click(object sender, RoutedEventArgs e)
        {
            selXY = new SelectData();

            selectedCity = "";
            selectedCity = Convert.ToString(cityList.Items[cityList.SelectedIndex]);

            selXY.SelectX(selectedCity);

            for (int i = 0; i < selXY.selectedCityList.Count; i++) 
            {
                if (selXY.selectedCityList[i].number != null) 
                {
                    TrainrideListDG.ItemsSource = selXY.selectedCityList;
                }
            }

            TimeSpan.TryParse(sTime.Text, out timeTrainride);

            selXY.SelectXY(timeTrainride);

            for (int i = 0; i < selXY.selectedCityTimeList.Count; i++) 
            {
                if (timeTrainrideLabelY.Visibility == Visibility.Hidden)
                {
                    TrainrideListDG.ItemsSource = selXY.selectedCityList;
                }
                else 
                {
                    TrainrideListDG.ItemsSource = selXY.selectedCityTimeList;
                }
            }
        }
        private void SelectXYMenuItem_Click(object sender, RoutedEventArgs e)
        {
            selTrainrideGroupBox.Visibility = Visibility.Visible;
            timeTrainrideLabelY.Visibility = Visibility.Visible;
            sTime.Visibility = Visibility.Visible;

            this.Width = 660;
            this.Height = 480;

            cityList.Items.Clear();

            FillCityList();

            MessageBox.Show("Для відбору рейсів не пізніше вказаного часу потрібно " + "також вказати місто прильоту", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void saveSelBtn_Click(object sender, RoutedEventArgs e)
        {
            selXY.WriteData(selXY.selectedCityList, selXY.selectedCityTimeList);
        }
    }
}
