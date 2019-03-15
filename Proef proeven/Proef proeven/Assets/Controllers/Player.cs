using Behaviours;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class Player : MonoBehaviour
    {
        private const int DEFAULT_POSITION_HEIGHT = 1;

        private PlayerID _playerID = new PlayerID();
        private GridPosition _playerPosition;
        private PlayerBehaviour _playerBehaviour;

        private void Awake()
        {
            _playerPosition = GetComponent<GridPosition>();
            _playerBehaviour = GetComponent<PlayerBehaviour>();
        }
        private void Start()
        {
            PlayerManager.instance.AddNewPlayer(this);
        }

        public void SetPlayerID(int newPlayerID)
        {
            _playerID.SetPlayerID(newPlayerID);
        }

        public int GetPlayerID()
        {
            return _playerID.GetPlayerID();
        }

        public void SetPlayerGridPosition(int x, int y)
        {
            bool positionChanged = false;

            if (_playerPosition.x != x)
            {
                _playerPosition.x = x;
                positionChanged = true;
            }

            if (_playerPosition.y != y)
            {
                _playerPosition.y = y;
                positionChanged = true;
            }

            if (positionChanged)
            {
                OnPositionChanged();
            }
        }

        public GridPosition GetPlayerGridPosition()
        {
            return _playerPosition;
        }

        private void OnPositionChanged()// tell the movement behaviour to move to the new coordinates
        {
            _playerBehaviour.MovePlayer(new Vector3(_playerPosition.x, DEFAULT_POSITION_HEIGHT, _playerPosition.y)); // kinda pseudo code
            //TODO: Get correct vec3 position from the grid.
        }

        public int BattleNumberGenerator()
        {
            return Mathf.FloorToInt(Random.Range(1, 10));
        }

        public void DestroyPlayer()
        {
            _playerBehaviour.DestroyPlayer();

        }
    }
}