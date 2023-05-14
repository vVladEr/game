using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public int Hp = 2;

    private void Update()
    {
        if(Hp <= 0)
            Destroy(gameObject);
    }
}
