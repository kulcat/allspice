namespace allspice.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AccountsController : ControllerBase
{
  private readonly AccountsService _accountsService;
  private readonly Auth0Provider _auth0Provider;

  public AccountsController(AccountsService accountsService, Auth0Provider auth0Provider)
  {
    _accountsService = accountsService;
    _auth0Provider = auth0Provider;
  }

  [HttpGet]
  [AllowAnonymous]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountsService.GetOrCreateAccount(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
