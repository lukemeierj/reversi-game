namespace ReversiUI
{
    partial class BoardVisual
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
            this.PlayerVsPlayer = new System.Windows.Forms.Button();
            this.CompVsComp = new System.Windows.Forms.Button();
            this.PlayerVsCompBlack = new System.Windows.Forms.Button();
            this.PlayerVsCompW = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.GamePanel = new System.Windows.Forms.Panel();
            this.OptionsPanel = new System.Windows.Forms.Panel();
            this.NextMoveBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.StopOrClear = new System.Windows.Forms.Button();
            this.OptionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlayerVsPlayer
            // 
            this.PlayerVsPlayer.Location = new System.Drawing.Point(39, 168);
            this.PlayerVsPlayer.Name = "PlayerVsPlayer";
            this.PlayerVsPlayer.Size = new System.Drawing.Size(141, 26);
            this.PlayerVsPlayer.TabIndex = 0;
            this.PlayerVsPlayer.Tag = "PvP";
            this.PlayerVsPlayer.Text = "Player vs. Player";
            this.PlayerVsPlayer.UseVisualStyleBackColor = true;
            this.PlayerVsPlayer.Click += new System.EventHandler(this.ChangeGameMode);
            // 
            // CompVsComp
            // 
            this.CompVsComp.Location = new System.Drawing.Point(39, 213);
            this.CompVsComp.Name = "CompVsComp";
            this.CompVsComp.Size = new System.Drawing.Size(141, 26);
            this.CompVsComp.TabIndex = 1;
            this.CompVsComp.Tag = "CvC";
            this.CompVsComp.Text = "Computer vs. Computer";
            this.CompVsComp.UseVisualStyleBackColor = true;
            this.CompVsComp.Click += new System.EventHandler(this.ChangeGameMode);
            // 
            // PlayerVsCompBlack
            // 
            this.PlayerVsCompBlack.Location = new System.Drawing.Point(39, 292);
            this.PlayerVsCompBlack.Name = "PlayerVsCompBlack";
            this.PlayerVsCompBlack.Size = new System.Drawing.Size(58, 26);
            this.PlayerVsCompBlack.TabIndex = 2;
            this.PlayerVsCompBlack.Tag = "CvP";
            this.PlayerVsCompBlack.Text = "Black";
            this.PlayerVsCompBlack.UseVisualStyleBackColor = true;
            this.PlayerVsCompBlack.Click += new System.EventHandler(this.ChangeGameMode);
            // 
            // PlayerVsCompW
            // 
            this.PlayerVsCompW.Location = new System.Drawing.Point(122, 292);
            this.PlayerVsCompW.Name = "PlayerVsCompW";
            this.PlayerVsCompW.Size = new System.Drawing.Size(58, 26);
            this.PlayerVsCompW.TabIndex = 3;
            this.PlayerVsCompW.Tag = "PvC";
            this.PlayerVsCompW.Text = "White";
            this.PlayerVsCompW.UseVisualStyleBackColor = true;
            this.PlayerVsCompW.Click += new System.EventHandler(this.ChangeGameMode);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 265);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Human vs. Computer";
            // 
            // GamePanel
            // 
            this.GamePanel.Location = new System.Drawing.Point(1, 0);
            this.GamePanel.Name = "GamePanel";
            this.GamePanel.Size = new System.Drawing.Size(668, 674);
            this.GamePanel.TabIndex = 5;
            // 
            // OptionsPanel
            // 
            this.OptionsPanel.Controls.Add(this.NextMoveBtn);
            this.OptionsPanel.Controls.Add(this.label3);
            this.OptionsPanel.Controls.Add(this.label2);
            this.OptionsPanel.Controls.Add(this.StopOrClear);
            this.OptionsPanel.Controls.Add(this.PlayerVsPlayer);
            this.OptionsPanel.Controls.Add(this.PlayerVsCompBlack);
            this.OptionsPanel.Controls.Add(this.CompVsComp);
            this.OptionsPanel.Controls.Add(this.label1);
            this.OptionsPanel.Controls.Add(this.PlayerVsCompW);
            this.OptionsPanel.Location = new System.Drawing.Point(675, 0);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(218, 563);
            this.OptionsPanel.TabIndex = 6;
            // 
            // NextMoveBtn
            // 
            this.NextMoveBtn.Location = new System.Drawing.Point(39, 377);
            this.NextMoveBtn.Name = "NextMoveBtn";
            this.NextMoveBtn.Size = new System.Drawing.Size(141, 27);
            this.NextMoveBtn.TabIndex = 8;
            this.NextMoveBtn.Text = "Next";
            this.NextMoveBtn.UseVisualStyleBackColor = true;
            this.NextMoveBtn.Click += new System.EventHandler(this.NextMove);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(36, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "by Luke Meier and Drew Hayward";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft New Tai Lue", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 62);
            this.label2.TabIndex = 6;
            this.label2.Text = "Othello!";
            // 
            // StopOrClear
            // 
            this.StopOrClear.Location = new System.Drawing.Point(39, 412);
            this.StopOrClear.Name = "StopOrClear";
            this.StopOrClear.Size = new System.Drawing.Size(141, 28);
            this.StopOrClear.TabIndex = 5;
            this.StopOrClear.Text = "Stop / Clear";
            this.StopOrClear.UseVisualStyleBackColor = true;
            this.StopOrClear.Click += new System.EventHandler(this.Reset);
            // 
            // BoardVisual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 672);
            this.Controls.Add(this.OptionsPanel);
            this.Controls.Add(this.GamePanel);
            this.Name = "BoardVisual";
            this.Text = "Othello";
            this.OptionsPanel.ResumeLayout(false);
            this.OptionsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PlayerVsPlayer;
        private System.Windows.Forms.Button CompVsComp;
        private System.Windows.Forms.Button PlayerVsCompBlack;
        private System.Windows.Forms.Button PlayerVsCompW;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel GamePanel;
        private System.Windows.Forms.Panel OptionsPanel;
        private System.Windows.Forms.Button StopOrClear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button NextMoveBtn;
    }
}

