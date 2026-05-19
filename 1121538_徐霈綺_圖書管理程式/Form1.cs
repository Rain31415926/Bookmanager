using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1121538_徐霈綺_圖書管理程式
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] books = { "三國演義", "西遊記", "唐詩三百首", "楚辭", "西廂記", "水滸傳", "紅樓夢", "牡丹亭" };
            
            ImageList imgLarge = new ImageList();
            imgLarge.ImageSize = new Size(100, 140);
            imgLarge.ColorDepth = ColorDepth.Depth32Bit;

            ImageList imgSmall = new ImageList();
            imgSmall.ImageSize = new Size(32, 32);
            imgSmall.ColorDepth = ColorDepth.Depth32Bit;

            for (int i = 0; i < books.Length; i++)
            {
                // 產生空白(灰色)的大圖
                Bitmap bmpLarge = new Bitmap(100, 140);
                using (Graphics g = Graphics.FromImage(bmpLarge))
                {
                    g.Clear(Color.Gray);
                    // 畫黃色垂直文字作為書籍封面示意
                    StringFormat format = new StringFormat();
                    format.FormatFlags = StringFormatFlags.DirectionVertical;
                    using (Font f = new Font("微軟正黑體", 12))
                    using (Brush b = new SolidBrush(Color.Yellow))
                    {
                        g.DrawString(books[i], f, b, new RectangleF(70, 10, 30, 120), format);
                    }
                }
                imgLarge.Images.Add(bmpLarge);

                // 產生空白(灰色)的小圖
                Bitmap bmpSmall = new Bitmap(32, 32);
                using (Graphics g = Graphics.FromImage(bmpSmall))
                {
                    g.Clear(Color.Gray);
                }
                imgSmall.Images.Add(bmpSmall);
            }

            listViewBooks.LargeImageList = imgLarge;
            listViewBooks.SmallImageList = imgSmall;

            // 增加詳細資料的欄位
            listViewBooks.Columns.Add("書名", 150);
            listViewBooks.Columns.Add("作者", 150);
            listViewBooks.Columns.Add("出版年份", 100);

            for (int i = 0; i < books.Length; i++)
            {
                ListViewItem item = new ListViewItem(books[i], i);
                item.SubItems.Add("作者佚名");
                item.SubItems.Add("西元前~清代");
                listViewBooks.Items.Add(item);
            }

            // 預設為大圖示
            comboBoxView.SelectedIndex = 0;

            // 借書事件 (雙擊加入清單)
            listViewBooks.DoubleClick += ListViewBooks_DoubleClick;
        }

        private void ListViewBooks_DoubleClick(object sender, EventArgs e)
        {
            if (listViewBooks.SelectedItems.Count > 0)
            {
                string bookName = listViewBooks.SelectedItems[0].Text;
                listBoxBorrowed.Items.Add(bookName);
            }
        }

        private void comboBoxView_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxView.SelectedIndex)
            {
                case 0:
                    listViewBooks.View = View.LargeIcon;
                    break;
                case 1:
                    listViewBooks.View = View.Details;
                    break;
                case 2:
                    listViewBooks.View = View.SmallIcon;
                    break;
                case 3:
                    listViewBooks.View = View.List;
                    break;
                case 4:
                    listViewBooks.View = View.Tile;
                    break;
            }
        }
    }
}
