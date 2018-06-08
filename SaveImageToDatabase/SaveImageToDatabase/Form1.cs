using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace SaveImageToDatabase
{
    public partial class Form1 : Form
    {
        string filename;
        List<MyPucture> list;

        public Form1()
        {
            InitializeComponent();
        }

        Image ConvertBinaryToImage(byte[] data)
        {
            using(MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.FocusedItem != null)
            {
                pictureBox.Image = ConvertBinaryToImage(list[listView1.FocusedItem.Index].Data);
                lblFilename.Text = listView1.FocusedItem.SubItems[0].Text;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog() { Filter="JPEG|*.jpg", ValidateNames=true, Multiselect=false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filename = ofd.FileName;
                    lblFilename.Text = filename;
                    pictureBox.Image = Image.FromFile(filename);
                }
            }
        }

         byte[] ConvertImageToBinary(Image img)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, Image.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
        
        private async void btnSave_Click(object sender, EventArgs e)
        {
            using(PicEntities db = new PicEntities())
            {
                MyPucture pic = new MyPucture() { FileName = filename, Data = ConvertImageToBinary(pictureBox.Image) };
                db.MyPuctures.Add(pic);
                await db.SaveChangesAsync();
                MessageBox.Show("You have been Successfuly Saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            listView1.Item.Clear();
            using (PicEntities db = new PicEntities())
            {
                list = db.MyPuctures.ToList();
                foreach(MyPucture pic in list)
                {
                    ListViewItem item = new ListViewItem(pic.FileName);
                    listView1.Items.Add(item);
                }
            }
        }
    }
}
