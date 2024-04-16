using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class WikyDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Commentaires { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public WikyDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TPCodeFirstEF;Trusted_Connection=True");
                optionsBuilder.UseInMemoryDatabase("TP_WikY_API");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var a1 = new Article { 
                Id = 1, 
                Author = "Jean Dujardin", 
                CreationDate = DateTime.Parse("12/01/2024"), 
                Content = "Ceci est un article de l'auteur Jean Dujardin",
                Priority = Priority.Low,
                ThemeID = 1
                };
			var a2 = new Article
			{
				Id = 2,
				Author = "Christine Carpentras",
				Content = "Ceci est un article de l'auteur Christine Carpentras",
				Priority = Priority.Medium,
				ThemeID = 3
			};
			var t1 = new Theme { Id = 1, Label = "theme 1"};
			var t2 = new Theme { Id = 2, Label = "theme 2" };
			var t3 = new Theme { Id = 3, Label = "theme 3" };
			var c1 = new Comment { 
                Id =1, 
                Author = "Victoire Jarbi", 
                Content = "J'aime beaucoup cet article",
                ArticleID = 1
            };
			var c2 = new Comment
			{
				Id = 1,
				Author = "Robert Breton",
				Content = "Bof bof",
				ArticleID = 2
			};
			modelBuilder.Entity<Article>().HasData(new List<Article> { a1,a2 });
            modelBuilder.Entity<Theme>().HasData(new List<Theme> { t1,t2,t3 });
			modelBuilder.Entity<Comment>().HasData(new List<Comment> { c1,c2 });
			base.OnModelCreating(modelBuilder);
        }
    }
}
