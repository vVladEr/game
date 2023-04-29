using UnityEngine;
namespace Game 
{
    public class Player : MonoBehaviour
    {
        [SerializeField] public Shadow leftShadow;
        [SerializeField] public Shadow rightShadow;
        [SerializeField] public Shadow topShadow;
        [SerializeField] public Shadow bottomShadow;
        public Vector2 InitialPosition = new Vector2(0.5f, 0.5f);
        public Vector2 PreviousPosition;
        // Start is called before the first frame update
        void Start()
        {
            transform.position = InitialPosition;
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 currentPosition = transform.position;
            Vector2 newPosition = currentPosition;
            if (Input.GetKeyDown(KeyCode.D) && rightShadow.NotCollided)
            {
                PreviousPosition = currentPosition;
                newPosition = new(currentPosition.x + 1f, currentPosition.y);
            }

            if (Input.GetKeyDown(KeyCode.A) && leftShadow.NotCollided)
            {
                PreviousPosition = currentPosition;
                newPosition = new(currentPosition.x - 1f, currentPosition.y);
            }

            if (Input.GetKeyDown(KeyCode.W) && topShadow.NotCollided)
            {
                PreviousPosition = currentPosition;
                newPosition = new(currentPosition.x, currentPosition.y + 1f);
            }

            if (Input.GetKeyDown(KeyCode.S) && bottomShadow.NotCollided)
            {
                PreviousPosition = currentPosition;
                newPosition = new(currentPosition.x, currentPosition.y - 1f);
            }
            transform.position = newPosition;
        }
    }
}

