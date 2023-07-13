using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private Astar.Grid _grid;

    private void Awake()
    {
        _grid = GetComponent<Astar.Grid>(); 
    }
    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }


    private IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        var waypoints = Array.Empty<Vector3>();
        var pathSuccess = false;

        var startNode = _grid.NodeFromWorldPoint(startPos);
        var targetNode = _grid.NodeFromWorldPoint(targetPos);

        if(startNode.Walkable != Astar.Walkable.Passable)
        {
            var neighbors = _grid.GetNeighbours(startNode);
            foreach(var n in  neighbors.Where(n =>n.Walkable == Astar.Walkable.Passable))
            {
                startNode = n;
            }
        }

        if(startNode.Walkable == Astar.Walkable.Passable && targetNode.Walkable == Astar.Walkable.Passable)
        {
            var openSet = new Heap<Astar.Node>(_grid.MaxSize);
            var closeSet = new HashSet<Astar.Node>();

            openSet.Add(startNode);

            while(openSet.Count > 0)
            {
                var currentNode = openSet.RemoveFirst();
                closeSet.Add(currentNode);

                if(currentNode.Equals(targetNode)) 
                {
                    pathSuccess = true;
                    break;
                }

                foreach(var neighbor in _grid.GetNeighbours(currentNode))
                {
                    if(neighbor.Walkable != Astar.Walkable.Passable || closeSet.Contains(neighbor))
                    {
                        continue;
                    }

                    var newMovementCostToNeighbor = currentNode.GCost + GetDistance(currentNode, neighbor) +neighbor.MovementPenalty;
                    if(newMovementCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
                    {
                        neighbor.GCost = newMovementCostToNeighbor;
                        neighbor.HCost = GetDistance(neighbor, targetNode);
                        neighbor.Parent = currentNode;
                        if(!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }
            yield return null;
            if(pathSuccess)
            {
                waypoints = RetracePath(startNode, targetNode);

            }

            //¸®Äù ¸Å´ÏÀú


        }
    }

    private static Vector3[] RetracePath(Astar.Node startNode, Astar.Node endNode)
    {
        var path = new List<Astar.Node>();
        var currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        var waypoints = SimplifyPath(path);
        var smoothedWaypoints = BezierPath(waypoints, 1.0f);
        Array.Reverse(smoothedWaypoints);
        return smoothedWaypoints;
    }


    private static Vector3[] SimplifyPath(List<Astar.Node> path) 
    {
        var waypoints = new List<Vector3>();
        var directionOld = Vector2.zero;

        for(var i =1; i<path.Count; i++)
        {
            var directionNew = new Vector2(path[i - 1].GridX - path[i].GridX, path[i - 1].GridY - path[i].GridY);
            if(directionNew != directionOld)
            {
                waypoints.Add(path[i].WorldPostion + Vector3.up);
            }
            directionOld = directionNew;
        }

        return waypoints.ToArray(); 
    }
    private static Vector3[] BezierPath(Vector3[] waypoints, float smoothness)
    {
        if (waypoints.Length <= 1)
            return waypoints;

        var waypointsLength = 0;
        var curvedLength = 0;

        if (smoothness < 1.0f) smoothness = 1.0f;

        waypointsLength = waypoints.Length;

        curvedLength = (waypointsLength * Mathf.RoundToInt(smoothness)) - 1;
        var smoothedWaypoints = new List<Vector3>(curvedLength);

        for (var pointInTimeOnCurve = 0; pointInTimeOnCurve < curvedLength + 1; pointInTimeOnCurve++)
        {
            var t = Mathf.InverseLerp(0, curvedLength, pointInTimeOnCurve);

            var points = new List<Vector3>(waypoints);

            for (var j = waypointsLength - 1; j > 0; j--)
            {
                for (var i = 0; i < j; i++)
                {
                    points[i] = (1 - t) * points[i] + t * points[i + 1];
                }
            }

            smoothedWaypoints.Add(points[0]);
        }

        return (smoothedWaypoints.ToArray());
    }

    private static int GetDistance(Astar.Node nodeA, Astar.Node nodeB)
    {
        var dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        var dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);
        if(dstX > dstY)
        {
            return 14 *dstY + 10*(dstX - dstY);
        }

        return 14 * dstX + 10 * (dstY - dstX);
    }
}
