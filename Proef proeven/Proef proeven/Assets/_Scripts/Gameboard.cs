using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Networking;

namespace Managers
{
    public class Gameboard : Grid
    {
        public static Gameboard instance { get; private set; }

        private int _maxItems;

        public GameObject card;
        public PlayerBase[] playerBases = new PlayerBase[4];
        private Vector2[] _playerPositions;

        public Color[] tileColors = new Color[2];

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

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

            columns = 8;
            rows = 8;
            _maxItems = 8;
            tiles = new Tile[columns, rows];
        }

        public void GenerateBoard()
        {
            _playerPositions = new Vector2[4];
            EmptyBoard();
            Generate();
            SpawnPlayers();
            SpawnItems();
            Checkered();
        }

        private void SpawnPlayers()
        {
            _playerPositions[0] = new Vector2(0, 0);
            _playerPositions[1] = new Vector2(0, rows - 1);
            _playerPositions[2] = new Vector2(columns - 1, 0);
            _playerPositions[3] = new Vector2(columns - 1, rows - 1);

            for (int i = 0; i < _playerPositions.Length; i++)
            {
                tiles[(int)_playerPositions[i].x, (int)_playerPositions[i].y].SetItem(playerBases[i].gameObject);
            }
        }

        private void SpawnItems()
        {
            for (int k = 0; k < _maxItems; k++)
            {
                int randomX = Random.Range(0, columns - 1);
                int randomY = Random.Range(0, columns - 1);

                if (tiles[randomX, randomY].GetItem() != null)
                {
                    k--;
                }
                else
                {
                    tiles[randomX, randomY].SetItem(card);
                }
            }
        }

        private void Checkered()
        {
            for (var i = 0; i < columns; i++)
            {
                for (var j = 0; j < rows; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0 || i % 2 == 0 && j % 2 == 0)
                    {
                        tiles[i, j].GetComponent<Renderer>().material.SetColor("_Color", tileColors[0]);
                    }
                    else
                    {
                        tiles[i, j].GetComponent<Renderer>().material.SetColor("_Color", tileColors[1]);
                    }
                }
            }
        }

        private void EmptyBoard()
        {
            for (var i = 0; i < columns; i++)
            {
                for (var j = 0; j < rows; j++)
                {
                    if (tiles[i, j] != null)
                    {
                        Destroy(tiles[i, j].gameObject);
                        tiles[i, j] = null;
                    }
                }
            }
        }

        public Vector2 GetPlayerStartingPos(int playerID)
        {
            return _playerPositions[playerID];
        }
    }
}


