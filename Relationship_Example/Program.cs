using Microsoft.EntityFrameworkCore;
using Relationship_Example.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddDbContext<RelationshipContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("RelationshipContext")), ServiceLifetime.Scoped);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseMigrationsEndPoint();
}

else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<RelationshipContext>();
    context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
