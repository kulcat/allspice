public class FavoritesService
{
  private readonly FavoritesRepository _repo;
  public FavoritesService(FavoritesRepository repo)
  {
    _repo = repo;
  }

  public List<Favorite> GetFavorites()
  {
    return _repo.GetAll();
  }

  public Favorite GetFavoriteById(int id)
  {
    return _repo.GetById(id);
  }

  public Favorite CreateFavorite(Favorite Favorite)
  {
    return _repo.Create(Favorite);
  }

  public bool DeleteFavorite(int id)
  {
    return _repo.Delete(id);
  }

  public Favorite UpdateFavorite(int id, Favorite updateData)
  {
    return _repo.Update(id, updateData);
  }
}