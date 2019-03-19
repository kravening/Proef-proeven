using Data;
using Managers;
using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    private Tile thisTile;
    // Start is called before the first frame update
    void Start()
    {
        thisTile = GetComponent<Tile>();
        gameObject.GetComponent<OnClickInteraction>().OnInteractionReceived += OnClick;
    }

    public void OnClick()
    {
        if (thisTile == null) // null check
        {
            Debug.LogError("no Tile component found");
            return;
        }

        if (PlayerManager.instance.HasPlayerMoved()) // player has already made a move this turn
        {
            return;
        }

        if (BoardGameManager.instance.GetCurrentGamePhase() != Enums.GamePhase.MovementPhase)
        {
            return;
        }

        CommandManager.instance.MoveCurrentPlayerToNextTile(thisTile);
    }
}
