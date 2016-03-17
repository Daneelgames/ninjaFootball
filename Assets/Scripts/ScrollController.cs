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
        else // if (bottomRightBound == null)
            bottomRightBound = activeRoom.gameObject.transform.Find("RightTopCorner").transform.position;

        if (!_pm.dialog && activeRoom != null && bottomRightBound != null)
        {
            //transform.position = Vector2.Lerp(transform.position, new Vector2 (player.transform.position.x + _pm.hAxis * 2, player.transform.position.y + 1 + _pm.vAxis * 2), 0.1f * scrollSpeed * Time.deltaTime);
             transform.position = Vector2.Lerp(transform.position,
                 new Vector2(
                     Mathf.Clamp(player.transform.position.x, activeRoom.transform.position.x + screenW / 2, bottomRightBound.x - screenW / 2),
                     Mathf.Clamp(player.transform.position.y, bottomRightBound.y + screenH / 2, activeRoom.transform.position.y - screenH / 2)),
                     0.1f * scrollSpeed * Time.deltaTime);
        }
    }

}
