using DataModels;
using DBEntites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOwnNotes1407.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;
//using static Azure.Core.HttpHeader;

namespace MyOwnNotes1407.Controllers
{
    public class NoteController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        public NoteController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult AddNote()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNoteAsync(Note note)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                note.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _ctx.Notes.Add(note);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("AddNote");

            }
            catch (Exception ex)
            {
                return View();
            }

        }    
        public async Task<IActionResult> EditNotesAsync(long id)
        {
            var note = await _ctx.Notes.FindAsync(id);
            return View(note);
        }
        [HttpPost]
        public async Task<IActionResult> EditNotesAsync(Note note)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                note.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _ctx.Notes.Update(note);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("NoteShowEdit");

            }
            catch (Exception ex)
            {
                return View();
            }

        }
        public async Task<IActionResult> DeleteNotesAsync(long id)
        {
            try
            {
                var note = _ctx.Notes.Find(id);
                if (note != null)
                {
                    _ctx.Notes.Remove(note);
                    await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {


            }
            return RedirectToAction("NoteShowEdit");

        }
        public IActionResult NotePaginSort(string userId = "", string term = "", string orderBy = "", int currentPage = 1)
        {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
                var empData = new NoteViewModel();
                // we are toggling order cases

                //empData.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "note_desc" : "";

                empData.NameSortOrder = orderBy == "name" ? "name_desc" : "name";

                empData.CreatedSortOrder = orderBy == "created" ? "created_desc" : "created";

                var notes = (from emp in _ctx.Notes
                             where/* term == "" || emp.Name.ToLower().StartsWith(term) && */emp.UserId == userId
                             select new Note
                             {
                                 Id = emp.Id,
                                 Name = emp.Name,
                                 Description = emp.Description,
                                 Created = emp.Created,
                                 UserId = emp.UserId
                             }
                                );

                switch (orderBy)
                {
                    case "name_desc":
                        notes = notes.OrderByDescending(a => a.Name);
                        break;
                    case "created_desc":
                        notes = notes.OrderByDescending(a => a.Created);
                        break;
                    case "name":
                        notes = notes.OrderBy(a => a.Name);
                        break;
                    default:
                        notes = notes.OrderBy(a => a.Created);
                        break;
                }
                int totalRecords = notes.Count();
                int pageSize = 5;
                int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                notes = notes.Skip((currentPage - 1) * pageSize).Take(pageSize);
                // current=1, skip= (1-1=0), take=5 
                // currentPage=2, skip (2-1)*5 = 5, take=5 ,

                empData.Notes = notes.ToList();
                empData.CurrentPage = currentPage;
                empData.TotalPages = totalPages;
                empData.Term = term;
                empData.PageSize = pageSize;
                empData.OrderBy = orderBy;

                return View(empData);
            
            
        }        
         public IActionResult NoteShowEdit(string userId = "", string term = "", string orderBy = "", int currentPage = 1)
        {
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var empData = new NoteViewModel();
            // we are toggling order cases
            empData.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "note_desc" : "";
            empData.CreatedSortOrder = orderBy == "created" ? "created_desc" : "created";

            var notes = (from emp in _ctx.Notes
                         where/* term == "" || emp.Name.ToLower().StartsWith(term) &&*/ emp.UserId == userId
                         select new Note
                         {
                             Id = emp.Id,
                             Name = emp.Name,
                             Created = emp.Created,
                             UserId = emp.UserId
                         }
                            );
            switch (orderBy)
            {
                case "name_desc":
                    notes = notes.OrderByDescending(a => a.Name);
                    break;
                case "created_desc":
                    notes = notes.OrderByDescending(a => a.Created);
                    break;
                case "note":
                    notes = notes.OrderBy(a => a.Name);
                    break;
                default:
                    notes = notes.OrderBy(a => a.Created);
                    break;
            }
            int totalRecords = notes.Count();
            int pageSize = 5;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            notes = notes.Skip((currentPage - 1) * pageSize).Take(pageSize);
            // current=1, skip= (1-1=0), take=5 
            // currentPage=2, skip (2-1)*5 = 5, take=5 ,
            empData.Notes =   notes.ToList();
            empData.CurrentPage = currentPage;
            empData.TotalPages = totalPages;
            empData.Term = term;
            empData.PageSize = pageSize;
            empData.OrderBy = orderBy;
            return View(empData);
        }

    }
}
