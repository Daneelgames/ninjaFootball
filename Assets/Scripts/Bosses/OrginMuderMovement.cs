using UnityEngine;
using System.Collections;

public class OrginMuderMovement : MonoBehaviour {

    public bool inBattle = false;

    [SerializeField]
    private float jumpPower = 1;
    [SerializeField]
    private float minT = 0.5f;
    [SerializeField]
    private float maxT = 4.5f;

    private float timer = 2f;

    private GameObject player;
    private Animator _animator;
    private Rigidbody2D _rb;

    private string state = "Idle";

    enum State {Idle, RightSide, RightDown, LeftSide, LeftDown};

    [SerializeField]
    private State bossState = State.Idle;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void Update ()
    {
        if (inBattle)
            StatesManager();
    }

    void StatesManager()
    {
        if (bossState == State.Idle)
        {
            if (timer > 0)
                timer -= 1f * Time.deltaTime;
            else
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

    public void Sidejump(string direction)
    {
        if (direction == "Left")
            _rb.AddForce(new Vector2(jumpPower * -1, 0f), ForceMode2D.Impulse);
        else if (direction == "Right")
            _rb.AddForce(new Vector2(jumpPower, 0f), ForceMode2D.Impulse);
    }

    public void SwordHit(Vector2 position)
    {

    }

    IEnumerator ReturnToIdle()
    {
        yield return new WaitForSeconds(1f);
        bossState = State.Idle;
    }

}
