using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerID playerID = new PlayerID();
    private GridPosition playerPosition = new GridPosition();

    private void Start()
    {
        PlayerManager.instance.AddNewPlayer(this);
    }

    public void SetPlayerID(int newPlayerID)
    {
        playerID.SetPlayerID(newPlayerID);
    }

    public int GetPlayerID()
    {
        return playerID.GetPlayerID();
    }

    public void SetPlayerGridPosition(int x, int y)
    {
        bool positionChanged = false;

        if (playerPosition.x != x)
        {
            playerPosition.x = x;
            positionChanged = true;
        }

        if (playerPosition.y != y)
        {
            playerPosition.y = y;
            positionChanged = true;
        }

        if (positionChanged)
        {
            OnPositionChanged();
        }
    }

    public GridPosition GetPlayerGridPosition()
    {
        return playerPosition;
    }

    private void OnPositionChanged()// tell the movement behaviour to move to the new coordinates
    {

    }
}
