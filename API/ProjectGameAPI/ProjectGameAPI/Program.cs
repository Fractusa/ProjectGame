using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectGameAPI.Data;
using ProjectGameAPI.GraphQL;

var builder = WebApplication.CreateBuilder(args);

// EF Core: SQLite example
builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseSqlite("Data Source=game.db"));

// GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .RegisterDbContextFactory<AppDbContext>()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

// Ensure DB exists (simple for exam)
using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
    using var db = factory.CreateDbContext();
    db.Database.EnsureCreated();
}

app.MapGraphQL("/graphql");

app.Run();