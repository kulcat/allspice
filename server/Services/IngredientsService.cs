public class IngredientsService
{
  private readonly IngredientsRepository _repo;
  public IngredientsService(IngredientsRepository repo)
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

  public Ingredient CreateIngredient(Ingredient ingredient)
  {
    return _repo.Create(ingredient);
  }

  public List<Ingredient> CreateIngredients(List<Ingredient> ingredients)
  {
    return _repo.CreateMultiple(ingredients);
  }

  public bool DeleteIngredient(int id)
  {
    return _repo.Delete(id);
  }

  public bool DeleteIngredients(List<int> ids)
  {
    return _repo.DeleteMultiple(ids);
  }

  public Ingredient UpdateIngredient(int id, Ingredient updateData)
  {
    return _repo.Update(id, updateData);
  }
}