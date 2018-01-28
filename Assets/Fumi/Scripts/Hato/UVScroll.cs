using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScroll : MonoBehaviour
{
    public Vector2 scrollUV;
    public string uvName = "_MainTex";

    Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        var uvOffset = mat.GetTextureOffset(uvName);
        uvOffset += scrollUV * Time.deltaTime;
        mat.SetTextureOffset(uvName, uvOffset);
    }
}
