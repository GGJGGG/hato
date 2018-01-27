using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float powerScale = 1;
    [SerializeField] float frontMoveSpeed = 0.1f;
    [SerializeField] float revivalLine;
    [SerializeField] float revivalCount = 0;
    public static bool faint = false;

    private MeshRenderer mr;
    private GameObject player;
    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerAnim pAnim = player.GetComponent<PlayerAnim>();
    }

    void OnEnable()
    {
        if (InputVoice.Instance != null)
        {
            InputVoice.Instance.OnUpdateVoiceInput += OnUpdateVoiceInput;
        }
    }

    void OnDisable()
    {
        if (InputVoice.Instance != null)
        {
            InputVoice.Instance.OnUpdateVoiceInput -= OnUpdateVoiceInput;
        }
    }

    void OnUpdateVoiceInput(bool isMute, float rate, int freq, float power)
    {
        if (isMute) return;

        Boost(rate);
    }

    void Boost(float rate)
    {
        var moveY = (rate - 0.5f) * powerScale * Time.deltaTime * 60;
        transform.position += new Vector3(0, moveY, 0);
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(frontMoveSpeed, 0, 0) * Time.fixedDeltaTime * 60;

        // 地面に反射した反動で、物理挙動的に上向きなどに進んでいたら力を徐々に打ち消す
        if (rigid.velocity.y > 0)
        {
            rigid.velocity *= 0.8f;
        }
    }

    void Update()
    {
        DebugInput();
        if (faint == true)
        {
            RevivalFaint();
        }
    }

    void DebugInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            frontMoveSpeed += 0.01f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            frontMoveSpeed -= 0.01f;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Boost(1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Boost(0);
        }
    }

    public static void FaintCheck()
    {
        faint = true;
        //pAnim.FiantAnimOn();
    }

    void RevivalFaint()
    {
        revivalCount += 1 * Time.deltaTime;
        mr.material.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
        if (revivalCount >= revivalLine)
        {
            faint = false;
            revivalCount = 0;
            ParticlePlaying.isPlaying = false;
            mr.material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }
}
