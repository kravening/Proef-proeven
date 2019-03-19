using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class UIController : MonoBehaviour
    {
        //Over het algemeen benaming beter doen. Het is nu echt ruk

        [Header("GameObjects")] [SerializeField]
        private GameObject readyCheckButton = null;

        [SerializeField] private GameObject turnUi = null;
        [SerializeField] private GameObject winUiImage = null;
        [SerializeField] private GameObject turnCards = null;
        [SerializeField] private GameObject SelectCardUI = null;

        [Header("UI Text")] [SerializeField] private Text TurnLives = null;

        [Header("Arrays")] [SerializeField] private Sprite[] WinUI = null;
        [SerializeField] private Sprite[] TurnImage = null;
        [SerializeField] private Sprite[] CardImages = null;
        [SerializeField] private Text[] AmountOfPlayerCards = null;
        [SerializeField] private Text[] AmountOfLivesText = null;


        public static UIController Instance;
        private static int _cards;
        private static int _lives;
        private int _switchCardsId;
        private static int _chosenCardId;

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
            SetPlayerInfo(2, 3);
            ShowWinUi(0);
        }
        /// <summary>
        /// Here I change to turn UI
        /// </summary>
        /// <param name="playerId"></param>
        public void ChangeTurnUI(int playerId)
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
            for (int i = 0; i < AmountOfPlayerCards.Length; i++)
            {
                if (playerId == i)
                {
                    AmountOfPlayerCards[i].text = _cards.ToString();
                }
            }

            for (int i = 0; i < AmountOfLivesText.Length; i++)
            {
                if (playerId == i)
                {
                    AmountOfLivesText[i].text = _lives.ToString();
                }
            }
        }
        /// <summary>
        /// Here I determine who's WinUI needs to be shown
        /// </summary>
        /// <param name="playerId"></param>
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
        /// <summary>
        /// Here I loop through the players to see whose turn is it and show their cards
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>

        private void SelectCard(int card1, int card2)
        {
            Image cardUi = SelectCardUI.GetComponent<Image>();
            if (_switchCardsId == 0)
            {
               for (int i = 0; i < CardImages.Length; i++)
               {
                  if (card1 == i)
                  {
                      cardUi.sprite = CardImages[i-1];
                      _chosenCardId = card1;
                      Debug.Log(card1);
                  }
               }

               if (_switchCardsId == 1)
               {
                   for (int i = 0; i < CardImages.Length; i++)
                   {
                       if (card2 == i)
                       {
                           cardUi.sprite = CardImages[i-1];
                           _chosenCardId = card2;
                           Debug.Log(card2);
                       }
                   }
               }
            }
        }

        /// <summary>
        /// Here I set player values for in the turn ui
        /// </summary>
        /// <param name="amountOfCards">Sets how many cards a has person in </param>
        /// <param name="Playerlives">How many lives set player has</param>
        private void SetPlayerInfo(int amountOfCards, int Playerlives)
        {
            Image cardImage = turnCards.GetComponent<Image>();
            cardImage.fillAmount = (float) (amountOfCards / 2.0);
            TurnLives.text = Playerlives.ToString();
            _cards = amountOfCards;
            _lives = Playerlives;
        }

        public void Click()
        {
            ChangeTurnUI(0);
            readyCheckButton.SetActive(false);
            //Start Game
        }

        public void ShowSelectCardUi()
        {
            SelectCardUI.SetActive(true);
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
                    SelectCard(6, 0);
                    _switchCardsId = 0;
                    break;
                case 1:
                    SelectCard(4, 0);
                    _switchCardsId = 1;
                    break;
            }
        }

        public void SelectedCard()
        {
            //call function SetCard battle value
            //Battle(_chosenCardID)
            Debug.Log("Call Battle");
        }
    }
}