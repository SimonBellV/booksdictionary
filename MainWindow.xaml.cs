using System.Windows;

namespace BookContainer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContainer.books = new BookDictionary();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataContainer.books.ExtractFromFile();
                MessageBox.Show("Данные успешно извлечены из файла");
            }
            catch { MessageBox.Show("Непредвиденная ошибка"); }
            Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                DataContainer.books.SaveToFile();
                MessageBox.Show("Данные успешно сохранены");
            }
            catch { MessageBox.Show("Непредвиденная ошибка"); }
            Refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddBook f = new AddBook();
            f.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SortWindow f = new SortWindow();
            f.Show();
        }

        private void Refresh()
        {
            BooksDG.ItemsSource = null;
            BooksDG.ItemsSource = DataContainer.books.Library.Values;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            FindWindow f = new FindWindow();
            f.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (GenreTB.Text != null)
            {
                int count = 0;
                foreach (var book in DataContainer.books.Library)
                    if (book.Value.Genre == GenreTB.Text)
                        count++;
                if (count != 0)
                    MessageBox.Show("Кол-во книг заданного жанра: " + count);
                else
                    MessageBox.Show("Нет книг этого жанра");
            }
        }
    }
}
