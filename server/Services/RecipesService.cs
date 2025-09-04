public class RecipesService
{
  private readonly RecipesRepository _repo;
  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }

  public List<Recipe> GetRecipes()
  {
    return _repo.GetAll();
  }

  public Recipe GetRecipeById(int id)
  {
    return _repo.GetById(id);
  }

  public Recipe CreateRecipe(Recipe Recipe)
  {
    return _repo.Create(Recipe);
  }

  public bool DeleteRecipe(int id)
  {
    return _repo.Delete(id);
  }

  public Recipe UpdateRecipe(int id, Recipe updateData)
  {
    return _repo.Update(id, updateData);
  }
}