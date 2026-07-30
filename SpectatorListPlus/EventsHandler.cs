using System.Reflection;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles;
using PlayerRoles.Spectating;


namespace SpectatorListPlus;

public class EventsHandler : CustomEventsHandler
{
    private CoroutineHandle _uiCoroutine;
    
    private static readonly FieldInfo? SyncedNetIdField = typeof(SpectatorRole)
        .GetField(nameof(SpectatorRole.SyncedSpectatedNetId), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    
    public override void OnServerRoundStarted()
    {
        _uiCoroutine = Timing.RunCoroutine(SpectatorListUI.Instance.SpectatorListCoroutine());
    }

    public override void OnServerRoundEnded(RoundEndedEventArgs ev)
    {
        Timing.KillCoroutines(_uiCoroutine);
    }

    
}