namespace ArtiluxEOL
{
    partial class Popup_msg
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
            this.components = new System.ComponentModel.Container();
            this.msg_title = new System.Windows.Forms.Label();
            this.msg_text = new System.Windows.Forms.Label();
            this.timerPopup = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // msg_title
            // 
            this.msg_title.AutoSize = true;
            this.msg_title.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msg_title.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.msg_title.Location = new System.Drawing.Point(12, 9);
            this.msg_title.Name = "msg_title";
            this.msg_title.Size = new System.Drawing.Size(65, 35);
            this.msg_title.TabIndex = 4;
            this.msg_title.Text = "Title";
            // 
            // msg_text
            // 
            this.msg_text.AutoSize = true;
            this.msg_text.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msg_text.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.msg_text.Location = new System.Drawing.Point(12, 63);
            this.msg_text.Name = "msg_text";
            this.msg_text.Size = new System.Drawing.Size(63, 35);
            this.msg_text.TabIndex = 5;
            this.msg_text.Text = "Text";
            // 
            // timerPopup
            // 
            this.timerPopup.Interval = 3000;
            this.timerPopup.Tick += new System.EventHandler(this.timerPopup_Tick);
            // 
            // Popup_msg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 114);
            this.Controls.Add(this.msg_text);
            this.Controls.Add(this.msg_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Popup_msg";
            this.Text = "Popup_msg";
            this.Load += new System.EventHandler(this.Popup_msg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label msg_title;
        private System.Windows.Forms.Label msg_text;
        private System.Windows.Forms.Timer timerPopup;
    }
}