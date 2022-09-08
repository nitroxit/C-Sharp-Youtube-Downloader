using HtmlAgilityPack;
using System.Linq.Expressions;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Converter;

namespace WebThing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var youtube = new YoutubeClient();

                // You can specify both video ID or URL
                var video = await youtube.Videos.GetAsync(textBox1.Text);

                var title = video.Title;
                var author = video.Author.ChannelTitle;
                var duration = video.Duration;
                label1.Text = (title).ToString();
                label2.Text = (author).ToString();
                label3.Text = (duration).ToString();
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(textBox1.Text);
                if (checkBox1.Checked) ;
                {
                    var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                    var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
                    await youtube.Videos.DownloadAsync(textBox1.Text, title + ".mp3", o => o
                    .SetContainer("mp3") // override format
                );
                }
                if (checkBox2.Checked) ;
                {
                    var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
                    var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
                    await youtube.Videos.DownloadAsync(textBox1.Text, title + ".mp4", o => o
                    .SetContainer("mp4") // override format
                );
                }
                if (checkBox1.Checked & !checkBox2.Checked) ;
                {
                    textBox2.Text = "ONLY SELECT ONE BOX";
                }
            }
            catch
            {
                textBox2.Text = "Invalid URL";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
        }
    }
}