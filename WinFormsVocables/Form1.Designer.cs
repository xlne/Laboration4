
namespace WinFormsVocables
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
            this.btn_loadList = new System.Windows.Forms.Button();
            this.btn_NewWord = new System.Windows.Forms.Button();
            this.btn_removeWord = new System.Windows.Forms.Button();
            this.btn_newList = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_sortList = new System.Windows.Forms.Button();
            this.btn_practice = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Languages = new System.Windows.Forms.ColumnHeader();
            this.lbl_Vocables = new System.Windows.Forms.Label();
            this.lbl_authors = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_loadList
            // 
            this.btn_loadList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_loadList.Location = new System.Drawing.Point(261, 82);
            this.btn_loadList.Name = "btn_loadList";
            this.btn_loadList.Size = new System.Drawing.Size(108, 35);
            this.btn_loadList.TabIndex = 1;
            this.btn_loadList.Text = "Load/Show List";
            this.btn_loadList.UseVisualStyleBackColor = true;
            this.btn_loadList.Click += new System.EventHandler(this.btn_viewList_Click);
            // 
            // btn_NewWord
            // 
            this.btn_NewWord.Enabled = false;
            this.btn_NewWord.Location = new System.Drawing.Point(261, 164);
            this.btn_NewWord.Name = "btn_NewWord";
            this.btn_NewWord.Size = new System.Drawing.Size(108, 35);
            this.btn_NewWord.TabIndex = 2;
            this.btn_NewWord.Text = "New Word";
            this.btn_NewWord.UseVisualStyleBackColor = true;
            this.btn_NewWord.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_removeWord
            // 
            this.btn_removeWord.Enabled = false;
            this.btn_removeWord.Location = new System.Drawing.Point(261, 205);
            this.btn_removeWord.Name = "btn_removeWord";
            this.btn_removeWord.Size = new System.Drawing.Size(108, 35);
            this.btn_removeWord.TabIndex = 3;
            this.btn_removeWord.Text = "Remove Word";
            this.btn_removeWord.UseVisualStyleBackColor = true;
            this.btn_removeWord.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_newList
            // 
            this.btn_newList.Location = new System.Drawing.Point(261, 123);
            this.btn_newList.Name = "btn_newList";
            this.btn_newList.Size = new System.Drawing.Size(108, 35);
            this.btn_newList.TabIndex = 4;
            this.btn_newList.Text = "New List";
            this.btn_newList.UseVisualStyleBackColor = true;
            this.btn_newList.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 325);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Number of Words: ";
            this.label1.Visible = false;
            // 
            // btn_sortList
            // 
            this.btn_sortList.Enabled = false;
            this.btn_sortList.Location = new System.Drawing.Point(261, 246);
            this.btn_sortList.Name = "btn_sortList";
            this.btn_sortList.Size = new System.Drawing.Size(108, 35);
            this.btn_sortList.TabIndex = 6;
            this.btn_sortList.Text = "Sort List";
            this.btn_sortList.UseVisualStyleBackColor = true;
            this.btn_sortList.Click += new System.EventHandler(this.btn_sortList_Click);
            // 
            // btn_practice
            // 
            this.btn_practice.Enabled = false;
            this.btn_practice.Location = new System.Drawing.Point(261, 287);
            this.btn_practice.Name = "btn_practice";
            this.btn_practice.Size = new System.Drawing.Size(108, 35);
            this.btn_practice.TabIndex = 7;
            this.btn_practice.Text = "Practice";
            this.btn_practice.UseVisualStyleBackColor = true;
            this.btn_practice.Click += new System.EventHandler(this.btn_practice_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Languages});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(28, 82);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(205, 240);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Languages
            // 
            this.Languages.Text = "Languages";
            this.Languages.Width = 200;
            // 
            // lbl_Vocables
            // 
            this.lbl_Vocables.AutoSize = true;
            this.lbl_Vocables.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbl_Vocables.Location = new System.Drawing.Point(28, 13);
            this.lbl_Vocables.Name = "lbl_Vocables";
            this.lbl_Vocables.Size = new System.Drawing.Size(107, 32);
            this.lbl_Vocables.TabIndex = 9;
            this.lbl_Vocables.Text = "Vocables";
            // 
            // lbl_authors
            // 
            this.lbl_authors.AutoSize = true;
            this.lbl_authors.Location = new System.Drawing.Point(38, 45);
            this.lbl_authors.Name = "lbl_authors";
            this.lbl_authors.Size = new System.Drawing.Size(208, 15);
            this.lbl_authors.TabIndex = 10;
            this.lbl_authors.Text = "By Linnéa Netzell and Andreas Nilsson";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 359);
            this.Controls.Add(this.lbl_authors);
            this.Controls.Add(this.lbl_Vocables);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btn_practice);
            this.Controls.Add(this.btn_sortList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_newList);
            this.Controls.Add(this.btn_removeWord);
            this.Controls.Add(this.btn_NewWord);
            this.Controls.Add(this.btn_loadList);
            this.Name = "Form1";
            this.Text = "Vocables";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_loadList;
        private System.Windows.Forms.Button btn_NewWord;
        private System.Windows.Forms.Button btn_removeWord;
        private System.Windows.Forms.Button btn_newList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_sortList;
        private System.Windows.Forms.Button btn_practice;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Languages;
        private System.Windows.Forms.Label lbl_Vocables;
        private System.Windows.Forms.Label lbl_authors;
    }
}

