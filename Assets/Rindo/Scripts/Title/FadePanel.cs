using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    float alpha = 0;
    [SerializeField] float speed = 0.01f;
    bool hasStart = false;

    void Start()
    {
        TitleEventManager.Instance.OnTransitioning += () =>
        {
            hasStart = true;
        };
    }

    void Update()
    {
        if (!hasStart) return;
        GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        alpha += speed;
    }
}
