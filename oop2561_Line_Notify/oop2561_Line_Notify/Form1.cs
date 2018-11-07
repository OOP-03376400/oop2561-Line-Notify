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
            ln.LINENotify(textBox1.Text, textBox2.Text);
        }
    }

    public class LineNotify
    {
        public string Status { get; set; }

        public HttpWebResponse LINENotify(string message, string Linetoken)
        {
            HttpWebRequest httpwebrequest = WebRequest.Create("https://notify-api.line.me/api/notify") as HttpWebRequest;
            httpwebrequest.KeepAlive = true;
            httpwebrequest.ContentType = "application/x-www-form-urlencoded";
            httpwebrequest.AllowAutoRedirect = true;
            httpwebrequest.Referer = "https://notify-api.line.me";
            httpwebrequest.Host = "notify-api.line.me";
            httpwebrequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            httpwebrequest.Accept = "application/json, text/javascript, */*; q=0.01";
            httpwebrequest.CookieContainer = new CookieContainer();
            httpwebrequest.Method = "POST";

            httpwebrequest.Headers["Authorization"] = "Bearer " + Linetoken + "";

            string postdata = "message=" + message;
            bool issupport = httpwebrequest.SupportsCookieContainer;
            byte[] dataBytes = UTF8Encoding.UTF8.GetBytes(postdata);
            httpwebrequest.ContentLength = dataBytes.Length;
            using (Stream postStream3 = httpwebrequest.GetRequestStream())
            {
                postStream3.Write(dataBytes, 0, dataBytes.Length);
            }
            HttpWebResponse httpResponse = httpwebrequest.GetResponse() as HttpWebResponse;
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {

            }
            else
            {

            }
            return httpResponse;
        }
    }

}
