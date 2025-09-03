namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
  private readonly RecipeService _service;
  private readonly Auth0Provider _auth0Provider;


  public RecipeController(RecipeService service, Auth0Provider auth0Provider)
  {
    _service = service;
    _auth0Provider = auth0Provider;
  }

  [HttpGet]
  public ActionResult<IEnumerable<Recipe>> GetRecipes()
  {
    try
    {
      var recipes = _service.GetRecipes();
      return Ok(recipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public ActionResult<Recipe> GetRecipeById(int id)
  {
    try
    {
      var recipe = _service.GetRecipeById(id);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPost]
  public ActionResult<Recipe> CreateRecipe([FromBody] Recipe Recipe)
  {
    try
    {
      var newRecipe = _service.CreateRecipe(Recipe);
      return Ok(newRecipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]
  public ActionResult<string> DeleteRecipe(int id)
  {
    try
    {
      var result = _service.DeleteRecipe(id);
      return Ok(result);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{id}")]
  public ActionResult<Recipe> UpdateRecipe(int id, [FromBody] Recipe update)
  {
    try
    {
      var recipe = _service.UpdateRecipe(id, update);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}