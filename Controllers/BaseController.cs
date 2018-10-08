using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

public class BaseController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly IConfiguration _config;

    public BaseController(IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _httpClientFactory = httpClientFactory;
        _config = config;
    }
    protected HttpClient GetClient(string route)
    {
        var client = _httpClientFactory.CreateClient("Marvel_APIs");

        string ts = DateTime.Now.Ticks.ToString();

        string baseURL = _config.GetSection("Marvel_APIs:BaseURL").Value;
        string key = _config.GetSection("Marvel_APIs:Key").Value;

        string hash = GerarHash(ts, key,
            _config.GetSection("Marvel_APIs:PrivateKey").Value);

        client.BaseAddress = new Uri(
            baseURL + route +
            $"?apikey={key}&hash={hash}");

        return client;
    }

    private string GerarHash(
            string ts, string publicKey, string privateKey)
    {
        byte[] bytes =
            Encoding.UTF8.GetBytes(ts + privateKey + publicKey);
        var gerador = MD5.Create();
        byte[] bytesHash = gerador.ComputeHash(bytes);
        return BitConverter.ToString(bytesHash)
            .ToLower().Replace("-", String.Empty);
    }
}