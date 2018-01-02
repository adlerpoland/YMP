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

        int currentIndex = -1;
        
        bool mouseDown = false;

        NAudio audioPlayer = new NAudio();

        Random ran;

        public Player()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            InitializeTimers();
            ran = new Random((int)System.DateTime.Now.Ticks);
        }

        private void InitializeTimers()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(updateTimeLabel);
            aTimer.Elapsed += new ElapsedEventHandler(checkContinueMode);
            aTimer.Interval = 500;
            aTimer.Enabled = true;
        }

        void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (sender.Equals(playList))
            {
                var list = (ListBox)sender;

                int index = list.IndexFromPoint(e.Location);

                if (index != System.Windows.Forms.ListBox.NoMatches)
                {
                    if (audioPlayer.playing)
                    {
                        String filePath = filePaths[playList.SelectedIndex];
                        currentIndex = playList.SelectedIndex;
                        audioPlayer.stopSound(filePath);
                        setLabel(getFilePath(filePath, 2));
                    }
                    else
                    {
                        audioPlayer.outputDevice?.Stop();
                        audioPlayer.audioFile?.Dispose();
                        audioPlayer.audioFile = null;
                        audioPlayer.playing = true;
                        String filePath = filePaths[playList.SelectedIndex];
                        currentIndex = playList.SelectedIndex;
                        if (audioPlayer.playSound(filePath))
                            setLabel(getFilePath(filePath, 2));
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

            filePaths = Directory.GetFiles(@path, "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".mp3") || s.EndsWith(".wav") || s.EndsWith(".aif") || s.EndsWith(".mp4") || s.EndsWith(".aac") || s.EndsWith(".wma")).ToArray();
            

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
            String pattern = @"\\(.*)\\(.*[.]*)";

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
            if (audioPlayer.audioFile != null)
            {
                //IF MOUSE IS UP AND NOT HOLDING SEEKBAR
                if (!mouseDown)
                {
                    int min = audioPlayer.audioFile.CurrentTime.Minutes;
                    int sec = audioPlayer.audioFile.CurrentTime.Seconds;
                    String seconds = sec.ToString();
                    if (sec < 10)
                        seconds = "0" + sec.ToString();
                    String txt = min.ToString() + ":" + seconds;
                    timeLabel.Text = txt;

                    //SEEKBAR
                    try
                    {
                        float length = (float)audioPlayer.audioFile.Length;
                        float position = (float)audioPlayer.audioFile.Position;
                        float value = position / length * 1000;

                        seekBar.Value = (int)value;
                    }
                    catch(NullReferenceException ex)
                    {
                        Debug.Print("Null reference" + ex.ToString());
                    }

                    
                }
            }
            else
                timeLabel.Text = "0:00";
        }

        private void checkContinueMode(object source, ElapsedEventArgs e)
        {
            if(audioPlayer.waitingForSong)
            {
                audioPlayer.waitingForSong = false;
                startNextSong();
            }
        }

        public void setLabel(string label)
        {
            playLabel.Text = label; 
            if(audioPlayer.playing)
                playBtn.Text = "Pause";
            else
                playBtn.Text = "Play";
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            if (playList.SelectedIndex >= 0)
            {
                audioPlayer.playing = !audioPlayer.playing;
                if (audioPlayer.playing)
                {
                    String filePath = filePaths[playList.SelectedIndex];
                    currentIndex = playList.SelectedIndex;
                    if(audioPlayer.playSound(filePath))
                    {
                        String name = filePath;
                        if (audioPlayer.audioFile.FileName.Equals(name))
                            setLabel(getFilePath(name,2));
                    }    
                }
                else
                {
                    playBtn.Text = "Play";
                    audioPlayer.outputDevice.Pause();
                }
            }
        }

        private void startNextSong()
        {
            //RESET LABELS
            setLabel("");

            //Choose next song from list
            int countFiles = _files.Count;
            
            if (audioPlayer.shuffleMode)
            {
                int random = ran.Next(0, countFiles);

                if(countFiles-1>1)
                {
                    while(random.Equals(playList.SelectedIndex))
                        random = ran.Next(0, countFiles);
                }
                playList.SelectedIndex = random;

                String filePath = filePaths[playList.SelectedIndex];
                currentIndex = playList.SelectedIndex;

                audioPlayer.playSoundForced(filePath);
                setLabel(getFilePath(filePath, 2));
            }
            else
            {
                if (currentIndex < countFiles - 1)
                {
                    playList.SelectedIndex = currentIndex + 1;

                    String filePath = filePaths[playList.SelectedIndex];
                    currentIndex = playList.SelectedIndex;

                    audioPlayer.playSoundForced(filePath);
                    setLabel(getFilePath(filePath, 2));
                }
                else
                {
                    playList.SelectedIndex = 0;

                    String filePath = filePaths[playList.SelectedIndex];
                    currentIndex = playList.SelectedIndex;

                    audioPlayer.playSoundForced(filePath);
                    setLabel(getFilePath(filePath, 2));
                }
            }
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            if (audioPlayer.stopSound())
            {
                playBtn.Text = "Play";
                seekBar.Value = 0;
            }
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            int countFiles = _files.Count;
            if (countFiles > 0)
            {
                if (audioPlayer.shuffleMode)
                {
                    int random = ran.Next(0, countFiles);

                    if (countFiles - 1 > 1)
                    {
                        while (random.Equals(playList.SelectedIndex))
                            random = ran.Next(0, countFiles);
                    }

                    if (audioPlayer.playing)
                    {
                        playList.SelectedIndex = random;
                        String filePath = filePaths[playList.SelectedIndex];
                        currentIndex = playList.SelectedIndex;
                        audioPlayer.stopSound(filePath);
                        setLabel(getFilePath(filePath, 2));
                    }
                    else
                    {
                        playList.SelectedIndex = random;
                    }
                }
                else
                {
                    if (audioPlayer.playing)
                    {
                        if (currentIndex < countFiles - 1)
                        {
                            currentIndex++;
                            playList.SelectedIndex = currentIndex;
                        }
                        else
                        {
                            playList.SelectedIndex = 0;
                        }
                        String filePath = filePaths[playList.SelectedIndex];
                        currentIndex = playList.SelectedIndex;
                        audioPlayer.stopSound(filePath);
                        setLabel(getFilePath(filePath, 2));
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
        }

        private void prevBtn_Click(object sender, EventArgs e)
        {
            int countFiles = _files.Count;
            if (countFiles > 0)
            {
                if (audioPlayer.playing)
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
                    String filePath = filePaths[playList.SelectedIndex];
                    currentIndex = playList.SelectedIndex;
                    audioPlayer.stopSound(filePath);
                    setLabel(getFilePath(filePath, 2));
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
            audioPlayer.continueMode = !audioPlayer.continueMode;
            if (audioPlayer.continueMode)
                continueBtn.BackColor = SystemColors.Highlight;
            else
                continueBtn.BackColor = SystemColors.Control;
        }

        private void shuffleBtn_Click(object sender, EventArgs e)
        {
            audioPlayer.shuffleMode = !audioPlayer.shuffleMode;
            if (audioPlayer.shuffleMode)
                shuffleBtn.BackColor = SystemColors.Highlight;
            else
                shuffleBtn.BackColor = SystemColors.Control;
        }

        private void seekBar_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (audioPlayer.audioFile != null)
            {
                //Seekbar current value 0-1000
                int value = seekBar.Value;

                //SEEKBAR
                float length = (float)audioPlayer.audioFile.Length;

                float percent = (float)value / 1000;

                float position = length * percent;

                //Debug.Print(length.ToString() + "/" + percent.ToString());

                audioPlayer.audioFile.Position = (long)position;

            }
            mouseDown = false;
        }

        void OnMouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        void OnMouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown && audioPlayer.audioFile != null)
            {
                //Seekbar current value 0-1000
                int value = seekBar.Value;

                float percent = (float)value / 1000;

                //Timer
                int min = audioPlayer.audioFile.TotalTime.Minutes;
                int sec = audioPlayer.audioFile.TotalTime.Seconds;

                float percentseconds = (float)(min * 60 + sec) * percent;

                sec = (int)Math.Round(percentseconds);

                min = (sec - (sec % 60)) / 60;
                sec = sec % 60;

                String seconds = sec.ToString();
                if (sec < 10)
                    seconds = "0" + sec.ToString();
                String txt = min.ToString() + ":" + seconds;
                timeLabel.Text = txt;
            }
        }

        private void changeVolume(object sender, EventArgs e)
        {
            if (audioPlayer.outputDevice != null)
            {
                try
                {
                    audioPlayer.outputDevice.Volume = volumeBar.Value / 20f;
                    audioPlayer.volume = volumeBar.Value;
                }
                catch (NullReferenceException ex)
                {
                    Debug.Print("changeVolume() exception :" + ex.ToString());
                }
            }
            else
            {
                audioPlayer.volume = volumeBar.Value;
            }
        }

        private void infoBtn_Click(object sender, EventArgs e)
        {
            if(playList.SelectedIndex >= 0)
            {
                String filePath = filePaths[playList.SelectedIndex];

                TagLib.File file = TagLib.File.Create(@filePath);

                String[] info = new string[10];
                info[0] = getFilePath(filePath, 2);
                info[1] = file.Tag.Title;
                info[2] = file.Tag.JoinedPerformers;
                info[3] = file.Tag.Album;
                info[4] = file.Tag.Year.ToString();
                info[5] = file.Tag.Track.ToString();
                info[6] = file.Tag.FirstGenre;
                info[7] = file.Tag.Copyright;
                info[8] = file.Properties.AudioBitrate.ToString();
                int seconds = (int)Math.Round(file.Properties.Duration.TotalSeconds);
                info[9] = seconds.ToString();
                
                Info infoForm = new Info();
                infoForm.setInfo(info);        

                infoForm.Show();
            }
        }

        private void playList_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ENTER KEY
            if(e.KeyChar.Equals((char)13))
            {
                if (audioPlayer.playing)
                {
                    String filePath = filePaths[playList.SelectedIndex];
                    currentIndex = playList.SelectedIndex;
                    audioPlayer.stopSound(filePath);
                    setLabel(getFilePath(filePath, 2));
                }
                else
                {
                    audioPlayer.outputDevice?.Stop();
                    audioPlayer.audioFile?.Dispose();
                    audioPlayer.audioFile = null;
                    audioPlayer.playing = true;
                    String filePath = filePaths[playList.SelectedIndex];
                    currentIndex = playList.SelectedIndex;
                    if (audioPlayer.playSound(filePath))
                        setLabel(getFilePath(filePath, 2));
                }
            }
            //DELETE KEY
            else if(e.KeyChar.Equals((Char)Keys.Delete))
            {
                // TO ADD REMOVE OPTION LATER
                // REMOVE FROM filePaths
                // REMOVE FROM list
            }
        }
    }
}
