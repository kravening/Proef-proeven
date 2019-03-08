using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject ReadyCheckButton;
    public static UIController instance;
    
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

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        
    }

    public void Click()
    {
       ReadyCheckButton.SetActive(false);
       //Start Game
    }

}
