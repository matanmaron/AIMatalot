using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Targil3
{
    public class Move : MonoBehaviour
    {
        public Transform goal;
        public float speed = 0.01f;
        public float distance = 1.5f;
        public ThirdPersonCharacter thirdPersonCharacter;
        public LineRenderer LineRenderer;

        private void Start()
        {
            LineRenderer.startColor = Color.red;
            LineRenderer.endColor = Color.blue;
            LineRenderer.startWidth = 0.1f;
            LineRenderer.endWidth = 0.1f;
        }

        void Update()
        {
            Vector3 realGoal = new Vector3(goal.position.x,
                transform.position.y, goal.position.z);
            Vector3 direction = realGoal - transform.position;

            if (direction.magnitude >= distance)
            {
                Vector3 pushVector = direction.normalized * speed;
                transform.Translate(pushVector, Space.World);
                thirdPersonCharacter.Move(pushVector, false, false);
            }
            Debug.DrawLine(transform.position, realGoal, Color.red);
            LineRenderer.SetPosition(0, transform.position);
            LineRenderer.SetPosition(1, realGoal);
        }
    }
}