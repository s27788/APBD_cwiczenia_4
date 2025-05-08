using KlinikaAPI.Data;
using KlinikaAPI.Modele;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<KlinikaDbContext>(options =>
    options.UseSqlite("Data Source=klinika.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// dane testowe
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<KlinikaDbContext>();

    if (!context.Zwierzeta.Any())
    {
        var zwierze = new Zwierze
        {
            Imie = "Lok",
            Gatunek = "Pies",
            Wiek = 5,
            Wizyty = new List<Wizyta>
            {
                new Wizyta
                {
                    Data = DateTime.Now,
                    Opis = "Kontrola"
                }
            }
        };

        context.Zwierzeta.Add(zwierze);
        context.SaveChanges();
    }
}

app.Run();

// testuj: http://localhost:5019/swagger