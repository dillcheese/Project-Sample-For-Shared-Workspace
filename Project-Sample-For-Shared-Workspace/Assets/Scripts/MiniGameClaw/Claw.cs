using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Claw : MonoBehaviour
{
    public float moveSpeed = 50f;
    public Vector2 targetPosition;
    private Vector2 startPosition;
    private RectTransform rectTransform;

    public GameObject arm;
    public GameObject grabber;

    public float loweringSpeed = 25f;
    public float extendingSpeed = 25f;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    private void Update()
    {
        rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetPosition, moveSpeed * Time.deltaTime);


        if (Input.anyKeyDown)
        {
           
            StartCoroutine(LowerAndExtend(grabber, arm, -460f, 1600f, loweringSpeed, extendingSpeed, 3f));
            Debug.Log("Press");
        }
    }

    public void LowerAndExtend(GameObject lowerObject, GameObject extendObject, float targetY, float maxHeight)
    {
        RectTransform lowerRect = lowerObject.GetComponent<RectTransform>();
        RectTransform extendRect = extendObject.GetComponent<RectTransform>();

        float currentY = lowerRect.anchoredPosition.y;
        Debug.Log(currentY);

        float height = extendRect.sizeDelta.y;
        Debug.Log("Function");


        if (currentY > targetY)
        {
            float newY = Mathf.Max(currentY - Time.deltaTime * loweringSpeed, targetY);
            lowerRect.anchoredPosition = new Vector2(lowerRect.anchoredPosition.x, newY);

            // Use Lerp to calculate the new height based on the current and max height
            float newHeight = Mathf.Lerp(height, maxHeight, (currentY - targetY) / (lowerObject.GetComponent<RectTransform>().sizeDelta.y - targetY));
            extendRect.sizeDelta = new Vector2(extendRect.sizeDelta.x, newHeight);
        }
    }

    public static IEnumerator LowerAndExtend(GameObject lowerObject, GameObject extendObject, float targetY, float maxHeight, float loweringSpeed, float extendingSpeed, float time)
    {
        RectTransform lowerRect = lowerObject.GetComponent<RectTransform>();
        RectTransform extendRect = extendObject.GetComponent<RectTransform>();

        float currentY = lowerRect.anchoredPosition.y;
        float height = extendRect.sizeDelta.y;

        float startY = currentY;
        float elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / time);
            float newY = Mathf.Lerp(startY, targetY, t);
            lowerRect.anchoredPosition = new Vector2(lowerRect.anchoredPosition.x, newY);
            extendRect.sizeDelta = new Vector2(extendRect.sizeDelta.x, Mathf.Lerp(height, maxHeight, t));
            yield return null;
        }

        lowerRect.anchoredPosition = new Vector2(lowerRect.anchoredPosition.x, targetY);
        extendRect.sizeDelta = new Vector2(extendRect.sizeDelta.x, maxHeight);
    }

 

}