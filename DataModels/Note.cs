using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
    public class Note
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsDone { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public string UserId { get; set; }



        //public string UserId { get; set; }

        //  public ApplicationUser ApplicationUsers { get; set; }

    }

}
