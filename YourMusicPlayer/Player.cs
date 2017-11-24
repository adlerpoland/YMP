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
using System.Threading;

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
        int currentIndex = -1;

        public Player()
        {
            InitializeComponent();
        }

        void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (sender.Equals(playList))
            {
                var list = (ListBox)sender;

                int index = list.IndexFromPoint(e.Location);

                if (index != System.Windows.Forms.ListBox.NoMatches)
                {
                    if (playing)
                    {
                        stopSound();
                        playSound();
                    }
                    else
                    {
                        outputDevice?.Stop();
                        audioFile?.Dispose();
                        audioFile = null;
                        playing = true;
                        playBtn.Text = "Play";
                        playSound();
                    }
                }

                
            }
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            Debug.Print("TEST");

            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            folderBrowser.Description = "Choose a folder containing music files";
            folderBrowser.SelectedPath = Environment.GetFolderPath((Environment.SpecialFolder.MyMusic));
            Debug.Print("FOLDER:" + folderBrowser.SelectedPath);
            folderBrowser.SelectedPath = @"C:\Users\Patryk\Music";

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
                _files = new List<string>();
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
            Debug.Print(playList.SelectedIndex.ToString());
            if (playList.SelectedIndex >= 0)
            {
                playing = !playing;
                if (playing)
                {
                    playSound();
                }
                else
                {
                    playBtn.Text = "Play";
                    outputDevice.Pause();
                }
            }
        }

        private void setLabel(string label)
        {
            playLabel.Text = label;
        }

        public void playSound()
        {
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
                    String filePath = filePaths[playList.SelectedIndex];
                    currentIndex = playList.SelectedIndex;
                    audioFile = new AudioFileReader(filePath);
                    outputDevice.Init(audioFile);
                    setLabel(getFilePath(filePath, 2));
                }
                outputDevice.Play();
            }
        }

        public void playSoundForced()
        {
            String filePath = filePaths[playList.SelectedIndex];
            currentIndex = playList.SelectedIndex;
            audioFile = new AudioFileReader(filePath);
            setLabel(getFilePath(filePath, 2));
            outputDevice.Init(audioFile);
            outputDevice.Play();
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            //outputDevice.Dispose();
            //outputDevice = null;
            //audioFile.Dispose();
            //audioFile = null;
        }

        private void stopSound()
        {
            outputDevice.Dispose();
            outputDevice = null;
            audioFile.Dispose();
            audioFile = null;
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            if(playing)
            {
                stopSound();
                playBtn.Text = "Play";
                playing = false;
            }
            else
            {
                outputDevice?.Stop();
                audioFile?.Dispose();
                audioFile = null;
            }
            setLabel("");   
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            outputDevice?.Stop();
            
            int countFiles = _files.Count;
            if (countFiles > 0)
            {
                if (playing)
                {
                    if(currentIndex < countFiles-1)
                    {
                        currentIndex++;
                        playList.SelectedIndex = currentIndex;
                    }
                    else
                    {
                        playList.SelectedIndex = 0;
                    }
                    playSoundForced();
                }
                else
                {
                    if (playList.SelectedIndex >= 0)
                    {
                        if (countFiles > playList.SelectedIndex + 1)
                            playList.SelectedIndex++;
                        else
                            playList.SelectedIndex = 0;
                    }
                    else
                    {
                        playList.SelectedIndex = 0;
                    }
                }
            }    
        }

        private void prevBtn_Click(object sender, EventArgs e)
        {
            outputDevice?.Stop();

            int countFiles = _files.Count;
            if (countFiles > 0)
            {
                if (playing)
                {
                    if (currentIndex > 0)
                    {
                        currentIndex--;
                        playList.SelectedIndex = currentIndex;
                    }
                    else
                    {
                        playList.SelectedIndex = countFiles - 1;
                    }
                    playSoundForced();
                }
                else
                {
                    if (playList.SelectedIndex > 0)
                    { 
                        playList.SelectedIndex--;
                    }
                    else
                    {
                        playList.SelectedIndex= countFiles-1;
                    }
                }
            }          
        }
    }
}
