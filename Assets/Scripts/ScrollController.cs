using UnityEngine;
using System.Collections;

public class ScrollController : MonoBehaviour {

    public float smoothTimeX;
    public float smoothTimeY;
    public bool scrollToPlayer = false;

    public float screenW;
    public float screenH;
    public float scrollSpeed;

    [ReadOnly]
    public GameObject player;
    [ReadOnly]
    public GameObject activeRoom;

    private PlayerMovement _pm;
    private Vector2 velocity;
    private Vector2 bottomRightBound;

    void Start () {
        player = GameObject.Find("Player");
        if (player != null)
            _pm = player.GetComponent<PlayerMovement>();
    }

    public void SetActiveRoom()
    {
        activeRoom = player.GetComponent<ActiveRoom>().activeRoom;
        bottomRightBound = activeRoom.GetComponent<GetRightTopCorner>().rightCornerPosition;
    }

    void Update()
    {
        if (_pm != null && !_pm.dialog)
        {
            transform.position = Vector2.Lerp(transform.position,
                new Vector2(
                    Mathf.Clamp(player.transform.position.x, activeRoom.transform.position.x + screenW / 2, bottomRightBound.x - screenW / 2),
                    Mathf.Clamp(player.transform.position.y, bottomRightBound.y + screenH / 2, activeRoom.transform.position.y - screenH / 2)),
                    0.1f * scrollSpeed * Time.deltaTime);
        }
    }

}
