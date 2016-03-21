using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChestController : MonoBehaviour {

    enum DropType { Ammo, Weapon, Money, Life };

    [SerializeField]
    private GameObject weaponDrop;
    [SerializeField]
    private GameObject[] weapons;
    [SerializeField]
    private GameObject ammoDrop;
    [SerializeField]
    private GameObject moneyDrop;
    [SerializeField]
    private GameObject lifeDrop;

    private DropType dropType;
    private int ammoAmount;
    private AudioSource _audio;
    private Animator _animator;
    private Collider2D _collider;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _audio = GetComponent<AudioSource>() as AudioSource;
        _collider = gameObject.GetComponent<Collider2D>();

        SetDrop();
    }

    void SetDrop()
    {
        dropType = Random.value < .5 ? DropType.Ammo : DropType.Weapon;
    }

    void OnCollisionEnter2D(Collision2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            OpenChest();
            Physics2D.IgnoreCollision(_collider, player.collider);
        }
    }

    public void OpenChest()
    {
        _audio.Play();
        _animator.SetTrigger("Open");

        switch (dropType){
            case DropType.Weapon:
                GameObject lastWeaponDrop = Instantiate(weaponDrop, transform.position, transform.rotation) as GameObject;
                lastWeaponDrop.GetComponent<WeaponDropController>().weapon = weapons[Random.Range(0, weapons.Length)];
                break;

            case DropType.Ammo:
                ammoAmount = Random.Range(25, 50);
                GameObject lastAmmoDrop = Instantiate(ammoDrop, transform.position, transform.rotation) as GameObject;
                lastAmmoDrop.GetComponent<DropController>().amount = ammoAmount;
                break;
        }

        
    }
}