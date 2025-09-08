using Microsoft.EntityFrameworkCore;
using People.Api.Models;

namespace People.Api.Data
{
    // A classe representa o banco de dados da aplicação
    // Aqui configurado entidades que vão ser usadas na tabela
    public class AppDbContext : DbContext
    {
        // Cria uma tabela chamada "People" no banco, baseada na classe Person
        public DbSet<Person> People => Set<Person>();

        // Construtor padrão, passando as opções pro Entity Framework
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
