using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Domain;
using Service.Interfaces;
using System;

namespace Application.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task JoinAnimalChat(int animalId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"animal_{animalId}");
        }

        public async Task LeaveAnimalChat(int animalId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"animal_{animalId}");
        }

        public async Task SendMessage(Message message)
        {
            // Salva a mensagem no banco
            var savedMessage = await _messageService.SendMessageAsync(message);

            // Envia a mensagem para todos no grupo do animal
            await Clients.Group($"animal_{message.AnimalId}").SendAsync("ReceiveMessage", savedMessage);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
} 