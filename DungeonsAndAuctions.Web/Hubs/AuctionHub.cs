using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace DNDA.Web.Hubs
{
    public class AuctionHub : Hub
    {
        // auctionId -> (connectionId -> ActiveUser)
        private static readonly ConcurrentDictionary<int, ConcurrentDictionary<string, ActiveUser>> _activeUsers
            = new();

        public async Task JoinAuction(string auctionId, int userId, string userName)
        {
            if (!int.TryParse(auctionId, out var aid)) return;

            var dict = _activeUsers.GetOrAdd(aid, _ => new ConcurrentDictionary<string, ActiveUser>());
            dict[Context.ConnectionId] = new ActiveUser { UserId = userId, UserName = userName ?? "—" };

            await Groups.AddToGroupAsync(Context.ConnectionId, $"auction-{aid}");
            await BroadcastActiveUsers(aid);
        }

        public async Task LeaveAuction(string auctionId)
        {
            if (!int.TryParse(auctionId, out var aid)) return;

            if (_activeUsers.TryGetValue(aid, out var dict))
            {
                dict.TryRemove(Context.ConnectionId, out _);
                if (dict.IsEmpty)
                    _activeUsers.TryRemove(aid, out _);
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"auction-{aid}");
            await BroadcastActiveUsers(aid);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // Remover la conexión de todas las subastas
            var keys = _activeUsers.Keys.ToList();
            foreach (var aid in keys)
            {
                if (_activeUsers.TryGetValue(aid, out var dict))
                {
                    if (dict.TryRemove(Context.ConnectionId, out _))
                    {
                        await BroadcastActiveUsers(aid);
                    }
                    if (dict.IsEmpty)
                        _activeUsers.TryRemove(aid, out _);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        private Task BroadcastActiveUsers(int auctionId)
        {
            if (_activeUsers.TryGetValue(auctionId, out var dict))
            {
                // Unificar por userId (varias conexiones -> 1 usuario)
                var users = dict.Values
                    .GroupBy(u => u.UserId)
                    .Select(g => new { userId = g.Key, userName = g.First().UserName })
                    .ToList();

                return Clients.Group($"auction-{auctionId}").SendAsync("ActiveUsersUpdated", users);
            }

            return Clients.Group($"auction-{auctionId}").SendAsync("ActiveUsersUpdated", Array.Empty<object>());
        }

        private record ActiveUser
        {
            public int UserId { get; init; }
            public string UserName { get; init; } = string.Empty;
        }
    }
}