using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YourMusicPlayer
{
    class NAudio
    {
        //NAudio
        public WaveOutEvent outputDevice;
        public AudioFileReader audioFile;

        //States
        public bool playing { get; set; }
        public bool stopped { get; set; }

        public bool waitingForSong { get; set; }

        //Settings
        public bool shuffleMode { get; set; }
        public bool continueMode { get; set; }

        public int volume { get; set; }

        private String nextSongPath = "";

        //StopTypes
        public enum PlaybackStopTypes
        {
            PlaybackStoppedByUser, PlaybackStoppedReachingEndOfFile
        }
        public PlaybackStopTypes PlaybackStopType { get; set; }

        public NAudio()
        {
            shuffleMode = false;
            continueMode = false;
            playing = false;
            stopped = false;
            volume = 20;
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            //Debug.Print(PlaybackStopType.ToString());
            //if ContinueMode==true and StoppedByEOF then play next song
            if (continueMode && PlaybackStopType != PlaybackStopTypes.PlaybackStoppedByUser)
            {
                //Debug.Print("CONTINUE");
                waitingForSong = true;
            }
            //else if continuePlaying and StoppedByUser
            else if (stopped && PlaybackStopType == PlaybackStopTypes.PlaybackStoppedByUser)
            {
                //Debug.Print("USER CONTINUE");
                stopped = false;

                if(!nextSongPath.Equals(""))
                {
                    playSound(nextSongPath);
                    nextSongPath = "";
                }
            }
            else if (PlaybackStopType != PlaybackStopTypes.PlaybackStoppedByUser)
            {
                //Debug.Print("STOPPED");
                stopSound();
            }
        }

        //Stop without next song
        public bool stopSound()
        {
            PlaybackStopType = PlaybackStopTypes.PlaybackStoppedByUser;
            if (playing)
            {
                outputDevice?.Dispose();
                outputDevice = null;
                try
                {
                    audioFile?.Dispose();
                }
                catch (Exception ex)
                {
                    Debug.Print("exception: " + ex.ToString());
                }
                audioFile = null;
                playing = false;
                return true;
            }
            else
            {
                outputDevice?.Stop();
                audioFile?.Dispose();
                audioFile = null;
                return false;
            }
            //setLabel("");
            //seekBar.Value = 0;
        }

        //Stop but start next song
        public bool stopSound(String filePath)
        {
            PlaybackStopType = PlaybackStopTypes.PlaybackStoppedByUser;
            if (playing)
            {
                stopped = true;
                nextSongPath = filePath;
                outputDevice?.Dispose();
                try
                {
                    audioFile?.Dispose();
                }
                catch (Exception ex)
                {
                    Debug.Print("exception: "+ex.ToString());
                }
                audioFile = null;
                return true;
            }
            else
            {
                outputDevice?.Stop();
                audioFile?.Dispose();
                audioFile = null;
                return false;
            }
            //setLabel("");
            //seekBar.Value = 0;
        }

        public bool playSound(String filePath)
        {
            if (playing)
            {
                if (outputDevice == null)
                {
                    outputDevice = new WaveOutEvent();
                    outputDevice.PlaybackStopped += OnPlaybackStopped;
                }
                if (audioFile == null)
                {
                    try
                    {
                        audioFile = new AudioFileReader(filePath);
                        outputDevice.Init(audioFile);
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Format exception, file not supported : ");
                    }
                   
                }
                try
                {
                    outputDevice.Play();
                }
                catch (InvalidOperationException ex)
                {
                    Debug.Print("playSound() InvalidOperationException : " + ex.ToString());

                }

                PlaybackStopType = PlaybackStopTypes.PlaybackStoppedReachingEndOfFile;
                return true;
            }
            return false;
        }

        public void playSoundForced(String filePath)
        {
            audioFile = new AudioFileReader(filePath);
            outputDevice.Init(audioFile);
            outputDevice.Play();
            PlaybackStopType = PlaybackStopTypes.PlaybackStoppedReachingEndOfFile;
        }

        public void getSoundInfo()
        {
            audioFile.
        }
    }
}
