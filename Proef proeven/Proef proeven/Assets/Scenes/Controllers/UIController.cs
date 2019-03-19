using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Controllers
{
    public class UIController : MonoBehaviour
    {
        //Over het algemeen benaming better doen. Het is nu echt ruk

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


        public static UIController instance;
        private static int Cards;
        private static int Lives;
        private int switchCardsID;

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

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            SetPlayerInfo(2, 3);
            ShowWinUi(0);
        }

        public void ChangeTurnUI(int playerId)
        {
            Image image = turnUi.GetComponent<Image>();
            for (int i = -1; i < TurnImage.Length; i++)
            {
                if (playerId == i)
                {
                    image.sprite = TurnImage[i];
                    PlayerInfo(playerId);
                }
            }
        }

        private void PlayerInfo(int playerId)
        {
            for (int i = 0; i < AmountOfPlayerCards.Length; i++)
            {
                if (playerId == i)
                {
                    AmountOfPlayerCards[i].text = Cards.ToString();
                }
            }

            for (int i = 0; i < AmountOfLivesText.Length; i++)
            {
                if (playerId == i)
                {
                    AmountOfLivesText[i].text = Lives.ToString();
                }
            }
        }

        private void ShowWinUi(int playerId)
        {
            Image winUi = winUiImage.GetComponent<Image>();
            for (int i = 0; i < WinUI.Length; i++)
            {
                if (playerId == i)
                {
                    //winUiImage.SetActive(true);
                    winUi.sprite = WinUI[i];
                }
            }
        }

        private void SelectCard(int card1, int card2)
        {
            Image cardUi = SelectCardUI.GetComponent<Image>();
            if (switchCardsID == 0)
            {
               for (int i = 0; i < CardImages.Length; i++)
               {
                  if (card1 == i)
                  {
                      cardUi.sprite = CardImages[i];
                  }
               }

               if (switchCardsID == 1)
               {
                   for (int i = 0; i < CardImages.Length; i++)
                   {
                       if (card2 == i)
                       {
                           cardUi.sprite = CardImages[i];
                       }
                   }
               }
            }
        }

        private void SetPlayerInfo(int amountOfCards, int Playerlives)
        {
            Image cardImage = turnCards.GetComponent<Image>();
            cardImage.fillAmount = (float) (amountOfCards / 2.0);
            TurnLives.text = Playerlives.ToString();
            Cards = amountOfCards;
            Lives = Playerlives;
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

        public void selectCardUI(int buttonId)
        {
            switch (buttonId)
            {
                case 0:
                    SelectCard(6, 0);
                    switchCardsID = 0;
                    break;
                case 1:
                    SelectCard(0, 5);
                    switchCardsID = 1;
                    break;
            }
        }
    }
}