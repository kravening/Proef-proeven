using Controllers;
using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class CommandManager : MonoBehaviour
    {
        public static CommandManager instance { get; private set; }

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

        public void MoveCurrentPlayerToNextTile(Tile nextTile)
        {
            Player currentPlayer = PlayerManager.instance.GetCurrentPlayer();

            GridCoordinates currentGridCoordinates = currentPlayer.GetPlayerGridCoordinates();

            GridCoordinates nextGridCoordinates = new GridCoordinates();
            nextGridCoordinates.x = (int)nextTile.transform.position.x;
            nextGridCoordinates.y = (int)nextTile.transform.position.z;

            bool legalMoveFound = false;

            if (currentGridCoordinates.x <= (nextGridCoordinates.x + 2) && currentGridCoordinates.x >= (nextGridCoordinates.x - 2) && currentGridCoordinates.y == nextGridCoordinates.y) // to move 2 tiles and not more in a straight line
            {
                legalMoveFound = true;
            }
            else if (currentGridCoordinates.y <= (nextGridCoordinates.y + 2) && currentGridCoordinates.y >= (nextGridCoordinates.y - 2) && currentGridCoordinates.x == nextGridCoordinates.x)
            {
                legalMoveFound = true;
            }
            else if (currentGridCoordinates.x <= (nextGridCoordinates.x + 1) && currentGridCoordinates.x >= (nextGridCoordinates.x - 1) && currentGridCoordinates.y <= (nextGridCoordinates.y + 1) && currentGridCoordinates.y >= (nextGridCoordinates.y - 1)) // check for diagonal move within 1 tile
            {
                legalMoveFound = true;
            }

            if (legalMoveFound)
            {
                currentPlayer.SetPlayerGridPosition(nextGridCoordinates.x, nextGridCoordinates.y);
            }
        }
    }
}
