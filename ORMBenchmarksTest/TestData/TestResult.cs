// Ali Bayat Dec 2016
// https://www.linkedin.com/in/alibayatgh
// http://microdev.ir/
namespace EFvsADO.TestData
{
    public class TestInput
    {
        public TestInput()
        {
            NumberOfPublishersPerRun = 2;
            NumberOfAuthorsPerPublisher = 4;
            NumberOfBooksPerAuthor = 100;
        }
        public int NumberOfPublishersPerRun { get; set; }
        public int NumberOfAuthorsPerPublisher { get; set; }
        public int NumberOfBooksPerAuthor { get; set; }
    }
    public class TestResult
    {
        public double BookByIDMilliseconds { get; set; }
        public double BooksForAuthorMilliseconds { get; set; }
        public double AuthorsForPublisherMilliseconds { get; set; }
        public Framework Framework { get; set; }
        public int Run { get; set; }

        public double Average
        {
            get { return (int)((BookByIDMilliseconds+ BooksForAuthorMilliseconds+ AuthorsForPublisherMilliseconds)/3); }
        }

    }
    public class MyData
    {
        public int Cat { get; set; }

        public double Value { get; set; }

        public Framework Framework { get; set; }
    }
    public enum Framework
    {
        EntityFramework,
        ADONet
    }   
}
