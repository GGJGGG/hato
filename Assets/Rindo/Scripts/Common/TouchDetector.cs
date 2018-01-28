using UnityEngine;
using UnityEngine.UI;

public class TouchDetector : Graphic
{
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);
        vh.Clear();
    }
}
