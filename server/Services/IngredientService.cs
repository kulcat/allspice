public class IngredientService
{
  private readonly IngredientsRepository _repo;
  public IngredientService(IngredientsRepository repo)
  {
    _repo = repo;
  }

  public List<Ingredient> GetIngredients()
  {
    return _repo.GetAll();
  }

  public Ingredient GetIngredientById(int id)
  {
    return _repo.GetById(id);
  }

  public Ingredient CreateIngredient(Ingredient Ingredient)
  {
    return _repo.Create(Ingredient);
  }

  public bool DeleteIngredient(int id)
  {
    return _repo.Delete(id);
  }

  public Ingredient UpdateIngredient(int id, Ingredient updateData)
  {
    return _repo.Update(id, updateData);
  }
}