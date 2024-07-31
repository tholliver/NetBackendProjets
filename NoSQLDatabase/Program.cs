using MongoDB.Bson;
using MongoDB.Driver;
using NoSQLDatabase.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// builder.Services.AddDbContext<DataContext>(
//     options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var connectionString = builder.Configuration.GetConnectionString("AtlasMongoDB");

app.MapGet("/", async () =>
{
    // myFirstDatabase.users

    var client = new MongoClient(connectionString);
    var collection = client.GetDatabase("myFirstDatabase").GetCollection<User>("users");
    var filter = Builders<User>.Filter.Eq("", "");
    var document = await collection.Find(_ => true).ToListAsync();
    return Results.Ok(document);
});

app.Run();

