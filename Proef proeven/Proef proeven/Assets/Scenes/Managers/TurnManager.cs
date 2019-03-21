using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class TurnManager : MonoBehaviour
    {

        public static TurnManager instance { get; private set; }

        private int _currentTurn = 0;

        public delegate void eventRaiser();
        public event eventRaiser OnTurnAdvanced;

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

        public void AdvanceTurn()
        {
            if (OnTurnAdvanced == null)
            {
                return;
            }
            IncrementCurrentTurn();
            OnTurnAdvanced();
        }

        private void IncrementCurrentTurn()
        {
            _currentTurn++;
        }

        public int GetCurrentTurn()
        {
            return _currentTurn;
        }
    }

}