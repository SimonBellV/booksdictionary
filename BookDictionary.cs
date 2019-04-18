using BookContainer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;

namespace BoolContainer
{
    internal class BookDictionary
    {
        public Dictionary<int, Book> Library { get; set; }

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

        public void SortAsPyramide()
        { }

        public void SortInternalMethod(BookFields type, bool ascending)
        {
            switch (type)
            {
                case BookFields.Authors:
                    Library = @ascending ? new Dictionary<int, Book>(Library.OrderBy(x => x.Value.Authors).ToDictionary(x => x.Key, x => x.Value)) : new Dictionary<int, Book>(Library.OrderByDescending(x => x.Value.Authors).ToDictionary(x => x.Key, x => x.Value));
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
                    Library = @ascending ? new Dictionary<int, Book>(Library.OrderBy(x => x.Value.Genres).ToDictionary(x => x.Key, x => x.Value)) : new Dictionary<int, Book>(Library.OrderByDescending(x => x.Value.Genres).ToDictionary(x => x.Key, x => x.Value));
                    break;
            }
        }

        public Book FindBinary(Book params)
        { }

        public Book FindInternalMethod(Book params)
        { }
    }
}
