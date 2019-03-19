using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject _currentItem;
    public Transform spawnPosition;
 
    public GameObject GetItem()
    {
        return _currentItem;
    }
 
    public void SetItem(GameObject itemToPlace)
    {
        _currentItem = Instantiate(itemToPlace, spawnPosition);
        _currentItem.transform.parent = null; // set parent of spawned object to null, meaning it won't be a child of this object and parentless, just like me after my dad went out to get milk.
    }
 
    public void RemoveItem()
    {
        Destroy(_currentItem.gameObject);
    }
 
}
