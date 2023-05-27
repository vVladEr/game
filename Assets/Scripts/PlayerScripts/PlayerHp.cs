using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game;
using System;

public class PlayerHp : MonoBehaviour
{

    private Player player;
    private SpriteRenderer spriteRenderer;
    public bool GetHit = false;
    private AudioSource hurtAudio;

    [SerializeField] private int health;
    private int maxHealth;
    [SerializeField] private List<Image> heartsImages;
    [SerializeField] private Sprite[] heartsSprites;
    [SerializeField] private GameObject HealthPanel;

    public void Start()
    {
        player = gameObject.GetComponent<Player>();
        maxHealth = health; 
        hurtAudio = GameObject.Find("PlayerHurt").GetComponent<AudioSource>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar() 
    {
        if (health > maxHealth)
            health = maxHealth;
        var heartsNumber = health / 2;
        for (var i = 0; i < maxHealth / 2; i++)
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

    public void AddHeart() 
    {
        maxHealth = Math.Min(maxHealth + 2,16);
        health += 2;
        heartsImages[(maxHealth-2)/2].sprite = heartsSprites[0];
        heartsImages[(maxHealth - 2) / 2].color = new Color(255, 255, 255, 1f);
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
        if (!player.isMoving) 
        {
            if (collision.gameObject.tag == "Trap" &&
                collision.GetComponent<Thorns>().IsActive)
            {
                TakeHit(1000);
            }
            else if(collision.gameObject.tag == "ExitDoor")
                RestartLevel();
        }


    }
}
