using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.VisualStyles;
using static Demo2.Graph;

namespace Demo2
{
	public partial class MainForm : Form
	{
		public Graph graph;
		DrawForm dform;
		public List<Node> nodes;
		public List<Edge> edges;
		public MainForm()
		{
			InitializeComponent();
			nodes = new List<Node>();
			edges = new List<Edge>();
		}



		private void duyetDoThiToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (graph == null || graph.n <= 0)
				return;

			string inputString = "";

			// Tạo một Form tùy chỉnh với ComboBox để chọn nút khởi đầu
			Form inputForm = new Form();
			inputForm.Text = "Chọn nút bắt đầu";
			inputForm.Height = 210;
			inputForm.Width = 400;
			inputForm.StartPosition = FormStartPosition.CenterScreen;

			Label label = new Label();
			label.Parent = inputForm;
			label.Text = "Hãy nhập đỉnh bắt đầu duyệt:";
			label.Size = new Size(200, 30);
			label.Location = new Point(20, 20);

			ComboBox comboBox = new ComboBox();
			comboBox.Parent = inputForm;
			comboBox.Location = new Point(20, 60);

			for (int i = 1; i <= graph.n; i++)
			{
				comboBox.Items.Add(i.ToString());
			}

			Button okButton = new Button();
			okButton.Parent = inputForm;
			okButton.Text = "OK";
			okButton.DialogResult = DialogResult.OK;
			okButton.Size = new Size(75, 30);
			okButton.Location = new Point(20, 110);

			Button cancelButton = new Button();
			cancelButton.Parent = inputForm;
			cancelButton.Text = "Cancel";
			cancelButton.DialogResult = DialogResult.Cancel;
			cancelButton.Size = new Size(75, 30);
			cancelButton.Location = new Point(100, 110);

			inputForm.AcceptButton = okButton;
			inputForm.CancelButton = cancelButton;

			// Hiển thị Form và lấy giá trị được chọn nếu người dùng nhấn OK
			if (inputForm.ShowDialog() == DialogResult.OK)
			{
				if (comboBox.SelectedItem != null)
				{
					inputString = comboBox.SelectedItem.ToString();
				}
			}

			inputForm.Dispose();

			// Kiểm tra xem người dùng đã chọn giá trị hay chưa
			if (inputString != "")
			{
				int inputNode = -1;
				
					inputNode = int.Parse(inputString);
		
				List<int> result = graph.dfs(inputNode);
				string result_dfs = "";
				for (int i = 0; i < result.Count; i++)
					result_dfs += result[i].ToString() + " ";

				MessageBox.Show(result_dfs);
			}


		}
		private Color[] RandomColor()
		{
			Color[] colors = new Color[10];

			Random random = new Random();
			List<Color> usedColors = new List<Color>();

			for (int i = 0; i < 10; i++)
			{
				Color color;
				do
				{
					int red = random.Next(256);
					int green = random.Next(256);
					int blue = random.Next(256);
					color = Color.FromArgb(red, green, blue);
				} while (usedColors.Contains(color));

				colors[i] = color;
				usedColors.Add(color);
			}

			return colors;

		}

		private void tinhLienThongToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool[] visited = new bool[this.graph.n + 1];
			Color[] colors = RandomColor();
			int num = 0;

			for (int i = 1; i <= this.graph.n; i++)
				visited[i] = false;

			for (int i = 1; i <= this.graph.n; i++)
			{
				if (!visited[i])
				{
					Brush brush = new SolidBrush(colors[i]);
					num++;
					List<int> dfs_result = this.graph.dfs(i);
					foreach (int node in dfs_result)
					{
						// Tô màu đỉnh đã duyệt
						nodes[node - 1].Color = brush;
						visited[node] = true;
					}
				}
			}

			MessageBox.Show("Số miền liên thông là : " + num.ToString());
			Refresh();

		}
		private void updateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dform = new DrawForm();
			dform.MdiParent = this;
			dform.Show();

		}

		private void eulerToolStripMenuItem_Click(object sender, EventArgs e)
		{


			if (graph == null || graph.n <= 0)
				return;

			List<int> eulerCycle = this.graph.Fleury(1);

			if (eulerCycle == null)
			{
				MessageBox.Show("Không tồn tại chu trình Euler!");
				return;
			}

			string cycleString = "";
			foreach (int node in eulerCycle)
				cycleString += node.ToString() + " ";

			MessageBox.Show("Chu trình Euler: " + cycleString);




		}

		private void thoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Ứng dụng tìm chu trình euler" + "\n" + "Niên luận cơ sở ngành kỹ thuật phần mềm" + "\n" + "Sinh viên thực hiện: " + "\n" + "Giảng viên hướng dẫn: Th.s Phan Huy Cường");
		}

		private void MainForm_Load(object sender, EventArgs e)
		{

		}
	}
}
