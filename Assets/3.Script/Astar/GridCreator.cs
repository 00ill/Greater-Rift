using Astar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : Astar.Grid
{
    public GameObject NodeMesh;

    public override void DrawGrid(Node node)
    {
        if(node == null)
        {
            return;
        }

        var pos = node.WorldPostion;

        pos.y += 0.1f;
        var nodeInstance = Instantiate(NodeMesh, pos, transform.rotation) as GameObject;
        nodeInstance.transform.localScale = Vector3.one * (NodeDiameter - 0.1f) * 0.1f;
        node.NodeMesh  =nodeInstance;
        if(node.NodeMesh.GetComponent<GridColor>())
        {
            node.NodeMesh.GetComponent<GridColor>().UpdateColor(node.Walkable);
        }
    }

}
