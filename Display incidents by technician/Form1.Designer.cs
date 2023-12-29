namespace Display_incidents_by_technician
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listBoxIncidents = new ListBox();
            SuspendLayout();
            // 
            // listBoxIncidents
            // 
            listBoxIncidents.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxIncidents.FormattingEnabled = true;
            listBoxIncidents.ItemHeight = 14;
            listBoxIncidents.Location = new Point(12, 12);
            listBoxIncidents.Name = "listBoxIncidents";
            listBoxIncidents.Size = new Size(1094, 284);
            listBoxIncidents.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1105, 451);
            Controls.Add(listBoxIncidents);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxIncidents;
    }
}