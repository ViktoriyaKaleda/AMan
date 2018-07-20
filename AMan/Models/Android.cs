using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AMan.Models
{
    public class Android
    {
		public int Id { get; set; }

		[Required]
		[RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "Use letters, numbers and symbol '-' only please.")]
		[StringLength(16, MinimumLength = 2, ErrorMessage = "The name must be at least 2 and at max 16 characters long.")]
		public string Name { get; set; }

		[DataType(DataType.Upload)]
		[DisplayName("Avatar")]
		public string AvatarPath { get; set; }

		public string SkillsTags { get; set; }

		[Range(0, 10, ErrorMessage = "Must be between 0 to 10.")]
		public int Reliability { get; set; } = 10;
		
		public bool Status { get; set; }

		public int? CurrentJobId { get; set; }
		public Job CurrentJob { get; set; }
	}
}
