using System.Collections.Generic;
using Dalamud.Configuration;

namespace PrivatePartyMarkers;

internal sealed class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 2;
    public bool Enabled { get; set; } = true;
    public bool MarkSelf { get; set; }
    public int SelfColor { get; set; }
    public string Language { get; set; } = "auto";
    public Dictionary<string, int> MarkedPlayers { get; set; } = new();
}
