// Created by Ali Bayat Dec 2016
// https://www.linkedin.com/in/alibayatgh
// http://microdev.ir/
using EFvsADO.DataAccess;
using EFvsADO.Models;
using EFvsADO.TestData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF
{
    public partial class MainWindow : Window
    {
        #region Constructors
        public MainWindow()
        {
            InitializeComponent();
            RunTest = new RelayCommand(runTestButton_Click, canExecute);
        }
        #endregion
        #region Properties
        public int NumberOfRuns
        {
            get { return (int)GetValue(NumberOfRunsProperty); }
            set { SetValue(NumberOfRunsProperty, value); }
        }

        public static readonly DependencyProperty NumberOfRunsProperty =
            DependencyProperty.Register("NumberOfRuns", typeof(int), typeof(MainWindow), new PropertyMetadata(10, numberOfRuns_Changed), numberOfRuns_Validate);

        private static bool numberOfRuns_Validate(object value)
        {
            int? d = value as int?;
            var res = d != null && d > 0 && d < 20;
            return res;
            //return true;
        }

        private static void numberOfRuns_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var u = d as MainWindow;
            u.RunTest.RaiseCanExecuteChanged();
        }

        public int NumberOfPublishersPerRun
        {
            get { return (int)GetValue(NumberOfPublishersPerRunProperty); }
            set { SetValue(NumberOfPublishersPerRunProperty, value); }
        }

        public static readonly DependencyProperty NumberOfPublishersPerRunProperty =
            DependencyProperty.Register("NumberOfPublishersPerRun", typeof(int), typeof(MainWindow), new PropertyMetadata(2, numberOfPublishersPerRun_Changed), numberOfPublishersPerRun_Validate);

        private static bool numberOfPublishersPerRun_Validate(object value)
        {
            int? d = value as int?;
            var res = d != null && d > 0 && d < 100;
            return res;
            //return true;
        }

        private static void numberOfPublishersPerRun_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var u = d as MainWindow;
            u.RunTest.RaiseCanExecuteChanged();
        }

        public int NumberOfAuthorsPerPublisher
        {
            get { return (int)GetValue(NumberOfAuthorsPerPublisherProperty); }
            set { SetValue(NumberOfAuthorsPerPublisherProperty, value); }
        }

        public static readonly DependencyProperty NumberOfAuthorsPerPublisherProperty =
            DependencyProperty.Register("NumberOfAuthorsPerPublisher", typeof(int), typeof(MainWindow), new PropertyMetadata(4, NumberOfAuthorsPerPublisher_Changed), NumberOfAuthorsPerPublisher_Validate);

        private static bool NumberOfAuthorsPerPublisher_Validate(object value)
        {
            int? d = value as int?;
            var res = d != null && d > 0 && d < 100;
            return res;
            //return true;
        }

        private static void NumberOfAuthorsPerPublisher_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var u = d as MainWindow;
            u.RunTest.RaiseCanExecuteChanged();
        }

        public int NumberOfBooksPerAuthor
        {
            get { return (int)GetValue(NumberOfBooksPerAuthorProperty); }
            set { SetValue(NumberOfBooksPerAuthorProperty, value); }
        }

        public static readonly DependencyProperty NumberOfBooksPerAuthorProperty =
            DependencyProperty.Register("NumberOfBooksPerAuthor", typeof(int), typeof(MainWindow), new PropertyMetadata(100, NumberOfBooksPerAuthor_Changed), NumberOfBooksPerAuthor_Validate);

        private static bool NumberOfBooksPerAuthor_Validate(object value)
        {
            int? d = value as int?;
            var res = d != null && d > 0 && d <= 100;
            return res;
            //return true;
        }

        private static void NumberOfBooksPerAuthor_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var u = d as MainWindow;
            u.RunTest.RaiseCanExecuteChanged();
        }

        public RelayCommand RunTest { get; set; }
        public TestResult EntityFrameworkTestResultAverage
        {
            get { return (TestResult)GetValue(EntityFrameworkTestResultAverageProperty); }
            set { SetValue(EntityFrameworkTestResultAverageProperty, value); }
        }

        public static readonly DependencyProperty EntityFrameworkTestResultAverageProperty =
            DependencyProperty.Register("EntityFrameworkTestResultAverage", typeof(TestResult), typeof(MainWindow), new PropertyMetadata(new TestResult()));

        public TestResult ADOTestResultAverage
        {
            get { return (TestResult)GetValue(ADOTestResultAverageProperty); }
            set { SetValue(ADOTestResultAverageProperty, value); }
        }

        public static readonly DependencyProperty ADOTestResultAverageProperty =
            DependencyProperty.Register("ADOTestResultAverage", typeof(TestResult), typeof(MainWindow), new PropertyMetadata(new TestResult()));

        public List<TestResult> ADOTestResult
        {
            get { return (List<TestResult>)GetValue(ADOTestResultProperty); }
            set { SetValue(ADOTestResultProperty, value); }
        }

        public static readonly DependencyProperty ADOTestResultProperty =
            DependencyProperty.Register("ADOTestResult", typeof(List<TestResult>), typeof(MainWindow), new PropertyMetadata(new List<TestResult>()));

        public List<TestResult> EntityFrameworkTestResult
        {
            get { return (List<TestResult>)GetValue(EntityFrameworkTestResultProperty); }
            set { SetValue(EntityFrameworkTestResultProperty, value); }
        }

        public static readonly DependencyProperty EntityFrameworkTestResultProperty =
            DependencyProperty.Register("EntityFrameworkTestResult", typeof(List<TestResult>), typeof(MainWindow), new PropertyMetadata(new List<TestResult>()));
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));



        public ObservableCollection<MyData> Data
        {
            get { return (ObservableCollection<MyData>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(ObservableCollection<MyData>), typeof(MainWindow), new PropertyMetadata(new ObservableCollection<MyData>()));
        #endregion
        #region Methods
        private List<TestResult> RunTests(int runID, Framework framework, ITest test, TestInput testInput)
        {
            List<TestResult> results = new List<TestResult>();

            TestResult result = new TestResult() { Run = runID, Framework = framework };
            List<long> bookByIDResults = new List<long>();
            for (int i = 1; i <= testInput.NumberOfBooksPerAuthor; i++)
            {
                bookByIDResults.Add(test.GetBookByID(i));
            }
            result.BookByIDMilliseconds = Math.Round(bookByIDResults.Average(), 2);

            List<long> booksForauthorResults = new List<long>();
            for (int i = 1; i <= testInput.NumberOfAuthorsPerPublisher; i++)
            {
                booksForauthorResults.Add(test.GetBooksForAuthor(i));
            }
            result.BooksForAuthorMilliseconds = Math.Round(booksForauthorResults.Average(), 2);
            List<long> authorsForpublisherResults = new List<long>();
            for (int i = 1; i <= testInput.NumberOfPublishersPerRun; i++)
            {
                authorsForpublisherResults.Add(test.GetAuthorsForPublisher(i));
            }
            result.AuthorsForPublisherMilliseconds = Math.Round(authorsForpublisherResults.Average(), 2);
            results.Add(result);

            return results;
        }
        private async void runTestButton_Click()
        {
            IsBusy = true;
            List<TestResult> testResults = new List<TestResult>();

            List<Publisher> publishers = Generator.GeneratePublishers(NumberOfPublishersPerRun);
            List<Author> authors = new List<Author>();
            List<Book> books = new List<Book>();
            foreach (var publisher in publishers)
            {
                var newAuthors = Generator.GenerateAuthors(publisher.Id, NumberOfAuthorsPerPublisher);
                authors.AddRange(newAuthors);
                foreach (var author in newAuthors)
                {
                    var newBooks = Generator.GenerateBooks(author.Id, NumberOfBooksPerAuthor);
                    books.AddRange(newBooks);
                }
            }
            using (var bookContext = new BookContext())
            {
                var slowTask0 = Task<List<TestResult>>.Run(() =>

                 {
                     System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<BookContext>());
                     //bookContext.Database.Initialize(true);
                     //InteractiveViews.SetViewCacheFactory(bookContext,
                     //new FileViewCacheFactory(@"C:\MyViews.xml"));
                     //new SqlServerViewCacheFactory(bookContext.Database.Connection.ConnectionString));


                     Database.Reset();
                     Database.Load(publishers, authors, books);

                 }
                 );
                await slowTask0;
                TestInput testInput= new TestInput {NumberOfAuthorsPerPublisher=this.NumberOfAuthorsPerPublisher,NumberOfBooksPerAuthor=this.NumberOfBooksPerAuthor,NumberOfPublishersPerRun=this.NumberOfPublishersPerRun };
                for (int i = 0; i < NumberOfRuns; i++)
                {
                    var adoReaderTest = new ADONet();
                    var slowTask1 = Task<List<TestResult>>.Run(() => RunTests(i, Framework.ADONet, adoReaderTest, testInput));
                    var efTest = new EntityFramework(bookContext);
                    var slowTask2 = Task<List<TestResult>>.Run(() => RunTests(i, Framework.EntityFramework, efTest, testInput));
                    await slowTask1;
                    await slowTask2;
                    testResults.AddRange(slowTask1.Result);

                    testResults.AddRange(slowTask2.Result);
                }
            }
            ADOTestResult = testResults.Where(x => x.Framework == Framework.ADONet).ToList();
            EntityFrameworkTestResult = testResults.Where(x => x.Framework == Framework.EntityFramework).ToList();



            ADOTestResultAverage = new TestResult
            {
                BookByIDMilliseconds = ADOTestResult.Average(x => x.BookByIDMilliseconds),
                AuthorsForPublisherMilliseconds = ADOTestResult.Average(x => x.AuthorsForPublisherMilliseconds),
                BooksForAuthorMilliseconds = ADOTestResult.Average(x => x.BooksForAuthorMilliseconds)
            };
            EntityFrameworkTestResultAverage = new TestResult
            {
                BookByIDMilliseconds = EntityFrameworkTestResult.Average(x => x.BookByIDMilliseconds),
                AuthorsForPublisherMilliseconds = EntityFrameworkTestResult.Average(x => x.AuthorsForPublisherMilliseconds),
                BooksForAuthorMilliseconds = EntityFrameworkTestResult.Average(x => x.BooksForAuthorMilliseconds)
            };

            Data = new ObservableCollection<MyData>();
            Data.Add(new MyData() { Cat = 1, Value = EntityFrameworkTestResultAverage.BookByIDMilliseconds, Framework = Framework.EntityFramework });
            Data.Add(new MyData() { Cat = 1, Value = ADOTestResultAverage.BookByIDMilliseconds, Framework = Framework.ADONet });
            Data.Add(new MyData() { Cat = 2, Value = EntityFrameworkTestResultAverage.BooksForAuthorMilliseconds, Framework = Framework.EntityFramework });
            Data.Add(new MyData() { Cat = 2, Value = ADOTestResultAverage.BooksForAuthorMilliseconds, Framework = Framework.ADONet });
            Data.Add(new MyData() { Cat = 3, Value = EntityFrameworkTestResultAverage.AuthorsForPublisherMilliseconds, Framework = Framework.EntityFramework });
            Data.Add(new MyData() { Cat = 3, Value = ADOTestResultAverage.AuthorsForPublisherMilliseconds, Framework = Framework.ADONet });
            chart.ItemsSource = Data;
            IsBusy = false;
        }

        private bool canExecute()
        {
            return !IsBusy && NumberOfRuns > 0 && NumberOfPublishersPerRun > 0 && NumberOfAuthorsPerPublisher > 0 && NumberOfBooksPerAuthor > 0;
        }

        #endregion
    }

}
