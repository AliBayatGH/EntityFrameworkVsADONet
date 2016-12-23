// Ali Bayat Dec 2016
// https://www.linkedin.com/in/alibayatgh
// http://microdev.ir/
namespace EFvsADO.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public virtual Author Author { get; set; }
        public int AuthorId { get; internal set; }
    }
}
