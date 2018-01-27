using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float powerScale = 1;
    [SerializeField] float frontMoveSpeed = 0.1f;
    [SerializeField] float revivalLine;
    [SerializeField] float revivalCount = 0;
    [SerializeField] ParticleSystem trailEffect;
    [SerializeField] ParticleSystem deadEffect;
    [SerializeField] GameObject playerModelRoot;

    [SerializeField] Bomb bomb;
    [SerializeField] AudioClip deadClip;

    public static bool faint = false;

    bool isOperable; //操作可能かどうか

    MeshRenderer mr;

    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();

        // TODO あとでタイミング変更するかも
        EnableOperation();
    }

    public void EnableOperation()
    {
        isOperable = true;
    }

    void OnEnable()
    {
        if (InputVoice.Instance != null)
        {
            InputVoice.Instance.OnUpdateVoiceInput += OnUpdateVoiceInput;
        }
        GameEventManager.Instance.OnPlayerDead += OnPlayerDead;
    }

    void OnDisable()
    {
        if (InputVoice.Instance != null)
        {
            InputVoice.Instance.OnUpdateVoiceInput -= OnUpdateVoiceInput;
        }
        GameEventManager.Instance.OnPlayerDead -= OnPlayerDead;
    }

    void OnUpdateVoiceInput(bool isMute, float rate, int freq, float power)
    {
        if (isMute) return;
        if (!isOperable) return;

        Boost(rate);
    }

    // ハトはバームクーヘン(ボム)を届ける指名がある
    // バームクーヘンを無くしたハトは自信をなくして自爆する。
    void OnPlayerDead()
    {
        EmitDeadParticle();
        Invoke("EmitDeadParticle", 0.2f);
        Invoke("EmitDeadParticle", 0.4f);

        var emission = trailEffect.emission;
        emission.enabled = false;
        playerModelRoot.SetActive(false);
        var col = GetComponent<Collider>();
        col.enabled = false;
        frontMoveSpeed = 0;
        isOperable = false;
        rigid.velocity = Vector3.zero;
        rigid.rotation = Quaternion.identity;
        bomb.Detach();
    }

    void EmitDeadParticle()
    {
        deadEffect.Emit(8);

        var audio = GetComponent<AudioSource>();
        audio.PlayOneShot(deadClip);
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
        #if UNITY_EDITOR
        DebugInput();
        #endif

        if (faint)
        {
            RevivalFaint();
        }
    }

    void DebugInput()
    {
        if (!isOperable) return;

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

    public void FaintCheck()
    {
        faint = true;
    }

    void RevivalFaint()
    {
        revivalCount += 1 * Time.deltaTime;
        mr.material.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
        if (revivalCount >= revivalLine)
        {
            faint = false;
            revivalCount = 0;
            mr.material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }
}
