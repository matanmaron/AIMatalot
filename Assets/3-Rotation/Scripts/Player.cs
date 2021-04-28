using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Targil3
{
    public class Player : MonoBehaviour
    {
        public Vector3 goal;
        public float speed = 0.03f;
        public float distance = 1.5f;
        public ThirdPersonCharacter thirdPersonCharacter;
        public LineRenderer LineRenderer;

        private void Start()
        {
            LineRenderer.startColor = Color.green;
            LineRenderer.endColor = Color.blue;
            LineRenderer.startWidth = 0.1f;
            LineRenderer.endWidth = 0.1f;
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log($"new target set - {hit.point}");
                    goal = hit.point;
                    LineRenderer.SetPosition(0, transform.position);
                    LineRenderer.SetPosition(1, goal);
                }
            }
            Vector3 realGoal = new Vector3(goal.x,
                transform.position.y, goal.z);
            Vector3 direction = realGoal - transform.position;

            if (direction.magnitude >= distance)
            {
                Vector3 pushVector = direction.normalized * speed;
                transform.Translate(pushVector, Space.World);
                thirdPersonCharacter.Move(pushVector, false, false);
            }
            Debug.DrawLine(transform.position, realGoal, Color.green);
        }
    }
}