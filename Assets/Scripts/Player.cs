using UnityEngine;
namespace Game 
{
    public class Player : MonoBehaviour
    {
        [SerializeField] public Shadow shadow;
        public Vector2 InitialPosition = new Vector2(0.5f, 0.5f);
        // Start is called before the first frame update
        void Start()
        {
            transform.position = InitialPosition;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = shadow.CurrentPosition;
            Debug.Log("move Player");
        }
    }
}

