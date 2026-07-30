using InventorySystem.Items;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using SCP662;

namespace SpectatorListPlus
{
    public class SpectatorListPlugin : Plugin<PluginConfig>
    {
        public static Plugin Instance { get; set; } = null!;
        public static PluginConfig PluginConfig { get; set; } = null!;
        public override string Name { get; } = "SpectatorListPlus";
        public override string Description { get; } = "Very nice spectator list :3";
        public override string Author { get; } = "Karlito";
        public override Version Version { get; } = new (1, 0, 0, 0);
        public override Version RequiredApiVersion { get; } = new (LabApiProperties.CompiledVersion);
    
        private static EventsHandler Events = new();
        
        public override void Enable()
        {
            Instance = this;
            PluginConfig = Config;
            CustomHandlersManager.RegisterEventsHandler(Events);
            SaveConfig();
        }

        public override void Disable()
        {
            Instance = null!;
            PluginConfig = null!;
        }
    
    }
    
}