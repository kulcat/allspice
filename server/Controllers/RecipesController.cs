namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RecipesController : ControllerBase
{
  private readonly RecipesService _service;
  private readonly Auth0Provider _auth0Provider;


  public RecipesController(RecipesService service, Auth0Provider auth0Provider)
  {
    _service = service;
    _auth0Provider = auth0Provider;
  }

  [AllowAnonymous]
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

  [AllowAnonymous]
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
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipe)
  {
    try
    {
      Account creator = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      recipe.Creator_id = creator.Id;
      var newRecipe = _service.CreateRecipe(recipe);
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