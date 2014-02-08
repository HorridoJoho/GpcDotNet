namespace Gpc
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.lbPolygons = new System.Windows.Forms.ListBox();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.btnAddPoly = new System.Windows.Forms.Button();
			this.btnRemPoly = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cbClipOps = new System.Windows.Forms.ComboBox();
			this.pbDrawing = new System.Windows.Forms.PictureBox();
			this.ofdPolygon = new System.Windows.Forms.OpenFileDialog();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbDrawing)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.pbDrawing, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(624, 442);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.tableLayoutPanel2);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(3, 285);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(618, 154);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Polygons";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel2.Controls.Add(this.lbPolygons, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 135F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(612, 135);
			this.tableLayoutPanel2.TabIndex = 5;
			// 
			// lbPolygons
			// 
			this.lbPolygons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbPolygons.FormattingEnabled = true;
			this.lbPolygons.Location = new System.Drawing.Point(0, 0);
			this.lbPolygons.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.lbPolygons.Name = "lbPolygons";
			this.lbPolygons.Size = new System.Drawing.Size(584, 135);
			this.lbPolygons.TabIndex = 4;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.Controls.Add(this.btnAddPoly, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.btnRemPoly, 0, 2);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(587, 0);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 4;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(25, 135);
			this.tableLayoutPanel3.TabIndex = 5;
			// 
			// btnAddPoly
			// 
			this.btnAddPoly.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnAddPoly.Location = new System.Drawing.Point(0, 42);
			this.btnAddPoly.Margin = new System.Windows.Forms.Padding(0);
			this.btnAddPoly.Name = "btnAddPoly";
			this.btnAddPoly.Size = new System.Drawing.Size(25, 25);
			this.btnAddPoly.TabIndex = 0;
			this.btnAddPoly.Text = "+";
			this.btnAddPoly.UseVisualStyleBackColor = true;
			this.btnAddPoly.Click += new System.EventHandler(this.btnAddPoly_Click);
			// 
			// btnRemPoly
			// 
			this.btnRemPoly.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnRemPoly.Location = new System.Drawing.Point(0, 67);
			this.btnRemPoly.Margin = new System.Windows.Forms.Padding(0);
			this.btnRemPoly.Name = "btnRemPoly";
			this.btnRemPoly.Size = new System.Drawing.Size(25, 25);
			this.btnRemPoly.TabIndex = 1;
			this.btnRemPoly.Text = "-";
			this.btnRemPoly.UseVisualStyleBackColor = true;
			this.btnRemPoly.Click += new System.EventHandler(this.btnRemPoly_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cbClipOps);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(3, 235);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(618, 44);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Clip Operation";
			// 
			// cbClipOps
			// 
			this.cbClipOps.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbClipOps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbClipOps.FormattingEnabled = true;
			this.cbClipOps.Location = new System.Drawing.Point(3, 16);
			this.cbClipOps.Name = "cbClipOps";
			this.cbClipOps.Size = new System.Drawing.Size(612, 21);
			this.cbClipOps.TabIndex = 0;
			this.cbClipOps.SelectedIndexChanged += new System.EventHandler(this.cbClipOps_SelectedIndexChanged);
			// 
			// pbDrawing
			// 
			this.pbDrawing.BackColor = System.Drawing.SystemColors.Control;
			this.pbDrawing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pbDrawing.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbDrawing.Location = new System.Drawing.Point(0, 0);
			this.pbDrawing.Margin = new System.Windows.Forms.Padding(0);
			this.pbDrawing.Name = "pbDrawing";
			this.pbDrawing.Size = new System.Drawing.Size(624, 232);
			this.pbDrawing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbDrawing.TabIndex = 2;
			this.pbDrawing.TabStop = false;
			// 
			// ofdPolygon
			// 
			this.ofdPolygon.Title = "Open Polygon File";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 442);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(640, 480);
			this.Name = "MainForm";
			this.Text = "GpcDotNet Visualizer";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbDrawing)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.OpenFileDialog ofdPolygon;
		private System.Windows.Forms.PictureBox pbDrawing;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox cbClipOps;
		private System.Windows.Forms.ListBox lbPolygons;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Button btnAddPoly;
		private System.Windows.Forms.Button btnRemPoly;
	}
}