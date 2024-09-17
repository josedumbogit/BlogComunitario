﻿using System.ComponentModel.DataAnnotations;

namespace BlogComunitario.Models
{
	public class Tag
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		// Relacionamento muitos para muitos com Post
		public List<PostTag> PostTags { get; set; }
	}
}
