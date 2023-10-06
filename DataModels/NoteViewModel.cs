using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DataModels
{
   public class NoteViewModel
    {
        public IQueryable<Note> Notes { get; set; }
        public string NameSortOrder { get; set; }
        public string CreatedSortOrder { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Term { get; set; }
        public string OrderBy { get; set; }
    }
}
