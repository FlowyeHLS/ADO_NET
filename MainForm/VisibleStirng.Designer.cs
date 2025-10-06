namespace MainForm
{
    partial class VisibleStirng
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
            this.checkedListBoxVisible = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // checkedListBoxVisible
            // 
            this.checkedListBoxVisible.FormattingEnabled = true;
            this.checkedListBoxVisible.Items.AddRange(new object[] {
            "group_id",
            "group_name",
            "direction"});
            this.checkedListBoxVisible.Location = new System.Drawing.Point(26, 12);
            this.checkedListBoxVisible.Name = "checkedListBoxVisible";
            this.checkedListBoxVisible.Size = new System.Drawing.Size(179, 49);
            this.checkedListBoxVisible.TabIndex = 0;
            // 
            // VisibleStirng
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(241, 112);
            this.Controls.Add(this.checkedListBoxVisible);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisibleStirng";
            this.Text = "VisibleStirng";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxVisible;
    }
}