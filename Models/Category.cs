using System.ComponentModel.DataAnnotations;

namespace BlogComunitario.Models
{
	public class Category
	{
		public int Id { get; set; }

        [Required(ErrorMessage = "O categoria é obrigatória")]
        [StringLength(100)]
		public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        // Relacionamento um para muitos (uma categoria pode ter vários posts)
        public List<Post> Posts { get; set; }
	}
}
