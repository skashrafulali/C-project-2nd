namespace C__project
{
    partial class Employee_Dash
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
            this.bNotice = new System.Windows.Forms.Button();
            this.bApplication = new System.Windows.Forms.Button();
            this.bUpdateProfile = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bNotice
            // 
            this.bNotice.BackColor = System.Drawing.SystemColors.Info;
            this.bNotice.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bNotice.Location = new System.Drawing.Point(42, 143);
            this.bNotice.Margin = new System.Windows.Forms.Padding(4);
            this.bNotice.Name = "bNotice";
            this.bNotice.Size = new System.Drawing.Size(301, 55);
            this.bNotice.TabIndex = 0;
            this.bNotice.Text = "Notice Board";
            this.bNotice.UseVisualStyleBackColor = false;
            this.bNotice.Click += new System.EventHandler(this.bNotice_Click);
            // 
            // bApplication
            // 
            this.bApplication.BackColor = System.Drawing.SystemColors.Info;
            this.bApplication.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bApplication.Location = new System.Drawing.Point(42, 282);
            this.bApplication.Margin = new System.Windows.Forms.Padding(4);
            this.bApplication.Name = "bApplication";
            this.bApplication.Size = new System.Drawing.Size(301, 65);
            this.bApplication.TabIndex = 1;
            this.bApplication.Text = "Create an Application";
            this.bApplication.UseVisualStyleBackColor = false;
            this.bApplication.Click += new System.EventHandler(this.bApplication_Click);
            // 
            // bUpdateProfile
            // 
            this.bUpdateProfile.BackColor = System.Drawing.SystemColors.Info;
            this.bUpdateProfile.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bUpdateProfile.Location = new System.Drawing.Point(42, 439);
            this.bUpdateProfile.Name = "bUpdateProfile";
            this.bUpdateProfile.Size = new System.Drawing.Size(301, 60);
            this.bUpdateProfile.TabIndex = 4;
            this.bUpdateProfile.Text = "Update Profile";
            this.bUpdateProfile.UseVisualStyleBackColor = false;
            this.bUpdateProfile.Click += new System.EventHandler(this.bUpdateProfile_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::C__project.Properties.Resources._12;
            this.pictureBox1.Location = new System.Drawing.Point(439, 78);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(476, 470);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Teal;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 32);
            this.label1.TabIndex = 6;
            this.label1.Text = "Welcome To The Employee Portal";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gray;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(749, 572);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 48);
            this.button1.TabIndex = 7;
            this.button1.Text = "Log Out";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // Employee_Dash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(944, 644);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.bUpdateProfile);
            this.Controls.Add(this.bApplication);
            this.Controls.Add(this.bNotice);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Employee_Dash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee_Dash";
            this.Load += new System.EventHandler(this.Employee_Dash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bNotice;
        private System.Windows.Forms.Button bApplication;
        private System.Windows.Forms.Button bUpdateProfile;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}