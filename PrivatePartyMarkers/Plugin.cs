using System;
using System.Linq;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace PrivatePartyMarkers;

public sealed class Plugin : IDalamudPlugin
{
    [PluginService] private static IDalamudPluginInterface PluginInterface { get; set; } = null!;
    [PluginService] private static ICommandManager Commands { get; set; } = null!;
    [PluginService] private static IPartyList Party { get; set; } = null!;
    [PluginService] private static IGameGui GameGui { get; set; } = null!;
    [PluginService] private static IObjectTable ObjectTable { get; set; } = null!;

    private readonly Configuration configuration;
    private readonly Localization i18n;
    private readonly WindowSystem windows = new("PrivatePartyMarkers");
    private readonly SettingsWindow settings;
    private readonly OverlayWindow overlay;

    public Plugin()
    {
        configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        if(configuration.Version<2)
        {
            configuration.Version=2;
            PluginInterface.SavePluginConfig(configuration);
        }
        i18n = new Localization(configuration, PluginInterface);
        settings = new SettingsWindow(configuration, i18n, Party, ObjectTable, Save);
        overlay = new OverlayWindow(configuration, Party, ObjectTable, GameGui);
        windows.AddWindow(settings); windows.AddWindow(overlay);
        PluginInterface.UiBuilder.Draw += windows.Draw;
        PluginInterface.UiBuilder.OpenMainUi += Toggle;
        PluginInterface.UiBuilder.OpenConfigUi += Toggle;
        Commands.AddHandler("/partydots", new CommandInfo((_, _) => Toggle())
        { HelpMessage = "Open local-only party floor-dot settings." });
    }

    private void Toggle() => settings.Toggle();
    private void Save() => PluginInterface.SavePluginConfig(configuration);
    public void Dispose()
    {
        Commands.RemoveHandler("/partydots");
        PluginInterface.UiBuilder.Draw -= windows.Draw;
        PluginInterface.UiBuilder.OpenMainUi -= Toggle;
        PluginInterface.UiBuilder.OpenConfigUi -= Toggle;
        windows.RemoveAllWindows();
    }
}

internal sealed class SettingsWindow : Window
{
    private readonly Configuration config; private readonly Localization i18n; private readonly IPartyList party;
    private readonly IObjectTable objectTable; private readonly Action save;
    public SettingsWindow(Configuration config, Localization i18n, IPartyList party,
        IObjectTable objectTable, Action save)
        : base("Private Party Markers###PrivatePartyMarkersSettings")
    { this.config=config; this.i18n=i18n; this.party=party; this.objectTable=objectTable; this.save=save; Size=new Vector2(620,520); SizeCondition=ImGuiCond.FirstUseEver; }
    public override void Draw()
    {
        WindowName=i18n.T("title")+"###PrivatePartyMarkersSettings";
        var enabled=config.Enabled; if(ImGui.Checkbox(i18n.T("enabled"),ref enabled)){config.Enabled=enabled;save();}
        ImGui.TextWrapped(i18n.T("privacy"));
        var li=Array.IndexOf(Localization.Codes,config.Language); if(li<0)li=0;
        if(ImGui.Combo(i18n.T("language"),ref li,string.Join('\0',Localization.Names)+"\0")){config.Language=Localization.Codes[li];save();}
        ImGui.Separator(); ImGui.TextUnformatted(i18n.T("self"));
        var markSelf=config.MarkSelf;
        if(ImGui.Checkbox(i18n.T("markSelf"),ref markSelf)){config.MarkSelf=markSelf;save();}
        if(markSelf)
        {
            ImGui.SameLine();
            DrawPalette("self", config.SelfColor, selected => { config.SelfColor=selected; save(); });
        }
        ImGui.Separator(); ImGui.TextUnformatted(i18n.T("party"));
        var any=false;
        foreach(var member in PluginParty.Snapshot(party, objectTable.LocalPlayer?.EntityId ?? 0))
        {
            any=true; var key=member.Key; var marked=config.MarkedPlayers.TryGetValue(key,out var color);
            if(ImGui.Checkbox($"{member.Display}##mark-{key}",ref marked))
            { if(marked)config.MarkedPlayers[key]=PartyMarkerPalette.Clamp(color); else config.MarkedPlayers.Remove(key); save(); }
            if(!marked)continue;
            ImGui.SameLine();
            DrawPalette(key, color, selected => { config.MarkedPlayers[key]=selected; save(); });
        }
        if(!any)ImGui.TextDisabled(i18n.T("empty"));
    }

    private void DrawPalette(string id, int selected, Action<int> select)
    {
        selected=PartyMarkerPalette.Clamp(selected);
        ImGui.PushID($"palette-{id}");
        if(ImGui.ColorButton("##current", PartyMarkerPalette.Colors[selected])) ImGui.OpenPopup("colors");
        ImGui.SameLine(0,5); ImGui.TextUnformatted(PartyMarkerPalette.Hex[selected]);
        if(ImGui.BeginPopup("colors"))
        {
            ImGui.TextDisabled(i18n.T("chooseColor"));
            for(var i=0;i<PartyMarkerPalette.Colors.Length;i++)
            {
                ImGui.PushID(i);
                if(ImGui.ColorButton("##swatch", PartyMarkerPalette.Colors[i],
                    ImGuiColorEditFlags.NoTooltip, new Vector2(28,28)))
                { select(i); ImGui.CloseCurrentPopup(); }
                if(ImGui.IsItemHovered()) ImGui.SetTooltip(PartyMarkerPalette.Hex[i]);
                ImGui.PopID();
                if(i%8!=7)ImGui.SameLine(0,5);
            }
            ImGui.EndPopup();
        }
        ImGui.PopID();
    }
}

internal sealed class OverlayWindow : Window
{
    private readonly Configuration config; private readonly IPartyList party; private readonly IObjectTable objectTable;
    private readonly IGameGui gameGui;
    public OverlayWindow(Configuration config, IPartyList party, IObjectTable objectTable, IGameGui gameGui)
        : base("###PrivatePartyMarkersOverlay", ImGuiWindowFlags.NoDecoration|ImGuiWindowFlags.NoInputs|
              ImGuiWindowFlags.NoBackground|ImGuiWindowFlags.NoSavedSettings|ImGuiWindowFlags.NoFocusOnAppearing|ImGuiWindowFlags.NoNav)
    { this.config=config;this.party=party;this.objectTable=objectTable;this.gameGui=gameGui;IsOpen=true;RespectCloseHotkey=false; }
    public override void PreDraw()
    { var vp=ImGui.GetMainViewport();ImGui.SetNextWindowPos(vp.Pos);ImGui.SetNextWindowSize(vp.Size); }
    public override void Draw()
    {
        IsOpen=true;if(!config.Enabled)return;var draw=ImGui.GetForegroundDrawList();
        var local=objectTable.LocalPlayer;
        if(config.MarkSelf && local!=null) DrawMarker(draw, local.Position, config.SelfColor);
        foreach(var member in party)
        {
            if(member?.GameObject==null)continue;var key=PluginParty.Key(member.Name.TextValue,(ushort)member.World.RowId);
            if(local!=null && member.EntityId==local.EntityId)continue;
            if(!config.MarkedPlayers.TryGetValue(key,out var index))continue;
            DrawMarker(draw, member.GameObject.Position, index);
        }
    }

    private void DrawMarker(ImDrawListPtr draw, Vector3 position, int index)
    {
        if(!gameGui.WorldToScreen(position,out var point))return;
        var value=PartyMarkerPalette.Colors[PartyMarkerPalette.Clamp(index)]; value.W=.92f;
        var color=ImGui.ColorConvertFloat4ToU32(value);
        draw.AddCircleFilled(point,6.5f,color,20);draw.AddCircle(point,10f,color,24,2f);
    }
}

internal static class PartyMarkerPalette
{
    // The first four entries retain the v1 palette indices for configuration compatibility.
    internal static readonly Vector4[] Colors =
    [
        Rgb(0x00FFFF), Rgb(0xFF4DA6), Rgb(0x7CFC00), Rgb(0xFF8C00),
        Rgb(0xFF3B30), Rgb(0xFFD60A), Rgb(0x0A84FF), Rgb(0xBF5AF2),
        Rgb(0x30D158), Rgb(0x64D2FF), Rgb(0xFF375F), Rgb(0xFFFFFF),
        Rgb(0xFFD700), Rgb(0xFF7F50), Rgb(0x8A2BE2), Rgb(0xA0A0A0)
    ];
    internal static readonly string[] Hex =
    [
        "#00FFFF", "#FF4DA6", "#7CFC00", "#FF8C00",
        "#FF3B30", "#FFD60A", "#0A84FF", "#BF5AF2",
        "#30D158", "#64D2FF", "#FF375F", "#FFFFFF",
        "#FFD700", "#FF7F50", "#8A2BE2", "#A0A0A0"
    ];
    internal static int Clamp(int value)=>Math.Clamp(value,0,Colors.Length-1);
    private static Vector4 Rgb(uint value)=>new(((value>>16)&255)/255f,((value>>8)&255)/255f,(value&255)/255f,1f);
}

internal static class PluginParty
{
    internal sealed record Row(string Key,string Display);
    internal static Row[] Snapshot(IPartyList party,uint localEntityId)=>party
        .Where(x=>x!=null && (localEntityId==0 || x.EntityId!=localEntityId)).Select(x=>
        new Row(Key(x!.Name.TextValue,(ushort)x.World.RowId),$"{x.Name.TextValue}@{x.World.Value.Name}" )).ToArray();
    internal static string Key(string name,ushort world)=>$"{name}@{world}";
}
