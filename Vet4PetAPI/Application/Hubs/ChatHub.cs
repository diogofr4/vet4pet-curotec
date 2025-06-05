using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Application.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinAppointmentGroup(string appointmentId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, appointmentId);
        }

        public async Task SendMessageToAppointment(string appointmentId, string user, string message)
        {
            await Clients.Group(appointmentId).SendAsync("ReceiveMessage", user, message);
        }
    }
} 