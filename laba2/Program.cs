using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var urls = new[]
        {
            "https://jsonplaceholder.typicode.com/posts/1",
            "https://jsonplaceholder.typicode.com/users/1",
            "https://jsonplaceholder.typicode.com/todos/1"
        };

        var stopwatch = Stopwatch.StartNew();

        using var client = new HttpClient();
        var tasks = new Task<string>[urls.Length];

        for (int i = 0; i < urls.Length; i++)
        {
            var url = urls[i];
            tasks[i] = GetJsonAsync(client, url);
        }

        try
        {
            var results = await Task.WhenAll(tasks);

            for (int i = 0; i < urls.Length; i++)
            {
                Console.WriteLine($"Ответ от {urls[i]}:\n{results[i]}\n");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            return;
        }

        stopwatch.Stop();
        Console.WriteLine($"Время выполнения: {stopwatch.Elapsed.TotalSeconds:F2} сек.");
    }

    static async Task<string> GetJsonAsync(HttpClient client, string url)
    {
        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Ошибка запроса к {url}: {response.StatusCode}");

        return await response.Content.ReadAsStringAsync();
    }
}
