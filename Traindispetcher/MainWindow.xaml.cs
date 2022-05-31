using System.Windows;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace Traindispetcher
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataAccess DataConnection;
        //public static DataAccess DataConnection;
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

            //TrainrideGroupBox.Visibility = Visibility.Hidden;
            
            this.SizeToContent = SizeToContent.Manual;
            TrainrideListDG.Height = 420;
            this.Width = TrainrideListDG.Margin.Left + TrainrideListDG.RenderSize.Width + 90;
            this.Height = TrainrideListDG.Margin.Top + TrainrideListDG.RenderSize.Height + 90;

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
    }
}
