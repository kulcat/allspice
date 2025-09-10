using System.Text.Json.Serialization;

namespace allspice.Models;

public class Favorite
{
  [JsonPropertyName("id")]
  public int Id { get; set; }
  public int Recipe_id { get; set; }
  public int Account_id { get; set; }

}
