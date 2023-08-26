using Microsoft.AspNetCore.SignalR.Client;

namespace ConsoleFrontJoimChat
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:32772/chatHub")
                .Build();
            
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Console.WriteLine($"Received message from {user}: {message}");
            });

            try
            {
                await connection.StartAsync();
                Console.WriteLine("Connected to SignalR hub.");

                Console.ReadLine();

                await connection.StopAsync();
                Console.WriteLine("Disconnected from SignalR hub.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: " + ex);
            }
            Console.ReadKey();
        }
    }
}