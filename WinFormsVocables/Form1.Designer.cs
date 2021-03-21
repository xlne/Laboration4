
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
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // btn_loadList
            // 
            this.btn_loadList.Location = new System.Drawing.Point(21, 18);
            this.btn_loadList.Name = "btn_loadList";
            this.btn_loadList.Size = new System.Drawing.Size(87, 35);
            this.btn_loadList.TabIndex = 1;
            this.btn_loadList.Text = "Load List";
            this.btn_loadList.UseVisualStyleBackColor = true;
            this.btn_loadList.Click += new System.EventHandler(this.btn_viewList_Click);
            // 
            // btn_NewWord
            // 
            this.btn_NewWord.Location = new System.Drawing.Point(133, 18);
            this.btn_NewWord.Name = "btn_NewWord";
            this.btn_NewWord.Size = new System.Drawing.Size(86, 35);
            this.btn_NewWord.TabIndex = 2;
            this.btn_NewWord.Text = "New Word";
            this.btn_NewWord.UseVisualStyleBackColor = true;
            this.btn_NewWord.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(238, 18);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 35);
            this.button3.TabIndex = 3;
            this.button3.Text = "Remove Word";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(355, 18);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 35);
            this.button4.TabIndex = 4;
            this.button4.Text = "New List";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 403);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Number of Words: ";
            this.label1.Visible = false;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(476, 18);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(95, 35);
            this.button5.TabIndex = 6;
            this.button5.Text = "Sort List";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(594, 18);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(122, 35);
            this.button6.TabIndex = 7;
            this.button6.Text = "Practice your words";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(28, 82);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(709, 278);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 300;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}

