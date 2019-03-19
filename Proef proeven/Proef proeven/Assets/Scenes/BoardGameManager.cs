using Controllers;
using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class BoardGameManager : MonoBehaviour
    {
        public static BoardGameManager instance { get; private set; }

        private const int FADE_LENGTH = 1;

        private Enums.GamePhase _currentGamePhase = Enums.GamePhase.None;

        private bool attackingCardPicked = false;

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
        }

        public void Start()
        {
            OnEnumChanged();
            TurnManager.instance.OnTurnAdvanced += OnTurnAdvanced;
        }

        public void SetNewState(Enums.GamePhase newState)
        {
            if (_currentGamePhase == newState)
            {
                Debug.LogError("Trying to set the same state");
                return;
            }
            _currentGamePhase = newState;
            OnEnumChanged();
        }

        private void OnEnumChanged()
        {
            switch (_currentGamePhase)
            {
                case Enums.GamePhase.None:

                    StartCoroutine(BoardSetUp());
                    break;

                case Enums.GamePhase.NextTurn:

                    StartCoroutine(NextTurnPhase());
                    break;

                case Enums.GamePhase.MovementPhase:

                    StartCoroutine(MovementPhase());
                    break;

                case Enums.GamePhase.EventPhase:

                    StartCoroutine(EventPhase());
                    break;

                case Enums.GamePhase.PickupPhase:

                    StartCoroutine(PickupPhase());
                    break;

                case Enums.GamePhase.DefendPhase:

                    StartCoroutine(DefendPhase());
                    break;

                case Enums.GamePhase.BattlePhase:

                    StartCoroutine(BattlePhase());
                    break;

                case Enums.GamePhase.RestPhase:

                    // nothing happens here, but being here allows you to pass the turn.
                    break;


            }
            Debug.Log("New state set" + " " + _currentGamePhase);
        }

        private IEnumerator BoardSetUp()
        {
            Gameboard.instance.GenerateBoard();

            yield return new WaitForSeconds(.1f);

            SetNewState(Enums.GamePhase.NextTurn);
        }

        private IEnumerator NextTurnPhase()
        {
            // play next turn animation
            yield return new WaitForSeconds(0);
            //start movement phase after animation has played
            SetNewState(Enums.GamePhase.MovementPhase);
        }

        private IEnumerator MovementPhase()
        {
            // allow player to move here
            yield return new WaitForSeconds(1f);
        }

        private IEnumerator EventPhase()
        {
            //check what tile player is on.
            GridCoordinates currentPlayerCoordinates = PlayerManager.instance.GetCurrentPlayer().GetPlayerGridCoordinates();
            GameObject tileObject = Gameboard.instance.tiles[currentPlayerCoordinates.x, currentPlayerCoordinates.y].GetItem();

            yield return new WaitForSeconds(.4f);

            if (tileObject == null) // nothing to interact with on this tile
            {
                SetNewState(Enums.GamePhase.RestPhase);
                yield break;
            }

            if (tileObject?.GetComponent<PlayerBase>() == true) // a base has been found on this tile
            {
                PlayerBase availablePlayerBase = tileObject?.GetComponent<PlayerBase>();

                if (availablePlayerBase?.ID == PlayerManager.instance.GetCurrentPlayer().GetPlayerID()) // reached your own base on this tile
                {
                    SetNewState(Enums.GamePhase.DefendPhase); // start defending your base
                    yield break;
                }

                SetNewState(Enums.GamePhase.BattlePhase); // enemy base found on this tile, start the battle phase
                yield break;
            }

            if (tileObject?.GetComponent<Card>() == true) // found card on this tile
            {
                SetNewState(Enums.GamePhase.PickupPhase);
                yield break;
            }

            yield return new WaitForSeconds(0);

            // default fallback.
            SetNewState(Enums.GamePhase.RestPhase);
        }

        private IEnumerator BattlePhase()
        {
            GridCoordinates currentPlayerCoordinates = PlayerManager.instance.GetCurrentPlayer().GetPlayerGridCoordinates();
            PlayerBase attackableBase = Gameboard.instance.tiles[currentPlayerCoordinates.x, currentPlayerCoordinates.y].GetItem().GetComponent<PlayerBase>();
            attackingCardPicked = false;

            // start battle animations
            // open UI
            while (attackingCardPicked == false)
            {
                //wait for player to pick a card from UI.
                yield return new WaitForSeconds(0);
            }

            if (PlayerManager.instance.GetCurrentPlayer().GetAttackingCardValue() > attackableBase.GetDefendingCard())
            {
                attackableBase.DamageBase();
            }

            // check cards

            yield return new WaitForSeconds(0);

            SetNewState(Enums.GamePhase.RestPhase);
        }

        private IEnumerator DefendPhase()
        {
            // place card on your base
            yield return new WaitForSeconds(0);
            SetNewState(Enums.GamePhase.RestPhase);
        }

        private IEnumerator PickupPhase()
        {
            yield return new WaitForSeconds(.1f);
            Player currentPlayer = PlayerManager.instance.GetCurrentPlayer();
            Tile[,] allTiles = Gameboard.instance.tiles;
            Tile currentTile = allTiles[currentPlayer.GetPlayerGridCoordinates().x,currentPlayer.GetPlayerGridCoordinates().y];


            currentPlayer.AddCardToInventory(currentTile.GetItem().GetComponent<Card>().GetCardNumber());
            currentTile.RemoveItem();

            yield return new WaitForSeconds(0);

            SetNewState(Enums.GamePhase.RestPhase);
        }



        public Enums.GamePhase GetCurrentGamePhase()
        {
            return _currentGamePhase;
        }


        private void OnTurnAdvanced() // only allow turn to be advanced if there is no event going on after moving
        {
            if (_currentGamePhase != Enums.GamePhase.RestPhase)
            {
                return;
            }
            SetNewState(Enums.GamePhase.MovementPhase);
        }

        public void PickedAttackingCard()
        {
            attackingCardPicked = true;
        }
    }
}
