using Controllers;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private const int MAX_ALLOWED_PLAYERS_ON_GAME_BOARD = 4;

        public static PlayerManager instance { get; private set; }

        private int _maxPlayers = 4;
        private int _currentPlayerIndex = -1;
        private int _currentPlayerCount = -1;

        private bool _hasCurrentPlayerMovedThisTurn;

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
            TurnManager.instance.OnTurnAdvanced += OnTurnAdvanced;
        }

        public void AddNewPlayer(Player newPlayer)
        {
            _currentPlayerCount++;

            players.Add(newPlayer);
            players[_currentPlayerCount].SetPlayerID(_currentPlayerCount);

        }

        public void OnTurnAdvanced()
        {
            _currentPlayerIndex++;

            if (_currentPlayerIndex > _maxPlayers - 1 || _currentPlayerIndex < 0)
            {
                _currentPlayerIndex = 0;
            }

            _hasCurrentPlayerMovedThisTurn = false;

            Debug.Log("it's player " + (_currentPlayerIndex + 1) + "'s turn!");
        }

        public Player GetPlayerByIndex(int playerIndex)
        {

            if (playerIndex < 0)
            {
                return null;
            }

            if (playerIndex > _maxPlayers - 1)
            {
                return null;
            }

            return players[playerIndex];
        }

        public Player GetCurrentPlayer()
        {
            Debug.Log(_currentPlayerIndex);
            if (players[_currentPlayerIndex] != null)
            {
                return players[_currentPlayerIndex];
            }

            return null;
        }

        public void PlayerMoved()
        {
            _hasCurrentPlayerMovedThisTurn = true;
        }

        public bool HasPlayerMoved()
        {
            return _hasCurrentPlayerMovedThisTurn;
        }

        public void DestroyPlayerByIndex(int index)
        {
            players[index].DestroyPlayer();
            players.RemoveAt(index);
            if (index < _currentPlayerIndex)
            {
                _currentPlayerIndex--; // decrement from currentplayerindex if an element lower in index has been removed, this corrects corresponding player indexes relative to the turn
            }
        }
    }
}