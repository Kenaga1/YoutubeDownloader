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
        MediaStreamInfoSet streamInfoSet;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            comboBox1.Enabled = false;
            //textBox1.Text = "Yusuf";
            //var client = new YoutubeClient();
            //var video = await client.GetVideoAsync(textBox1.Text);

            //var title = video.Title; // "Infected Mushroom - Spitfire [Monstercat Release]"
            //var author = video.Author; // "Monstercat"
            //var duration = video.Duration; // 00:07:14 
            //textBox1.Text = $"{title} {author} {duration}";

            var client = new YoutubeClient();
            //var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(textBox1.Text);

            //var streamInfo = streamInfoSet.Muxed.WithHighestVideoQuality();

            var streamInfo = streamInfoSet.Muxed[comboBox1.SelectedIndex];

            var ext = streamInfo.Container.GetFileExtension();
            await client.DownloadMediaStreamAsync(streamInfo, $"downloaded_video.{ext}");
            MessageBox.Show("Download Completed!");
            button1.Enabled = true;
            button2.Enabled = true;
            comboBox1.Enabled = true;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            var url = textBox1.Text;
            var id = "";
            if (url.Contains("youtube.com") || url.Contains("youtu.be"))
            {
                id = YoutubeClient.ParseVideoId(url); // "bnsUkE8i0tU"
            } else
            {
                id = url;
            }

            var client = new YoutubeClient();
            var video = await client.GetVideoAsync(id);

            streamInfoSet = await client.GetVideoMediaStreamInfosAsync(id);
            comboBox1.Items.Clear();
            foreach (var v in streamInfoSet.Muxed)
            {
                comboBox1.Items.Add($"Video: {v.VideoQualityLabel} {v.Size} {v.VideoEncoding} {v.Container.GetFileExtension()}");
            }
            //foreach (var a in streamInfoSet.Audio)
            //{
            //    comboBox1.Items.Add($"Audio: {a.AudioEncoding} {a.Size}");
            //}
            if (comboBox1.Items.Count > 0) {
                comboBox1.SelectedIndex = 0;
            }
            button2.Enabled = true;
        }
    }
}
