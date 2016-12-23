using EFvsADO.Models;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace EFvsADO.DataAccess
{
    public class EntityFramework : ITest
    {
        private readonly BookContext context;
        public EntityFramework(BookContext bookContext)
        {
            context = bookContext;
        }

        public long GetBookByID(int id)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var book = context.Books.Find(id);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public  long GetBooksForAuthor(int authorId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var books = context.Books.AsNoTracking().Where(x => x.AuthorId == authorId).ToList();
            //var books = context.Database.SqlQuery<Book>("SELECT * FROM Books WHERE Id = @ID", new SqlParameter("@ID", authorId)).ToList();
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetAuthorsForPublisher(int publisherId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
             var authors = context.Authors.AsNoTracking().Include("Books").Where(x => x.PublisherId == publisherId).ToList();
            //var books = context.Database.SqlQuery<Book>("SELECT b.*,a.* FROM Books b INNER JOIN Authors a ON b.AuthorId = a.Id WHERE a.PublisherId = @ID", new SqlParameter("@ID", publisherId)).ToList();
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}
