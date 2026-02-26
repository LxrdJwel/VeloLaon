namespace stagev2
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            btnParcourir = new Button();
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            bt_pdf = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(1695, 12);
            button1.Name = "button1";
            button1.Size = new Size(32, 34);
            button1.TabIndex = 0;
            button1.Text = "______";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(1733, 12);
            button2.Name = "button2";
            button2.Size = new Size(39, 34);
            button2.TabIndex = 1;
            button2.Text = "o";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(1778, 12);
            button3.Name = "button3";
            button3.Size = new Size(32, 34);
            button3.TabIndex = 2;
            button3.Text = "X";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // btnParcourir
            // 
            btnParcourir.Location = new Point(12, 12);
            btnParcourir.Name = "btnParcourir";
            btnParcourir.Size = new Size(152, 34);
            btnParcourir.TabIndex = 3;
            btnParcourir.Text = "CSV";
            btnParcourir.UseVisualStyleBackColor = true;
            btnParcourir.Click += btnParcourir_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(167, 52);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1529, 129);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(12, 205);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(1780, 535);
            dataGridView2.TabIndex = 5;
            dataGridView2.CellContentClick += dataGridView2_CellContentClick;
            dataGridView2.SelectionChanged += dataGridView2_SelectionChanged;
            // 
            // bt_pdf
            // 
            bt_pdf.Location = new Point(1669, 771);
            bt_pdf.Name = "bt_pdf";
            bt_pdf.Size = new Size(132, 26);
            bt_pdf.TabIndex = 6;
            bt_pdf.Text = "PDF";
            bt_pdf.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGray;
            ClientSize = new Size(1813, 865);
            Controls.Add(bt_pdf);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Controls.Add(btnParcourir);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button btnParcourir;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Button bt_pdf;
    }
}
