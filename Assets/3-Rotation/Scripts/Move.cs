using System.Collections;
using TMPro;
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
        public TextMeshProUGUI Msg;
        public bool CanBoost = true;
        bool run = false;

        private void Start()
        {
            Msg.text = "Run Away !";
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
                if (run)
                {
                    direction = new Vector3(-direction.x, direction.y, -direction.z);
                }
                Vector3 pushVector = direction.normalized * speed;
                transform.Translate(pushVector, Space.World);
                thirdPersonCharacter.Move(pushVector, false, false);
            }
            else if (CanBoost)
            {
                ChangeRole();
            }
            Debug.DrawLine(transform.position, realGoal, Color.red);
            LineRenderer.SetPosition(0, transform.position);
            LineRenderer.SetPosition(1, realGoal);
        }

        private void ChangeRole()
        {
            run = !run;
            if (run)
            {
                Debug.Log("run");
                StartCoroutine(SpeedBoost());
            }
            else
            {
                Debug.Log("catch");
                StartCoroutine(Wait());
                goal.GetComponent<Player>().Boost();
            }
            Msg.text = run ? "Get HIM !" : "Run Away !";
        }

        IEnumerator SpeedBoost()
        {
            CanBoost = false;
            var oldspeed = speed;
            speed = 0.05f;
            yield return new WaitForSeconds(2);
            speed = oldspeed;
            CanBoost = true;
        }

        IEnumerator Wait()
        {
            CanBoost = false;
            yield return new WaitForSeconds(2);
            CanBoost = true;
        }
    }
}