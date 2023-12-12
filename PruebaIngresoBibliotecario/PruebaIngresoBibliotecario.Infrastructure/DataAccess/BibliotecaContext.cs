namespace PruebaIngresoBibliotecario.Infrastructure.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using PruebaIngresoBibliotecario.Core.Entities;
    using PruebaIngresoBibliotecario.Infrastructure.Common.Constants;
    using PruebaIngresoBibliotecario.Infrastructure.Common.Dtos;

    public class BibliotecaContext : DbContext
    {
        #region Properties
        private readonly IConfiguration _configuration;
        private DataBaseDto? _dataBaseDto;
        #endregion

        public BibliotecaContext(DbContextOptions<BibliotecaContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        #region Entities
        public virtual DbSet<Loan>? Loans { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            DataBaseDto instance = _dataBaseDto = new DataBaseDto();
            _configuration.Bind(DataBaseConstant.ConnectionStrings, instance);
            var connectionString = _dataBaseDto.database;
            if (!string.IsNullOrEmpty(connectionString)) optionsBuilder.UseInMemoryDatabase(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_configuration.GetValue<string>("Biblioteca"));
            modelBuilder.Entity<Loan>(entity =>
            {
                entity.ToTable("LOAN");
                entity.HasKey(e => e.Id)
                    .HasName("PK_LOAN");
                entity.Property(e => e.Isbn)
                     .HasColumnName("ISBN");
                entity.Property(e => e.UserId)
                    .HasColumnName("USERID");
                entity.Property(e => e.UserType)
                     .HasColumnName("USERTYPE");
                entity.Property(e => e.DevolutionDate)
                    .HasColumnName("DEVOLUTIONDATE");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
