using Microsoft.EntityFrameworkCore;
using movies_api.Entities;
using System;
using System.Collections.Generic;

namespace EFCoreSqlServer.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Location> Locations { get; set; }

        //Para Simplificar, decidi inicializar a tabela de gêneros com os gêneros abaixo, e assim, o usuário irá escolher apenas dentre os gêneros existentes
        //Ao menos, numa primeira versão dessa aplicação, fator tempo foi determinante para essa tomada de decisão
        public void InitializeGenders()
        {
            var genders = new List<Gender>
            {
                new Gender { Name = "Ação", Active = 1, Date = DateTime.Now },
                new Gender { Name = "Aventura", Active = 1, Date = DateTime.Now },
                new Gender { Name = "Comédia", Active = 1, Date = DateTime.Now },
                new Gender { Name = "Documentário" , Active = 1, Date = DateTime.Now },
                new Gender { Name = "Drama", Active = 1, Date = DateTime.Now },
                new Gender { Name = "Fantasia", Active = 1, Date = DateTime.Now },
                new Gender { Name = "Ficção Científica", Active = 1, Date = DateTime.Now },
                new Gender { Name = "Romance", Active = 1, Date = DateTime.Now },
                new Gender { Name = "Suspense", Active = 1, Date = DateTime.Now },
                new Gender { Name = "Terror", Active = 1, Date = DateTime.Now },
                new Gender { Name = "Outro", Active = 1, Date = DateTime.Now }
            };

            Genders.AddRange(genders);
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Movie>()
                .HasOne(f => f.Gender)
                .WithMany()            
                .HasForeignKey(f => f.GenderId);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.Movies) // Uma locação tem muitos filmes
                .WithMany(f => f.Locations) // Um filme está envolvido em muitas locações
                .UsingEntity(j => j
                    .ToTable("LocationsMovies") // Tabela intermediária
            );

        }
    }
}