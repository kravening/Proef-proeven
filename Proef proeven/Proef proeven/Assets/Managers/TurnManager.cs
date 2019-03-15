using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class TurnManager : MonoBehaviour
    {

        public static TurnManager instance { get; private set; }

        private int _currentTurn = 1;

        public UnityEvent OnTurnAdvanced;

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

            if (OnTurnAdvanced == null)
            {
                OnTurnAdvanced = new UnityEvent();
            }
        }

        private void Start()
        {
            OnTurnAdvanced.AddListener(IncrementCurrentTurn);
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
            OnTurnAdvanced.Invoke();
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