using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHp : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int Hp = 2;
    public GameObject Hitmarker;
    public bool GetHit = false;
    private AudioSource hurtAudio;

    public void Start()
    {
        hurtAudio = GameObject.Find("PlayerHurtSound").GetComponent<AudioSource>();
    }

    public void TakeHit(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
            RestartLevel();
        hurtAudio.Play();
        StartCoroutine(DamageFlashRed());
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
}
