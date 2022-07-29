namespace Standalone
{
    partial class StandaloneWindow
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
            this.listBoxPatient = new System.Windows.Forms.ListBox();
            this.listBoxCourse = new System.Windows.Forms.ListBox();
            this.listBoxPlanSetup = new System.Windows.Forms.ListBox();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listBoxPatient
            // 
            this.listBoxPatient.FormattingEnabled = true;
            this.listBoxPatient.Location = new System.Drawing.Point(12, 38);
            this.listBoxPatient.Name = "listBoxPatient";
            this.listBoxPatient.Size = new System.Drawing.Size(246, 238);
            this.listBoxPatient.TabIndex = 0;
            //this.listBoxPatient.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // listBoxCourse
            // 
            this.listBoxCourse.FormattingEnabled = true;
            this.listBoxCourse.Location = new System.Drawing.Point(264, 12);
            this.listBoxCourse.Name = "listBoxCourse";
            this.listBoxCourse.Size = new System.Drawing.Size(246, 264);
            this.listBoxCourse.TabIndex = 1;
            //this.listBoxCourse.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // listBoxPlanSetup
            // 
            this.listBoxPlanSetup.FormattingEnabled = true;
            this.listBoxPlanSetup.Location = new System.Drawing.Point(516, 12);
            this.listBoxPlanSetup.Name = "listBoxPlanSetup";
            this.listBoxPlanSetup.Size = new System.Drawing.Size(246, 264);
            this.listBoxPlanSetup.TabIndex = 2;
            //this.listBoxPlanSetup.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // textBoxId
            // 
            this.textBoxId.Location = new System.Drawing.Point(12, 12);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(246, 20);
            this.textBoxId.TabIndex = 3;
            //this.textBoxId.TextChanged += new System.EventHandler(this.textBoxId_TextChanged);
            // 
            // StandaloneWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 290);
            this.Controls.Add(this.textBoxId);
            this.Controls.Add(this.listBoxPlanSetup);
            this.Controls.Add(this.listBoxCourse);
            this.Controls.Add(this.listBoxPatient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StandaloneWindow";
            this.Text = "Select patient...";
            //this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StandaloneWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxPatient;
        private System.Windows.Forms.ListBox listBoxCourse;
        private System.Windows.Forms.ListBox listBoxPlanSetup;
        private System.Windows.Forms.TextBox textBoxId;
    }
}

