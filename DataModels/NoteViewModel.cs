using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DataModels
{
    public class NoteViewModel
    {
        //public string? UserNId { get; set; }
        public List<Note> Notes { get; set; }
        public string NameSortOrder { get; set; }
        public string CreatedSortOrder { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Term { get; set; }
        public string OrderBy { get; set; }
    }
}
