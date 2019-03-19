using UnityEngine;



public class Inventory : MonoBehaviour
{
    public int[] _cardList;

    private void Awake()
    {
        _cardList = new int[4];
    }

    public int GetItemFromInventory(int index)
    {
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
    
}
