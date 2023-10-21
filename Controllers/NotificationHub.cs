using Microsoft.AspNetCore.SignalR;

public class NotificationHub : Hub
{
    public async Task SendMotionAlert()
    {
        await Clients.All.SendAsync("ReceiveMotionAlert", "Motion detected!");
    }
}
