using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oop2561_Line_Notify
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LineNotify ln = new LineNotify();
            ln.notifyMessage(textBox1.Text);
        }
    }

    public class LineNotify
    {
        public void notifyPicture(string url)
        {
            LINENotify(" ", 0, 0, url);
        }

        public void notifySticker(int stickerID, int stickerPackageID)
        {
            LINENotify("", stickerPackageID, stickerID, "");
        }

        public void notifyMessage(string message)
        {
            LINENotify(message, 0, 0, "");
        }

        public void LINENotify(string message, int stickerPackageID, int stickerID, string pictureUrl)
        {
            string token = "eXqaDEzTQo1depPLtaDESimCDiJ9DyEcKqjrQ95Tq6x";

            var request = (HttpWebRequest)WebRequest.Create("https://notify-api.line.me/api/notify");

            var postData = string.Format("message={0}", message);
            
            // Sticker
            if (stickerPackageID > 0 && stickerID > 0)
            {
                var stickerPackageId = string.Format("stickerPackageId={0}", stickerPackageID);
                var stickerId = string.Format("stickerId={0}", stickerID);
                postData += "&" + stickerPackageId.ToString() + "&" + stickerId.ToString();
            }

            // Picture
            if (pictureUrl != "")
            {
                var imageThumbnail = string.Format("imageThumbnail={0}", pictureUrl);
                var imageFullsize = string.Format("imageFullsize={0}", pictureUrl);
                postData += "&" + imageThumbnail.ToString() + "&" + imageFullsize.ToString();
            }

            var data = Encoding.UTF8.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.Headers.Add("Authorization", "Bearer " + token);

            using (var stream = request.GetRequestStream()) stream.Write(data, 0, data.Length);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }

}
