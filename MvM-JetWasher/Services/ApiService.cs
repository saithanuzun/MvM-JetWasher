using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        // This HttpClient instance is automatically configured (e.g., with the BaseAddress) 
        // by the HttpClientFactory when you register it as a Typed Client.
        _httpClient = httpClient;
    }

    public async Task SendMail(ContactForm form)
    {
        Console.WriteLine("starting");

        Debug.WriteLine("starting");
        // 1. Corrected Mapping (Assuming ContactForm has Name, Email, MobileNumber, Comment)
        var model = new ContactViewModel()
        {
            FullName = form.Name,
            Email = form.MobileNumber,           // Use form.Email (if available)
            PhoneNumber = form.MobileNumber, 
            Subject = "leicester jet washer request",
            Massage = form.Comment,       // Note: "Massage" is usually "Message"
        };
        
        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync("api/contactapi", content);

            if (response.IsSuccessStatusCode)
            {
                // 2. Critical Fix: Await the ReadAsStringAsync() call
                var responseContent = await response.Content.ReadAsStringAsync(); 
                Debug.WriteLine($"Success! API Response: {responseContent}");
            }
            else
            {
                Debug.WriteLine($"Error! Status: {response.StatusCode}");
                // Optional: Read error content for debugging
                // var errorContent = await response.Content.ReadAsStringAsync();
                // Debug.WriteLine($"Error Content: {errorContent}");
                
                // You should probably throw an exception here or return an error status
            }
        }
        catch (Exception ex)
        {
            // Handle network errors, serialization errors, etc.
            Debug.WriteLine($"An exception occurred: {ex.Message}");
        }
    }
}

// Ensure your ContactViewModel matches the expected JSON format on the server.
public class ContactViewModel
{
    // The [JsonPropertyName] attributes are useful but often optional if property 
    // names match the server's expected casing (PascalCase by default in .NET).
    [JsonPropertyName("FullName")]
    public string FullName { get; set; }

    [JsonPropertyName("Email")]
    public string Email { get; set; }

    [JsonPropertyName("PhoneNumber")]
    public string PhoneNumber { get; set; }
    
    [JsonPropertyName("Subject")]
    public string Subject { get; set; }

    [JsonPropertyName("Massage")] // Consider renaming this to "Message" in all codebases
    public string Massage { get; set; }
}