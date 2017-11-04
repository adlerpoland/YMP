﻿namespace YourMusicPlayer
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
            this.SuspendLayout();
            // 
            // prevBtn
            // 
            this.prevBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prevBtn.Location = new System.Drawing.Point(12, 222);
            this.prevBtn.Name = "prevBtn";
            this.prevBtn.Size = new System.Drawing.Size(80, 29);
            this.prevBtn.TabIndex = 0;
            this.prevBtn.Text = "<-";
            this.prevBtn.UseVisualStyleBackColor = true;
            // 
            // nextBtn
            // 
            this.nextBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextBtn.Location = new System.Drawing.Point(183, 222);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(80, 29);
            this.nextBtn.TabIndex = 1;
            this.nextBtn.Text = "->";
            this.nextBtn.UseVisualStyleBackColor = true;
            // 
            // playBtn
            // 
            this.playBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playBtn.Location = new System.Drawing.Point(98, 222);
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(80, 29);
            this.playBtn.TabIndex = 2;
            this.playBtn.Text = "PAUSE";
            this.playBtn.UseVisualStyleBackColor = true;
            // 
            // playList
            // 
            this.playList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playList.FormattingEnabled = true;
            this.playList.Location = new System.Drawing.Point(12, 47);
            this.playList.Name = "playList";
            this.playList.Size = new System.Drawing.Size(251, 160);
            this.playList.TabIndex = 3;
            this.playList.SelectedIndexChanged += new System.EventHandler(this.playList_SelectedIndexChanged);
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(12, 12);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(80, 29);
            this.loadBtn.TabIndex = 4;
            this.loadBtn.Text = "LOAD";
            this.loadBtn.UseVisualStyleBackColor = true;
            // 
            // Player
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.loadBtn);
            this.Controls.Add(this.playList);
            this.Controls.Add(this.playBtn);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.prevBtn);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "Player";
            this.Text = "Your Music Player";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button prevBtn;
        private System.Windows.Forms.Button nextBtn;
        private System.Windows.Forms.Button playBtn;
        private System.Windows.Forms.ListBox playList;
        private System.Windows.Forms.Button loadBtn;
    }
}

