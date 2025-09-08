using Microsoft.EntityFrameworkCore;
using People.Api.Data;
using People.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Connection string do Azure (Jeito A e Jeito B) ou usa local como fallback
var conn =
    builder.Configuration.GetConnectionString("DefaultConnection")                         // appsettings/Configuração (Jeito B)
    ?? builder.Configuration["ConnectionStrings:DefaultConnection"]                        // env var estilo "ConnectionStrings:..."
    ?? Environment.GetEnvironmentVariable("CUSTOMCONNSTR_DefaultConnection")               // Cadeias de conexão (Jeito A)
    ?? builder.Configuration.GetConnectionString("Default")                                // Nome antigo
    ?? "Data Source=people.db";                                                            // fallback local

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(conn));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API lista de Pessoas",
        Version = "v1",
        Description = "API simples para cadastro de pessoas (Nome, Sobrenome, Telefone)"
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Swagger sempre habilitado
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Endpoints
app.MapGet("/api/people", async (AppDbContext db) =>
    await db.People.AsNoTracking().ToListAsync());

app.MapGet("/api/people/{id:guid}", async (Guid id, AppDbContext db) =>
    await db.People.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id) is Person p
        ? Results.Ok(p)
        : Results.NotFound());

app.MapPost("/api/people", async (Person person, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(person.Nome))
        return Results.BadRequest("Nome é obrigatório.");

    db.People.Add(person);
    await db.SaveChangesAsync();
    return Results.Created($"/api/people/{person.Id}", person);
});

app.MapPut("/api/people/{id:guid}", async (Guid id, Person input, AppDbContext db) =>
{
    var person = await db.People.FindAsync(id);
    if (person is null) return Results.NotFound();

    person.Nome = input.Nome;
    person.Sobrenome = input.Sobrenome;
    person.Telefone = input.Telefone;

    await db.SaveChangesAsync();
    return Results.Ok(person);
});

app.MapDelete("/api/people/{id:guid}", async (Guid id, AppDbContext db) =>
{
    var person = await db.People.FindAsync(id);
    if (person is null) return Results.NotFound();

    db.People.Remove(person);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
