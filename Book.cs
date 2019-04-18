using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContainer
{
    enum BookFields
    {
        Name = 0,
        Authors,
        YearPublication,
        YearCreation,
        Publisher,
        PagesCount,
        Genres
    }

    [Serializable]
    internal class Book
    {
        public string Name { get; set; }
        public List<string> Authors { get; set; }
        public short YearPublication { get; set; }
        public short YearCreation { get; set; }
        public string Publisher { get; set; }
        public short PagesCount { get; set; }
        public List<string> Genres { get; set; }
    }
}
