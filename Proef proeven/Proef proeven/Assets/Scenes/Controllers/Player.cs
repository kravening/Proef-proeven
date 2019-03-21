using Behaviours;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class Player : MonoBehaviour
    {
        private const float DEFAULT_POSITION_HEIGHT = .5f;

        private PlayerID _playerID = new PlayerID();
        private GridCoordinates _playerCoordinates = new GridCoordinates();
        private PlayerBehaviour _playerBehaviour;
        private Inventory _playerInventory;

        private int _attackingCardValue = 0;

        private void Awake()
        {
            _playerBehaviour = GetComponent<PlayerBehaviour>();
            _playerInventory = GetComponent<Inventory>();
            PlayerManager.instance.AddNewPlayer(this);
        }
        private void Start()
        {
            
            SetPlayerStartingPosition((int)Gameboard.instance.GetPlayerStartingPos(_playerID.GetPlayerID()).x, (int)Gameboard.instance.GetPlayerStartingPos(_playerID.GetPlayerID()).y);
        }

        public void SetPlayerID(int newPlayerID)
        {
            _playerID.SetPlayerID(newPlayerID);
        }

        public int GetPlayerID()
        {
            return _playerID.GetPlayerID();
        }

        public void SetPlayerStartingPosition(int x, int y)
        {


            if (_playerCoordinates.x != x)
            {
                _playerCoordinates.x = x;
           
            }

            if (_playerCoordinates.y != y)
            {
                _playerCoordinates.y = y;
               
            }
        }

        public void SetPlayerGridPosition(int x, int y)
        {
            bool positionChanged = false;

            if (_playerCoordinates.x != x)
            {
                _playerCoordinates.x = x;
                positionChanged = true;
            }

            if (_playerCoordinates.y != y)
            {
                _playerCoordinates.y = y;
                positionChanged = true;
            }

            if (positionChanged)
            {
                OnPositionChanged();
            }
        }

        public GridCoordinates GetPlayerGridCoordinates()
        {
            return _playerCoordinates;
        }

        private void OnPositionChanged()// tell the movement behaviour to move to the new coordinates
        {
            Vector3 tilePosition = Gameboard.instance.tiles[_playerCoordinates.x,_playerCoordinates.y].transform.position; // convert 2d array index to corresponding vector 3 pos
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

        public void AddCardToInventory(int cardStrength)
        {
            _playerInventory.AddItem(cardStrength);
        }

        public void SetAttackingCard(int inventorySlot)
        {
            _attackingCardValue = GetCardFromInventorySlot(inventorySlot);
            BoardGameManager.instance.PickedAttackingCard();
        }

        public Inventory GetInventory()
        {
            return _playerInventory;
        }


        public int GetAttackingCardValue()
        {
            return _attackingCardValue;
        }
    }
}