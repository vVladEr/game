using UnityEngine;
namespace Game 
{
    public class Player : MonoBehaviour
    {
        public float speed = 1.0f;
        public Vector2 PreviousPosition;
        // Start is called before the first frame update
        void Start()
        {
            transform.position = new Vector2(0.5f, 0.5f);
            PreviousPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 currentPosition = transform.position;
            PreviousPosition = currentPosition;
            Vector2 newPosition = currentPosition;
            if (Input.GetKeyDown(KeyCode.D))
                newPosition = new(currentPosition.x + 1f, currentPosition.y);
            if (Input.GetKeyDown(KeyCode.A))
                newPosition = new(currentPosition.x - 1f, currentPosition.y);
            if (Input.GetKeyDown(KeyCode.W))
                newPosition = new(currentPosition.x, currentPosition.y + 1f);
            if (Input.GetKeyDown(KeyCode.S))
                newPosition = new(currentPosition.x, currentPosition.y - 1f);
            transform.position = newPosition;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("collision detected");
            transform.position = PreviousPosition;
        }

    }
}

