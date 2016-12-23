// Ali Bayat Dec 2016
// https://www.linkedin.com/in/alibayatgh
// http://microdev.ir/
namespace EFvsADO.Models
{
    using System.Data.Entity;
    public partial class BookContext : DbContext
    {
        public BookContext(): base("name=BookDb")
        {
        }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
    }
}
