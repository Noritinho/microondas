using Microsoft.AspNetCore.SignalR;
using Microwave.Application.Contracts.Responses;
using Microwave.Domain.Interfaces;
using Microwave.Domain.Models.Heating;

namespace Microwave.Api.Socket;

//[Authorize]
public class HeatingHub(
    IUserContext userContext, //TODO: Fazer funcionar userCOntext
    ICache cache,
    IHttpContextAccessor httpContextAccessor) : Hub
{
    public async Task StartHeating()
    {
        var usedId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst("id")?.Value);

        var heating = cache.TryGetCachedValue<Heating>(usedId);

        if (heating == null)
            throw new InvalidOperationException("Heating not found");

        await heating.HeatingTimer.StartTimerAsync(async () =>
        {
            var response = new HeatingTimerResponse(
                (int)heating.HeatingTimer.GetElapsedTime().TotalSeconds,
                heating.Potency.Value);

            await Clients.All.SendAsync("ReceiveHeatingData", response);
        });

        await Clients.All.SendAsync("ReceiveHeatingCompleted", 
            new HeatTimerCompletedResponse(){ Message = "Completed"});

        cache.Remove(usedId);
    }
}
