using System;
using System.Diagnostics;
using System.Net.Http;

class Program
{
    static void Main()
    {
        var urls = new[]
        {
            "https://jsonplaceholder.typicode.com/posts/1",
            "https://jsonplaceholder.typicode.com/users/1",
            "https://jsonplaceholder.typicode.com/todos/1"
        };

        var stopwatch = Stopwatch.StartNew();

        using var client = new HttpClient();

        foreach (var url in urls)
        {
            try
            {
                var response = client.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Ошибка запроса к {url}: {response.StatusCode}");
                    return;
                }

                var json = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Ответ от {url}:\n{json}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при запросе к {url}: {ex.Message}");
                return;
            }
        }

        stopwatch.Stop();
        Console.WriteLine($"Время выполнения: {stopwatch.Elapsed.TotalSeconds:F2} сек.");
    }
}
