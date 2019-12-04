using System.Linq;
using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Context
{
    public class DevIOContext : DbContext
    {
        public DevIOContext(DbContextOptions options) : base(options){}

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Setar campo string default para varchar(100)
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
            {
                property.Relational().ColumnType = "varchar(100)";
            }
            #endregion

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DevIOContext).Assembly);

            #region Desabilitar CascateDelete
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}