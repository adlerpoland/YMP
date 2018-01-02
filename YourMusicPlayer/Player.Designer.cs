namespace YourMusicPlayer
{
    partial class Player
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.prevBtn = new System.Windows.Forms.Button();
            this.nextBtn = new System.Windows.Forms.Button();
            this.playBtn = new System.Windows.Forms.Button();
            this.playList = new System.Windows.Forms.ListBox();
            this.loadBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.playLabel = new System.Windows.Forms.Label();
            this.continueBtn = new System.Windows.Forms.Button();
            this.shuffleBtn = new System.Windows.Forms.Button();
            this.timeLabel = new System.Windows.Forms.Label();
            this.seekBar = new System.Windows.Forms.TrackBar();
            this.volumeBar = new System.Windows.Forms.TrackBar();
            this.infoBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.seekBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).BeginInit();
            this.SuspendLayout();
            // 
            // prevBtn
            // 
            this.prevBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prevBtn.Location = new System.Drawing.Point(54, 319);
            this.prevBtn.Name = "prevBtn";
            this.prevBtn.Size = new System.Drawing.Size(45, 30);
            this.prevBtn.TabIndex = 8;
            this.prevBtn.Text = "<-";
            this.prevBtn.UseVisualStyleBackColor = true;
            this.prevBtn.Click += new System.EventHandler(this.prevBtn_Click);
            // 
            // nextBtn
            // 
            this.nextBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextBtn.Location = new System.Drawing.Point(237, 319);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(45, 30);
            this.nextBtn.TabIndex = 11;
            this.nextBtn.Text = "->";
            this.nextBtn.UseVisualStyleBackColor = true;
            this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);
            // 
            // playBtn
            // 
            this.playBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playBtn.Location = new System.Drawing.Point(171, 319);
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(60, 30);
            this.playBtn.TabIndex = 10;
            this.playBtn.Text = "Play";
            this.playBtn.UseVisualStyleBackColor = true;
            this.playBtn.Click += new System.EventHandler(this.playBtn_Click);
            // 
            // playList
            // 
            this.playList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playList.FormattingEnabled = true;
            this.playList.Location = new System.Drawing.Point(12, 47);
            this.playList.Name = "playList";
            this.playList.Size = new System.Drawing.Size(372, 186);
            this.playList.TabIndex = 5;
            this.playList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.playList_KeyPress);
            this.playList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseDoubleClick);
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(12, 12);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(80, 29);
            this.loadBtn.TabIndex = 1;
            this.loadBtn.Text = "LOAD";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.stopBtn.Location = new System.Drawing.Point(105, 319);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(60, 30);
            this.stopBtn.TabIndex = 9;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // playLabel
            // 
            this.playLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playLabel.Location = new System.Drawing.Point(12, 241);
            this.playLabel.Name = "playLabel";
            this.playLabel.Size = new System.Drawing.Size(321, 20);
            this.playLabel.TabIndex = 13;
            this.playLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // continueBtn
            // 
            this.continueBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.continueBtn.BackColor = System.Drawing.SystemColors.Control;
            this.continueBtn.Location = new System.Drawing.Point(355, 12);
            this.continueBtn.Name = "continueBtn";
            this.continueBtn.Size = new System.Drawing.Size(29, 29);
            this.continueBtn.TabIndex = 3;
            this.continueBtn.Text = "C";
            this.continueBtn.UseVisualStyleBackColor = false;
            this.continueBtn.Click += new System.EventHandler(this.continueBtn_Click);
            // 
            // shuffleBtn
            // 
            this.shuffleBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.shuffleBtn.Location = new System.Drawing.Point(320, 12);
            this.shuffleBtn.Name = "shuffleBtn";
            this.shuffleBtn.Size = new System.Drawing.Size(29, 29);
            this.shuffleBtn.TabIndex = 2;
            this.shuffleBtn.Text = "S";
            this.shuffleBtn.UseVisualStyleBackColor = true;
            this.shuffleBtn.Click += new System.EventHandler(this.shuffleBtn_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLabel.Location = new System.Drawing.Point(288, 324);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(45, 20);
            this.timeLabel.TabIndex = 14;
            this.timeLabel.Text = "0:00";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // seekBar
            // 
            this.seekBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.seekBar.Location = new System.Drawing.Point(15, 264);
            this.seekBar.Maximum = 1000;
            this.seekBar.Name = "seekBar";
            this.seekBar.Size = new System.Drawing.Size(318, 45);
            this.seekBar.TabIndex = 6;
            this.seekBar.MouseCaptureChanged += new System.EventHandler(this.seekBar_MouseCaptureChanged);
            this.seekBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.seekBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.seekBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // volumeBar
            // 
            this.volumeBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeBar.Location = new System.Drawing.Point(339, 239);
            this.volumeBar.Maximum = 20;
            this.volumeBar.Name = "volumeBar";
            this.volumeBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.volumeBar.Size = new System.Drawing.Size(45, 108);
            this.volumeBar.TabIndex = 12;
            this.volumeBar.Value = 20;
            this.volumeBar.Scroll += new System.EventHandler(this.changeVolume);
            // 
            // infoBtn
            // 
            this.infoBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.infoBtn.Location = new System.Drawing.Point(12, 319);
            this.infoBtn.Name = "infoBtn";
            this.infoBtn.Size = new System.Drawing.Size(35, 30);
            this.infoBtn.TabIndex = 7;
            this.infoBtn.Text = "Info";
            this.infoBtn.UseVisualStyleBackColor = true;
            this.infoBtn.Click += new System.EventHandler(this.infoBtn_Click);
            // 
            // Player
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 361);
            this.Controls.Add(this.infoBtn);
            this.Controls.Add(this.volumeBar);
            this.Controls.Add(this.seekBar);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.shuffleBtn);
            this.Controls.Add(this.continueBtn);
            this.Controls.Add(this.playLabel);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.loadBtn);
            this.Controls.Add(this.playList);
            this.Controls.Add(this.playBtn);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.prevBtn);
            this.MinimumSize = new System.Drawing.Size(412, 400);
            this.Name = "Player";
            this.Text = "Your Music Player 1";
            ((System.ComponentModel.ISupportInitialize)(this.seekBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button prevBtn;
        private System.Windows.Forms.Button nextBtn;
        private System.Windows.Forms.Button playBtn;
        private System.Windows.Forms.ListBox playList;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Label playLabel;
        private System.Windows.Forms.Button continueBtn;
        private System.Windows.Forms.Button shuffleBtn;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.TrackBar seekBar;
        private System.Windows.Forms.TrackBar volumeBar;
        private System.Windows.Forms.Button infoBtn;
    }
}

