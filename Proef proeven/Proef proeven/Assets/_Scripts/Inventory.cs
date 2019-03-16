using UnityEngine;



public class Inventory : MonoBehaviour
{
    private int[] _itemList;

    private void Awake()
    {
        _itemList = new int[4];
    }

    public int GetItemFromInventory(int index)
    {
        return _itemList[index];
    }

    public void AddItem(int newItem)
    {
        for (int i = 0; i < _itemList.Length; i++)
        {
            if (_itemList[i] == 0)
            {
                _itemList[i] = newItem;
            }
        }
    }

    public void RemoveItem(int index)
    {
        _itemList[index] = 0;
    }
    
}
