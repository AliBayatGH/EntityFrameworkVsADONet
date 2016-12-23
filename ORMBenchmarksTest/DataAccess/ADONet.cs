using AutoMapper;
using EFvsADO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
namespace EFvsADO.DataAccess
{
    public class ADONet : ITest
    {
        public long GetBookByID(int id)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["BookDb"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Books WHERE Id = @ID", conn))
                {
                    command.Parameters.Add(new SqlParameter("@ID", id));
                    var reader = command.ExecuteReader();
                    var item = AutoMapper.Mapper.DynamicMap<List<Book>>(reader);
                }
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetBooksForAuthor(int authorId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["BookDb"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Books WHERE AuthorId = @ID", conn))
                {
                    command.Parameters.Add(new SqlParameter("@ID", authorId));
                    var reader = command.ExecuteReader();
                    var items = Mapper.DynamicMap<List<Book>>(reader);
                }
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetAuthorsForPublisher(int publisherId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["BookDb"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand
                    ("SELECT b.*,a.* FROM Books b INNER JOIN Authors a ON b.AuthorId = a.Id WHERE a.PublisherId = @ID", conn))
                {
                    command.Parameters.Add(new SqlParameter("@ID", publisherId));
                    IDataReader reader = command.ExecuteReader();
                    var books = new List<Book>();
                    while (reader.Read())
                   {
                        books.Add(new Book
                        {
                            Id = (int)reader["Id"],
                            Title = (string)reader["Title"],
                            PublishDate = (DateTime)reader["PublishDate"],
                            AuthorId = (int)reader["AuthorId"],
                            Author = new Author
                            {
                                Id = (int)reader["Id"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                BirhtDate = (DateTime)reader["BirhtDate"],
                            }
                        }



                        );
                    }
                    var tempBooks = new List<Book>();
                    foreach (var b in books)
                    {
                        b.Author.Books = books;
                        tempBooks.Add(b);

                    }
                     var authors = (tempBooks.Select(a => a.Author).ToList()).Distinct();

                }
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}
