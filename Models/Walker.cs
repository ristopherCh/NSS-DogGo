﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Walker
    {
        public int Id { get; set; }

		[Required(ErrorMessage = "Hmmm... You should really add a Name...")]
		[MaxLength(35)]
		public string Name { get; set; }

		[Required]
		public int NeighborhoodId { get; set; }
        public string ImageUrl { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}