using AMan.Models;
using System.Collections.Generic;

namespace AMan.ViewModels
{
	public class JobDetailsViewModel
    {
		public Job Job { get; set; }

		public List<Android> AssignedAndroids { get; set; }

		public JobDetailsViewModel() { }

		public JobDetailsViewModel(Job job, List<Android> assignedAndroids)
		{
			Job = job;
			AssignedAndroids = assignedAndroids;
		}
    }
}
