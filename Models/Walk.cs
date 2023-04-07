using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
	public class Walk
	{
		public int Id { get; set; }
		[DisplayFormat(DataFormatString = "{0:MMM dd, yyyy hh:mm tt}")]
		public DateTime Date { get; set; }
		public int Duration { get; set; }
		public int WalkerId { get; set; }
		public Owner Owner { get; set; }
		public Dog Dog { get; set; }
		public List<Dog> Dogs { get; set; }
	}
}