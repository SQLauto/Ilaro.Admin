﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ilaro.Admin.Attributes;
using Ilaro.Admin.ViewModels;

namespace Ilaro.Admin.Sample.Models.Northwind
{
	public class Category
	{
		public int CategoryID { get; set; }

		[StringLength(15)]
		public string CategoryName { get; set; }

		public string Description { get; set; }

		//public byte[] Picture { get; set; }

		[OnDelete(DeleteOption.AskUser)]
		public ICollection<Product> Products { get; set; }
	}
}