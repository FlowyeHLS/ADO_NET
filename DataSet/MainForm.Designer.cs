namespace DataSet
{
    partial class MainForm
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
            this.comboBoxStudendsGroup = new System.Windows.Forms.ComboBox();
            this.comboBoxStudentsDirection = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxStudendsGroup
            // 
            this.comboBoxStudendsGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudendsGroup.FormattingEnabled = true;
            this.comboBoxStudendsGroup.Location = new System.Drawing.Point(34, 32);
            this.comboBoxStudendsGroup.Name = "comboBoxStudendsGroup";
            this.comboBoxStudendsGroup.Size = new System.Drawing.Size(248, 21);
            this.comboBoxStudendsGroup.TabIndex = 0;
            // 
            // comboBoxStudentsDirection
            // 
            this.comboBoxStudentsDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudentsDirection.FormattingEnabled = true;
            this.comboBoxStudentsDirection.Location = new System.Drawing.Point(332, 32);
            this.comboBoxStudentsDirection.Name = "comboBoxStudentsDirection";
            this.comboBoxStudentsDirection.Size = new System.Drawing.Size(205, 21);
            this.comboBoxStudentsDirection.TabIndex = 1;
            this.comboBoxStudentsDirection.SelectedIndexChanged += new System.EventHandler(this.comboBoxStudentsDirection_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBoxStudentsDirection);
            this.Controls.Add(this.comboBoxStudendsGroup);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxStudendsGroup;
        private System.Windows.Forms.ComboBox comboBoxStudentsDirection;
    }
}

