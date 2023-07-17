using UnityEngine;
using Color = UnityEngine.Color;

public class GridColor : MonoBehaviour
{
    public Color defaultColor;
    public Color currentColor;

    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        defaultColor = new Color();
        ColorUtility.TryParseHtmlString("#00FFFF6F", out defaultColor);
        rend.material.color = defaultColor;
    }

    public void UpdateColor(Astar.Walkable isWalkable)
    {
        if (isWalkable == Astar.Walkable.Passable)
        {
            rend.material.color = defaultColor;
        }
        else
        {
            rend.material.color = Color.red;
        }
    }
}

