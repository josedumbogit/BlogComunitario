using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogComunitario.Models
{
	public class Comment
	{
		public int Id { get; set; }

		[Required]
		public string Content { get; set; }

		public DateTime DatePosted { get; set; } = DateTime.Now;

		// Foreign Key para Post
		public int PostId { get; set; }
		public Post Post { get; set; }

		// Foreign Key para o autor do comentário (usuário)
		public string UserId { get; set; }
		public IdentityUser User { get; set; }
	}

}
