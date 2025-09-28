namespace MainForm
{
    partial class FormAddGroups
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
            this.labelAddNewGroup = new System.Windows.Forms.Label();
            this.textGroupsAddNew = new System.Windows.Forms.TextBox();
            this.buttonOkey = new System.Windows.Forms.Button();
            this.buttonCansel = new System.Windows.Forms.Button();
            this.labelAddDiscipline = new System.Windows.Forms.Label();
            this.comboBoxAddDirection = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelAddNewGroup
            // 
            this.labelAddNewGroup.AutoSize = true;
            this.labelAddNewGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAddNewGroup.Location = new System.Drawing.Point(12, 37);
            this.labelAddNewGroup.Name = "labelAddNewGroup";
            this.labelAddNewGroup.Size = new System.Drawing.Size(305, 29);
            this.labelAddNewGroup.TabIndex = 0;
            this.labelAddNewGroup.Text = "Введите название группы";
            // 
            // textGroupsAddNew
            // 
            this.textGroupsAddNew.Location = new System.Drawing.Point(391, 44);
            this.textGroupsAddNew.Name = "textGroupsAddNew";
            this.textGroupsAddNew.Size = new System.Drawing.Size(187, 22);
            this.textGroupsAddNew.TabIndex = 1;
            // 
            // buttonOkey
            // 
            this.buttonOkey.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOkey.Location = new System.Drawing.Point(212, 218);
            this.buttonOkey.Name = "buttonOkey";
            this.buttonOkey.Size = new System.Drawing.Size(204, 49);
            this.buttonOkey.TabIndex = 4;
            this.buttonOkey.Text = "Ok";
            this.buttonOkey.UseVisualStyleBackColor = true;
            this.buttonOkey.Click += new System.EventHandler(this.buttonOkey_Click);
            // 
            // buttonCansel
            // 
            this.buttonCansel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCansel.Location = new System.Drawing.Point(437, 218);
            this.buttonCansel.Name = "buttonCansel";
            this.buttonCansel.Size = new System.Drawing.Size(189, 49);
            this.buttonCansel.TabIndex = 5;
            this.buttonCansel.Text = "Cansel";
            this.buttonCansel.UseVisualStyleBackColor = true;
            // 
            // labelAddDiscipline
            // 
            this.labelAddDiscipline.AutoSize = true;
            this.labelAddDiscipline.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAddDiscipline.Location = new System.Drawing.Point(15, 99);
            this.labelAddDiscipline.Name = "labelAddDiscipline";
            this.labelAddDiscipline.Size = new System.Drawing.Size(247, 29);
            this.labelAddDiscipline.TabIndex = 2;
            this.labelAddDiscipline.Text = "Введите дисциплину";
            // 
            // comboBoxAddDirection
            // 
            this.comboBoxAddDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAddDirection.FormattingEnabled = true;
            this.comboBoxAddDirection.Location = new System.Drawing.Point(391, 103);
            this.comboBoxAddDirection.Name = "comboBoxAddDirection";
            this.comboBoxAddDirection.Size = new System.Drawing.Size(187, 24);
            this.comboBoxAddDirection.TabIndex = 6;
            // 
            // FormAddGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 279);
            this.Controls.Add(this.comboBoxAddDirection);
            this.Controls.Add(this.buttonCansel);
            this.Controls.Add(this.buttonOkey);
            this.Controls.Add(this.labelAddDiscipline);
            this.Controls.Add(this.textGroupsAddNew);
            this.Controls.Add(this.labelAddNewGroup);
            this.Name = "FormAddGroups";
            this.Text = "FormAddGroups";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAddNewGroup;
        private System.Windows.Forms.TextBox textGroupsAddNew;
        private System.Windows.Forms.Button buttonOkey;
        private System.Windows.Forms.Button buttonCansel;
        private System.Windows.Forms.Label labelAddDiscipline;
        private System.Windows.Forms.ComboBox comboBoxAddDirection;
    }
}