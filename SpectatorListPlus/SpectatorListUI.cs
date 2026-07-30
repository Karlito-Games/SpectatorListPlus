
using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles;
using RueI.API;
using RueI.API.Elements;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;

namespace SpectatorListPlus;

public class SpectatorListUI
{
    private static readonly Lazy<SpectatorListUI> _instance = 
        new (() => new SpectatorListUI());

    public static SpectatorListUI Instance => _instance.Value;

    private SpectatorListUI() { }
    
    Tag spectatorListTag = new();

    private List<string> GetSpectatorsString(Player player)
    {
        var spectators = player.CurrentSpectators;

        var nameList = new List<string>();

        foreach (var spectator in spectators)
        {
            nameList.Add(spectator.Nickname);
        }
        return nameList;
    }
    
    public bool HasBadgeHidden(Player player)
    {
        var serverRoles = player.ReferenceHub.serverRoles;
        
        return serverRoles.HasBadgeHidden;
    }
    
    private void UpdateSpectatorList(Player player)
    {
        RueDisplay display = RueDisplay.Get(player);
        var spectators = player.CurrentSpectators;
        var spectatorsString = "";

        foreach (var spectator in spectators)
        {
            if (HasBadgeHidden(spectator))
            {
                spectatorsString += $"{spectator.Nickname}\n";
            }
            else
            {
                spectatorsString += $"<color={spectator.GroupColor}>{spectator.Nickname}</color>\n";
            }
        }

        if (spectators.Count == 0)
        {
            return;
        }
        
        display.Show(spectatorListTag, new BasicElement(800, $"<align=right><size=60%>{SpectatorListPlugin.PluginConfig.SpectatorsLabel}</size><size=55%>{spectatorsString}</size>" ));
    }
    
    public IEnumerator<float> SpectatorListCoroutine()
    {
        while (true)
        {
            var players = Player.ReadyList.ToList();
            foreach (var player in players)
            {
                if (player.IsAlive)
                {
                    UpdateSpectatorList(player);
                }
            }
            
            yield return Timing.WaitForSeconds(1); 
        }
        
    }
}

