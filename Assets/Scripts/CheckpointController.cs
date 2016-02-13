using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour {

    [ReadOnly]
    public Transform activeCheckpoint;
    [ReadOnly]
    public PlayerMovement _playerMovement;

    private bool canRespawn = true;

    void Start()
    {
        _playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Checkpoint")
            activeCheckpoint = coll.gameObject.transform;
    }

    void Update()
    {
        if (_playerMovement.playerLives == 0 && canRespawn)
            StartCoroutine(RespawnPlayer(1F));
    }

    IEnumerator RespawnPlayer(float waitTime)
    {
        canRespawn = false;
        yield return new WaitForSeconds(waitTime);
        transform.position = activeCheckpoint.position;
        _playerMovement.playerLives = 1;
        canRespawn = true;
    }
}
