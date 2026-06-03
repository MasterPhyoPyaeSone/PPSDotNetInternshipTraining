
using System.Text.Json.Serialization;
using System.Text.Json;

Console.WriteLine("Hello, World!");
HttpClient httpClient = new HttpClient();
// var response = await httpClient.GetAsync("http://localhost:5107/api/Author");

// if (response != null && response.IsSuccessStatusCode)
// {
//     var responseContent =await response.Content.ReadAsStringAsync();
//     Console.WriteLine(responseContent);
// }
// else
// {
//     Console.WriteLine("No response received from the API.");
// }



// this is the code to send POST request to API to create new author

// AuthorCreateRequestModel authorCreateRequestModel = new AuthorCreateRequestModel
// {
//     Name = "John Doe",
//     Bio = "John Doe is a prolific author known for his engaging storytelling and captivating characters. With a passion for writing that spans over two decades, John has published numerous best-selling novels across various genres, including mystery, romance, and science fiction. His works have garnered critical acclaim and a dedicated fan base worldwide. John's unique ability to weave intricate plots and create memorable characters has solidified his place as one of the most influential authors of our time."
// };
// // change model to json string
// var jsonString = JsonSerializer.Serialize(authorCreateRequestModel);
// // Convert the JSON string to StringContent 
// var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
// // send POST request to API
// var response = await httpClient.PostAsync("http://localhost:5107/api/Author", content);

// if (response.IsSuccessStatusCode)
// {
//     var responseContent = await response.Content.ReadAsStringAsync();
//     Console.WriteLine(responseContent);
// }
// else
// {
//     Console.WriteLine($"Failed to create author. Status code: {response.StatusCode}");
// }

// public class AuthorCreateRequestModel
// {
//     public string Name { get; set; } = null!;
//     public string? Bio { get; set; }
// }



// this is Put request to update existing author with id 4002

// int authorIdToUpdate = 4002;
// AuthorUpdateRequestModel updateModel = new AuthorUpdateRequestModel
// {
//     Name = "John Doe Updated",
//     Bio = "This is the updated bio for John Doe."
// };

// var jsonString = JsonSerializer.Serialize(updateModel);
// var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
// var response = await httpClient.PutAsync($"http://localhost:5107/api/Author/{authorIdToUpdate}", content);
// if(response.IsSuccessStatusCode){
//     var responseContent = await response.Content.ReadAsStringAsync();
//     Console.WriteLine(responseContent);
// }
// else
// {
//     Console.WriteLine($"Failed to update author. Status code: {response.StatusCode}");
// }

// internal class AuthorUpdateRequestModel
// {
//     public string Name { get; set; }
//     public string Bio { get; set; }
// }



int AuthorId = 4002;

var response = await httpClient.DeleteAsync($"http://localhost:5107/api/Author/{AuthorId}");

if(response.IsSuccessStatusCode)
{
    var responseContent = await response.Content.ReadAsStringAsync();
    Console.WriteLine(responseContent);
}
else
{
    Console.WriteLine($"Failed to delete author. Status code: {response.StatusCode}");
}
