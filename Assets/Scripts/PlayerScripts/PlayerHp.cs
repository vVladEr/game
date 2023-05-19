using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHp : MonoBehaviour
{
    public int Hp = 2;
    public GameObject Hitmarker;
    public bool GetHit = false;

    public void TakeHit(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
            RestartLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
