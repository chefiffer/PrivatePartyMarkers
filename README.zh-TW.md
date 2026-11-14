# Private Party Markers

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [한국어](README.ko-KR.md) | [Русский](README.ru-RU.md) | [Português (Brasil)](README.pt-BR.md) | [Español](README.es-ES.md) | [ไทย](README.th-TH.md) | [Tiếng Việt](README.vi-VN.md) | [日本語](README.ja-JP.md) | [Deutsch](README.de-DE.md) | [Français](README.fr-FR.md)

Private Party Markers 是一個 Dalamud 外掛，可在自己和手動選擇的小隊成員腳下繪製僅本機可見的小圓點。

- 標記只在自己的客戶端中繪製。
- 不設定或修改遊戲內的目標標點與場景標點。
- 不自動選擇目標，不執行技能，也不上傳玩家身份或位置。
- 每位選中的成員可以使用 16 種預設顏色之一。
- 隊友離開小隊、不再載入或無法投影到螢幕時，立即停止繪製。
- 支援簡體中文、繁體中文、英語和日語。

## 安裝

1. 安裝並啟動 XIVLauncher，確認 Dalamud 已啟用。
2. 開啟 **Dalamud 設定 → 測試版**。
3. 新增下面的自定義外掛倉庫地址：

   ```text
   https://raw.githubusercontent.com/chefiffer/dalamud-plugins/main/repo.json
   ```

4. 開啟外掛安裝器，搜尋 **Private Party Markers** 並安裝。
5. 在遊戲聊天欄輸入 `/partydots` 開啟設定視窗。

選中的成員只會以 `名字@主伺服器ID` 的形式儲存在外掛本地配置中。分享配置檔案前請自行檢查。

## 許可證

Private Party Markers 使用 GNU Affero General Public License v3.0，完整條款請參閱 [LICENSE](LICENSE)。
