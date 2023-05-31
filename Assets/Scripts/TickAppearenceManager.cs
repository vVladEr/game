using System;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class TickAppearenceManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audio; 
    
    [SerializeField]
    [Range(-0.45f, 0.45f)]
     private float anvilAnimationOffset = 0.05f;
    [SerializeField]
    [Range(-0.45f, 0.45f)]
     private float anvilSoundOffset = 0.05f;
    private float DeltaTime => 60 / General.BPM;
    [SerializeField] private General General;
    public bool isSoundPlayed = false;
    public bool isAnimationPlayed = false;
    private float eps = 0.05f;

    private void Update()
    {    
        var currentTime = General.Time - anvilSoundOffset;
        if (currentTime % DeltaTime < eps)
            {
                if (!isSoundPlayed)
                {
                    _audio.Play();
                    isSoundPlayed = true;
                }
            }
        else isSoundPlayed = false;
        
        currentTime = General.Time - anvilAnimationOffset;
        if (currentTime % DeltaTime < eps)
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
