using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGameClaw
{
    public class ClawMovement : MonoBehaviour
    {
        public RectTransform claw;
        public RectTransform arm;
        public RectTransform grabber;
        public float targetX;
        public float lowerY;
        public float raiseTime;
        public float idleTime;
        public float moveTime;

        private Vector2 initialPosition;
        private Vector2 initialArmSize;
        private Vector2 initialGrabberPosition;

        public RectTransform prize; // prize box that the grab picks up

        private bool gotPrize;

        public GameObject winUI;

        [Range(0.0f, 1.0f)]
        public float dropSpeed=.5f;

        private void Start()
        {
            // Record initial positions and sizes
            initialPosition = claw.anchoredPosition;
            initialArmSize = arm.sizeDelta;
            initialGrabberPosition = grabber.anchoredPosition;
            gotPrize = false;
            winUI.SetActive(false);
        }

        void Update()
        {
            if (Input.anyKeyDown && !gotPrize)
            {
                // Start the movement coroutine
                StartCoroutine(MoveClaw());
            }

            if (gotPrize)
            {
                winUI.SetActive(true);

            }



        }

        private IEnumerator MoveClaw()
        {
            // Move the claw to the target X position
            float startTime = Time.time;
            while (Time.time < startTime + moveTime)
            {
                float t = (Time.time - startTime) / moveTime;
                claw.anchoredPosition = new Vector2(Mathf.Lerp(initialPosition.x, targetX, t), claw.anchoredPosition.y);
                yield return null;
            }

            // Lower the grabber to the lower Y position
            startTime = Time.time;
            while (grabber.anchoredPosition.y > lowerY)
            {
                float t = (Time.time - startTime) / moveTime;
                grabber.anchoredPosition = new Vector2(grabber.anchoredPosition.x,
                    Mathf.Lerp(initialGrabberPosition.y, lowerY, t));
                // arm.sizeDelta = new Vector2(initialArmSize.x, Mathf.Lerp(initialArmSize.y, initialArmSize.y + (initialGrabberPosition.y + grabber.anchoredPosition.y), t));
                arm.sizeDelta = new Vector2(initialArmSize.x, Mathf.Lerp(initialArmSize.y, 1600f, t));
                yield return null;
            }


            // Idle for 1 second
            yield return new WaitForSeconds(idleTime);
            prize.anchoredPosition =
                new Vector2(grabber.anchoredPosition.x, grabber.anchoredPosition.y - grabber.sizeDelta.y);

            // Raise the grabber back to the initial Y position
            startTime = Time.time;
            while (grabber.anchoredPosition.y < initialGrabberPosition.y)
            {
                float t = (Time.time - startTime) / raiseTime;
                grabber.anchoredPosition = new Vector2(grabber.anchoredPosition.x,
                    Mathf.Lerp(lowerY, initialGrabberPosition.y, t));
                //   arm.sizeDelta = new Vector2(initialArmSize.x, Mathf.Lerp(1600f, initialArmSize.y, initialArmSize.y + (initialGrabberPosition.y - grabber.anchoredPosition.y), t));
                arm.sizeDelta = new Vector2(initialArmSize.x, Mathf.Lerp(1600f, initialArmSize.y, t));
                prize.anchoredPosition = new Vector2(grabber.anchoredPosition.x,
                    grabber.anchoredPosition.y - grabber.sizeDelta.y - 0.1f);

                yield return null;
            }

            // Move the claw back to the initial X position
            startTime = Time.time;
            while (Time.time < startTime + moveTime)
            {
                float t = (Time.time - startTime) / moveTime;
                claw.anchoredPosition = new Vector2(Mathf.Lerp(targetX, initialPosition.x, t), claw.anchoredPosition.y);
                prize.anchoredPosition = new Vector2(claw.anchoredPosition.x,
                    grabber.anchoredPosition.y - grabber.sizeDelta.y - 0.1f);

                yield return null;
            }


            while (prize.anchoredPosition.y > -500f)
            {
                prize.anchoredPosition = new Vector2(claw.anchoredPosition.x, prize.anchoredPosition.y - dropSpeed);

                yield return null;

            }

            gotPrize = true;
        }
    }
}