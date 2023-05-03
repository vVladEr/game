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
        private float lastTime = 0f;
        private bool isAllowedToMove = true;
        public Vector2 Position => transform.position;

        public const float DeltaTime = 1f;
        public const float Eps = 0.1f;

        void Start()
        {
            transform.position = InitialPosition;
            shadowsDictionary = new Dictionary<Vector2, Shadow>() { {new Vector2(1, 0), rightShadow},
                {new Vector2(-1, 0), leftShadow}, {new Vector2(0, 1), topShadow}, {new Vector2(0, -1), bottomShadow} };
        }

        void Update()
        {
            IsAllowedToMove();
            if (isAllowedToMove)
                PlayerUpdate();
        }

        private Vector2 GetDirectionVector()
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

        private void PlayerUpdate() 
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
                isAllowedToMove = false;
                transform.position = newPosition;
            }

        }

        private void IsAllowedToMove() 
        {
            var curTime = Time.time;
            if (curTime - lastTime > Eps && (curTime-lastTime+Eps)%DeltaTime < 2*Eps) 
            {
                Debug.Log(curTime - lastTime);
                isAllowedToMove = true;
                lastTime = curTime;
            }
        }
    }
}
