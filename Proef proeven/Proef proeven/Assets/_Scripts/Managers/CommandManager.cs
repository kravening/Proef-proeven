using System.Collections;
using System.Collections.Generic;
using Controllers;
using Data;
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

            currentPlayer.SetPlayerGridPosition((int)nextTile.transform.position.x,(int)nextTile.transform.position.z);

            BoardGameManager.instance.SetNewState(Enums.GamePhase.EventPhase);
        }

        private void GrabCard()
        {

        }

        private void AttackBase()
        {

        }
    }
}
