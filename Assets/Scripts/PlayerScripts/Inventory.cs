using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public BasicWeapon EquipedWeapon;
    public List<GameObject> Weapons;
    public bool TakeWeaponOnThisTurn = false;


    private void Start()
    {
        EquipedWeapon = gameObject.AddComponent<Dagger>();
        EquipedWeapon.InisialisePlayer();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!TakeWeaponOnThisTurn && collision.gameObject.tag == "Weapon") 
        {
            Instantiate(GetWeaponToDrop(), transform.position, transform.rotation);
            CollectWeapon(collision);
            Destroy(collision.gameObject);
            TakeWeaponOnThisTurn = true;
        }
    }

    private void CollectWeapon(Collider2D coll) 
    {
        var temp = coll.GetComponent<BasicWeapon>();
        switch (temp) 
        {
            case Spear:
                if (!gameObject.GetComponent<Spear>())
                    EquipedWeapon = gameObject.AddComponent<Spear>();
                else
                    EquipedWeapon = gameObject.GetComponent<Spear>();
                break;

            case Axe:
                if (!gameObject.GetComponent<Axe>())
                    EquipedWeapon = gameObject.AddComponent<Axe>();
                else
                    EquipedWeapon = gameObject.GetComponent<Axe>();
                break;

            case Bow:
                if (!gameObject.GetComponent<Bow>())
                    EquipedWeapon = gameObject.AddComponent<Bow>();
                else
                    EquipedWeapon = gameObject.GetComponent<Bow>();
                break;
            default:
                break;
        }
        Debug.Log(EquipedWeapon);
        EquipedWeapon.InisialisePlayer();   
    }

    private GameObject GetWeaponToDrop()
    {
        foreach (var weapon in Weapons) 
        {
            Debug.Log(weapon.GetComponent<BasicWeapon>().Name);
            Debug.Log(EquipedWeapon.Name);
            if (weapon.GetComponent<BasicWeapon>().Name == EquipedWeapon.Name) 
            {
                Debug.Log(weapon.GetComponent<BasicWeapon>().Name);
                return weapon;
            }
        }
        return null;
    }
}
