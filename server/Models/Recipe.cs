using System.Text.Json.Serialization;

namespace allspice.Models;

public class Recipe
{
  [JsonPropertyName("id")]
  public int Id { get; set; }
  public string Title { get; set; }
  public string Instructions { get; set; }
  public string Img { get; set; }
  public string Category { get; set; }
  public string CreatorId { get; set; }
  public Account Creator { get; set; }
  public List<Ingredient> Ingredients { get; set; }

  public List<Favorite> Favorites { get; set; }
  public int FavoriteCount { get; set; }
}
