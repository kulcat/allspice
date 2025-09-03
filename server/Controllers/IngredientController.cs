namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientController : ControllerBase
{
  private readonly IngredientService _service;
  private readonly Auth0Provider _auth0Provider;


  public IngredientController(IngredientService service, Auth0Provider auth0Provider)
  {
    _service = service;
    _auth0Provider = auth0Provider;
  }

  [HttpGet]
  public ActionResult<IEnumerable<Ingredient>> GetIngredients()
  {
    try
    {
      var ingredients = _service.GetIngredients();
      return Ok(ingredients);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public ActionResult<Ingredient> GetIngredientById(int id)
  {
    try
    {
      var ingredient = _service.GetIngredientById(id);
      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPost]
  public ActionResult<Ingredient> CreateIngredient([FromBody] Ingredient Ingredient)
  {
    try
    {
      var newIngredient = _service.CreateIngredient(Ingredient);
      return Ok(newIngredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]
  public ActionResult<string> DeleteIngredient(int id)
  {
    try
    {
      var result = _service.DeleteIngredient(id);
      return Ok(result);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{id}")]
  public ActionResult<Ingredient> UpdateIngredient(int id, [FromBody] Ingredient update)
  {
    try
    {
      var ingredient = _service.UpdateIngredient(id, update);
      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}