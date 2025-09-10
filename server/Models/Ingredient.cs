using System.Text.Json.Serialization;

namespace allspice.Models;

public class Ingredient
{
  [JsonPropertyName("id")]
  public int Id { get; set; }
  public string Name { get; set; }
  public string Quantity { get; set; }
  public int Recipe_Id { get; set; }
}
