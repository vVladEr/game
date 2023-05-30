using System;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class TickAppearenceManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audio; 
    
    [SerializeField] private float epsForAnvilAnimation = 0.05f;
    [SerializeField] private float epsForAnvilSound = 0.05f;
    private float DeltaTime => 60 / General.BPM;
    [SerializeField] private General General;
    public bool isSoundPlayed = false;
    public bool isAnimationPlayed = false;
    

    private void Update()
    {    
        var currentTime = General.Time;
        if (currentTime % DeltaTime < epsForAnvilSound)
            {
                if (!isSoundPlayed)
                {
                    _audio.Play();
                    isSoundPlayed = true;
                }
            }
        else isSoundPlayed = false;
        
        currentTime = General.Time;
        if (currentTime % DeltaTime > DeltaTime - epsForAnvilAnimation)
            {
                if (!isAnimationPlayed)
                {
                    _animator.Play("anvil_hit");
                    isAnimationPlayed = true;
                }
            }
        else isAnimationPlayed = false;
    }
}
