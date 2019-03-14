using UnityEngine;

public class BattleBehaviour : MonoBehaviour
{
    public void Battle(Player playerA, Player playerB)
    {
        int playerACard = playerA.BattleNumberGenrator();
        int playerBCard = playerB.BattleNumberGenrator();

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
