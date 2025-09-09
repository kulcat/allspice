
namespace allspice.Repositories;

public class IngredientsRepository : IRepository<Ingredient>
{
  private readonly IDbConnection _db;

  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  public Ingredient Create(Ingredient data)
  {
    string sql = @"
    INSERT INTO 
    Ingredients(name, quantity, recipe_id) 
    VALUES(@Name, @Quantity, @Recipe_id)";

    int id = _db.ExecuteScalar<int>(sql, data);
    data.Id = id;
    return data;
  }

  public bool Delete(int id)
  {
    string sql = @"DELETE FROM Ingredients WHERE id = @id LIMIT 1";
    int rowsAffected = _db.Execute(sql, new { id });
    return rowsAffected > 0;
  }

  public List<Ingredient> GetAll()
  {
    string sql = @"SELECT * FROM Ingredients";
    return _db.Query<Ingredient>(sql).ToList();
  }

  public List<Ingredient> GetByRecipeId(int id)
  {
    string sql = @"SELECT * FROM Ingredients WHERE recipe_id = @id";
    return _db.Query<Ingredient>(sql).ToList();
  }

  public Ingredient GetById(int id)
  {
    string sql = @"SELECT * FROM Ingredients WHERE id = @id";
    return _db.QueryFirstOrDefault<Ingredient>(sql, new { id });
  }

  public Ingredient Update(Ingredient updateData)
  {
    string sql = @"
      UPDATE Ingredients
      SET 
        name = @Name,
        quantity = @Quantity,
        recipe_id = @Recipe_id,
      WHERE id = @Id;
      SELECT * FROM Ingredients WHERE id = @Id;
    ";

    return _db.QueryFirstOrDefault<Ingredient>(sql, updateData);
  }

  IEnumerable<Ingredient> IRepository<Ingredient>.GetAll()
  {
    return GetAll();
  }

  public Ingredient Update(int id, Ingredient updateData)
  {
    updateData.Id = id;
    return Update(updateData);
  }
}

