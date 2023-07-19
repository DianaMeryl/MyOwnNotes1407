using Microsoft.AspNetCore.Identity;

namespace DataModels
{
    public class ApplicationUser : IdentityUser
    {
        //public new string Id = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? ProfilePicture { get; set; }

       // public ICollection<Note> Notes { get; set; }
    }
}
