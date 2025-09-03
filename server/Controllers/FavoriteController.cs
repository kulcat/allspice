namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoriteController : ControllerBase
{
  private readonly FavoriteService _service;
  private readonly Auth0Provider _auth0Provider;

  public FavoriteController(FavoriteService service, Auth0Provider auth0Provider)
  {
    _service = service;
    _auth0Provider = auth0Provider;
  }

  [HttpGet]
  public ActionResult<IEnumerable<Favorite>> GetFavorites()
  {
    try
    {
      var favorites = _service.GetFavorites();
      return Ok(favorites);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public ActionResult<Favorite> GetFavoriteById(int id)
  {
    try
    {
      var favorite = _service.GetFavoriteById(id);
      return Ok(favorite);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPost]
  public ActionResult<Favorite> CreateFavorite([FromBody] Favorite Favorite)
  {
    try
    {
      var newFavorite = _service.CreateFavorite(Favorite);
      return Ok(newFavorite);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]
  public ActionResult<string> DeleteFavorite(int id)
  {
    try
    {
      var result = _service.DeleteFavorite(id);
      return Ok(result);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{id}")]
  public ActionResult<Favorite> UpdateFavorite(int id, [FromBody] Favorite update)
  {
    try
    {
      var favorite = _service.UpdateFavorite(id, update);
      return Ok(favorite);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}