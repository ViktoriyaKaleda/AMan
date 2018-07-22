using AMan.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AMan.ViewModels
{
	public class AndroidViewModel
    {
		public Android Android { get; set; }

		public IEnumerable<SelectListItem> Jobs { get; set; }

		public AndroidViewModel() { }

		public AndroidViewModel(Android android, IEnumerable<SelectListItem> jobs)
		{
			Android = android;
			Jobs = jobs;
		}
	}
}
