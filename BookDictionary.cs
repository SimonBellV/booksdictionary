using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace BookContainer
{
    public class PyramidSorting
    {
        //добавление элемента к пирамиде
        static int add2pyramidByYearPublication(Dictionary<int, Book> arr, int i, int N)
        {
            int imax;
            Book buf;
            if ((2 * i + 2) < N)
            {
                if (arr[2 * i + 1].YearPublication < arr[2 * i + 2].YearPublication) imax = 2 * i + 2;
                else imax = 2 * i + 1;
            }
            else imax = 2 * i + 1;
            if (imax >= N) return i;
            if (arr[i].YearPublication < arr[imax].YearPublication)
            {
                buf = arr[i];
                arr[i] = arr[imax];
                arr[imax] = buf;
                if (imax < N / 2) i = imax;
            }
            return i;
        }
        static int add2pyramidByYearCreation(Dictionary<int, Book> arr, int i, int N)
        {
            int imax;
            Book buf;
            if ((2 * i + 2) < N)
            {
                if (arr[2 * i + 1].YearCreation < arr[2 * i + 2].YearCreation) imax = 2 * i + 2;
                else imax = 2 * i + 1;
            }
            else imax = 2 * i + 1;
            if (imax >= N) return i;
            if (arr[i].YearCreation < arr[imax].YearCreation)
            {
                buf = arr[i];
                arr[i] = arr[imax];
                arr[imax] = buf;
                if (imax < N / 2) i = imax;
            }
            return i;
        }
        static int add2pyramidByPageCount(Dictionary<int, Book> arr, int i, int N)
        {
            int imax;
            Book buf;
            if ((2 * i + 2) < N)
            {
                if (arr[2 * i + 1].PagesCount < arr[2 * i + 2].PagesCount) imax = 2 * i + 2;
                else imax = 2 * i + 1;
            }
            else imax = 2 * i + 1;
            if (imax >= N) return i;
            if (arr[i].PagesCount < arr[imax].PagesCount)
            {
                buf = arr[i];
                arr[i] = arr[imax];
                arr[imax] = buf;
                if (imax < N / 2) i = imax;
            }
            return i;
        }
        public Dictionary<int, Book> sortingByYearCreation(Dictionary<int, Book> arr, int len)
        {
            //шаг 1: постройка пирамиды
            for (int i = len / 2 - 1; i >= 0; --i)
            {
                long prev_i = i;
                i = add2pyramidByYearCreation(arr, i, len);
                if (prev_i != i) ++i;
            }

            //шаг 2: сортировка
            Book buf;
            for (int k = len - 1; k > 0; --k)
            {
                buf = arr[0];
                arr[0] = arr[k];
                arr[k] = buf;
                int i = 0, prev_i = -1;
                while (i != prev_i)
                {
                    prev_i = i;
                    i = add2pyramidByYearCreation(arr, i, k);
                }
            }
            return arr;
        }
        public Dictionary<int, Book> sortingByYearPublication(Dictionary<int, Book> arr, int len)
        {
            //шаг 1: постройка пирамиды
            for (int i = len / 2 - 1; i >= 0; --i)
            {
                long prev_i = i;
                i = add2pyramidByYearPublication(arr, i, len);
                if (prev_i != i) ++i;
            }

            //шаг 2: сортировка
            Book buf;
            for (int k = len - 1; k > 0; --k)
            {
                buf = arr[0];
                arr[0] = arr[k];
                arr[k] = buf;
                int i = 0, prev_i = -1;
                while (i != prev_i)
                {
                    prev_i = i;
                    i = add2pyramidByYearPublication(arr, i, k);
                }
            }
            return arr;
        }
        public Dictionary<int, Book> sortingByPagesCount(Dictionary<int, Book> arr, int len)
        {
            //шаг 1: постройка пирамиды
            for (int i = len / 2 - 1; i >= 0; --i)
            {
                long prev_i = i;
                i = add2pyramidByPageCount(arr, i, len);
                if (prev_i != i) ++i;
            }

            //шаг 2: сортировка
            Book buf;
            for (int k = len - 1; k > 0; --k)
            {
                buf = arr[0];
                arr[0] = arr[k];
                arr[k] = buf;
                int i = 0, prev_i = -1;
                while (i != prev_i)
                {
                    prev_i = i;
                    i = add2pyramidByPageCount(arr, i, k);
                }
            }
            return arr;
        }
    }
    public class BookDictionary
    {
        public PyramidSorting HeapSorter = new PyramidSorting();
        public Dictionary<int, Book> Library { get; set; }

        public BookDictionary()
        {
            Library = new Dictionary<int, Book>();
        }

        public void Add(Book book)
        {
            int id;
            try
            {
                id = Library.Keys.Max() + 1;
            }
            catch
            {
                id = 0;
            }
            Library.Add(id, book);
        }

        public void SaveToFile()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (var sw = new FileStream("data.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(sw, Library);
            }
        }

        public void ExtractFromFile()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("data.dat", FileMode.OpenOrCreate))
            {
                Library = (Dictionary<int, Book>)formatter.Deserialize(fs);
            }
        }

        /// <summary>
        /// Универсальная функция сортировки
        /// </summary>
        /// <param name="type"> false - внутренний, true - хипсорт </param>
        /// <param name="field"> поле из enum BookField</param>
        /// <param name="ascend"> false - по убыванию, true - по возрастанию</param>
        public void Sort(bool type, BookFields field, bool ascend)
        {
            if (!type)
                SortInternalMethod(field, ascend);
            else
            {
                switch (field)
                {
                    case BookFields.YearCreation:
                        Library = new Dictionary<int, Book>(HeapSorter.sortingByYearCreation(Library, Library.Count));
                        break;
                    case BookFields.YearPublication:
                        Library = new Dictionary<int, Book>(HeapSorter.sortingByYearPublication(Library, Library.Count));
                        break;
                    case BookFields.PagesCount:
                        Library = new Dictionary<int, Book>(HeapSorter.sortingByPagesCount(Library, Library.Count));
                        break;
                }
            }
        }

        public void Find(bool binarySearch, string param, BookFields type)
        {
            Dictionary<int, Book> result;
            if (!binarySearch)
            {
                result = new Dictionary<int, Book>(FindInternalMethod(type, param));
                if (result.Count == 0)
                    MessageBox.Show("Не найдено ничего");
            }
            else
            {
                result = new Dictionary<int, Book>();
                int i = FindBinary(type, param);
                if (i == -1)
                    MessageBox.Show("Не найдено ничего");
                else
                    result.Add(Library.Keys.Max() + 1, Library[i]);
            }
            foreach (var book in result)
                MessageBox.Show(book.Value.GetString());
        }

        public void SortInternalMethod(BookFields type, bool ascending)
        {
            switch (type)
            {
                case BookFields.Authors:
                    Library = @ascending ? new Dictionary<int, Book>(Library.OrderBy(x => x.Value.Author).ToDictionary(x => x.Key, x => x.Value)) : new Dictionary<int, Book>(Library.OrderByDescending(x => x.Value.Author).ToDictionary(x => x.Key, x => x.Value));
                    break;
                case BookFields.Name:
                    Library = @ascending ? new Dictionary<int, Book>(Library.OrderBy(x => x.Value.Name).ToDictionary(x => x.Key, x => x.Value)) : new Dictionary<int, Book>(Library.OrderByDescending(x => x.Value.Name).ToDictionary(x => x.Key, x => x.Value));
                    break;
                case BookFields.PagesCount:
                    Library = @ascending ? new Dictionary<int, Book>(Library.OrderBy(x => x.Value.PagesCount).ToDictionary(x => x.Key, x => x.Value)) : new Dictionary<int, Book>(Library.OrderByDescending(x => x.Value.PagesCount).ToDictionary(x => x.Key, x => x.Value));
                    break;
                case BookFields.YearCreation:
                    Library = @ascending ? new Dictionary<int, Book>(Library.OrderBy(x => x.Value.YearCreation).ToDictionary(x => x.Key, x => x.Value)) : new Dictionary<int, Book>(Library.OrderByDescending(x => x.Value.YearCreation).ToDictionary(x => x.Key, x => x.Value));
                    break;
                case BookFields.YearPublication:
                    Library = @ascending ? new Dictionary<int, Book>(Library.OrderBy(x => x.Value.YearPublication).ToDictionary(x => x.Key, x => x.Value)) : new Dictionary<int, Book>(Library.OrderByDescending(x => x.Value.YearPublication).ToDictionary(x => x.Key, x => x.Value));
                    break;
                case BookFields.Publisher:
                    Library = @ascending ? new Dictionary<int, Book>(Library.OrderBy(x => x.Value.Publisher).ToDictionary(x => x.Key, x => x.Value)) : new Dictionary<int, Book>(Library.OrderByDescending(x => x.Value.Publisher).ToDictionary(x => x.Key, x => x.Value));
                    break;
                case BookFields.Genres:
                    Library = @ascending ? new Dictionary<int, Book>(Library.OrderBy(x => x.Value.Genre).ToDictionary(x => x.Key, x => x.Value)) : new Dictionary<int, Book>(Library.OrderByDescending(x => x.Value.Genre).ToDictionary(x => x.Key, x => x.Value));
                    break;
            }
        }

        public int FindBinary(BookFields type, string param)
        {
            int result = -1;
            SortInternalMethod(type, true);
            switch (type)
            {
                case BookFields.Authors:
                    result = BinarySearchAuthor(param, Comparer<string>.Default);
                    break;
                case BookFields.Name:
                    result = BinarySearchName(param, Comparer<string>.Default);
                    break;
                case BookFields.Publisher:
                    result = BinarySearchPublisher(param, Comparer<string>.Default);
                    break;
                case BookFields.Genres:
                    result = BinarySearchGenre(param, Comparer<string>.Default);
                    break;
                default:
                    MessageBox.Show("Ошибка типизации");
                    break;
            }
            return result;
        }

        public static int BinarySearchAuthor(string searchFor, Comparer<string> comparer)
        {
            int high, low, mid;
            high = DataContainer.books.Library.Count - 1;
            low = 0;
            if (DataContainer.books.Library[0].Author.Equals(searchFor))
                return 0;
            else if (DataContainer.books.Library[high].Author.Equals(searchFor))
                return high;
            else
            {
                while (low <= high)
                {
                    mid = (high + low) / 2;
                    if (comparer.Compare(DataContainer.books.Library[mid].Author, searchFor) == 0)
                        return mid;
                    else if (comparer.Compare(DataContainer.books.Library[mid].Author, searchFor) > 0)
                        high = mid - 1;
                    else
                        low = mid + 1;
                }
                return -1;
            }
        }

        public static int BinarySearchName(string searchFor, Comparer<string> comparer)
        {
            int high, low, mid;
            high = DataContainer.books.Library.Count - 1;
            low = 0;
            if (DataContainer.books.Library[0].Name.Equals(searchFor))
                return 0;
            else if (DataContainer.books.Library[high].Name.Equals(searchFor))
                return high;
            else
            {
                while (low <= high)
                {
                    mid = (high + low) / 2;
                    if (comparer.Compare(DataContainer.books.Library[mid].Name, searchFor) == 0)
                        return mid;
                    else if (comparer.Compare(DataContainer.books.Library[mid].Name, searchFor) > 0)
                        high = mid - 1;
                    else
                        low = mid + 1;
                }
                return -1;
            }
        }

        public static int BinarySearchPublisher(string searchFor, Comparer<string> comparer)
        {
            int high, low, mid;
            high = DataContainer.books.Library.Count - 1;
            low = 0;
            if (DataContainer.books.Library[0].Publisher.Equals(searchFor))
                return 0;
            else if (DataContainer.books.Library[high].Publisher.Equals(searchFor))
                return high;
            else
            {
                while (low <= high)
                {
                    mid = (high + low) / 2;
                    if (comparer.Compare(DataContainer.books.Library[mid].Publisher, searchFor) == 0)
                        return mid;
                    else if (comparer.Compare(DataContainer.books.Library[mid].Publisher, searchFor) > 0)
                        high = mid - 1;
                    else
                        low = mid + 1;
                }
                return -1;
            }
        }

        public static int BinarySearchGenre(string searchFor, Comparer<string> comparer)
        {
            int high, low, mid;
            high = DataContainer.books.Library.Count - 1;
            low = 0;
            if (DataContainer.books.Library[0].Genre.Equals(searchFor))
                return 0;
            else if (DataContainer.books.Library[high].Genre.Equals(searchFor))
                return high;
            else
            {
                while (low <= high)
                {
                    mid = (high + low) / 2;
                    if (comparer.Compare(DataContainer.books.Library[mid].Genre, searchFor) == 0)
                        return mid;
                    else if (comparer.Compare(DataContainer.books.Library[mid].Genre, searchFor) > 0)
                        high = mid - 1;
                    else
                        low = mid + 1;
                }
                return -1;
            }
        }

        private void InternalSearchAuthor(ref Dictionary<int, Book> buffer, string author)
        {
            foreach (var book in buffer.ToArray())
                if (!book.Value.Author.Contains(author))
                    buffer.Remove(book.Key);
        }

        private void InternalSearchName(ref Dictionary<int, Book> buffer, string name)
        {
            foreach (var book in buffer.ToArray())
                if (book.Value.Name != name)
                    buffer.Remove(book.Key);
        }

        private void InternalSearchPublisher(ref Dictionary<int, Book> buffer, string publisher)
        {
            foreach (var book in buffer.ToArray())
                if (book.Value.Publisher != publisher)
                    buffer.Remove(book.Key);
        }

        private void InternalSearchGenre(ref Dictionary<int, Book> buffer, string genre)
        {
            foreach (var book in buffer.ToArray())
                if (book.Value.Genre != genre)
                    buffer.Remove(book.Key);
        }

        public Dictionary<int, Book> FindInternalMethod(BookFields type, string param)
        {
            Dictionary<int, Book> result = new Dictionary<int, Book>(Library);
            switch (type)
            {
                case BookFields.Authors:
                    InternalSearchAuthor(ref result, param);
                    break;
                case BookFields.Name:
                    InternalSearchName(ref result, param);
                    break;
                case BookFields.Publisher:
                    InternalSearchPublisher(ref result, param);
                    break;
                case BookFields.Genres:
                    InternalSearchGenre(ref result, param);
                    break;
            }
            return result;
        }
    }
}