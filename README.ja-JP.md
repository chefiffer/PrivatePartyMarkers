# Private Party Markers

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [한국어](README.ko-KR.md) | [Русский](README.ru-RU.md) | [Português (Brasil)](README.pt-BR.md) | [Español](README.es-ES.md) | [ไทย](README.th-TH.md) | [Tiếng Việt](README.vi-VN.md) | **日本語** | [Deutsch](README.de-DE.md) | [Français](README.fr-FR.md)

Private Party Markers は、自分と手動で選択したパーティメンバーの足元に、小さなプライベート用の床面ドットを描画する Dalamud プラグインです。

- マーカーは自分のクライアント上だけに描画されます。
- ゲーム内のターゲットマーカーやフィールドマーカーを設定・変更しません。
- ターゲット選択やアクションを自動化せず、プレイヤーの身元や位置をアップロードしません。
- 選択したメンバーごとに、16 色のプリセットから 1 色を使用できます。
- メンバーがパーティから離脱した、読み込まれなくなった、または画面上に投影できない場合、描画を停止します。
- 簡体字中国語、繁体字中国語、英語、日本語に対応しています。

## インストール

1. Dalamud を有効にした XIVLauncher をインストールして起動します。
2. **Dalamud Settings → Experimental** を開きます。
3. 次のカスタムリポジトリ URL を追加します。

   ```text
   https://raw.githubusercontent.com/chefiffer/dalamud-plugins/main/repo.json
   ```

4. プラグインインストーラーを開き、**Private Party Markers** を検索して **Install** を選択します。
5. ゲーム内チャットで `/partydots` を実行して設定画面を開きます。

選択したメンバーは、`name@home-world-ID` の形式でプラグインのローカル設定にのみ保存されます。設定ファイルを共有する前に内容を確認してください。

## ライセンス

Private Party Markers は GNU Affero General Public License v3.0 でライセンスされています。詳細は [LICENSE](LICENSE) を参照してください。
