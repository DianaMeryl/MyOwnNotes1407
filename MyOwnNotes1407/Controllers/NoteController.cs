using DataModels;
using DBEntites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOwnNotes1407.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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
                _ctx.Notes.Add(note);
                await _ctx.SaveChangesAsync();
                TempData["msg"] = "Added successfully";
                return RedirectToAction("AddNote");

            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could not added!!!";
                return View();
            }

        }

        public async Task<IActionResult> DisplayNotesAsync()
        {
            var notes = await _ctx.Notes.ToListAsync();

            return View(notes);
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
                _ctx.Notes.Update(note);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("DisplayNotes");

            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could not update!!!";
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
            return RedirectToAction("DisplayNotes");

        }

    }
}
