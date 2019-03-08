using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const int MAX_ALLOWED_PLAYERS_ON_GAME_BOARD = 4;

    public static PlayerManager instance { get; private set; }

    private int maxPlayers = 4;
    private int currentPlayerIndex = 0;

    private List<Player> players = new List<Player>();

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

    public void AddNewPlayer(Player newPlayer)
    {
        players.Add(newPlayer);

        for (int i = 0; i < players.Count; i++)
        {
            players[i].SetPlayerID(i);
        }
    }

    public void OnTurnAdvanced()
    {
        currentPlayerIndex++;

        if (currentPlayerIndex > maxPlayers - 1)
        {
            currentPlayerIndex = 0;
        }

        Debug.Log("it's player " + currentPlayerIndex + "'s turn!");
    }

    public Player GetPlayerByIndex(int playerIndex)
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

    public Player GetCurrentPlayer()
    {
        if (players[currentPlayerIndex] != null)
        {
            return players[currentPlayerIndex];
        }

        return null;
    }


}
