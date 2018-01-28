using System.Collections;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public float fadeTime = 1f;

    public bool hasStarted { get; set; }

    private float currentRemainTime;
    private SpriteRenderer spRenderer;

    // Use this for initialization
    void Start()
    {
        // 初期化
        currentRemainTime = fadeTime;
        spRenderer = GetComponent<SpriteRenderer>();
        hasStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            return;
        }

        // 残り時間を更新
        currentRemainTime -= Time.deltaTime;

        if (currentRemainTime <= 0f)
        {
            // 残り時間が無くなったら自分自身を消滅
            GameObject.Destroy(gameObject);
            return;
        }

        // フェードアウト
        float alpha = currentRemainTime / fadeTime;
        var color = spRenderer.color;
        color.a = alpha;
        spRenderer.color = color;
    }

    public void StartFadeOut()
    {
        hasStarted = true;
    }
}
