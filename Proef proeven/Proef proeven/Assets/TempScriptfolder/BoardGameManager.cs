using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Managers
{
    public class BoardGameManager : MonoBehaviour
    {
        private const int FADE_LENGTH = 1;

        private Enums.GamePhase _currentGamePhase = Enums.GamePhase.None;

        public void Start()
        {
            // generate board
            // fade in scene
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
                    BoardSetUp();
                    break;
                case Enums.GamePhase.NextTurn:
                    NextTurnPhase();
                    break;
                case Enums.GamePhase.MovementPhase:
                    //
                    break;
                case Enums.GamePhase.PickPhase:
                    //
                    break;
                case Enums.GamePhase.BattlePhase:
                    //
                    break;

            }
        }

        private IEnumerator BoardSetUp()
        {
            // generate board
            // generate bases
            // generate cards
            yield return new WaitForSeconds(0);
            // fade in scene
            yield return new WaitForSeconds(0);

            SetNewState(Enums.GamePhase.NextTurn);
        }

        private IEnumerator NextTurnPhase()
        {
            // play next turn animation
            yield return new WaitForSeconds(0);
        }

        private IEnumerator MovementPhase()
        {
            yield return new WaitForSeconds(0);
        }

        private IEnumerator BattlePhase()
        {
            // start battle animations
            yield return new WaitForSeconds(0);
            // check cards
            yield return new WaitForSeconds(0);
            // start next turn
        }


    }
}
