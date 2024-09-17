using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BlogComunitario.Models
{
	public class Post
	{
		public int Id { get; set; }

		[Required]
		[StringLength(200)]
		public string Title { get; set; }

		[Required]
		public string Content { get; set; }

		public string ImageUrl { get; set; }

		public DateTime DatePosted { get; set; } = DateTime.Now;

		// Foreign Key para Categoria
		public int CategoryId { get; set; }
		public Category Category { get; set; }

		// Relacionamento muitos para muitos com Tag
		public List<PostTag> PostTags { get; set; }

		// Relacionamento um para muitos com Comentário
		public List<Comment> Comments { get; set; }

		// Referência ao autor do post (usuário que criou o post)
		public string UserId { get; set; }
		public IdentityUser User { get; set; }
	}
}
