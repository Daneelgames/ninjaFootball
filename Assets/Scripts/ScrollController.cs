using UnityEngine;
using System.Collections;

public class ScrollController : MonoBehaviour {

    public float smoothTimeX;
    public float smoothTimeY;

    [ReadOnly]
    public GameObject cameraAim;
    [ReadOnly]
    public GameObject zone;
    public float offsetX = 1f;
    public float offsetTop = 1f;
    public float offsetBottom = 2f;

    private Vector2 velocity;
    public bool scrollToPlayer = false;

    void Start () {
        cameraAim = GameObject.Find("CameraAim");
        zone = GameObject.Find("Zone");
    }

    void Update () {
        if (scrollToPlayer)
            Scroll();

        GetPlayerOffset();
    }

    void GetPlayerOffset()
    {
        float playerX = cameraAim.transform.position.x;
        float playerY = cameraAim.transform.position.y;
        float zoneX = zone.transform.position.x;
        float zoneY = zone.transform.position.y;

        if (playerX > zoneX + offsetX || playerX < zoneX - offsetX || playerY > zoneY + offsetTop || playerY < zoneY - offsetBottom)
            scrollToPlayer = true;
        else
            scrollToPlayer = false;
    }

    void Scroll()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, cameraAim.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, cameraAim.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }

}
