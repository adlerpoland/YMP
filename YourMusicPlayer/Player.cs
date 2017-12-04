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
using System.Timers;

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
        bool stopped = false;

        //StopTypes
        public enum PlaybackStopTypes
        {
            PlaybackStoppedByUser, PlaybackStoppedReachingEndOfFile
        }
        public PlaybackStopTypes PlaybackStopType { get; set; }

        //Settings
        bool shuffleMode = false;
        bool continueMode = false;

        public Player()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            InitializeTimers();
        }

        private void InitializeTimers()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(updateTimeLabel);
            aTimer.Interval = 100;
            aTimer.Enabled = true;
            Debug.Print("TEST");
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
                        stopSound(true);
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
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            folderBrowser.Description = "Choose a folder containing music files";
            folderBrowser.SelectedPath = Environment.GetFolderPath((Environment.SpecialFolder.MyMusic));
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
            //Wczytanie listy plikow
            filePaths = Directory.GetFiles(@path, "*.mp3", SearchOption.AllDirectories);

            //Wczytanie ilosci plikow
            int length = filePaths.GetLength(0);
            int i;
            for (i = 0; i < length; i++)
            {
                
                ListViewItem item = new ListViewItem();
                String folder = getFilePath(filePaths[i],1);
                String filename = getFilePath(filePaths[i],2);
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

        private void updateTimeLabel(object source, ElapsedEventArgs e)
        {
            if (audioFile!=null)
            {
                int min = audioFile.CurrentTime.Minutes;
                int sec = audioFile.CurrentTime.Seconds;
                String seconds = sec.ToString();
                if (sec < 10)
                    seconds = "0" + sec.ToString();
                String txt = min.ToString() + ":" + seconds;
                timeLabel.Text = txt;
            }
            else
                timeLabel.Text = "0:00";
        }

        private void setLabel(string label)
        {
            playLabel.Text = label; 
        }

        private void playSound()
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

                PlaybackStopType = PlaybackStopTypes.PlaybackStoppedReachingEndOfFile;
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

            PlaybackStopType = PlaybackStopTypes.PlaybackStoppedReachingEndOfFile;
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            Debug.Print(PlaybackStopType.ToString());
            //if ContinueMode==true and StoppedByEOF then play next song
            if (continueMode && PlaybackStopType != PlaybackStopTypes.PlaybackStoppedByUser)
            {
                //RESET LABELS
                setLabel("");

                //Choose next song from list
                int countFiles = _files.Count;
                if (shuffleMode)
                {

                    Random ran = new Random(Guid.NewGuid().GetHashCode());
                    int random = ran.Next(0, countFiles - 1);

                    playList.SelectedIndex = random;
                    playSoundForced();
                }
                else
                {
                    if (currentIndex < countFiles - 1)
                    {
                        playList.SelectedIndex = currentIndex + 1;
                        playSoundForced();
                    }
                    else
                    {
                        playList.SelectedIndex = 0;
                        playSoundForced();
                    }
                }
            }
            //else if continuePlaying and StoppedByUser
            else if (stopped && PlaybackStopType == PlaybackStopTypes.PlaybackStoppedByUser)
            {
                stopped = false;
                playSoundForced();
            }
            else if(PlaybackStopType != PlaybackStopTypes.PlaybackStoppedByUser)
            {
                stopSound(false);
            }
        }

        private void stopSound(bool continuePlaying)
        {       
            PlaybackStopType = PlaybackStopTypes.PlaybackStoppedByUser;
            if (playing)
            {
                if (continuePlaying)
                {
                    stopped = true;
                    outputDevice?.Dispose();
                    audioFile?.Dispose();
                    audioFile = null;
                }
                else
                {
                    outputDevice?.Dispose();
                    outputDevice = null;
                    audioFile?.Dispose();
                    audioFile = null;
                    playBtn.Text = "Play";
                    playing = false;
                }
            }
            else
            {
                outputDevice?.Stop();
                audioFile?.Dispose();
                audioFile = null;
            }
            setLabel("");
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
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

        private void stopBtn_Click(object sender, EventArgs e)
        {
            stopSound(false); 
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
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
                    stopSound(true);
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
                    stopSound(true);
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

        private void continueBtn_Click(object sender, EventArgs e)
        {
            continueMode = !continueMode;
            if (continueMode)
                continueBtn.BackColor = SystemColors.Highlight;
            else
                continueBtn.BackColor = SystemColors.Control;
        }

        private void shuffleBtn_Click(object sender, EventArgs e)
        {
            shuffleMode = !shuffleMode;
            if (shuffleMode)
                shuffleBtn.BackColor = SystemColors.Highlight;
            else
                shuffleBtn.BackColor = SystemColors.Control;
        }
    }
}
