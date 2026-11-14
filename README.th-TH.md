# Private Party Markers

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [한국어](README.ko-KR.md) | [Русский](README.ru-RU.md) | [Português (Brasil)](README.pt-BR.md) | [Español](README.es-ES.md) | **ไทย** | [Tiếng Việt](README.vi-VN.md) | [日本語](README.ja-JP.md) | [Deutsch](README.de-DE.md) | [Français](README.fr-FR.md)

Private Party Markers เป็นปลั๊กอิน Dalamud ที่วาดจุดเล็ก ๆ บนพื้นแบบส่วนตัวใต้ตัวคุณและสมาชิกปาร์ตี้ที่เลือก

- เครื่องหมายจะแสดงเฉพาะบนไคลเอนต์ของคุณ
- ไม่ตั้งค่าหรือแก้ไขเครื่องหมายเป้าหมายหรือเครื่องหมายภาคสนามในเกม
- ไม่ทำให้การเลือกเป้าหมายหรือการใช้แอ็กชันเป็นอัตโนมัติ และไม่อัปโหลดตัวตนหรือตำแหน่งของผู้เล่น
- สมาชิกที่เลือกแต่ละคนสามารถใช้หนึ่งในสีที่กำหนดไว้ล่วงหน้า 16 สี
- หยุดวาดทันทีเมื่อสมาชิกออกจากปาร์ตี้ ไม่ได้ถูกโหลดอีกต่อไป หรือไม่สามารถฉายตำแหน่งบนหน้าจอได้
- รองรับภาษาจีนตัวย่อ ภาษาจีนตัวเต็ม ภาษาอังกฤษ และภาษาญี่ปุ่น

## การติดตั้ง

1. ติดตั้งและเปิด XIVLauncher โดยเปิดใช้งาน Dalamud
2. เปิด **Dalamud Settings → Experimental**
3. เพิ่ม URL คลังปลั๊กอินแบบกำหนดเองต่อไปนี้:

   ```text
   https://raw.githubusercontent.com/chefiffer/dalamud-plugins/main/repo.json
   ```

4. เปิด Plugin Installer ค้นหา **Private Party Markers** แล้วเลือก **Install**
5. ใช้ `/partydots` ในแชตเกมเพื่อเปิดหน้าต่างตั้งค่า

สมาชิกที่เลือกจะถูกบันทึกไว้เฉพาะในการตั้งค่าภายในเครื่องของปลั๊กอินในรูปแบบ `name@home-world-ID` โปรดตรวจสอบไฟล์ตั้งค่าก่อนแบ่งปัน

## สัญญาอนุญาต

Private Party Markers เผยแพร่ภายใต้ GNU Affero General Public License v3.0 ดู [LICENSE](LICENSE)
