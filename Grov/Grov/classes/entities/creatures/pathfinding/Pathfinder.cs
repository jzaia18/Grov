using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Grov
{
    class Pathfinder
    {
        #region fields
        // ************* Fields ************* //

        private PriorityQueue<TileNode> openSet;
        private List<TileNode> closedSet;
        private TileNode[,] grid;
        private Point start;
        private Point end;
        private TileNode current;
        #endregion

        #region constructor
        // ************* Constructor ************* //

        public Pathfinder()
        {
            Room room = FloorManager.Instance.CurrRoom;
            int width = 32;
            int height = 18;
            this.grid = new TileNode[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grid[x, y] = new TileNode(x, y, room[x, y]);
                }
            }
            openSet = new PriorityQueue<TileNode>();
            closedSet = new List<TileNode>();
        }
        #endregion

        #region methods
        // ************* Methods ************* //

        /// <summary>
        /// Helper function that checks whether a given point is in bounds and relevant to be checked
        /// </summary>
        public bool SafeToAdd(int x, int y)
        {
            if (x < 0 || y < 0 || x >= grid.GetLength(0) || y >= grid.GetLength(1))
                return false;

            if (closedSet.IndexOf(grid[x,y]) != -1)
                return false;

            if (!grid[x,y].IsPassable)
                return false;

            return true;
        }

        /// <summary>
        /// Overload method to make getting neighbors simpler
        /// </summary>
        /// <param name="p">The point in the graph to check</param>
        /// <returns>A list of the point's neighbors</returns>
        public List<TileNode> GetNeightbors(Point p)
        {
            return GetNeighbors(p.X, p.Y);
        }

        /// <summary>
        /// Gets the relevant neighbors at a given index in the graph
        /// </summary>
        /// <returns>A list of the point's neighbors</returns>
        public List<TileNode> GetNeighbors(int x, int y)
        {
            List<TileNode> ret = new List<TileNode>();

            if (SafeToAdd(x - 1, y - 1)) ret.Add(grid[x - 1, y - 1]);
            if (SafeToAdd(x - 1, y)) ret.Add(grid[x - 1, y]);
            if (SafeToAdd(x - 1, y + 1)) ret.Add(grid[x - 1, y + 1]);
            if (SafeToAdd(x, y - 1)) ret.Add(grid[x, y - 1]);
            if (SafeToAdd(x, y + 1)) ret.Add(grid[x, y + 1]);
            if (SafeToAdd(x + 1, y - 1)) ret.Add(grid[x + 1, y - 1]);
            if (SafeToAdd(x + 1, y)) ret.Add(grid[x + 1, y]);
            if (SafeToAdd(x + 1, y + 1)) ret.Add(grid[x + 1, y + 1]);

            return ret;
        }

        /// <summary>
        /// Calculates the heuristic from a given point to the end
        /// </summary>
        /// <returns>The heuristic distance</returns>
        public int Heuristic(int x, int y)
        {
            x = Math.Abs(end.X - x);
            y = Math.Abs(end.Y - y);
            return x + y;
        }

        /// <summary>
        /// Runs the algorithm synchronously
        /// </summary>
        public List<Tile> GetPathToTarget(Vector2 self, Vector2 target)
        {
            this.start = new Point((int) (self.X / FloorManager.TileWidth), (int) (self.Y / FloorManager.TileHeight));
            this.end = new Point((int)(target.X / FloorManager.TileWidth), (int)(target.Y / FloorManager.TileHeight));

            Start();
            while (!Step()) /*While not finished, do a step*/;

            List<Tile> ret = new List<Tile>();

            TileNode current = closedSet[closedSet.Count - 1];
            while (current != null)
            {
                ret.Add(current.Tile);
                current = current.PathNeighbor;
            }

            ret.Reverse();

            return ret;
        }

        /// <summary>
        /// Set up necessary node for running the algorithm
        /// </summary>
        public void Start()
        {
            current = grid[start.X, start.Y];
            current.H = 0;
            current.G = 0;
        }

        /// <summary>
        /// Makes one "step" of the algorithm, checks the current node, gets its neighbor and selects the next node
        /// </summary>
        /// <returns>A boolean representing if the algorithm has finished</returns>
        public bool Step()
        {
            List<TileNode> currNeighbors = GetNeighbors(current.X, current.Y);

            foreach (TileNode neighbor in currNeighbors)
            {
                if (neighbor.Checked)
                {
                    if (current.G + 1 < neighbor.G)
                    {
                        neighbor.G = current.G + 1;
                        neighbor.PathNeighbor = current;

                        openSet.ModifyPriority(neighbor.F, neighbor);
                    }
                }
                else
                {
                    neighbor.Checked = true;
                    neighbor.G = current.G + 1;
                    neighbor.H = Heuristic(neighbor.X, neighbor.Y);
                    openSet.Enqueue(neighbor.F, neighbor);
                }
            }

            if (openSet.IsEmpty)
                return true;

            current = openSet.Dequeue();
            closedSet.Add(current);

            return current == grid[end.X, end.Y];
        }
        #endregion
    }
}
