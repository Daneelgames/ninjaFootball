using UnityEngine;
using System.Collections;

public class OrginMuderMovement : MonoBehaviour {

    public bool inBattle = false;


    [SerializeField]
    private GameObject spikes;
    [SerializeField]
    private GameObject spikesHolder;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject explosionHolder;
    [SerializeField]
    private GameObject darkCang2;
    [SerializeField]
    private Vector3 darkCangPosition;
    [SerializeField]
    private float jumpPower = 1;

    [SerializeField]
    private EnemyHealth _healthScript;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip victoryTheme;

    private GameObject healthbar;
    private BossHealthbarController _healthbarScript;
    private GameObject player;
    private PlayerMovement pm;
    private Animator _animator;
    private Rigidbody2D _rb;

    private float timer = 0f;
    private float minT = 0f;
    private float maxT = 2f;

    private bool canSpikes = false;
    private bool dead = false;

    enum State {Idle, RightSide, RightDown, LeftSide, LeftDown, Spikes};

    [SerializeField]
    private State bossState = State.Idle;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        pm = player.GetComponent<PlayerMovement>();
        healthbar = GameObject.Find("BossHealthbar");
        _healthbarScript = healthbar.GetComponent<BossHealthbarController>();
    }

    void Update ()
    {
        if (inBattle)
        {
            if (!_audioSource.isPlaying)
                _audioSource.Play();

            if (_healthbarScript.maxBossHealth == 0)
                 _healthbarScript.maxBossHealth = _healthScript.health;

            _healthbarScript.curBossHealth = _healthScript.health;

            if (_healthScript.health > 0)
            {
                StatesManager();
            }
            else
            {
                BossKilled();
            }
        }

        if (pm.playerLives <= 0)
            Destroy(gameObject, 1f);
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
                    float attackChoose = Random.Range(0f, 1f);
                    if (player.transform.position.x >= transform.position.x)
                        {
                            if (attackChoose < 0.2)
                                bossState = State.RightSide;
                            else if (attackChoose > 0.8)
                                bossState = State.RightDown;
                            else
                            {
                                if (_healthScript.health < 50 && canSpikes)
                                    {
                                        bossState = State.Spikes;
                                        canSpikes = false;
                                    }
                            else
                                    {
                                        bossState = State.RightSide;
                                        canSpikes = true;
                                    }
                            }
                        }
                    else
                        {
                            if (attackChoose < 0.2)
                                bossState = State.LeftSide;
                            else if (attackChoose > 0.9)
                                bossState = State.LeftDown;
                            else
                            {
                                if (_healthScript.health < 50 && canSpikes)
                                    {
                                        bossState = State.Spikes;
                                        canSpikes = false;
                                    }
                                else
                                    {
                                        bossState = State.LeftSide;
                                        canSpikes = true;
                                    }
                            }
                    }
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
        if (bossState == State.Spikes)
           _animator.SetTrigger("Spikes");
    }

    public void Sidejump()
    {
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, transform.position.y));

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

    void BossKilled()
    {
        if (!dead)
        {
            Instantiate(darkCang2, darkCangPosition, transform.rotation);
            _audioSource.clip = victoryTheme;
            _audioSource.Play();
            dead = true;
            _animator.SetBool("Dead", true);
            StartCoroutine("GiveNewWeapon");
        }
    }

    IEnumerator GiveNewWeapon()
    {
        yield return new WaitForSeconds(2F);
        Weapon playerWeapon = GameObject.Find("Player").GetComponent<Weapon>() as Weapon;
        playerWeapon.GetNewWeapon("MUDERSWORD");
    }

    void OnDestroy()
    {
        _healthbarScript.maxBossHealth = 0;
        _healthbarScript.curBossHealth = 0;
    }
}
