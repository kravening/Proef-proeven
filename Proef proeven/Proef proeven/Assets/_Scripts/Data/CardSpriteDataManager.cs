using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpriteDataManager : MonoBehaviour
{
    public List<Sprite> cardSprites = new List<Sprite>();

    public static CardSpriteDataManager instance { get; private set; }

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void OnDestroy()
    {

        if (instance == this)
        {
            instance = null;
        }
    }

    public Sprite GetSpriteForCard(int cardNumber)
    {
        return cardSprites[cardNumber];
    }
}
