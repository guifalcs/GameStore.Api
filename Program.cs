using Gamestore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new (1, "The Legend of Zelda: Breath of the Wild", "Action-adventure", 59.99m, new DateOnly(2017, 3, 3)),
    new (2, "Super Mario Odyssey", "Platformer", 59.99m, new DateOnly(2017, 10, 27)),
    new (3, "Animal Crossing: New Horizons", "Social simulation", 59.99m, new DateOnly(2020, 3, 20)),
];

app.MapGet("games", () => games);

app.MapGet("games/{id}", (int id) =>
{
    var game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);

})
    .WithName("GetGameById");

app.MapPost("games", (CreateGameDto newGame) =>
{
    GameDto game = new GameDto(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);

    return Results.CreatedAtRoute("getGameById", new { id = game.Id }, game);
});

app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var index = games.FindIndex(game => game.Id == id);

    if (index == -1)
    {
        return Results.NotFound();
    }

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    return Results.NoContent();
});

app.MapDelete("games/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);
    return Results.NoContent();
});

app.Run();