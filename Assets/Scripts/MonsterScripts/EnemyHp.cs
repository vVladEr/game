using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public int Hp = 2;
    public bool GetHit = false;
    
    [SerializeField] public float FlashTime = 0.2f;

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
        yield return new WaitForSeconds(FlashTime);
        Destroy(gameObject);
    }

    private IEnumerator DamageFlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(FlashTime);
        spriteRenderer.color = Color.white;
    }
}
