using System.Collections;
using System.Collections.Generic;
using Controllers;
using Managers;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private int _baseHealth = 3;
    private int _currentID;
    public Player _correspondingPlayer;
    private int _defendingCard;

    private void Start()
    {
        SpawnCorespondingPlayer();
    }

    private void SpawnCorespondingPlayer()
    {
        Instantiate(_correspondingPlayer.gameObject,transform.position,_correspondingPlayer.gameObject.transform.rotation);
    }

    public void SetDefendingCard(int newDefendingCard)
    {
        _defendingCard = newDefendingCard;
    }

    public int GetDefendingCard()
    {
        return _defendingCard;
    }

    public int ID
    {
        get { return _currentID; }
        set { _currentID = value; }
    }

    public void DamageBase()
    {
        _baseHealth--;
        if (BaseHealth == 0)
        {
            Debug.Log("Player " + ID + "died");
            PlayerManager.instance.DestroyPlayerByIndex(ID);
        }
    }

    public int BaseHealth
    {
        get { return _baseHealth; }
    }
}
