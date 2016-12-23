using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFvsADO.DataAccess
{
    public interface ITest
    {
        long GetBookByID(int id);
        long GetBooksForAuthor(int authorID);
        long GetAuthorsForPublisher(int publisherID);
    }
}
