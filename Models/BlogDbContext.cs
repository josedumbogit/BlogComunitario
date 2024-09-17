using Microsoft.EntityFrameworkCore;

namespace BlogComunitario.Models
{
	public class BlogDbContext : DbContext
	{
		public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

		public DbSet<Post> Posts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<PostTag> PostTags { get; set; }
		public DbSet<Comment> Comments { get; set; }
	}
}
