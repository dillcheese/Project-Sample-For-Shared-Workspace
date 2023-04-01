using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGameClaw
{
    public class ClawMovement : MonoBehaviour
    {
        public GameObject mainCanvas;
        public RectTransform claw;
        public RectTransform arm;
        public RectTransform grabber;

        public GameObject touchArea;
        public GameObject text1, text2;

        public Image grabberSprite;

        public Sprite grabberClamp;
        public Sprite grabberRelease;

        [Tooltip("How low on the y axis the claw should go when getting a prize")]
        public float lowerY;

        public float raiseTime;
        public float idleTime;
        public float moveTime;

        [Tooltip("Time for how long the prize should drop")]
        public float dropTime;

        private Vector2 initialPosition;
        private Vector2 initialArmSize;
        private Vector2 initialGrabberPosition;
        [SerializeField]private float targetX;

        public RectTransform prize; // prize box that the grab picks up

        private bool gotPrize;
        private bool gettingPrize;

        public GameObject winUI;

        private void Start()
        {
            // Record initial positions and sizes
            initialPosition = claw.anchoredPosition;
            initialArmSize = arm.sizeDelta;
            initialGrabberPosition = grabber.anchoredPosition;
            gotPrize = false;
            gettingPrize = false;
            winUI.SetActive(false);
        }

        private void Update()
        {
            //get mouse input for pc debug
            if (Input.GetMouseButtonDown(0) && !gotPrize && !gettingPrize)
            {
                RectTransform canvasRect = mainCanvas.GetComponent<RectTransform>();
                Vector2 canvasPosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, Camera.main, out canvasPosition);


                RectTransform touchRect = touchArea.GetComponent<RectTransform>();

                // if (touchRect.rect.Contains(canvasPosition))
                if (RectTransformUtility.RectangleContainsScreenPoint(touchRect, Input.mousePosition, Camera.main))
                { //touched inside

                    Debug.Log("inside");

                    // Set the target X position to the converted canvas position
                    targetX = canvasPosition.x;

                    gettingPrize = true;

                    // Set the ball's position to the touch coordinates
                    StartCoroutine(MoveClaw(targetX));
                }

                else //touched outside so random position (-100 to 450)
                {
                    // Set the target X position to the converted canvas position
                    // targetX = Random.Range(-100,450);

                    //gettingPrize = true;
                    // Set the ball's position to the touch coordinates
                    //StartCoroutine(MoveClaw(targetX));
                }


                text1.SetActive(false);
                text2.SetActive(false);
                touchArea.SetActive(false);
            }

            //get touch from user
            if (Input.touchCount == 1 && !gotPrize && !gettingPrize)
            {
                // Convert touch position to canvas space
                RectTransform canvasRect = mainCanvas.GetComponent<RectTransform>();
                Vector2 canvasPosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.GetTouch(0).position, Camera.main, out canvasPosition);
                
                RectTransform touchRect = touchArea.GetComponent<RectTransform>();

          // if (touchRect.rect.Contains(canvasPosition))
                if (RectTransformUtility.RectangleContainsScreenPoint(touchRect, Input.GetTouch(0).position, Camera.main))
                { //touched inside

                    Debug.Log("inside");

                    // Set the target X position to the converted canvas position
                    targetX = canvasPosition.x;

                    gettingPrize = true;

                    // Set the ball's position to the touch coordinates
                    StartCoroutine(MoveClaw(targetX));
                }

                else //touched outside so random position (-100 to 450)
                {
                    // Set the target X position to the converted canvas position
                   // targetX = Random.Range(-100,450);

                    //gettingPrize = true;
                    // Set the ball's position to the touch coordinates
                    //StartCoroutine(MoveClaw(targetX));
                }
                

                text1.SetActive(false);
                text2.SetActive(false);
                touchArea.SetActive(false);
            }

            if (gotPrize)
            {
                winUI.SetActive(true);
            }
        }

        private IEnumerator MoveClaw(float targetX)
        {
            // Move the claw to the target X position
            float startTime = Time.time;
            //float distance = Mathf.Abs(targetX - initialPosition.x);
            //float speed = distance / moveTime;
            //float minDistance = 200;
            //Debug.Log(distance);
            while (Time.time < startTime + moveTime)
            {
                float t = (Time.time - startTime) / moveTime;
                          claw.anchoredPosition = new Vector2(Mathf.Lerp(initialPosition.x, targetX, t), claw.anchoredPosition.y);
                //if (distance < minDistance)
                //{
                //    claw.anchoredPosition += new Vector2(Mathf.Sign(targetX - initialPosition.x) * speed * Time.deltaTime, 0f);
                //}
                //else
                //{
                //    claw.anchoredPosition = new Vector2(Mathf.Lerp(initialPosition.x, targetX, t), claw.anchoredPosition.y);
                //}
                yield return null;
            }

            // Lower the grabber to the lower Y position
            startTime = Time.time;
            while (grabber.anchoredPosition.y > lowerY)
            {
                float t = (Time.time - startTime) / moveTime;
                grabber.anchoredPosition = new Vector2(grabber.anchoredPosition.x,
                    Mathf.Lerp(initialGrabberPosition.y, lowerY, t));
                arm.sizeDelta = new Vector2(initialArmSize.x, Mathf.Lerp(initialArmSize.y, 1600f, t));
                yield return null;
            }

            // Idle for 1 second
            yield return new WaitForSeconds(idleTime);
            prize.anchoredPosition = new Vector2(grabber.anchoredPosition.x, grabber.anchoredPosition.y - grabber.sizeDelta.y);
            grabberSprite.sprite = grabberClamp;

            // Raise the grabber back to the initial Y position
            startTime = Time.time;
            while (grabber.anchoredPosition.y < initialGrabberPosition.y)
            {
                float t = (Time.time - startTime) / raiseTime;
                grabber.anchoredPosition = new Vector2(grabber.anchoredPosition.x,
                    Mathf.Lerp(lowerY, initialGrabberPosition.y, t));
                arm.sizeDelta = new Vector2(initialArmSize.x, Mathf.Lerp(1600f, initialArmSize.y, t));
                prize.anchoredPosition = new Vector2(claw.anchoredPosition.x, grabber.anchoredPosition.y - grabber.sizeDelta.y + 25);

                yield return null;
            }

            // Move the claw back to the initial X position
            startTime = Time.time;
            while (Time.time < startTime + moveTime)
            {
                float t = (Time.time - startTime) / moveTime;
                claw.anchoredPosition = new Vector2(Mathf.Lerp(targetX, initialPosition.x, t), claw.anchoredPosition.y);
                prize.anchoredPosition = new Vector2(claw.anchoredPosition.x, grabber.anchoredPosition.y - grabber.sizeDelta.y + 25);

                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            grabberSprite.sprite = grabberRelease;
            float initialPrize = prize.anchoredPosition.y;
            //while (prize.anchoredPosition.y > -600f)
            //{
            //    prize.anchoredPosition = new Vector2(claw.anchoredPosition.x, prize.anchoredPosition.y - dropSpeed);

            //    yield return null;
            //}

            startTime = Time.time;
            // Debug.Log(dropTime);
            while (Time.time < startTime + dropTime)
            {
                float t = (Time.time - startTime) / dropTime;
                //   Debug.Log(t);
                float newY = Mathf.Lerp(initialPrize, -590f, t);
                // Debug.Log("Y: " + newY);
                prize.anchoredPosition = new Vector2(prize.anchoredPosition.x, newY);
                yield return null;
            }

            gotPrize = true;
        }
    }
}