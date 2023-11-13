namespace MiniAccounting.Service.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly Operator _operator;
    private readonly ILogger _logger;

    public UserController(ILogger logger, Operator @operator)
    {
        _operator = @operator;
        _logger = logger;
    }

    [HttpPost]
    public void Save(User user)
    {
        _operator.SaveUser(user);
    }

    [HttpPost]
    public void SaveUsers(IEnumerable<User> users)
    {
        _operator.SaveUsers(users);
    }

    [HttpGet]
    public List<User> ReadUsers()
    {
        return _operator.ReadUsers();
    }

    [HttpGet]
    public User Read(Guid userUid)
    {
        return _operator.ReadUser(userUid);
    }

    [HttpPut]
    public void Edit(User user)
    {
        _operator.Edit(user);
    }

    [HttpDelete]
    public void Delete(Guid userUid)
    {
        _operator.Delete(userUid);
    }
}
