# 智慧圖書管理與借閱系統 (Library Management System)

這是一個基於 **C# Windows Forms** 開發的互動式圖書管理程式。專案在 UI 佈局上採用彈性的響應式控制項配置，並結合 `ListView` 的多元檢視架構、動態資源載入與 GDI+ 繪圖技術，提供使用者流暢的書籍瀏覽、檢視切換與雙擊借閱功能。

## 🚀 核心功能

- **彈性響應式介面 (Responsive UI Layout)**：
  - 使用 **`SplitContainer`** 元件將視窗分為左（圖書清單）右（管理面板）兩大區塊。
  - 控制項皆設定為彈性錨定（如 `Dock = DockStyle.Fill`），支援視窗縮放與自適應拉伸，確保介面在不同解析度下完美不破版。
- **多元檢視模式切換**：
  - 整合 `ComboBox` 下拉選單，支援 `ListView` 的五種原生視圖（大圖示、詳細資料、小圖示、清單、大圖示加詳細資料）無縫即時切換。
- **動態封面生成技術 (GDI+ Custom Drawing)**：
  - **自動容錯機制**：若系統內未提供對應的書籍封面資源（如 `Book1`, `book9`），程式會啟動 GDI+ 自動繪製機制。
  - **精緻排版渲染**：開啟 `AntiAlias` 反鋸齒，並配合垂直文字格式化（`DirectionVertical`），動態產生優雅的鋼藍底、白字虛擬書籍封面。
- **直覺式借閱互動**：
  - 支援「雙擊（DoubleClick）」圖書項目直接加入右側的借書清單（`ListBox`）。

## 🖥️ 使用畫面展示
<img width="1057" height="723" alt="image" src="https://github.com/user-attachments/assets/b0d8ae99-7aa6-4b0d-b780-91ab25de95dc" />

### 📚 操作導引
1. **調整視窗大小**：可以自由拖曳視窗邊框或中間的分隔線（Splitter），左側的圖書清單會自動依比例縮放。
2. **切換檢視**：使用右上角的「檢視方式」下拉選單切換佈局。
3. **借閱書籍**：在左側清單上**雙擊滑鼠左鍵**，該書名即會自動加入右側的借書清單中。

## 🛠️ 技術實作細節

### 1. 視窗佈局控制架構
透過 Designer 原始碼可以看到，專案利用控制項的容器結構，在兼顧排版整齊度的同時保留了自由調整的彈性：
```csharp
// 建立左右分割容器，並填滿整個視窗
this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
this.splitContainer1.SplitterDistance = 799; // 初始分割左側預留 799 像素空間

// 將圖書清單填滿左側區塊
this.listViewBooks.Dock = System.Windows.Forms.DockStyle.Fill;
