using EFvsADO.DataAccess;
using EFvsADO.Models;
using EFvsADO.TestData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFvsADO
{
    class Program
    {
        public static int NumberOfBooksPerAuthor { get; set; }
        public static int NumberOfAhthorPerPublisher { get; set; }
        public static int NumberOfPublishersPerRun { get; set; }
        public static int NumberOfRuns { get; set; }
        static void Main(string[] args)
        {
                List<TestResult> testResults = new List<TestResult>();
                Console.WriteLine("\n");
                Console.Write("Number of Test Runs: ");
                NumberOfRuns = int.Parse(Console.ReadLine());

                //Gather Details for Test
                Console.Write("Number of Publishers per Run: ");
                NumberOfPublishersPerRun = int.Parse(Console.ReadLine());

                Console.Write("Number of authors per publisher: ");
                NumberOfAhthorPerPublisher = int.Parse(Console.ReadLine());

                Console.Write("Number of Books per author: ");
                NumberOfBooksPerAuthor = int.Parse(Console.ReadLine());


                List<Publisher> Publishers = Generator.GeneratePublishers(NumberOfPublishersPerRun);
                List<Author> authors = new List<Author>();
                List<Book> books = new List<Book>();
                foreach (var publisher in Publishers)
                {
                    var newauthors = Generator.GenerateAuthors(publisher.Id, NumberOfAhthorPerPublisher);
                    authors.AddRange(newauthors);
                    foreach (var author in newauthors)
                    {
                        var newbooks = Generator.GenerateBooks(author.Id, NumberOfBooksPerAuthor);
                        books.AddRange(newbooks);
                    }
                }
                using (var bookContext = new BookContext())
                {
                    System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<BookContext>());
                    bookContext.Database.Initialize(true);
                    //InteractiveViews.SetViewCacheFactory(bookContext,
                    //new FileViewCacheFactory(@"C:\MyViews.xml"));
                    //new SqlServerViewCacheFactory(bookContext.Database.Connection.ConnectionString));


                    Database.Reset();
                    Database.Load(Publishers, authors, books);

                    for (int i = 0; i < NumberOfRuns; i++)
                    {
                        var adoReaderTest = new ADONet();
                        testResults.AddRange(RunTests(i, Framework.ADONet, adoReaderTest));

                        var efTest = new EntityFramework(bookContext);
                        testResults.AddRange(RunTests(i, Framework.EntityFramework, efTest));
                    }
                }
                ProcessResults(testResults);

            Console.ReadKey();
        }


        public static List<TestResult> RunTests(int runID, Framework framework, ITest testSignature)
        {
            List<TestResult> results = new List<TestResult>();

            TestResult result = new TestResult() { Run = runID, Framework = framework };
            List<long> bookByIDResults = new List<long>();
            for (int i = 1; i <= NumberOfBooksPerAuthor; i++)
            {
                bookByIDResults.Add(testSignature.GetBookByID(i));
            }
            result.BookByIDMilliseconds = Math.Round(bookByIDResults.Average(), 2);

            List<long> booksForauthorResults = new List<long>();
            for (int i = 1; i <= NumberOfAhthorPerPublisher; i++)
            {
                booksForauthorResults.Add(testSignature.GetBooksForAuthor(i));
            }
            result.BooksForAuthorMilliseconds = Math.Round(booksForauthorResults.Average(), 2);
            List<long> authorsForpublisherResults = new List<long>();
            for (int i = 1; i <= NumberOfPublishersPerRun; i++)
            {
                authorsForpublisherResults.Add(testSignature.GetAuthorsForPublisher(i));
            }
            result.AuthorsForPublisherMilliseconds = Math.Round(authorsForpublisherResults.Average(), 2);
            results.Add(result);

            return results;
        }

        public static void ProcessResults(List<TestResult> results)
        {
            List<string> AverageList=new List<string>();
            var groupedResults = results.GroupBy(x => x.Framework);
            foreach (var group in groupedResults)
            {
                Console.WriteLine("\n");
                Console.WriteLine(group.Key.ToString() + " Results");
                Console.WriteLine("Run Number\tbook by ID\t\tbooks per author\t\tauthors per publisher");
                var orderedResults = group.OrderBy(x => x.Run);
                foreach (var orderResult in orderedResults)
                {
                    Console.WriteLine(orderResult.Run.ToString() + "\t\t\t" + orderResult.BookByIDMilliseconds + "\t\t\t" + orderResult.BooksForAuthorMilliseconds + "\t\t\t\t" + orderResult.AuthorsForPublisherMilliseconds);
                }
                Console.WriteLine("-------------------------------------------------------------------------------------------------");
                Console.WriteLine("Average" + "\t\t\t" + orderedResults.Average(x => x.BookByIDMilliseconds) + "\t\t\t" + orderedResults.Average(x => x.BooksForAuthorMilliseconds) + "\t\t\t\t" + orderedResults.Average(x => x.AuthorsForPublisherMilliseconds)+"\n");
                var avg= ((int)(orderedResults.Average(x => x.BookByIDMilliseconds)+ orderedResults.Average(x => x.BooksForAuthorMilliseconds)+ orderedResults.Average(x => x.AuthorsForPublisherMilliseconds))/ 3).ToString();
                AverageList.Add(string.Format("Average of {0} is {1} millisocond", group.Key.ToString(), avg));
            }
            foreach (var average in AverageList)
            {
                Console.WriteLine(average);
            }
            Console.WriteLine("\n");
        }

    }
}
