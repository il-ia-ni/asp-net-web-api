using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebSocketsTutorial.Controllers
{
    [ApiController]
    [Route("[controller")]
    public class WebSocketsController : Controller
    {
        private readonly ILogger<WebSocketsController> _logger;

        // Add the logger for category WebSocketsController to the constructor of the Controller to activate logger using dependency injection
        public WebSocketsController(ILogger<WebSocketsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/ws")]  // Add a new route called ws/
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)  // Checks if there was a handshake initiated to upgrade the HTTP connection to a WebSocket and the current request is via WebSockets
                                                            // HttpContext.WebSockets gets an object that manages the establishment of WebSocket connections for a request of the executing HTTP-action. 
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();  // Wait until client initiates a request and transits the request to a WebSocket connection returning a WebSocket-Task 
                _logger.Log(LogLevel.Information, "WebSocket connection established");
                await Echo(webSocket);  // calls the async class method for client-server 2-side communication's logic IF the request of the action established a WebSocket connection
            }
            else
            {
                HttpContext.Response.StatusCode = 400;  // Gets the HttpResponse object for a request of the HTTP-action. Sends an error code 400 BAD REQUEST
            }
        }

        private async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4]; //
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            _logger.Log(LogLevel.Information, "Message received from Client");  // receives data over the webSocket and splits it into the delimited array of byte structs of the specified size. Keeps the connection open

            while (!result.CloseStatus.HasValue)  // Keeps communication running as long as the connection is alive
            {
                
                var serverMsg = Encoding.UTF8.GetBytes($"Server: Hello! You said: {Encoding.UTF8.GetString(buffer)}");  // Encodes in UTF8 format a string message of a server response that contains a decoded message currently contained in the buffer received by the server from a client

                await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);  // Sends the generated message to the client. Keeps the connection open
                _logger.Log(LogLevel.Information, "Server sent a message to Client");

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);  // Waits for receiving a new data from a client over the WebSocket connection and overwrites the method variable with the new data. Keeps the connection open
                _logger.Log(LogLevel.Information, "Message received from Client");
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);  // Closes the WebSocket connection using the close handshake and returns a task object representing the asynchronous operation with the reason and description of closing the connection
            _logger.Log(LogLevel.Information, "WebSocket connection closed");
        }
    }
}
