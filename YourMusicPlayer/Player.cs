using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace YourMusicPlayer
{
    public partial class Player : Form
    {
        String[] filePaths;
        List<string> _files = new List<string>();

        //NAudio
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;

        //States
        bool playing = false;

        public Player()
        {
            InitializeComponent();
        }

        private void playList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                //Wczytanie folderu
                //textBox1.Text = fbd.SelectedPath;
                loadFiles(folderBrowser.SelectedPath);
                
            }
        }

        public void loadFiles(String path)
        {
            //Wyczyszczenie listy plikow
            if (filePaths != null)
            {
                playList.Items.Clear();
            }
            Debug.Print(path);
            //Wczytanie listy plikow
            filePaths = Directory.GetFiles(@path, "*.mp3", SearchOption.AllDirectories);

            //Wczytanie ilosci plikow
            int length = filePaths.GetLength(0);
            int i;
            for (i = 0; i < length; i++)
            {
                
                ListViewItem item = new ListViewItem();
                String folder = getFilePath(filePaths[i],1);
                //Debug.Print(folder);
                String filename = getFilePath(filePaths[i],2);
                Debug.Print(filename);
                if (folder != "ERROR" && filename != "ERROR")
                {
                    item.SubItems.Add(filename);
                    _files.Add(filename);
                    //playList.Items.Add(item.Text);
                }
            }
            playList.DataSource = _files;
        }

        //Use i = 1 for folderPath, i = 2 for fileName
        public String getFilePath(String path,int i)
        {
            //String pattern = @"\\([a - zA - Z0 - 9_() !@#$%^&*]*.xml)";
            String pattern = @"\\(.*)\\(.*[.]mp3)";

            MatchCollection matches = Regex.Matches(path, pattern);

            matches = Regex.Matches(path, pattern, RegexOptions.Singleline);
            string name = "ERROR";
            foreach (Match match in matches)
            {
                name = match.Groups[i].Value;
            }
            return name;
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            playing = !playing;
            if (playing)
            {
                playBtn.Text = "Pause";
                if (outputDevice == null)
                {
                    outputDevice = new WaveOutEvent();
                    outputDevice.PlaybackStopped += OnPlaybackStopped;
                }
                if (audioFile == null)
                {
                    audioFile = new AudioFileReader(@"D:\Muzyka\Dokumenty\The Weeknd - Rockin.mp3");
                    outputDevice.Init(audioFile);
                }
                outputDevice.Play();
            }
            else
            {
                playBtn.Text = "Play";
                outputDevice.Pause();
            }
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            outputDevice.Dispose();
            outputDevice = null;
            audioFile.Dispose();
            audioFile = null;
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            playBtn.Text = "Play";
            playing = false;
            outputDevice?.Stop();
        }
    }
}
