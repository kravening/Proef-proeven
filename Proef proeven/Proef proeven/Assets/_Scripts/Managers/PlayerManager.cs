using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const int MAX_ALLOWED_PLAYERS_ON_GAME_BOARD = 4;

    public static PlayerManager instance { get; private set; }

    private int _maxPlayers = 4;
    private int _currentPlayerIndex = -1;

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

    private void Start()
    {
        TurnManager.instance.OnTurnAdvanced.AddListener(OnTurnAdvanced);
    }

    public void SetMaxPlayers(int newMaxPlayers)
    {
    
        if (_maxPlayers > MAX_ALLOWED_PLAYERS_ON_GAME_BOARD)
        {
            Debug.LogError("Invalid player number, number too high");
            return;
        }

        if (_maxPlayers < 1)
        {
            Debug.LogError("Invalid player number, number too low");
            return;
        }

        _maxPlayers = newMaxPlayers;
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
    
        _currentPlayerIndex++;

        if (_currentPlayerIndex > _maxPlayers - 1 || _currentPlayerIndex < 0)
        {
            _currentPlayerIndex = 0;
        }
        
        Debug.Log("it's player " + (_currentPlayerIndex + 1) + "'s turn!");
    }
    
    public Player GetPlayerByIndex(int playerIndex)
    {

        if (playerIndex < 0)
        {
            return null;
        }

        if (playerIndex < _maxPlayers)
        {
            return null;
        }

        return players[playerIndex];
    }

    public Player GetCurrentPlayer()
    {
    
        if (players[_currentPlayerIndex] != null)
        {
            return players[_currentPlayerIndex];
        }

        return null;
    }

}
