using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Test_ACME.Models;

namespace IdentityApp.Data 
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyField> SurveyFields { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura la relación entre SurveyResponses y Surveys
            modelBuilder.Entity<SurveyResponse>()
                .HasOne(sr => sr.Survey)
                .WithMany()
                .HasForeignKey(sr => sr.SurveyId)
                .OnDelete(DeleteBehavior.Restrict); // Evita la eliminación en cascada

            // Configuración de la relación entre SurveyField y SurveyResponse
            modelBuilder.Entity<SurveyField>()
                .HasMany(f => f.SurveyResponses)
                .WithOne(r => r.Field)
                .HasForeignKey(r => r.FieldId)
                .OnDelete(DeleteBehavior.Cascade); // Elimina respuestas al eliminar el campo
        }


    }
}
