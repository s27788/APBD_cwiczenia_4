using KlinikaAPI.Data;
using Microsoft.EntityFrameworkCore;
using KlinikaAPI.Modele;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<KlinikaDbContext>(options =>
    options.UseSqlite("Data Source=klinika.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<KlinikaDbContext>();

    if (!context.Zwierzeta.Any())
    {
        var zwierze = new Zwierze
        {
            Imie = "Miga",
            Gatunek = "Pies",
            Wizyty = new List<Wizyta>
            {
                new Wizyta
                {
                    Data = DateTime.Now,
                    Opis = "Odrobalanie"
                }
            }
        };

        context.Zwierzeta.Add(zwierze);
        context.SaveChanges();
    }
}

app.Run();