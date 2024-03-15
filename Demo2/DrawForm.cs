using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Demo2
{
	public partial class DrawForm : Form
	{
		public List<Node> nodes;
		public List<Edge> edges;
		Node startNode, endNode;
		static int NODE_RADIUS = 22;
		public Graph graph;


		public DrawForm()
		{
			InitializeComponent();
			nodes = new List<Node>();
			edges = new List<Edge>();
			myPanel.MouseDown += myPanel_MouseDown;

		}

		public Node GetNodeAt(Point location)
		{
			//Tìm đỉnh ở vị trí xác định trên màn hình
			foreach (Node node in nodes)
			{
				int nodeSize = 2 * NODE_RADIUS;
				Rectangle nodeRect = new Rectangle(node.X - NODE_RADIUS, node.Y - NODE_RADIUS, nodeSize, nodeSize);

				if (nodeRect.Contains(location))
					return node;
			}

			return null;
		}

		private Edge GetEdgeOf(Node startNode, Node endNode)
		{
			foreach (Edge edge in edges)
			{
				if ((edge.StartNode == startNode && edge.EndNode == endNode) || (edge.StartNode == endNode && edge.EndNode == startNode))
				{
					return edge;
				}
			}

			return null;
		}

		public void update()
		{
			Graph g = new Graph(nodes.Count);
			//thêm cung
			foreach (Edge edge in edges)
			{
				int startNode = Convert.ToInt16(edge.StartNode.Label);
				int endNode = Convert.ToInt16(edge.EndNode.Label);

				g.addEdge(startNode, endNode);
			}

			MainForm frm = (MainForm)this.ParentForm;
			frm.graph = g;
			frm.nodes = nodes;
			frm.edges = edges;

		}
		public void resetcolor()
		{
			foreach (Node node in nodes)
			{
				node.Color = Brushes.White;
			}
		}
		private void myPanel_Paint(object sender, PaintEventArgs e)
		{
			int outerCircleThickness = 4; // Độ dày của đường tròn bên ngoài
			Pen outerCirclePen = new Pen(Color.Black, outerCircleThickness);

			foreach (Node node in nodes)
			{
				if (node.Color == null)
				{
					node.Color = Brushes.White;
				}
				int circleRadius = NODE_RADIUS - outerCircleThickness / 2; // Bán kính của đường tròn (để lại khoảng trắng bên trong)
				Rectangle circleRect = new Rectangle(node.X - circleRadius, node.Y - circleRadius, circleRadius * 2, circleRadius * 2);
				e.Graphics.DrawEllipse(outerCirclePen, circleRect); // Vẽ đường tròn bên ngoài
				e.Graphics.FillEllipse(node.Color, circleRect); // Tô màu cho đường tròn bên trong

				SizeF textSize = e.Graphics.MeasureString(node.Label, Font); // Kích thước của số
				float textX = node.X - textSize.Width / 2; // Tọa độ X để căn giữa theo chiều ngang
				float textY = node.Y - textSize.Height / 2; // Tọa độ Y để căn giữa theo chiều dọc
				e.Graphics.DrawString(node.Label, Font, Brushes.Black, textX, textY); // Hiển thị số ở giữa đường tròn
			}

			foreach (Edge edge in edges)
			{
				e.Graphics.DrawLine(Pens.Black, edge.StartNode.X, edge.StartNode.Y, edge.EndNode.X, edge.EndNode.Y);
			}

			if (startNode != null && endNode != null)
			{
				e.Graphics.DrawLine(Pens.Black, startNode.X, startNode.Y, endNode.X, endNode.Y);
			}



		}


		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{

			Graph g = new Graph(nodes.Count);
			//thêm cung
			foreach (Edge edge in edges)
			{
				int startNode = Convert.ToInt16(edge.StartNode.Label);
				int endNode = Convert.ToInt16(edge.EndNode.Label);

				g.addEdge(startNode, endNode);
			}

			MainForm frm = (MainForm)this.ParentForm;
			frm.graph = g;
			MessageBox.Show("da cap nhat");

		}

		private void updateLabel(Node node)
		{
			int nodeIndex = Convert.ToInt16(node.Label);

			for (int i = 0; i < nodes.Count; i++)
			{
				int nodeLabel = Convert.ToInt16(nodes[i].Label);

				if (nodeLabel > nodeIndex)
				{
					nodeLabel--;
					nodes[i].Label = nodeLabel.ToString();
				}
			}


		}

		private Edge GetEdgeAt(Point location)
		{
			foreach (Edge edge in edges)
			{
				GraphicsPath path = new GraphicsPath();
				path.AddLine(edge.StartNode.X, edge.StartNode.Y, edge.EndNode.X, edge.EndNode.Y);
				if (path.IsOutlineVisible(location, new Pen(Color.Black, NODE_RADIUS)))
				{
					return edge;
				}
			}

			return null;
		}
		private void myPanel_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (isEditMode)
				{
					if (e.Button == MouseButtons.Left)
					{
						nowNode = GetNodeAt(e.Location);
					}

				}
				else
				{
					//Nếu chưa có node tại vị trí click thì tạo nút và thêm vào list
					if (GetNodeAt(e.Location) == null)
					{
						Node node = new Node() { X = e.X, Y = e.Y, Label = (nodes.Count + 1).ToString(), Color = Brushes.White };
						//Thêm node vào danh sách đỉnh
						nodes.Add(node);

					}
					else
					{
						if (startNode == null)
							startNode = GetNodeAt(e.Location);
						else
						{
							endNode = GetNodeAt(e.Location);
							if (GetNodeAt(e.Location) != startNode && nodes.IndexOf(startNode) >= 0)
							{
								Edge clickedEdge = GetEdgeOf(startNode, endNode);
								if (clickedEdge == null)
								{
									Edge edge = new Edge()
									{
										StartNode = startNode,
										EndNode = endNode,

									};


									edges.Add(edge);

									update();


								}

							}
							startNode = null;
						}


					}
					update();
				}


			}
			if (e.Button == MouseButtons.Right)
			{
				if (isEditMode) ;

				else
				{

					Node clickedNode = GetNodeAt(e.Location);
					if (clickedNode != null)
					{
						// Xóa đỉnh và các cung liên quan
						nodes.Remove(clickedNode);
						edges.RemoveAll(edge => edge.StartNode == clickedNode || edge.EndNode == clickedNode);

						// Cập nhật nhãn của các đỉnh còn lại
						updateLabel(clickedNode);
						update();


					}
					else
					{
						Edge clickedEdge = GetEdgeAt(e.Location);
						if (clickedEdge != null)
						{
							// Xóa cung
							edges.Remove(clickedEdge);
							update();


						}
					}


				}
			}
			update();

			Refresh();
		}

		private Node draggingNode = null;
		private bool doubleClicked = false;

		private Node selectedNode; // Biến instance để lưu đỉnh đang được di chuyển
		Node nowNode = null;
		private void myPanel_MouseDown(object sender, MouseEventArgs e)
		{

			if (e.Button == MouseButtons.Left)
			{
				if (isEditMode)
					selectedNode = GetNodeAt(e.Location); // Lưu đỉnh được click vào biến instance
			}
		}

		private void myPanel_MouseMove(object sender, MouseEventArgs e)
		{

			if (startNode != null)
			{
				endNode = new Node() { X = e.X, Y = e.Y };
				Refresh();
			}
			if (selectedNode != null)
			{
				if (isEditMode)
				{
					selectedNode.X = e.X;
					selectedNode.Y = e.Y;
					myPanel.Invalidate(); // Vẽ lại panel để hiển thị việc di chuyển đỉnh
				}
			}
		}

		private void myPanel_MouseUp(object sender, MouseEventArgs e)
		{
			if (selectedNode != null)
			{
				if (isEditMode)
					selectedNode = null; // Xác nhận việc di chuyển đỉnh đã kết thúc
			}
			draggingNode = null;
			Refresh();
		}
		bool isNodeSelected = false;



		private void DrawForm_Load(object sender, EventArgs e)
		{


		}


		bool isEditMode = false;
		private int clickCount = 0;
		private void btnEdit_Click(object sender, EventArgs e)
		{

			isEditMode = !isEditMode; // đảo trạng thái
			clickCount++;


			if (clickCount % 2 == 1)
			{
				btnEdit.Text = "Vẽ";

			}
			else
			{
				btnEdit.Text = "Chỉnh sửa";


			}
		}

		private void btnXoa_Click(object sender, EventArgs e)
		{
			isEditMode = !isEditMode; // đảo trạng thái
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			resetcolor();
			update();
			Refresh();
		}
		public Node GetNodeAt(int label)
		{
			//Tìm đỉnh ở vị trí xác định trên màn hình
			foreach (Node node in nodes)
			{
				if (String.Compare(node.Label, label.ToString(), true) == 0)
					return node;
			}

			return null;
		}
		private void btnSave_Click_1(object sender, EventArgs e)
		{
			{

				{
					saveFileDialog.Filter = "Text Files (.txt)|.txt|All Files (.)|*.*";
					saveFileDialog.Title = "Lưu đồ thị";
					saveFileDialog.ShowDialog();


					string path = saveFileDialog.FileName;
					string content = "";
					string content_nd = "";

					content += nodes.Count + "\n";
					content_nd += content;

					foreach (Node node in nodes)
					{
						content_nd += node.X + " " + node.Y + " " + node.Label + "\n";
					}

					foreach (Edge edge in edges)
					{
						content += edge.StartNode.Label + " " + edge.EndNode.Label + " " + '\n';
						content_nd += edge.StartNode.Label + " " + edge.EndNode.Label + " " + "\n";
					}



					if (path != "")
					{
						File.WriteAllText(path, content);
						File.WriteAllText(path + "k", content_nd);
					}
				}
			}

		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			openFileDialog.Filter = "Text Files (*.txtk)|*.txtk|All Files (.)|*.*";
			openFileDialog.Title = "Mở đồ thị";
			openFileDialog.ShowDialog();
			edges.Clear();
			nodes.Clear();
			int count = 0;
			Node begin_node = new Node();
			Node end_node = new Node();
			string[] number;

			if (openFileDialog.FileName != "")
			{
				string[] lines = File.ReadAllLines(openFileDialog.FileName);

				count = Convert.ToInt16(lines[0]);

				for (int i = 1; i <= count; i++)
				{

					number = lines[i].Split(' ');

					Node node = new Node()
					{
						X = Convert.ToInt16(number[0]),
						Y = Convert.ToInt16(number[1]),
						Label = (number[2]),
						Color = Brushes.White
					};

					nodes.Add(node);
				}

				for (int i = count + 1; i < lines.Length; i++)
				{
					number = lines[i].Split(' ');
					begin_node = GetNodeAt(Convert.ToInt16(number[0]));
					end_node = GetNodeAt(Convert.ToInt16(number[1]));

					Edge edge = new Edge()
					{
						StartNode = begin_node,
						EndNode = end_node,


					};

					edges.Add(edge);
				}

				Update();
				Refresh();

			}
		}

		
		

	}
}

