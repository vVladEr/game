using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    public bool IsActive = false;
    private float deltaTime => 60 / general.BPM;
    private float eps => general.RightTimeWindow;
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer currentSprite;
    private int currentTick = -1;
    private General general;

    private void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        general = GameObject.Find("General").GetComponent<General>();
    }
    // Update is called once per frame
    void Update()
    {
        var curTime = general.Time;
        var tick = (int)(curTime / deltaTime);
        if (tick != currentTick)
        {
            currentTick = tick;
            IsActive = !IsActive;
            if (IsActive)
                currentSprite.sprite = sprites[1];
            else
                currentSprite.sprite = sprites[0];
        }
    }
}