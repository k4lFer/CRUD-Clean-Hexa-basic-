using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Communication
{
    //[Authorize]
    public class NotificationHub : Hub
    {
        // Metodo para extraer el Role del usuario conectado (JWT) EN UNA LISTA DE ROLES
       /* public List<string> GetRoles()
        {
            var roles = Context.User.Claims.Where(x => x.Type == "Role").Select(x => x.Value).ToList();
            return roles;
        }*/
        // Metodo para extraer el Role del usuario conectado (JWT) EN UN STRING
        // public string? GetRole() => Context.User.Claims.FirstOrDefault(x => x.Type == "Role")?.Value;
        // Método para unirse a un grupo
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        // Método para salir de un grupo
        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
