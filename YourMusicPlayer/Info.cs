using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YourMusicPlayer
{
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }

        private void Info_Load(object sender, EventArgs e)
        {
        }


        public void setInfo(String[] data)
        {
            this.Text = data[0];

            ListViewItem title = new ListViewItem();
            title.Group = listView.Groups[0];
            title.Text = "Title";
            title.SubItems.Add(data[1]);

            ListViewItem JoinedPerformers = new ListViewItem();
            JoinedPerformers.Group = listView.Groups[1];
            JoinedPerformers.Text = "Performers";
            JoinedPerformers.SubItems.Add(data[2]);

            ListViewItem Album = new ListViewItem();
            Album.Group = listView.Groups[1];
            Album.Text = "Album";
            Album.SubItems.Add(data[3]);

            ListViewItem Year = new ListViewItem();
            Year.Group = listView.Groups[1];
            Year.Text = "Year";
            Year.SubItems.Add(data[4]);

            ListViewItem Track = new ListViewItem();
            Track.Group = listView.Groups[1];
            Track.Text = "Track";
            Track.SubItems.Add(data[5]);

            ListViewItem Genre = new ListViewItem();
            Genre.Group = listView.Groups[1];
            Genre.Text = "Genre";
            Genre.SubItems.Add(data[6]);

            ListViewItem Copyright = new ListViewItem();
            Copyright.Group = listView.Groups[1];
            Copyright.Text = "Copyright";
            Copyright.SubItems.Add(data[7]);

            ListViewItem Beats = new ListViewItem();
            Beats.Group = listView.Groups[2];
            Beats.Text = "Bps";
            Beats.SubItems.Add(data[8]);

            ListViewItem Duration = new ListViewItem();
            Duration.Group = listView.Groups[2];
            Duration.Text = "Duration";
            int seconds = 0;
            //Debug.Print(data[9].ToString());
            if (Int32.TryParse(data[9], out seconds))
            {
                //Debug.Print(seconds.ToString());
                int minutes = (seconds - (seconds % 60)) / 60;
                seconds = seconds % 60;
                String sec;
                if (seconds < 10)
                    sec = "0" + seconds.ToString();
                else
                    sec = seconds.ToString();
                Duration.SubItems.Add(minutes+":"+ sec);
            }
            else
                Duration.SubItems.Add("");


            listView.Items.Add(title);
            listView.Items.Add(JoinedPerformers);
            listView.Items.Add(Album);
            listView.Items.Add(Year);
            listView.Items.Add(Track);
            listView.Items.Add(Genre);
            listView.Items.Add(Copyright);
            listView.Items.Add(Beats);
            listView.Items.Add(Duration);
        }

        public class ID
        {
            public string Title { get; set; }
            public string JoinedPerformers { get; set; }
            public string Album { get; set; }
            public string Year { get; set; }
            public string Track { get; set; }
            public string FirstGenre { get; set; }
            public string Disc { get; set; }
            public string BeatsPerMinute { get; set; }
            public string Copyright { get; set; }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
