// Ali Bayat Dec 2016
// https://www.linkedin.com/in/alibayatgh
// http://microdev.ir/
namespace EFvsADO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirhtDate { get; set; }
        public ICollection<Book> Books { get; set; }
        public Publisher Publisher { get; set; }
        public int PublisherId { get; internal set; }
    }
}
