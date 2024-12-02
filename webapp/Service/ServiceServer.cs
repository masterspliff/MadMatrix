namespace webapp.Service;
using System.Net.Http.Json;


public class ServiceServer : IService
{
    private readonly HttpClient _httpClient;

    public ServiceServer(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    // Indsæt metoder herunder
}


