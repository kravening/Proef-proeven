using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{

    public static TurnManager instance { get; private set; }

    private int currentTurn = 1;

    public UnityEvent OnTurnAdvanced;

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

        if (OnTurnAdvanced == null)
        {
            OnTurnAdvanced = new UnityEvent();
        }
    }

    private void Start()
    {
        OnTurnAdvanced.AddListener(IncrementCurrentTurn);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void AdvanceTurn()
    {
        OnTurnAdvanced.Invoke();
    }

    private void IncrementCurrentTurn()
    {
        currentTurn++;
    }

    public int GetCurrentTurn()
    {
        return currentTurn;
    }
}
