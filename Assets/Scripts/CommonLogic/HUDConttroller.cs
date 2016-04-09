using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDConttroller : MonoBehaviour {

    public Sprite[] heartSprites;
    public Image heartUI;

    private PlayerMovement player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        heartUI.sprite = heartSprites[player.playerLives];
    }
}
