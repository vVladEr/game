using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int Hp = 2;
    public GameObject Hitmarker;
    public bool GetHit = false;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeHit(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
            StartCoroutine(DestroyEnemy());
        else StartCoroutine(DamageFlashRed());
    }

    private IEnumerator DestroyEnemy()
    {
        spriteRenderer.color = Color.black;
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    private IEnumerator DamageFlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
