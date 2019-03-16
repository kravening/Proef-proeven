using Behaviours;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class Player : MonoBehaviour
    {
        private const float DEFAULT_POSITION_HEIGHT = .5f;

        private PlayerID _playerID = new PlayerID();
        private GridPosition _playerPosition;
        private PlayerBehaviour _playerBehaviour;
        private Inventory _playerInventory;

        private int _attackingCardValue = 0;

        private void Awake()
        {
            _playerPosition = GetComponent<GridPosition>();
            _playerBehaviour = GetComponent<PlayerBehaviour>();
        }
        private void Start()
        {
            PlayerManager.instance.AddNewPlayer(this);
            SetPlayerGridPosition((int)Gameboard.instance.GetPlayerStartingPos(_playerID.GetPlayerID()).x, (int)Gameboard.instance.GetPlayerStartingPos(_playerID.GetPlayerID()).y);
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
            Vector3 tilePosition = Gameboard.instance.tiles[_playerPosition.x,_playerPosition.y].transform.position; // convert 2d array index to corresponding vector 3 pos
            _playerBehaviour.MovePlayer(new Vector3(tilePosition.x, DEFAULT_POSITION_HEIGHT, tilePosition.z));
            PlayerManager.instance.PlayerMoved();
        }

        public int BattleNumberGenerator()
        {
            return Mathf.FloorToInt(Random.Range(1, 10));
        }

        public void DestroyPlayer()
        {
            _playerBehaviour.DestroyPlayer();

        }

        public int GetCardFromInventorySlot(int inventorySlot)
        {
            return _playerInventory.GetItemFromInventory(inventorySlot);
        }

        public void AddCardToInventory(int card)
        {
            _playerInventory.AddItem(card);
        }

        public void SetAttackingCard(int inventorySlot)
        {
            _attackingCardValue = GetCardFromInventorySlot(inventorySlot);
            BoardGameManager.instance.PickedAttackingCard();
        }

        public int GetAttackingCardValue()
        {
            return _attackingCardValue;
        }
    }
}