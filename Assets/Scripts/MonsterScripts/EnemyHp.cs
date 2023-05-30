using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public int Hp = 2;
    public bool GetHit = false;
    public bool IsAlive = true;
    [SerializeField] GameObject coin;
    private bool isVase = false;
    
    [SerializeField] public float FlashTime = 0.2f;

    public void Start()
    {
        coin = GameObject.FindGameObjectsWithTag("Coin")[0];
        spriteRenderer = GetComponent<SpriteRenderer>();
        isVase = GetComponent<Vase>();
    }

    public void TakeHit(int damage)
    {
        Hp -= damage;
        if (Hp <= 0) 
        {
            IsAlive = false;
            if(!isVase)
                Instantiate(coin, transform.position, transform.rotation);
            StartCoroutine(DestroyEnemy());
        }
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Check");
        if (collision.gameObject.tag == "Trap" && collision.GetComponent<Thorns>().IsActive)
        {
                TakeHit(1000);
        }
    }
}
