// Ali Bayat Dec 2016
// https://www.linkedin.com/in/alibayatgh
// http://microdev.ir/
using EFvsADO.Models;
using System;
using System.Collections.Generic;

namespace EFvsADO.TestData
{
    public static class Generator
    {
        public static List<Book> GenerateBooks(int authorId, int count)
        {
            List<Book> books = new List<Book>();

            var allBookTitles = SampleNames.GetBookTitles();
            Random rand = new Random();
            DateTime start = new DateTime(1975, 1, 1);
            DateTime end = new DateTime(1998, 1, 1);

            for (int i = 0; i < count; i++)
            {
                Book book = new Book();
                int newTitle = rand.Next(0, allBookTitles.Count - 1);
                book.Title = allBookTitles[newTitle];
                book.AuthorId = authorId;
                book.Id = (((authorId - 1) * count) + (i + 1));
                books.Add(book);
                book.PublishDate = RandomDay(rand, start, end);
            }

            return books;
        }

        public static List<Author> GenerateAuthors(int publisherId, int count)
        {
            List<Author> authors = new List<Author>();
            var allFirstNames = SampleNames.GetFirstNames();
            var allLastNames = SampleNames.GetLastNames();

            Random rand = new Random();
            DateTime start = new DateTime(1900, 1, 1);
            DateTime end = new DateTime(2010, 1, 1);

            for (int i = 0; i < count; i++)
            {
                Author author = new Author();
                int newFirstName = rand.Next(0, allFirstNames.Count - 1);
                int newLastName = rand.Next(0, allLastNames.Count - 1);
                author.FirstName = allFirstNames[newFirstName];
                author.LastName = allLastNames[newLastName];
                author.BirhtDate = RandomDay(rand, start, end);
                author.PublisherId = publisherId;
                author.Id = (((publisherId - 1) * count) + (i + 1));
                authors.Add(author);
            }

            return authors;
        }

        public static List<Publisher> GeneratePublishers(int count)
        {
            List<Publisher> publishers = new List<Publisher>();
            var allpublisherNames = SampleNames.GetPublisherNames();
            Random rand = new Random();

            for (int i = 0; i < count; i++)
            {
                int newPublisher = rand.Next(0, allpublisherNames.Count - 1);
                publishers.Add(new Publisher()
                {
                    Name = allpublisherNames[newPublisher],
                    Id = i + 1
                });
            }

            return publishers;
        }

        private static DateTime RandomDay(Random rand, DateTime start, DateTime end)
        {
            int range = (end - start).Days;
            return start.AddDays(rand.Next(range));
        }
    }
}
