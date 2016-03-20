using UnityEngine;
using System.Collections;

public class ScrollController : MonoBehaviour {

    public float smoothTimeX;
    public float smoothTimeY;
    public bool scrollToPlayer = false;

    public float screenW;
    public float screenH;
    public float scrollSpeed;

    private GameObject player;
    [SerializeField]
    private GameObject activeRoom = null;
    private PlayerMovement _pm;
    private Vector2 velocity;
    [SerializeField]
    private Vector2 bottomRightBound;
    [SerializeField]
    private float offset = 0;
    private float offsetTime = 1f;

    void Start () {
        player = GameObject.Find("Player");
        _pm = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (activeRoom == null)
        {
            activeRoom = GameObject.FindGameObjectWithTag("Room");
        }
        else
            bottomRightBound = activeRoom.gameObject.transform.Find("RightTopCorner").transform.position;

        if (!_pm.dialog && activeRoom != null && bottomRightBound != null)
        {
            Scroll();
            VerticalOffset();
        }
        else
        {
            offsetTime = 1;
            offset = 0;
        }
            
    }

    void VerticalOffset()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                if (offsetTime > 0)
                    offsetTime -= 5 * Time.deltaTime;

                else if (offsetTime <= 0)
                {
                    offsetTime = 0;
                    if (Input.GetAxisRaw("Vertical") > 0)
                        offset = 4;
                    else if (Input.GetAxisRaw("Vertical") < 0)
                        offset = -4;
                }
            }
            else
            {
                offsetTime = 1;
                offset = 0;
            }
        }
        else
        {
            offsetTime = 1;
            offset = 0;
        }
            
    }

    void Scroll()
    {
        transform.position = Vector2.Lerp(transform.position,
                 new Vector2(
                     Mathf.Clamp(player.transform.position.x, activeRoom.transform.position.x + screenW / 2, bottomRightBound.x - screenW / 2), 
                     Mathf.Clamp(player.transform.position.y + offset, bottomRightBound.y + screenH / 2, activeRoom.transform.position.y - screenH / 2)),
                     0.1f * scrollSpeed * Time.deltaTime);
    }
}