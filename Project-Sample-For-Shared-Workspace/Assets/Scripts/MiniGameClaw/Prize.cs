using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prize : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;

    private Image imageComponent;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        int spriteIndex = Random.Range(1, 6);

        switch (spriteIndex)
        {
            case 1:
                imageComponent.sprite = sprite1;
                break;
            case 2:
                imageComponent.sprite = sprite2;
                break;
            case 3:
                imageComponent.sprite = sprite3;
                break;
            case 4:
                imageComponent.sprite = sprite4;
                break;
            case 5:
                imageComponent.sprite = sprite5;
                break;
            default:
                Debug.LogError("Invalid sprite index");
                break;
        }
    }
}
