using System;

namespace BookContainer
{
    public enum BookFields
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
    public class Book
    {
        public Book()
        {
        }

        public Book(string name, string author, short yearPublication, short yearCreation, string publisher, short pagesCount, string genre)
        {
            Name = name;
            Author = author;
            YearPublication = yearPublication;
            YearCreation = yearCreation;
            Publisher = publisher;
            PagesCount = pagesCount;
            Genre = genre;
        }

        public string Name { get; set; }
        public string Author { get; set; }
        public short YearPublication { get; set; }
        public short YearCreation { get; set; }
        public string Publisher { get; set; }
        public short PagesCount { get; set; }
        public string Genre { get; set; }

        public string GetString()
        {
            return "Автор: " + Author + "\nНазвание: " + Name + "\nИздатель: " + Publisher + "\nЖанр: " + Genre + "\nГод написания: " + YearCreation + "\nГод издания: " + YearPublication + "\nКоличество страниц: " + PagesCount;
        }
    }
}
