using UnityEngine;
using System.Collections;

public class OrginMuderMovement : MonoBehaviour {

    public bool inBattle = false;

    [SerializeField]
    private GameObject explosionHolder;
    [SerializeField]
    private GameObject explosion;


    [SerializeField]
    private float jumpPower = 1;
    [SerializeField]
    private float minT = 0.5f;
    [SerializeField]
    private float maxT = 4.5f;

    [SerializeField]
    private float timer = 2f;

    [SerializeField]
    private EnemyHealth _healthScript;

    private BossHealthbarController _healthbarScript;
    private GameObject player;
    private PlayerMovement pm;
    private Animator _animator;
    private Rigidbody2D _rb;

    enum State {Idle, RightSide, RightDown, LeftSide, LeftDown};

    [SerializeField]
    private State bossState = State.Idle;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        pm = player.GetComponent<PlayerMovement>();

        _healthbarScript = GameObject.Find("BossHealthbar").GetComponent<BossHealthbarController>();
        _healthbarScript.maxBossHealth = _healthScript.health;
    }

    void Update ()
    {
        if (pm.playerLives <= 0)
            Destroy(gameObject, 1f);

        if (_healthScript != null)
            _healthbarScript.curBossHealth = _healthScript.health;

        if (inBattle && _healthScript.health > 0)
            StatesManager();
    }

    void StatesManager()
    {
        if (bossState == State.Idle)
        {
            if (timer > 0)
                timer -= 1f * Time.deltaTime;
            else if (timer <= 0)
            {
                timer = Random.Range(minT, maxT);
                if (player.transform.position.y < transform.position.y + 13f)
                {
                    if (player.transform.position.x >= transform.position.x)
                        bossState = State.RightDown;
                    else
                        bossState = State.LeftDown;
                }
                else
                {
                    if (player.transform.position.x >= transform.position.x)
                        bossState = State.RightSide;
                    else
                        bossState = State.LeftSide;
                }
            }
        }
        else
            StartCoroutine(ReturnToIdle());

        if (bossState == State.LeftDown)
            _animator.SetTrigger("LD");
        if (bossState == State.LeftSide)
            _animator.SetTrigger("LS");
        if (bossState == State.RightDown)
            _animator.SetTrigger("RD");
        if (bossState == State.RightSide)
            _animator.SetTrigger("RS");
    }

    public void Sidejump()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (player.transform.position.x < transform.position.x)
            _rb.AddForce(new Vector2(jumpPower * distance/2 * -1, 0f), ForceMode2D.Impulse);
        else
            _rb.AddForce(new Vector2(jumpPower * distance/2, 0f), ForceMode2D.Impulse);
    }

    public void SwordHitGround()
    {
        Instantiate(explosion, explosionHolder.transform.position, explosionHolder.transform.rotation);
    }

    IEnumerator ReturnToIdle()
    {
        yield return new WaitForSeconds(1f);
        bossState = State.Idle;
    }

}
