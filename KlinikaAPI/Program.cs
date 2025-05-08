using KlinikaAPI.Modele;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// dane testowe
var animals = new List<Zwierze>
{
    new() { Id = 1, Name = "Reksio", Category = "pies", Weight = 12.5, FurColor = "brązowy" },
    new() { Id = 2, Name = "Mruczek", Category = "kot", Weight = 4.2, FurColor = "czarny" }
};

var visits = new List<Wizyta>
{
    new() { Id = 1, AnimalId = 1, Date = new DateTime(2024, 01, 10), Description = "Szczepienie", Price = 100 },
    new() { Id = 2, AnimalId = 2, Date = new DateTime(2024, 02, 05), Description = "Kontrola", Price = 80 }
};

// endpointy zwierze

app.MapGet("/animals", () => animals);

app.MapGet("/animals/{id}", (int id) =>
    animals.FirstOrDefault(a => a.Id == id) is { } a ? Results.Ok(a) : Results.NotFound());

app.MapPost("/animals", (Zwierze animal) =>
{
    animal.Id = animals.Max(a => a.Id) + 1;
    animals.Add(animal);
    return Results.Created($"/animals/{animal.Id}", animal);
});

app.MapPut("/animals/{id}", (int id, Zwierze updated) =>
{
    var index = animals.FindIndex(a => a.Id == id);
    if (index == -1) return Results.NotFound();
    updated.Id = id;
    animals[index] = updated;
    return Results.Ok(updated);
});

app.MapDelete("/animals/{id}", (int id) =>
{
    var a = animals.FirstOrDefault(a => a.Id == id);
    if (a == null) return Results.NotFound();
    animals.Remove(a);
    visits.RemoveAll(v => v.AnimalId == id);
    return Results.NoContent();
});

// endpointy wizyta

app.MapGet("/animals/{id}/visits", (int id) =>
    visits.Where(v => v.AnimalId == id).ToList());

app.MapPost("/animals/{id}/visits", (int id, Wizyta visit) =>
{
    if (!animals.Any(a => a.Id == id))
        return Results.NotFound($"Brak zwierzaka o ID {id}");

    visit.Id = visits.Any() ? visits.Max(v => v.Id) + 1 : 1;
    visit.AnimalId = id;
    visits.Add(visit);
    return Results.Created($"/animals/{id}/visits/{visit.Id}", visit);
});

app.Run();
