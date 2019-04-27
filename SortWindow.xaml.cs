using System.Windows;
using System.Windows.Controls;

namespace BookContainer
{
    /// <summary>
    /// Логика взаимодействия для SortWindow.xaml
    /// </summary>
    public partial class SortWindow : Window
    {
        public SortWindow()
        {
            InitializeComponent();
            MethodCB.Items.Add("Внутренний");
            MethodCB.Items.Add("Пирамидальный");

            TypeCB.Items.Add("По возрастанию");
            TypeCB.Items.Add("По убыванию");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MethodCB.SelectedItem != null && FieldCB.SelectedItem != null && TypeCB.SelectedItem != null)
            {
                bool type = false;
                if (MethodCB.SelectedItem.ToString() == "Пирамидальный")
                    type = true;
                bool ascend = true;
                if (TypeCB.SelectedItem.ToString() == "По убыванию")
                    ascend = false;
                BookFields a = new BookFields();
                switch (FieldCB.SelectedItem.ToString())
                {
                    case "Имя":
                        a = BookFields.Name;
                        break;
                    case "Автор":
                        a = BookFields.Authors;
                        break;
                    case "Количество страниц":
                        a = BookFields.PagesCount;
                        break;
                    case "Год создания":
                        a = BookFields.YearCreation;
                        break;
                    case "Год публикация":
                        a = BookFields.YearPublication;
                        break;
                    case "Издатель":
                        a = BookFields.Publisher;
                        break;
                    case "Жанр":
                        a = BookFields.Genres;
                        break;
                }
                DataContainer.books.Sort(type, a, ascend);
            }
            else
                MessageBox.Show("Выберите все поля");
        }

        private void MethodCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MethodCB.SelectedItem.ToString() == "Внутренний")
            {
                FieldCB.Items.Clear();
                FieldCB.Items.Add("Имя");
                FieldCB.Items.Add("Автор");
                FieldCB.Items.Add("Количество страниц");
                FieldCB.Items.Add("Год создания");
                FieldCB.Items.Add("Год публикация");
                FieldCB.Items.Add("Издатель");
                FieldCB.Items.Add("Жанр");
                TypeCB.Items.Clear();
                TypeCB.Items.Add("По возрастанию");
                TypeCB.Items.Add("По убыванию");
            }
            else if (MethodCB.SelectedItem.ToString() == "Пирамидальный")
            {
                FieldCB.Items.Clear();
                FieldCB.Items.Add("Количество страниц");
                FieldCB.Items.Add("Год создания");
                FieldCB.Items.Add("Год публикация");
                TypeCB.Items.Clear();
                TypeCB.Items.Add("По возрастанию");
            }
        }
    }
}
