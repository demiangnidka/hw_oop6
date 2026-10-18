using System.Text.Json;
using System.Text.Json.Serialization;

public class PublishingHouse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Adress { get; set; }

    public PublishingHouse(int id, string name, string adress)
    {
        Id = id;
        Name = name;
        Adress = adress;
    }
}

public class Book
{
    public int PublishingHouseId { get; set; }
    public string Title { get; set; }
    public PublishingHouse PublishingHouse { get; set; }

    public Book(int publishingHouseId, string title, PublishingHouse publishingHouse)
    {
        PublishingHouseId = publishingHouseId;
        Title = title;
        PublishingHouse = publishingHouse;
    }
}

partial class Program
{
    static async Task Main(string[] args)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };
        string path = "book.json";

        using (FileStream fstream = new FileStream(path, FileMode.Open))
        {
            var books = await JsonSerializer.DeserializeAsync<List<Book>>(fstream);

            foreach (var book in books)
            {
                Console.WriteLine($"{book.PublishingHouseId} – {book.Title} – \n Publisher: [ID: {book.PublishingHouse?.Id}, Name: {book.PublishingHouse?.Name}, {book.PublishingHouse?.Adress}]");
            }
        }
    }
}



//2. Як необхідно змінити опис класів, щоб не серіалізувати в подальшому параметр PublishingHouseId ?
//3. Як необхідно змінити опис класів, щоб серіалізований об’єкт містив параметр Title з назвою “Name” ?


/*     
      2. Потрібно додати в класі Book [JsonIgnore] над PublishingHouseId:
      
            [JsonIgnore] 
            public int PublishingHouseId { get; set; } 
        
        
      3. Потрібно додати в класі Book [JsonPropertyName("Name")] над  public string Title:
            [JsonPropertyName("Name")]
            public string Title { get; set; }
 
 */
