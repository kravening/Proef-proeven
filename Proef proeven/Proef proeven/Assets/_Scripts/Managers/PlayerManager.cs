using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const int MAX_ALLOWED_PLAYERS_ON_GAME_BOARD = 4;

    public static PlayerManager instance { get; private set; }

    private int maxPlayers = 4;
    private int currentPlayerIndex = 0;

    private List<GameObject> players = new List<GameObject>(); //TODO: change gameObject list to the player class

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

    public void SetMaxPlayers(int newMaxPlayers)
    {
        if (maxPlayers > MAX_ALLOWED_PLAYERS_ON_GAME_BOARD)
        {
            Debug.LogError("Invalid player number, number too high");
            return;
        }

        if (maxPlayers < 1)
        {
            Debug.LogError("Invalid player number, number too low");
            return;
        }

        maxPlayers = newMaxPlayers;
    }

    public void AddNewPlayer(GameObject newPlayer)
    {
        players.Add(newPlayer);
    }

    public void OnTurnAdvanced() //TODO: call this when an event from the turn manager starts.
    {
        currentPlayerIndex++;

        if (currentPlayerIndex > maxPlayers - 1)
        {
            currentPlayerIndex = 0;
        }
    }

    public GameObject GetPlayerByIndex(int playerIndex)
    {

        if (playerIndex < 0)
        {
            return null;
        }

        if (playerIndex < maxPlayers)
        {
            return null;
        }

        return players[playerIndex];
    }

    public GameObject GetCurrentPlayer()
    {
        if (players[currentPlayerIndex] != null)
        {
            return players[currentPlayerIndex];
        }

        return null;
    }


}
