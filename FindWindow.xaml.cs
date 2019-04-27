using System.Windows;

namespace BookContainer
{
    /// <summary>
    /// Логика взаимодействия для FindWindow.xaml
    /// </summary>
    public partial class FindWindow : Window
    {
        public FindWindow()
        {
            InitializeComponent();
            FieldCB.Items.Add("Автор");
            FieldCB.Items.Add("Имя");
            FieldCB.Items.Add("Издатель");
            FieldCB.Items.Add("Жанр");
            SearchTypeCB.Items.Add("Встроенный");
            SearchTypeCB.Items.Add("Бинарный");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (FieldCB.SelectedItem != null && SearchTypeCB.SelectedItem != null)
            {
                BookFields fieldType = BookFields.Authors;
                switch (FieldCB.SelectedItem.ToString())
                {
                    case "Автор":
                        fieldType = BookFields.Authors;
                        break;
                    case "Имя":
                        fieldType = BookFields.Name;
                        break;
                    case "Издатель":
                        fieldType = BookFields.Publisher;
                        break;
                    case "Жанр":
                        fieldType = BookFields.Genres;
                        break;
                }
                if (SearchTypeCB.SelectedItem.ToString() == "Встроенный")
                    DataContainer.books.Find(false, ValueTB.Text.ToString(), fieldType);
                else
                    DataContainer.books.Find(true, ValueTB.Text.ToString(), fieldType);
            }
        }
    }
}
