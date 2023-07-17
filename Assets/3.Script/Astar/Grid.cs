using System.Collections.Generic;
using UnityEngine;

namespace Astar
{
    public class Grid : MonoBehaviour
    {
        public bool DisplayGridGizmos;
        public LayerMask UnwalkableMask;
        public Vector2 GridWorldSize;
        public float NodeRadius;
        public TerrainType[] WalkableRegions;
        public float CheckRadiusModifier = 2;
        public float TerrainOffset = 3;

        private LayerMask _walkalbeMask;
        private readonly Dictionary<int, int> _walkableRegionDictionary = new();

        public Astar.Node[,] grid;

        protected float NodeDiameter;
        protected int GridSizeX, GridSizeY;
        public int MaxSize => GridSizeX * GridSizeY;

        private void Awake()
        {
            NodeDiameter = NodeRadius * 2;
            GridSizeX = Mathf.RoundToInt(GridWorldSize.x / NodeDiameter);
            GridSizeY = Mathf.RoundToInt(GridWorldSize.y / NodeDiameter);


            foreach (var region in WalkableRegions)
            {
                _walkalbeMask.value |= region.terrainMask.value;
                _walkableRegionDictionary.Add((int)Mathf.Log((float)region.terrainMask.value, 2f), region.terrainPenalty);
            }
            CreateGrid();
        }

        void CreateGrid()
        {
            grid = new Astar.Node[GridSizeX, GridSizeY];
            var worldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;

            for (var x = 0; x < GridSizeX; x++)
            {
                for (var y = 0; y < GridSizeY; y++)
                {
                    var worldPoint = worldBottomLeft + Vector3.right * (x * NodeDiameter * NodeRadius)
                        + Vector3.forward * (y * NodeDiameter * NodeRadius);
                    var walkable = !(Physics.CheckSphere(worldPoint, NodeRadius * CheckRadiusModifier, UnwalkableMask));

                    var movementPenalty = 0;
                    float height = 0;

                    if (walkable)
                    {
                        var ray = new Ray(worldPoint + Vector3.up * 500, Vector3.down);
                        if (Physics.Raycast(ray, out var hit, 500, _walkalbeMask))
                        {
                            _walkableRegionDictionary.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);
                            worldPoint.y = height = (hit.point.y) - 0.25f;
                        }
                    }

                    var walkableEnum = Astar.Walkable.Passable;
                    if (!walkable)
                    {
                        walkableEnum = Astar.Walkable.Impassable;
                    }

                    grid[x, y] = new Node(walkableEnum, worldPoint, x, y, height, movementPenalty);

                    DrawGrid(grid[x, y]);
                }
            }

        }
        public List<Astar.Node> GetNeighbours(Astar.Node node)
        {
            var neightbours = new List<Astar.Node>();
            // 주변 노드 탐색
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    var checkX = node.GridX + x;
                    var checkY = node.GridY + y;
                    //그리드 밖으로 나가지 않게
                    if (checkX >= 0 && checkX < GridSizeX && checkY >= 0 && checkY < GridSizeY)
                    {
                        neightbours.Add(grid[checkX, checkY]);
                    }
                }
            }
            return neightbours;
        }

        /// <summary>
        /// 월드 좌표로 그에 해당하는 노드 찾기
        /// </summary>
        /// <param name="worldPosiotion"></param>
        /// <returns></returns>
        public Astar.Node NodeFromWorldPoint(Vector3 worldPosiotion)
        {
            var percentX = (worldPosiotion.x + GridWorldSize.x / 2) / GridWorldSize.x;
            var percentY = (worldPosiotion.z + GridWorldSize.y / 2) / GridWorldSize.y;
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            var x = Mathf.RoundToInt((GridSizeX - 1) * percentX);
            var y = Mathf.RoundToInt((GridSizeY - 1) * percentY);
            return grid[x, y];
        }

        public virtual void DrawGrid(Astar.Node node) { }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));
            if (grid == null || DisplayGridGizmos)
            {
                return;
            }
            foreach (var n in grid)
            {
                Gizmos.color = (n.Walkable == Astar.Walkable.Passable) ? Color.blue : Color.red;
                Gizmos.DrawCube(n.WorldPostion, Vector3.one * (NodeDiameter - (NodeDiameter - .1f)));
            }

        }


    }

    [System.Serializable]
    public class TerrainType
    {
        public LayerMask terrainMask;
        public int terrainPenalty;
    }
}
