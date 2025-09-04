
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
    Recipes(title, instructions, img, category, creator_Id)
    VALUES(@Title, @Instructions, @Img, @Category, @Creator_id);
    SELECT LAST_INSERT_ID();";

    int id = _db.ExecuteScalar<int>(sql, data);
    data.Id = id;
    return data;
  }

  public bool Delete(int id)
  {
    string sql = @"DELETE FROM Recipes WHERE id = @id LIMIT 1";
    int rowsAffected = _db.Execute(sql, new { id });
    return rowsAffected > 0;
  }

  public List<Recipe> GetAll()
  {
    string sql = @"SELECT * FROM Recipes";
    return _db.Query<Recipe>(sql).ToList();
  }

  public Recipe GetById(int id)
  {
    string sql = @"SELECT * FROM Recipes WHERE id = @id";
    return _db.QueryFirstOrDefault<Recipe>(sql, new { id });
  }

  public Recipe Update(Recipe updateData)
  {
    string sql = @"
      UPDATE Recipes
      SET 
        title = @Title,
        instructions = @Instructions,
        img = @Img,
        category = @Category,
        creator_id = @Creator_id
      WHERE id = @Id;
      SELECT * FROM Recipes WHERE id = @Id;
    ";

    return _db.QueryFirstOrDefault<Recipe>(sql, updateData);
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
}

