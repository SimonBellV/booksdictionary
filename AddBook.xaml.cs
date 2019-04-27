using System;
using System.Windows;

namespace BookContainer
{
    /// <summary>
    /// Логика взаимодействия для AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        public AddBook()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Book book;
            try
            {
                book = new Book(NameTB.Text, AuthorTB.Text, YPIUP.Value.Value, YCIUP.Value.Value, PublisherTB.Text, PageCountIUD.Value.Value, GenreTB.Text);
                DataContainer.books.Add(book);
            }
            catch (Exception ex) { MessageBox.Show("Ошибка. Возможно, не заполнены некоторые поля"); MessageBox.Show(ex.ToString()); }
        }
    }
}
