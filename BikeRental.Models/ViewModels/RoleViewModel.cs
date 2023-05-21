using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BikeRental.Models.ViewModels
{
	public class RoleViewModel
	{
		[Required]
		[Display(Name = "Nazwa użytkownika")]
		public string userName { get; set; }

		[Required]
		[Display(Name = "Rola")]
		public string role { get; set; }
    }
}
