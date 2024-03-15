using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Demo2
{
    public class Graph
    {
        public int n;
        public int[,] G;

        public Graph(int num)
        {
            this.n = num;
            G = new int[n + 1, n + 1];

            for (int i = 1; i <= num; i++)
                for (int j = 1; j <= num; j++)
                    G[i, j] = 0;
        }
		

		public int Number_Of_Connected_Components()
		{
			bool[] visted = new bool[this.n + 1];
			int num = 0;

			for (int i = 1; i <= this.n; i++)
				visted[i] = false;

			for (int i = 1; i <= this.n; i++)
			{
				if (!visted[i])
				{
					num++;
					List<int> dfs_resut = this.dfs(i);

					foreach (int node in dfs_resut)
						visted[node] = true;
				}
			}

			return num;
		}
		public List<int> Fleury(int start_node)
		{
			// Kiểm tra xem đồ thị có chu trình Euler không
			if (!hasEulerCycle())
				return null;

			List<int> euler_cycle = new List<int>();

			// Tạo một bản sao của ma trận kề để tránh thay đổi ma trận gốc
			int[,] G_copy = (int[,])G.Clone();

			Stack<int> stack = new Stack<int>();
			stack.Push(start_node);

			while (stack.Count > 0)
			{
				int current_node = stack.Peek();
				List<int> neighbors = this.neighbors(current_node);

				bool hasUnvisitedNeighbor = false;

				foreach (int neighbor in neighbors)
				{
					if (G_copy[current_node, neighbor] != 0)
					{
						hasUnvisitedNeighbor = true;

						// Lưu lại cung được thêm vào để xóa sau
						int w = G_copy[current_node, neighbor];

						// Thêm đỉnh kề vào stack và xóa cung nối giữa hai đỉnh này
						stack.Push(neighbor);
						G_copy[current_node, neighbor] = 0;
						G_copy[neighbor, current_node] = 0;

						break;
					}
				}

				if (!hasUnvisitedNeighbor)
				{
					// Nếu không còn đỉnh kề chưa được duyệt thì lấy đỉnh trên cùng của stack
					// để thêm vào chu trình Euler và loại bỏ đỉnh đó khỏi stack
					int node = stack.Pop();
					euler_cycle.Add(node);
				}
			}

			return euler_cycle;
		}

		private bool hasEulerCycle()
		{
			// Đồ thị không liên thông hoặc có ít nhất một đỉnh có bậc lẻ
			if (Number_Of_Connected_Components() > 1)
				return false;

			for (int i = 1; i <= n; i++)
			{
				int degree = 0;
				for (int j = 1; j <= n; j++)
				{
					degree += G[i, j];
				}

				if (degree % 2 != 0)
					return false;
			}

			return true;
		}


		public void addEdge(int node1, int node2)
        {
            G[node1, node2] = 1;
            G[node2, node1] = 1;
        }

        public void removeEdge(int node1, int node2)
        {
            G[node1, node2] = 0;
            G[node2, node1] = 0;
        }


        public bool isAdjacent(int node1, int node2)
        {
            return G[node1, node2] != 0;
        }

        public List<int> neighbors(int node)
        {
            List<int> neighbors = new List<int>();
            for (int i = 1; i <= this.n; i++)
            {
                if (isAdjacent(node, i))
                    neighbors.Add(i);
            }

            return neighbors;
        }

        public List<int> dfs(int start_node)
        {
            List<int> dfs_result = new List<int>();

            bool[] visited = new bool[this.n + 1];
            Stack<int> stack = new Stack<int>();

            for (int i = 1; i <= this.n; i++)
                visited[i] = false;

            visited[start_node] = true;
            stack.Push(start_node);

            while (stack.Count > 0)
            {
                int current_node = stack.Pop();
                dfs_result.Add(current_node);

                List<int> neighbors = this.neighbors(current_node);

                foreach(int neighbor in neighbors)
                {
                    if (!visited[neighbor])
                    {
                        visited[neighbor] = true;
                        stack.Push(neighbor);
                    }
                }
				

			}

            return dfs_result;
           
        }

       
      
    }
	

}
