using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Dog
    {
        public int Id { get; set; }

		[Required(ErrorMessage = "Hmmm... You should really add a Name...")]
		[MaxLength(35)]
		public string DogName { get; set; }

		[Required]
		public int OwnerId { get; set; }

		[Required(ErrorMessage = "Add a breed")]
		[MaxLength(35)]
		public string Breed { get; set; }
        public string Notes { get; set; }
        public string ImageUrl { get; set; }

    }
}
