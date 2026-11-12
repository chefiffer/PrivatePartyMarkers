using System;
using System.Collections.Generic;
using Dalamud.Plugin;

namespace PrivatePartyMarkers;

internal sealed class Localization
{
    private readonly Configuration configuration;
    private readonly IDalamudPluginInterface pluginInterface;
    private static readonly Dictionary<string, string[]> Text = new()
    {
        ["title"] = ["私有队友标记", "私有隊友標記", "Private Party Markers", "プライベートPTマーカー"],
        ["enabled"] = ["启用脚下圆点", "啟用腳下圓點", "Enable floor dots", "足元ドットを有効化"],
        ["privacy"] = ["只在你的客户端绘制。不会设置游戏内标点，不会上传，也不会被其他玩家看到。", "只在你的用戶端繪製。不會設定遊戲內標點、不會上傳，也不會被其他玩家看到。", "Rendered only on your client. It does not set game markers, upload data, or appear to other players.", "自分のクライアントにのみ描画されます。ゲーム内マーカーの変更、アップロード、他プレイヤーへの表示は行いません。"],
        ["party"] = ["当前小队", "目前小隊", "Current party", "現在のパーティ"],
        ["self"] = ["自己", "自己", "Self", "自分"],
        ["markSelf"] = ["在自己脚下显示标记", "在自己腳下顯示標記", "Show a marker beneath yourself", "自分の足元にマーカーを表示"],
        ["chooseColor"] = ["选择预设颜色", "選擇預設顏色", "Choose a preset color", "プリセット色を選択"],
        ["empty"] = ["当前没有可选择的小队成员", "目前沒有可選擇的小隊成員", "No selectable party members", "選択できるパーティメンバーがいません"],
        ["language"] = ["语言", "語言", "Language", "言語"],
        ["color"] = ["颜色", "顏色", "Color", "色"],
        ["cyan"] = ["青", "青", "Cyan", "シアン"],
        ["magenta"] = ["洋红", "洋紅", "Magenta", "マゼンタ"],
        ["lime"] = ["青绿", "青綠", "Lime", "ライム"],
        ["orange"] = ["橙", "橙", "Orange", "オレンジ"]
    };
    internal static readonly string[] Codes = ["auto", "zh-CN", "zh-TW", "en", "ja"];
    internal static readonly string[] Names = ["Auto", "简体中文", "繁體中文", "English", "日本語"];

    public Localization(Configuration configuration, IDalamudPluginInterface pluginInterface)
    { this.configuration = configuration; this.pluginInterface = pluginInterface; }

    public string T(string key) => Text.TryGetValue(key, out var values) ? values[Index()] : key;
    private int Index()
    {
        var value = configuration.Language == "auto" ? pluginInterface.UiLanguage : configuration.Language;
        if (value.StartsWith("zh", StringComparison.OrdinalIgnoreCase))
            return value.Contains("TW", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
        if (value.StartsWith("ja", StringComparison.OrdinalIgnoreCase)) return 3;
        return 2;
    }
}
