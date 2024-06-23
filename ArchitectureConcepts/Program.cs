// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.Json;
using ArchitectureConcepts.Common.External.API.Endpoints;

Console.WriteLine("Architecture Concepts");
Console.WriteLine("-----------------------");

Console.WriteLine("- (o)nion architecture");
Console.WriteLine("- (c)lean architecture");
Console.WriteLine("- (e)xit the program");
var architecture = Console.ReadLine();

switch (architecture)
{
    case "o": break;
    case "c": break;
    case "e": return;
    default: Console.WriteLine("Invalid input"); return;
}

var @continue = true;
while (@continue)
{
    Console.WriteLine("- (c)reate an observation");
    Console.WriteLine("- (u)pdate an observation");
    Console.WriteLine("- (d)elete an observation");
    Console.WriteLine("- (g)et an observation");
    Console.WriteLine("- (l)ist observations");
    Console.WriteLine("- (e)xit the program");
    var userInput = Console.ReadLine();

    switch (userInput)
    {
        case "c" : CreateObservationAsync(); break;
        case "u" : UpdateObservationAsync(); break;
        case "d" : DeleteObservationAsync(); break;
        case "g" : GetObservationAsync(); break;
        case "l" : ListObservationsAsync(); break;
        case "e" : @continue = false; break;
        default : Console.WriteLine("Invalid input"); break;
    }
}
return;

async void CreateObservationAsync()
{
    Console.WriteLine("- Observation name:");
    var observationName = Console.ReadLine();
    
    Console.WriteLine("- Observation description:");
    var observationDescrption = Console.ReadLine();
    
    Console.WriteLine("- User name:");
    var userName = Console.ReadLine();
    
    var createObservationRequest = new CreateObservationRequest(observationName, observationDescrption, userName);
    
    using var httpClient = new HttpClient();
    httpClient.BaseAddress = GetArchitectureBaseAddress(architecture);
    using var request = new HttpRequestMessage(HttpMethod.Post, "api/observations");
    using var content = new StringContent(JsonSerializer.Serialize(createObservationRequest), Encoding.UTF8, "application/json");
    request.Content = content;
    using var response = await httpClient.SendAsync(request);
    if (response.IsSuccessStatusCode)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
    }
    else
    {
        Console.WriteLine("Error creating observation");
    }
}

async void UpdateObservationAsync()
{
    Console.WriteLine("- Observation Id:");
    var observationId = Console.ReadLine();
    
    Console.WriteLine("- Observation name:");
    var observationName = Console.ReadLine();
    
    Console.WriteLine("- Observation description:");
    var observationDescrption = Console.ReadLine();
    
    var updateObservationRequest = new UpdateObservationRequest(observationName, observationDescrption);
    
    var httpClient = new HttpClient();
    httpClient.BaseAddress = GetArchitectureBaseAddress(architecture);
    var request = new HttpRequestMessage(HttpMethod.Put, $"api/observations/{observationId}");
    var content = new StringContent(JsonSerializer.Serialize(updateObservationRequest), Encoding.UTF8, "application/json");
    request.Content = content;
    var response = await httpClient.SendAsync(request);
    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("Observation updated");
    }
    else
    {
        Console.WriteLine("Error updating observation");
    }
}

async void DeleteObservationAsync()
{
    Console.WriteLine("- Observation Id:");
    var observationId = Console.ReadLine();
    
    var httpClient = new HttpClient();
    httpClient.BaseAddress = GetArchitectureBaseAddress(architecture);
    var request = new HttpRequestMessage(HttpMethod.Delete, $"api/observations/{observationId}");
    var response = await httpClient.SendAsync(request);
    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("Observation deleted");
    }
    else
    {
        Console.WriteLine("Error deleting observation");
    }
}

async void GetObservationAsync()
{
    Console.WriteLine("- Observation Id:");
    var observationId = Console.ReadLine();
    
    var httpClient = new HttpClient();
    httpClient.BaseAddress = GetArchitectureBaseAddress(architecture);
    var request = new HttpRequestMessage(HttpMethod.Get, $"api/observations/{observationId}");
    var response = await httpClient.SendAsync(request);
    if (response.IsSuccessStatusCode)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
    }
    else
    {
        Console.WriteLine("Error getting observation");
    }
}

async void ListObservationsAsync()
{
    var httpClient = new HttpClient();
    httpClient.BaseAddress = GetArchitectureBaseAddress(architecture);
    var request = new HttpRequestMessage(HttpMethod.Get, "api/observations");
    var response = await httpClient.SendAsync(request);
    if (response.IsSuccessStatusCode)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
    }
    else
    {
        Console.WriteLine("Error listing observations");
    }
}

Uri GetArchitectureBaseAddress(string architecture)
{
    return architecture switch
    {
        "o" => new Uri("https://localhost:5001"),
        "c" => new Uri("https://localhost:5002"),
        _ => throw new NotImplementedException()
    };
}