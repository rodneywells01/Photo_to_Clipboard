namespace Photo_to_Clipboard
{
    partial class Form1
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
            this.buttonSetClipboard = new System.Windows.Forms.Button();
            this.textBoxDesiredText = new System.Windows.Forms.TextBox();
            this.labelInformation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSetClipboard
            // 
            this.buttonSetClipboard.Location = new System.Drawing.Point(35, 197);
            this.buttonSetClipboard.Name = "buttonSetClipboard";
            this.buttonSetClipboard.Size = new System.Drawing.Size(203, 53);
            this.buttonSetClipboard.TabIndex = 0;
            this.buttonSetClipboard.Text = "Set Clipboard";
            this.buttonSetClipboard.UseVisualStyleBackColor = true;
            this.buttonSetClipboard.Click += new System.EventHandler(this.buttonSetClipboard_Click);
            // 
            // textBoxDesiredText
            // 
            this.textBoxDesiredText.Location = new System.Drawing.Point(58, 52);
            this.textBoxDesiredText.Name = "textBoxDesiredText";
            this.textBoxDesiredText.Size = new System.Drawing.Size(158, 20);
            this.textBoxDesiredText.TabIndex = 1;
            // 
            // labelInformation
            // 
            this.labelInformation.AutoSize = true;
            this.labelInformation.Location = new System.Drawing.Point(114, 126);
            this.labelInformation.Name = "labelInformation";
            this.labelInformation.Size = new System.Drawing.Size(35, 13);
            this.labelInformation.TabIndex = 2;
            this.labelInformation.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.labelInformation);
            this.Controls.Add(this.textBoxDesiredText);
            this.Controls.Add(this.buttonSetClipboard);
            this.Name = "Form1";
            this.Text = "Photo to Clipboard!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSetClipboard;
        private System.Windows.Forms.TextBox textBoxDesiredText;
        private System.Windows.Forms.Label labelInformation;
    }
}

