using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game;

public class PlayerHp : MonoBehaviour
{

    private Player player;
    public SpriteRenderer spriteRenderer;
    public bool GetHit = false;
    private AudioSource hurtAudio;

    [SerializeField] private int health;
    private int maxHealth;
    [SerializeField] private Image[] heartsImages;
    [SerializeField] private Sprite[] heartsSprites;

    public void Start()
    {
        player = gameObject.GetComponent<Player>();
        maxHealth = health; 
        hurtAudio = GameObject.Find("PlayerHurt").GetComponent<AudioSource>();
    }


    private void Update()
    {
        UpdateHealhBar();
    }

    private void UpdateHealhBar() 
    {
        if (health > maxHealth)
            health = maxHealth;
        var heartsNumber = health / 2;
        for (var i = 0; i < heartsImages.Length; i++)
        {
            if (i < heartsNumber)
                heartsImages[i].sprite = heartsSprites[2];
            else
                heartsImages[i].sprite = heartsSprites[0];
        }
        if (health % 2 == 1)
            heartsImages[heartsNumber].sprite = heartsSprites[1];
    }

    public void TakeHit(int damage)
    {
        health -= damage;
        if (health <= 0)
            RestartLevel();
        hurtAudio.Play();
        StartCoroutine(DamageFlashRed());
    }

    public void GainHealth(int amount) 
    {
        health += amount;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator DamageFlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("�������");
        if (!player.isMoving &&
            collision.gameObject.tag == "Trap" &&
             collision.GetComponent<Thorns>().IsActive) 
        {
            Debug.Log("���������");
            TakeHit(1000);
        }

    }
}
