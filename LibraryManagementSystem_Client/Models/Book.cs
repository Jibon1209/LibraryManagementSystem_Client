using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem_Client.Models
{
    public class Book
    {
        public int BookID { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishedDate { get; set; }
        public string Title { get; set; }
        public int AvailableCopies { get; set; }
        public string ISBN { get; set; }
        public int TotalCopies { get; set; }
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
    }
}
