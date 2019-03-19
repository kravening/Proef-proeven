using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public GameObject cardSprite;
    private int _currentNumber;

    private void Start()
    {
        GenerateNumber();
    }

    private void GenerateNumber()
    {
        int randomNumber = Random.Range(1, 9);
        _currentNumber = randomNumber;   
    }
}
