using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private int _currentNumber;

    private void Start()
    {
        GenerateNumber();
        SetCardSprite();
    }

    private void GenerateNumber()
    {
        int randomNumber = Random.Range(1, 9);
        _currentNumber = randomNumber;   
    }

    public int GetCardNumber()
    {
        return _currentNumber;
    }

    private void SetCardSprite()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRender component found on this object");
            return;
        }
        spriteRenderer.sprite = CardSpriteDataManager.instance.GetSpriteForCard(0);
    }
}
