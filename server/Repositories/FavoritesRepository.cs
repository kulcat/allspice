
namespace allspice.Repositories;

public class FavoritesRepository : IRepository<Favorite>
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  public Favorite Create(Favorite data)
  {
    string sql = @"
    INSERT INTO 
    Favorites(recipe_id, creator_Id)
    VALUES(@Recipe_id, @Creator_id))";

    int id = _db.ExecuteScalar<int>(sql, data);
    data.Id = id;
    return data;
  }

  public bool Delete(int id)
  {
    string sql = @"DELETE FROM Favorites WHERE id = @id LIMIT 1";
    int rowsAffected = _db.Execute(sql, new { id });
    return rowsAffected > 0;
  }

  public List<Favorite> GetAll()
  {
    string sql = @"SELECT * FROM Favorites";
    return _db.Query<Favorite>(sql).ToList();
  }

  public Favorite GetById(int id)
  {
    string sql = @"SELECT * FROM Favorites WHERE id = @id";
    return _db.QueryFirstOrDefault<Favorite>(sql, new { id });
  }

  public Favorite Update(Favorite updateData)
  {
    string sql = @"
      UPDATE Favorites
      SET 
        recipe_id = @Recipe_id,
        creator_id = @Creator_id
      WHERE id = @Id;
      SELECT * FROM Favorite WHERE id = @Id;
    ";

    return _db.QueryFirstOrDefault<Favorite>(sql, updateData);
  }

  IEnumerable<Favorite> IRepository<Favorite>.GetAll()
  {
    return GetAll();
  }

  public Favorite Update(int id, Favorite updateData)
  {
    updateData.Id = id;
    return Update(updateData);
  }
}

