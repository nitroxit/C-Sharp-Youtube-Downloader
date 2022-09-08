using System.Linq.Expressions;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Converter;
using YoutubeExplode.Common;
using System.Windows.Forms;

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

                // You can specify both video ID or URL "https://www.youtube.com/watch?v=UmioPP3WD0w"
                var video = await youtube.Videos.GetAsync(textBox1.Text);

                var title = video.Title;
                var author = video.Author.ChannelTitle;
                var duration = video.Duration;
                label4.Text = (title).ToString();
                label5.Text = (author).ToString();
                label6.Text = (duration).ToString();
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(textBox1.Text);
                var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
                var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
                await youtube.Videos.DownloadAsync(textBox1.Text, "video.mp4", o => o
                .SetContainer("mp4") // override format
                .SetPreset(ConversionPreset.UltraFast) // change preset
                .SetFFmpegPath(@"C:\ffmpeg\bin\ffmpeg.exe")); // custom FFmpeg location
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Invalid Link/ID. Click Help for more info.");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var youtube = new YoutubeClient();
                var video = await youtube.Videos.GetAsync(textBox1.Text);
                var title = video.Title;
                var author = video.Author.ChannelTitle;
                var duration = video.Duration;
                label4.Text = (title).ToString();
                label5.Text = (author).ToString();
                label6.Text = (duration).ToString();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Invalid Link/ID. Click Help for more info.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Download - Downloads the video + Sounds\n\n Download Audio Only - Should be self explanatory\n\n The Check mark grabs the video Metadata and displays it for you\n\nALL VIDEOS ARE SAVED TO THE DIRECTORY THAT THIS APP IS IN");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var youtube = new YoutubeClient();

            // You can specify both video ID or URL "https://www.youtube.com/watch?v=UmioPP3WD0w"
            var video = await youtube.Videos.GetAsync(textBox1.Text);

            var title = video.Title;
            var author = video.Author.ChannelTitle;
            var duration = video.Duration;
            label4.Text = (title).ToString();
            label5.Text = (author).ToString();
            label6.Text = (duration).ToString();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(textBox1.Text);
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
            var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
            await youtube.Videos.DownloadAsync(textBox1.Text, "video." + comboBox1.Text, o => o
            .SetContainer(comboBox1.Text) // override format
            .SetPreset(ConversionPreset.UltraFast) // change preset
            .SetFFmpegPath(@"C:\ffmpeg\bin\ffmpeg.exe")); // custom FFmpeg location
        }
    }
}