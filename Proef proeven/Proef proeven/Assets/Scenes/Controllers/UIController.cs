using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Over het algemeen benaming better doen. Het is nu echt ruk
    [SerializeField] private GameObject ReadyCheckButton = null;
    [SerializeField] private GameObject TurnUI = null;
    [SerializeField ]private GameObject WinUIImage = null;
    [SerializeField] private Text TurnUICards = null;
    
    [SerializeField] private Sprite[] WinUI = null;
    [SerializeField] private Sprite[] TurnImage = null;
    [SerializeField] private Text[] PlayerCards = null;

    
    public static UIController instance;
    private static int Cards;
    
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
        AmountOfCards(2);
    }

    private void ChangeTurnUI(int playerId)
    {
        playerId = 0;
        Image image = TurnUI.GetComponent<Image>();
        for (int i = 0; i < TurnImage.Length; i++)
        {
            if (playerId == i)
            {
                image.sprite = TurnImage[i];
                UpdateAmountOfCards(playerId);
            }    
        }
    }

    private void UpdateAmountOfCards(int playerId)
    {
        for (int i = 0; i < PlayerCards.Length; i++)
        {
            if (playerId == i)
            {
                PlayerCards[i].text= Cards.ToString();
            }
        }
    }

    private void ShowWinUI(int playerId)
    {
        Image winUI = WinUIImage.GetComponent<Image>();
        for (int i = 0; i < WinUI.Length; i++)
        {
            if (playerId == 1)
            {
                winUI.sprite = WinUI[i];
            }
        }
    }
    
    private void AmountOfCards(int amountOfCards)
    {
        TurnUICards.text = amountOfCards.ToString();
        Cards = amountOfCards;
    }

    public void Click()
    {
       ChangeTurnUI(0);
       ReadyCheckButton.SetActive(false);
       //Start Game
    }
}