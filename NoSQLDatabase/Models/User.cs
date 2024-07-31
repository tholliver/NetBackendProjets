namespace NoSQLDatabase.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public partial class User
{
    public ObjectId Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("username")]
    public string Username { get; set; }

    [BsonElement("date")]
    public DateTime Date { get; set; }

    [BsonElement("active")]
    public bool Active { get; set; }

    [BsonElement("__v")]
    public int V { get; set; }

}

// public partial class Date
// {
//     [JsonProperty("$date")]
//     public DateClass DateDate { get; set; }
// }

// public partial class DateClass
// {
//     [JsonProperty("$numberLong")]
//     public string NumberLong { get; set; }
// }

// public partial class Id
// {
//     [JsonProperty("$oid")]
//     public string Oid { get; set; }
// }

// public partial class V
// {
//     [JsonProperty("$numberInt")]
//     [JsonConverter(typeof(ParseStringConverter))]
//     public long NumberInt { get; set; }
// }
