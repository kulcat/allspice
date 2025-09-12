
namespace allspice.Repositories;

public class RecipesRepository : IRepository<Recipe>
{
  private readonly IDbConnection _db;

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  public Recipe Create(Recipe data)
  {
    string sql = @"
    INSERT INTO 
    Recipes(title, instructions, img, category, creator_id)
    VALUES(@Title, @Instructions, @Img, @Category, @CreatorId);
    SELECT LAST_INSERT_ID();";

    int id = _db.ExecuteScalar<int>(sql, data);
    return GetById(id);
  }

  public bool Delete(int id)
  {
    string sql = @"DELETE FROM Recipes WHERE id = @id LIMIT 1";
    int rowsAffected = _db.Execute(sql, new { id });
    return rowsAffected > 0;
  }

  public List<Recipe> GetAll()
  {
    string sql = @"
        SELECT 
            r.id, r.title, r.instructions, r.img, r.category, r.created_at, r.updated_at,
            a.id, a.name, a.email, a.picture, a.created_at, a.updated_at,
            i.id, i.name, i.quantity, i.created_at, i.updated_at, i.recipe_id,
            f.id, f.account_id, f.created_at, f.updated_at,
            (SELECT COUNT(*) FROM Favorites WHERE recipe_id = r.id) AS FavoriteCount
        FROM Recipes r
        JOIN Accounts a ON a.id = r.creator_id
        LEFT JOIN Ingredients i ON i.recipe_id = r.id
        LEFT JOIN Favorites f ON f.recipe_id = r.id
        ORDER BY r.created_at DESC;
    ";

    var recipeDict = new Dictionary<int, Recipe>();

    var recipes = _db.Query<Recipe, Account, Ingredient, Favorite, Recipe>(
        sql,
        (recipe, account, ingredient, favorite) =>
        {
          if (!recipeDict.TryGetValue(recipe.Id, out var currentRecipe))
          {
            currentRecipe = recipe;
            currentRecipe.Creator = account;
            currentRecipe.Ingredients = new List<Ingredient>();
            currentRecipe.Favorites = new List<Favorite>();
            recipeDict.Add(currentRecipe.Id, currentRecipe);
          }

          if (ingredient != null && ingredient.Id != 0 &&
              !currentRecipe.Ingredients.Any(i => i.Id == ingredient.Id))
          {
            currentRecipe.Ingredients.Add(ingredient);
          }

          if (favorite != null && favorite.Id != 0 &&
              !currentRecipe.Favorites.Any(f => f.Id == favorite.Id))
          {
            currentRecipe.Favorites.Add(favorite);
          }

          return currentRecipe;
        },
        splitOn: "Id,Id,Id"
    );

    return recipeDict.Values.ToList();
  }



  public Recipe GetById(int id)
  {
    string sql = @"
        SELECT 
            r.id, r.title, r.instructions, r.img, r.category, r.created_at, r.updated_at,
            a.id, a.name, a.email, a.picture, a.created_at, a.updated_at,
            i.id, i.name, i.quantity, i.created_at, i.updated_at, i.recipe_id,
            f.id, f.account_id, f.created_at, f.updated_at,
            (SELECT COUNT(*) FROM Favorites WHERE recipe_id = r.id) AS FavoriteCount
        FROM Recipes r
        JOIN Accounts a ON a.id = r.creator_id
        LEFT JOIN Ingredients i ON i.recipe_id = r.id
        LEFT JOIN Favorites f ON f.recipe_id = r.id
        WHERE r.id = @Id
    ";

    var recipeDict = new Dictionary<int, Recipe>();

    var recipes = _db.Query<Recipe, Account, Ingredient, Favorite, Recipe>(
        sql,
        (recipe, account, ingredient, favorite) =>
        {
          if (!recipeDict.TryGetValue(recipe.Id, out var currentRecipe))
          {
            currentRecipe = recipe;
            currentRecipe.Creator = account;
            currentRecipe.Ingredients = new List<Ingredient>();
            currentRecipe.Favorites = new List<Favorite>();
            recipeDict.Add(currentRecipe.Id, currentRecipe);
          }

          if (ingredient != null && ingredient.Id != 0 &&
              !currentRecipe.Ingredients.Any(i => i.Id == ingredient.Id))
          {
            currentRecipe.Ingredients.Add(ingredient);
          }

          if (favorite != null && favorite.Id != 0 &&
              !currentRecipe.Favorites.Any(f => f.Id == favorite.Id))
          {
            currentRecipe.Favorites.Add(favorite);
          }

          return currentRecipe;
        },
        new { Id = id },
        splitOn: "Id,Id,Id"
    );

    return recipeDict.Values.FirstOrDefault();
  }


  public Recipe Update(Recipe updateData)
  {
    string sql = @"
        UPDATE Recipes
        SET 
            title = @Title,
            instructions = @Instructions,
            img = COALESCE(@Img, img),
            category = COALESCE(@Category, category) 
        WHERE id = @Id;
    ";

    _db.Execute(sql, updateData);

    return GetById(updateData.Id);
  }

  IEnumerable<Recipe> IRepository<Recipe>.GetAll()
  {
    return GetAll();
  }

  public Recipe Update(int id, Recipe updateData)
  {
    updateData.Id = id;
    return Update(updateData);
  }
  
  // public List<Ingredient> GetIngredientsByRecipeId(int id)
  // {
  //   updateData.Id = id;
  //   return Update(updateData);
  // }
}

