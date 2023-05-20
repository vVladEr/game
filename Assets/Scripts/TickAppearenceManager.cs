using Game;
using UnityEngine;
using UnityEngine.UI;

public class TickAppearenceManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Image TickImage;
    [SerializeField] private Sprite[] TickSprites;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (player.RightTime)
            TickImage.sprite = TickSprites[0];
        else
            TickImage.sprite = TickSprites[1];
    }
}
