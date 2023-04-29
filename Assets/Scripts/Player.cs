using System.Collections.Generic;
using UnityEngine;
namespace Game 
{
    public class Player : MonoBehaviour
    {
        [SerializeField] public Shadow leftShadow;
        [SerializeField] public Shadow rightShadow;
        [SerializeField] public Shadow topShadow;
        [SerializeField] public Shadow bottomShadow;
        public IReadOnlyDictionary<Vector2, Shadow> shadowsDictionary;
        public Vector2 InitialPosition = new Vector2(0.5f, 0.5f);
        public Vector2 PreviousPosition;

        void Start()
        {
            transform.position = InitialPosition;
            shadowsDictionary = new Dictionary<Vector2, Shadow>() { {new Vector2(1, 0), rightShadow},
                {new Vector2(-1, 0), leftShadow}, {new Vector2(0, 1), topShadow}, {new Vector2(0, -1), bottomShadow} };
        }

        void Update()
        {
            Vector2 currentPosition = transform.position;
            Vector2 newPosition = currentPosition;
            if (Input.anyKeyDown)
            {
                var directionVector = GetDirectionVector();
                if (directionVector != new Vector2(0, 0) && shadowsDictionary[directionVector].NotCollided)
                {
                    PreviousPosition = currentPosition;
                    newPosition = currentPosition + directionVector;
                }
            }

            transform.position = newPosition;
        }

        Vector2 GetDirectionVector()
        {
            if (Input.GetKeyDown(KeyCode.D))
                return new Vector2(1, 0);
            if (Input.GetKeyDown(KeyCode.A))
                return new Vector2(-1, 0);
            if (Input.GetKeyDown(KeyCode.W))
                return new Vector2(0, 1);
            if (Input.GetKeyDown(KeyCode.S))
                return new Vector2(0, -1);

            return new Vector2(0, 0);
        }
    }
}
