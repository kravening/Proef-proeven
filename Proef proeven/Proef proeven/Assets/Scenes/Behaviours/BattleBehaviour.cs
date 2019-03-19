using Controllers;
using UnityEngine;

namespace Behaviours
{
    public class BattleBehaviour : MonoBehaviour
    {
        public void Battle(Player playerA, Player playerB)
        {
            int playerACard = playerA.BattleNumberGenerator();
            int playerBCard = playerB.BattleNumberGenerator();

            if (playerACard > playerBCard)
            {
                Debug.Log("player " + playerA.GetPlayerID() + " Wins");
                playerA.DestroyPlayer();
            }
            else
            {
                Debug.Log("player " + playerB.GetPlayerID() + " Wins");
                playerB.DestroyPlayer();
            }
        }
    }
}