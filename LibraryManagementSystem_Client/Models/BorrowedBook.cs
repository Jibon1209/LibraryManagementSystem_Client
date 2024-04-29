using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem_Client.Models
{
    public class BorrowedBook
    {
        public int BorrowID { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BorrowDate { get; set; }
        public int MemberId { get; set; }
        public string? MemberName { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReturnDate { get; set; }
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string Status { get; set; }
    }
}
