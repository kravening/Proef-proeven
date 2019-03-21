using Managers;
using UnityEngine;



public class Inventory : MonoBehaviour
{
    public int[] _cardList;
    public int _currentAmountOfCardsinInventory = 0;

    private void Awake()
    {
        _cardList = new int[2];
    }

    public int GetItemFromInventory(int index)
    {
        Debug.Log(index);
        return _cardList[index];
    }

    public void AddItem(int newItem)
    {
        bool cardSlotFound = false;
        for (int i = 0; i < _cardList.Length; i++)
        {
            if (_cardList[i] == 0)
            {
                _cardList[i] = newItem;
                cardSlotFound = true;
                _currentAmountOfCardsinInventory++;
                UpdatePlayerInfo();
                break;
            }
        }

        if (cardSlotFound == false)
        {
            Debug.LogError("no more inventory slots");
        }   
    }

    public void RemoveCard(int index)
    {
        _cardList[index] = 0;
    }
    
    public void UpdatePlayerInfo()
    {
        for (int i = 0; i < Gameboard.instance.playerBases.Length; i++)
        {
            Gameboard.instance.playerBases[PlayerManager.instance.GetCurrentPlayer().GetPlayerID()].SetPlayerInfo();
        }
    }
    
}
