using System;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class TickAppearenceManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audio; 

    private void Update()
    {
        animator.SetBool("shouldSmash", player.RightTime);
    }

    public void play_sound()
    {
        audio.Play();
    }
}
