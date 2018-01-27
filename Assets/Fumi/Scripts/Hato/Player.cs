using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float powerScale = 1;
    [SerializeField] float frontMoveSpeed = 0.1f;
    [SerializeField] float revivalLine;
    [SerializeField] float revivalCount = 0;
    [SerializeField] float heightLimit = 8.6f;
    [SerializeField] ParticleSystem trailEffect;
    [SerializeField] ParticleSystem deadEffect;
    [SerializeField] GameObject playerModelRoot;

    [SerializeField] Bomb bomb;
    [SerializeField] AudioClip deadClip;

    public bool faint = false;

    bool isOperable; //操作可能かどうか

    PlayerAnim pAnim;
    GameObject pl;
    GameObject pig;

    Rigidbody rigid;

    float reserveMoveHeight;   //移動予定の高さ

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        pl = GameObject.FindGameObjectWithTag("Player");
        pig = GameObject.FindGameObjectWithTag("pigeon");
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
        reserveMoveHeight += moveY;
    }

    void FixedUpdate()
    {
        var newPos = transform.position;
        var newHeight = Mathf.Min(transform.position.y + reserveMoveHeight, heightLimit);
        reserveMoveHeight = 0;
        newPos.y = newHeight;
        newPos.x += frontMoveSpeed * Time.fixedDeltaTime * 60;
        transform.position = newPos;

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
            rigid.AddForce(new Vector3(0.0f, -1.0f, 0.0f));
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
        PlayerAnim pAnim = pig.GetComponent<PlayerAnim>();
        pAnim.FiantAnimOn();
    }

    void RevivalFaint()
    {
        revivalCount += 1 * Time.deltaTime;
        if (revivalCount >= revivalLine)
        {
            ParticlePlaying pp = pl.GetComponent<ParticlePlaying>();
            faint = false;
            revivalCount = 0;
            pp.isPlaying = false;
            PlayerAnim pAnim = pig.GetComponent<PlayerAnim>();
            pAnim.FiantAnimOff();      
            rigid.velocity = Vector3.zero;
        }
    }
}
