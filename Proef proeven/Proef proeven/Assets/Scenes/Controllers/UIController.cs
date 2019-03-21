using System;
using Data;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Controllers
{
    public class UIController : MonoBehaviour
    {
        //Over het algemeen benaming beter doen. Het is nu echt ruk

        [Header("GameObjects")] [SerializeField]
        private GameObject readyCheckButton = null;

        [SerializeField]private AnimationClip _fadeIn;
        [SerializeField] private GameObject camera;
        [SerializeField] private GameObject turnUi = null;
        [SerializeField] private GameObject winUiImage = null;
        [SerializeField] private GameObject turnCards = null;
        [SerializeField] private GameObject selectCardUi = null;
        [SerializeField] private GameObject showCardUi = null;

        [Header("UI Text")] 
        [SerializeField] private Text TurnLives = null;

        [Header("Arrays")] [SerializeField] private Sprite[] WinUI = null;
        [SerializeField] private Sprite[] TurnImage = null;
        [SerializeField] private Sprite[] CardImages = null;
        [SerializeField] private Text[] AmountOfPlayerCards = null;
        [SerializeField] private Text[] AmountOfLivesText = null;


        public static UIController Instance;
        private static int _cards;
        private static int _lives;
        private int _switchCardsId;
        private int _chosenCardId;
        private Animation _animation;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void Start()
        {
            Init();
        }
        
        private void Init()
        {
            _animation = camera.GetComponent<Animation>();
        }
        /// <summary>
        /// Here I change to turn UI
        /// </summary>
        /// <param name="playerId"></param>
        public void ChangeTurnUi(int playerId)
        {
            Image image = turnUi.GetComponent<Image>();
            for (int i = 0; i < TurnImage.Length; i++)
            {
                if (playerId == i)
                {
                    image.sprite = TurnImage[i];
                    PlayerInfo(playerId);
                }
            }
        }
        /// <summary>
        /// Here I check how many how many that person has and I show it on the canvas
        /// </summary>
        /// <param name="playerId"></param>
        private void PlayerInfo(int playerId)
        {
            Debug.Log(playerId);
            for (int i = 0; i < AmountOfPlayerCards.Length; i++)
            {
                if (playerId == i)
                {
                    //AmountOfPlayerCards[i].text = _cards.ToString();
                }
            }

            for (int i = 0; i < AmountOfLivesText.Length; i++)
            {
                if (playerId == i)
                {
                    //AmountOfLivesText[i].text = _lives.ToString();
                }
            }
        }
        /// <summary>
        /// Here I determine who's WinUI needs to be shown
        /// </summary>
        /// <param name="playerId"></param>
        /*
        private void ShowWinUi(int playerId)
        {
            Image winUi = winUiImage.GetComponent<Image>();
            for (int i = 1; i < WinUI.Length; i++)
            {
                if (playerId == i)
                {
                    //winUiImage.SetActive(true);
                    winUi.sprite = WinUI[i];
                }
            }
        }
        */
        /// <summary>
        /// Here I loop through the players to see whose turn is it and show their cards
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>
        private void SelectCard(int CardId)
        {
            Player currentPlayer = PlayerManager.instance.GetCurrentPlayer();
            int card1 = currentPlayer.GetCardFromInventorySlot(0);
            int card2 = currentPlayer.GetCardFromInventorySlot(1);
            Image cardUi = showCardUi.GetComponent<Image>();
            if (CardId == 0)
            {
                for (int i = 0; i < CardImages.Length; i++)
                {
                    if (card1 == i)
                    {
                        cardUi.sprite = CardImages[i];
                        _chosenCardId = card1;
                        Debug.Log(card1);
                    }
                }
            }

            if (CardId == 1)
            {
                Debug.Log(card2);
                for (int i = 0; i < CardImages.Length; i++)
                {
                    Debug.Log(card2);
                    if (card2 == i)
                    {
                           cardUi.sprite = CardImages[i];
                           _chosenCardId = card2;
                           Debug.Log(card2);
                    }
                }
             }

            _switchCardsId = CardId;
        }

        /// <summary>
        /// Here I set player values for in the turn ui
        /// </summary>
        /// <param name="amountOfCards">Sets how many cards a has person in </param>
        /// <param name="playerlives">How many lives set player has</param>
        public void SetPlayerInfo(int amountOfCards, int playerlives, int CurrentPlayer)
        {
            
            print(playerlives);
            _cards = amountOfCards;    
            _lives = playerlives;
            Image cardImage = turnCards.GetComponent<Image>();
            cardImage.fillAmount = (float) (amountOfCards / 2.0);
            TurnLives.text = _lives.ToString();
            PlayerInfo(CurrentPlayer);
            AmountOfLivesText[CurrentPlayer].text = _lives.ToString();
            AmountOfPlayerCards[CurrentPlayer].text = _cards.ToString();
        }

        public void Click()
        {
            TurnManager.instance.AdvanceTurn();
            readyCheckButton.SetActive(false);
            _animation.clip = _fadeIn;
            _animation.Play();
            //Start Game
        }

        public void ShowSelectCardUi(bool isActive)
        {
            selectCardUi.SetActive(isActive);
        }
        
        /// <summary>
        /// Check which button is clicked and sents the int aka the card value to Select card
        /// </summary>
        /// <param name="buttonId"> These are the ID of the buttons clicked</param>
        public void selectCardUI(int buttonId)
        {
            switch (buttonId)
            {
                case 0:
                    SelectCard(0);
                    break;
                case 1:
                    Debug.Log("iets");
                    SelectCard(1);
                    break;
            }
        }

        public void SelectedCard()
        {
            ShowSelectCardUi(false);
            if (BoardGameManager.instance.GetCurrentGamePhase() == Enums.GamePhase.BattlePhase)
            {
                PlayerManager.instance.GetCurrentPlayer().SetAttackingCard(_switchCardsId);
                PlayerManager.instance.GetCurrentPlayer().GetInventory().RemoveCard(_switchCardsId);
            }
            else if (BoardGameManager.instance.GetCurrentGamePhase() == Enums.GamePhase.DefendPhase)
            {
                Gameboard.instance.playerBases[PlayerManager.instance.GetCurrentPlayer().GetPlayerID()].SetDefendingCard(_chosenCardId);
                PlayerManager.instance.GetCurrentPlayer().GetInventory().RemoveCard(_switchCardsId);
            }
            Debug.Log(_chosenCardId);
        }
    }
}