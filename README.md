# Private Party Markers

**English** | [简体中文](README.zh-CN.md)

Private Party Markers is a Dalamud plugin that draws small, private floor dots beneath you and selected party members.

- Markers are rendered only on your client.
- It does not set or modify in-game target or field markers.
- It does not automate targeting or actions and does not upload player identities or positions.
- Each selected member can use one of 16 preset colors.
- Markers stop drawing when a member leaves the party, is no longer loaded, or cannot be projected on screen.
- Simplified Chinese, Traditional Chinese, English, and Japanese are supported.

## Installation

1. Install and start XIVLauncher with Dalamud enabled.
2. Open **Dalamud Settings → Experimental**.
3. Add this custom repository URL:

   ```text
   https://raw.githubusercontent.com/chefiffer/dalamud-plugins/main/repo.json
   ```

4. Open the Plugin Installer, search for **Private Party Markers**, and select **Install**.
5. Use `/partydots` in game chat to open the settings window.

Selected members are stored only in the plugin's local configuration as `name@home-world-ID`. Review configuration files before sharing them.

## License

Private Party Markers is licensed under the GNU Affero General Public License v3.0. See [LICENSE](LICENSE).
