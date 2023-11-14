namespace MiniAccounting.Infrastructure;

public class MiniAccountingClient
{
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;
    public string Address { get; }

    public MiniAccountingClient(ILogger logger, HttpClient httpClient, string address)
    {
        _logger = logger;
        _httpClient = httpClient;
        Address = FormatAdress(address);
    }

    private string FormatAdress(string address)
    {
        if (!address.StartsWith("http"))
            address = "http://" + address;

        address = address.Replace("https", "http");

        address = address.TrimEnd('/') + "/";

        return address;
    }
    private async Task<string> SendAsync(HttpRequestMessage request, CancellationToken token)
    {
        try
        {
            using var result = await _httpClient.SendAsync(request, token).ConfigureAwait(false);

            var content = await result.Content?.ReadAsStringAsync();

            if (result.StatusCode == HttpStatusCode.OK)
                return content;

            throw new Exception($"Неуспешный StatusCode ({request.RequestUri}): {result.StatusCode}. Content='{content}'");
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка во время запроса ({request.RequestUri}): {ex}");
        }
    }

    public async Task<double> TopUpTotalBalanceAsync(double addMoney, string comment, CancellationToken token = default)
    {
        // http://localhost:5099/Operator/TopUpTotalBalance?addMoney=100&comment=123
        var fullAddress = $"{Address}Operator/TopUpTotalBalance";
        var @params = new Dictionary<string, string>() { { "addMoney", addMoney.ToString() }, { "comment", comment } };
        var url = new Uri(QueryHelpers.AddQueryString(fullAddress, @params));

        using var request = new HttpRequestMessage(HttpMethod.Put, url);
        var content = await SendAsync(request, token).ConfigureAwait(false);
        if (!double.TryParse(content, out var doubleResult))
            throw new Exception($"Неизвестный ответ ({url}): {content}");

        return doubleResult;
    }



    public async Task<double> RemoveFromTotalBalanceAsync(double removeMoney, string comment, CancellationToken token = default)
    {
        var fullAddress = $"{Address}Operator/RemoveFromTotalBalance";
        var @params = new Dictionary<string, string>() { { "removeMoney", removeMoney.ToString() }, { "comment", comment } };
        var url = new Uri(QueryHelpers.AddQueryString(fullAddress, @params));

        using var request = new HttpRequestMessage(HttpMethod.Put, url);
        var content = await SendAsync(request, token).ConfigureAwait(false);
        if (!double.TryParse(content, out var doubleResult))
            throw new Exception($"Неизвестный ответ ({url}): {content}");

        return doubleResult;
    }

    public async Task<double> GetTotalBalanceAsync(CancellationToken token = default)
    {
        var fullAddress = $"{Address}Operator/GetTotalBalance";

        using var request = new HttpRequestMessage(HttpMethod.Get, fullAddress);
        var content = await SendAsync(request, token).ConfigureAwait(false);
        if (!double.TryParse(content, out var doubleResult))
            throw new Exception($"Неизвестный ответ ({fullAddress}): {content}");

        return doubleResult;
    }

    public async Task SaveAsync(User user, CancellationToken token = default)
    {
        var fullAddress = $"{Address}User/Save";
        var url = new Uri(fullAddress);

        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        await SendAsync(request, token).ConfigureAwait(false);
    }

    public async Task SaveUsersAsync(IEnumerable<User> users, CancellationToken token = default)
    {
        var fullAddress = $"{Address}User/SaveUsers";
        var url = new Uri(fullAddress);

        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");

        await SendAsync(request, token).ConfigureAwait(false);
    }

    public async Task<List<User>> ReadUsersAsync(CancellationToken token = default)
    {
        var fullAddress = $"{Address}User/ReadUsers";
        var url = new Uri(fullAddress);

        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        var content = await SendAsync(request, token).ConfigureAwait(false);

        List<User> result = default;
        try
        {
            result = JsonConvert.DeserializeObject<List<User>>(content);
        }
        catch (Exception ex)
        {
            throw new Exception($"Неизвестный ответ ({url}): {ex}");
        }

        return result;
    }

    public async Task<User> ReadAsync(Guid userUid, CancellationToken token = default)
    {
        var fullAddress = $"{Address}User/ReadUsers";
        var url = new Uri(fullAddress);

        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        var content = await SendAsync(request, token).ConfigureAwait(false);

        User result = default;
        try
        {
            result = JsonConvert.DeserializeObject<User>(content);
        }
        catch (Exception ex)
        {
            throw new Exception($"Неизвестный ответ ({url}): {ex}");
        }

        return result;
    }

    public async Task EditAsync(User user, CancellationToken token = default)
    {
        var fullAddress = $"{Address}User/Edit";
        var url = new Uri(fullAddress);

        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
        await SendAsync(request, token).ConfigureAwait(false);
    }

    public async Task DeleteAsync(Guid userUid, CancellationToken token = default)
    {
        var fullAddress = $"{Address}User/Delete";
        var @params = new Dictionary<string, string>() { { "userUid", userUid.ToString() } };
        var url = new Uri(QueryHelpers.AddQueryString(fullAddress, @params));

        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        await SendAsync(request, token).ConfigureAwait(false);
    }
}
