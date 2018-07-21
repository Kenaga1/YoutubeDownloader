using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;

namespace YoutubeDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //textBox1.Text = "Yusuf";
            //var client = new YoutubeClient();
            //var video = await client.GetVideoAsync(textBox1.Text);

            //var title = video.Title; // "Infected Mushroom - Spitfire [Monstercat Release]"
            //var author = video.Author; // "Monstercat"
            //var duration = video.Duration; // 00:07:14 
            //textBox1.Text = $"{title} {author} {duration}";

            var client = new YoutubeClient();
            var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(textBox1.Text);

            var streamInfo = streamInfoSet.Muxed.WithHighestVideoQuality();
            var ext = streamInfo.Container.GetFileExtension();
            await client.DownloadMediaStreamAsync(streamInfo, $"downloaded_video.{ext}");
            textBox1.Text = "Bitti.";
        }
    }
}
