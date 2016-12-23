// Ali Bayat Dec 2016
// https://www.linkedin.com/in/alibayatgh
// http://microdev.ir/
using EFvsADO.Models;
using System.Collections.Generic;

namespace EFvsADO.TestData
{
    public static class Database
    {
        public static void Reset()
        {
            using(BookContext context = new BookContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM Books");
                context.Database.ExecuteSqlCommand("DELETE FROM Authors");
                context.Database.ExecuteSqlCommand("DELETE FROM Publishers");
            }
        }

        public static void Load(List<Publisher> publishers, List<Author> author, List<Book> books)
        {
            AddPublishers(publishers);
            AddAuthors(author);
            AddBooks(books);
        }

        private static void AddBooks(List<Book> books)
        {
            using (BookContext context = new BookContext())
            {
                foreach (var book in books)
                {
                    context.Books.Add(new Book()
                    {
                        Id = book.Id,
                        Title=book.Title,
                        AuthorId=book.AuthorId,
                        PublishDate=book.PublishDate
                    });
                }

                context.SaveChanges();
            }
        }

        private static void AddAuthors(List<Author> authors)
        {
            using (BookContext context = new BookContext())
            {
                foreach (var author in authors)
                {
                    context.Authors.Add(new Author()
                    {
                        Id = author.Id,
                        FirstName = author.FirstName,
                        LastName = author.LastName,
                        PublisherId = author.PublisherId,
                        BirhtDate = author.BirhtDate
                    });
                }

                context.SaveChanges();
            }
        }

        private static void AddPublishers(List<Publisher> publishers)
        {
            using (BookContext context = new BookContext())
            {
                foreach (var publisher in publishers)
                {
                    context.Publishers.Add(new Publisher()
                    {
                        Id = publisher.Id,
                        Name = publisher.Name
                    });
                }

                context.SaveChanges();
            }
        }
    }
}
