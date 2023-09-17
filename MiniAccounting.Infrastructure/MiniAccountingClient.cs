using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;

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

    private async Task<string> SendAsync(HttpRequestMessage request, CancellationToken token)
    {
        using var result = await _httpClient.SendAsync(request, token).ConfigureAwait(false);

        var content = await result.Content?.ReadAsStringAsync();

        if (result.StatusCode == HttpStatusCode.OK)
            return content;

        throw new Exception($"Неуспешный StatusCode ({request.u}): {result.StatusCode}. Content='{content}'");
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

    public async Task SaveAsync(User user, CancellationToken token = default)
    {
        var fullAddress = $"{Address}UserController/Save";
        var url = new Uri(fullAddress);

        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        await SendAsync(request, token).ConfigureAwait(false);
    }

    public async Task SaveUsersAsync(IEnumerable<User> users, CancellationToken token = default)
    {
        var fullAddress = $"{Address}UserController/SaveUsers";
        var url = new Uri(fullAddress);

        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");

        await SendAsync(request, token).ConfigureAwait(false);
    }

    public async Task<List<User>> ReadUsersAsync(CancellationToken token = default)
    {
        var fullAddress = $"{Address}UserController/SaveUsers";
        var url = new Uri(fullAddress);

        using var request = new HttpRequestMessage(HttpMethod.Post, url);
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

    public Task<User> ReadAsync(Guid userUid)
    {

    }

    public Task EditAsync(User user)
    {

    }

    public Task DeleteAsync(Guid userUid)
    {

    }
}
