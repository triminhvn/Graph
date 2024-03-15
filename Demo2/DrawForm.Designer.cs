namespace Demo2
{
	partial class DrawForm
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
			myPanel = new Panel();
			btnExit = new Button();
			btnEdit = new Button();
			btnBack = new Button();
			saveFileDialog = new SaveFileDialog();
			openFileDialog = new OpenFileDialog();
			btnSave = new Button();
			btnOpen = new Button();
			SuspendLayout();
			// 
			// myPanel
			// 
			myPanel.BackColor = SystemColors.ButtonHighlight;
			myPanel.Location = new Point(7, 3);
			myPanel.Name = "myPanel";
			myPanel.Size = new Size(675, 482);
			myPanel.TabIndex = 0;
			myPanel.Paint += myPanel_Paint;
			myPanel.MouseClick += myPanel_MouseClick;
			myPanel.MouseDown += myPanel_MouseDown;
			myPanel.MouseMove += myPanel_MouseMove;
			myPanel.MouseUp += myPanel_MouseUp;
			// 
			// btnExit
			// 
			btnExit.Location = new Point(702, 137);
			btnExit.Name = "btnExit";
			btnExit.Size = new Size(94, 29);
			btnExit.TabIndex = 3;
			btnExit.Text = "Thoát";
			btnExit.UseVisualStyleBackColor = true;
			btnExit.Click += btnExit_Click;
			// 
			// btnEdit
			// 
			btnEdit.FlatStyle = FlatStyle.System;
			btnEdit.Location = new Point(702, 33);
			btnEdit.Name = "btnEdit";
			btnEdit.Size = new Size(94, 29);
			btnEdit.TabIndex = 1;
			btnEdit.Text = "Chỉnh sửa";
			btnEdit.UseVisualStyleBackColor = true;
			btnEdit.Click += btnEdit_Click;
			// 
			// btnBack
			// 
			btnBack.FlatStyle = FlatStyle.System;
			btnBack.Location = new Point(702, 85);
			btnBack.Name = "btnBack";
			btnBack.Size = new Size(94, 29);
			btnBack.TabIndex = 2;
			btnBack.Text = "Trở lại";
			btnBack.UseVisualStyleBackColor = true;
			btnBack.Click += btnBack_Click;
			// 
			// openFileDialog
			// 
			openFileDialog.FileName = "openFileDialog1";
			// 
			// btnSave
			// 
			btnSave.FlatStyle = FlatStyle.System;
			btnSave.Location = new Point(702, 189);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(94, 29);
			btnSave.TabIndex = 4;
			btnSave.Text = "Lưu lại";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += btnSave_Click_1;
			// 
			// btnOpen
			// 
			btnOpen.FlatStyle = FlatStyle.System;
			btnOpen.Location = new Point(702, 241);
			btnOpen.Name = "btnOpen";
			btnOpen.Size = new Size(94, 29);
			btnOpen.TabIndex = 5;
			btnOpen.Text = "Mở lại";
			btnOpen.UseVisualStyleBackColor = true;
			btnOpen.Click += btnOpen_Click;
			// 
			// DrawForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(820, 487);
			Controls.Add(btnOpen);
			Controls.Add(btnSave);
			Controls.Add(btnBack);
			Controls.Add(btnEdit);
			Controls.Add(btnExit);
			Controls.Add(myPanel);
			Name = "DrawForm";
			Text = "Draw Form";
			Load += DrawForm_Load;
			ResumeLayout(false);
		}

		#endregion

		private Panel myPanel;
		private Button btnExit;
		private Button btnEdit;
		private Button btnBack;
		private SaveFileDialog saveFileDialog;
		private OpenFileDialog openFileDialog;
		private Button btnSave;
		private Button btnOpen;
	}
}