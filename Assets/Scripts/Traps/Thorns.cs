using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    public bool IsActive = false;
    [SerializeField] private float deltaTime;
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer currentSprite;
    private int currentTick = -1;

    private void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        var tick = (int)(Time.time / deltaTime);
        if (tick != currentTick) 
        {
            currentTick = tick;
            if (tick % 2 == 1)
                IsActive = true;
            else
                IsActive = false;
            currentSprite.sprite = sprites[tick % 2];
        }
    }
}
