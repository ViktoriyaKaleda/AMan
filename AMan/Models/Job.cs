using System.ComponentModel.DataAnnotations;

namespace AMan.Models
{
	public class Job
    {
		public int Id { get; set; }

		[Required]
		[RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "Use letters, numbers and symbol '-' only please.")]
		[StringLength(16, MinimumLength = 2, ErrorMessage = "The name must be at least 2 and at max 16 characters long.")]
		public string Name { get; set; }

		[StringLength(255, ErrorMessage = "The description must be at max 255 characters long.")]
		public string Description { get; set; }

		[Required]
		public ComplexityLevel Complexity { get; set; }
	}

	public enum ComplexityLevel
	{
		Low = 1,
		Medium = 2,
		High = 3
	}
}
