
using System.Diagnostics;
using System.Text;
using System.Text.Json;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task SendMail(ContactForm form)
    {

        var json = JsonSerializer.Serialize(form);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("home", content);


        if (response.IsSuccessStatusCode)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            Debug.WriteLine("oldu");
        }
        else
        {
            Debug.WriteLine("error");
        }
    }

}

