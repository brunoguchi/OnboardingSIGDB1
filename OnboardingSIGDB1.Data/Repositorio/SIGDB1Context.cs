using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Mapeamentos;
using OnboardingSIGDB1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace OnboardingSIGDB1.Data.Repositorio
{
    public class SIGDB1Context : DbContext
    {
        public SIGDB1Context(DbContextOptions<SIGDB1Context> options) : base(options) { }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<FuncionarioCargo> FuncionariosCargos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EmpresaMapping());
            modelBuilder.ApplyConfiguration(new FuncionarioMapping());
            modelBuilder.ApplyConfiguration(new CargoMapping());
            modelBuilder.ApplyConfiguration(new FuncionarioCargoMapping());
        }
    }
}
