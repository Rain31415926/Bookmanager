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
            string[] books = { "三國演義", "西遊記", "唐詩三百首", "楚辭", "西廂記", "水滸傳", "紅樓夢", "牡丹亭", "地獄變" };

            ImageList imgLarge = new ImageList();
            imgLarge.ImageSize = new Size(100, 140);
            imgLarge.ColorDepth = ColorDepth.Depth32Bit;

            ImageList imgSmall = new ImageList();
            imgSmall.ImageSize = new Size(32, 32);
            imgSmall.ColorDepth = ColorDepth.Depth32Bit;

            // 取得對應 Resources 的 ResourceManager
            System.Resources.ResourceManager rm = Properties.Resources.ResourceManager;

            for (int i = 0; i < books.Length; i++)
            {
                Image resImage = null;

                // 嘗試從 Resources 取得對應的圖片 (找 Book1, Book2... 或是 book9 等小寫)
                object obj = rm.GetObject("Book" + (i + 1));
                if (obj == null)
                {
                    // 嘗試小寫開頭的，因為您有一張叫做 book9
                    obj = rm.GetObject("book" + (i + 1));
                }
                resImage = (Image)obj;

                if (resImage != null)
                {
                    imgLarge.Images.Add(resImage);
                    imgSmall.Images.Add(resImage);
                }
                else
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
            }

            listViewBooks.LargeImageList = imgLarge;
            listViewBooks.SmallImageList = imgSmall;

            // 增加詳細資料的欄位
            listViewBooks.Columns.Add("書名", 150);
            listViewBooks.Columns.Add("作者", 150);
            listViewBooks.Columns.Add("出版年份", 100);

            string[] authors = { "羅貫中", "吳承恩", "孫洙(蘅塘退士)", "屈原 等", "王實甫", "施耐庵", "曹雪芹", "湯顯祖", "芥川龍之介" };
            string[] years = { "明代", "明代", "清代", "戰國時代", "元代", "明代", "清代", "明代", "大正7年" };

            for (int i = 0; i < books.Length; i++)
            {
                ListViewItem item = new ListViewItem(books[i], i);
                item.SubItems.Add(authors[i]);
                item.SubItems.Add(years[i]);
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
