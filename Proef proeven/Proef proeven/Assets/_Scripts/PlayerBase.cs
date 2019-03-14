using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private int _currentID;
    
    public int ID
    {
        get { return _currentID; }
        set { _currentID = value; }
    }
    
}
