using System;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class TickAppearenceManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource _audio; 

    private void Update()
    {
        animator.SetBool("shouldSmash", player.RightTime);
    }

    public void play_sound()
    {
        _audio.Play();
    }
}
