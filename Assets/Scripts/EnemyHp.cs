using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public int Hp = 2;
    public GameObject Hitmarker;
    public bool GetHit = false;

    public void TakeHit(int damage)
    {
        var clone = Instantiate(Hitmarker, transform.position, transform.rotation);
        Hp -= damage;
        if (Hp <= 0)
            Destroy(gameObject);
        Destroy(clone);
    }
}
