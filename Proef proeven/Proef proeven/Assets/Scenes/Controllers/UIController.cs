using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Over het algemeen benaming better doen. Het is nu echt ruk

    [Header("GameObjects")]
    [SerializeField] private GameObject readyCheckButton = null;
    [SerializeField] private GameObject turnUi = null;
    [SerializeField] private GameObject winUiImage = null;
    [SerializeField] private GameObject turnCards = null;
    
    [Header("UI Text")]  
    [SerializeField] private Text TurnLives = null;
    
    [Header("Arrays")]
    [SerializeField] private Sprite[] WinUI = null;
    [SerializeField] private Sprite[] TurnImage = null;
    [SerializeField] private Text[] AmountOfPlayerCards = null;
    [SerializeField] private Text[] AmountOfLivesText = null;

    
    public static UIController instance;
    private static int Cards;
    private static int Lives;
    
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
        SetPlayerInfo(3, 3);
    }

    private void ChangeTurnUI(int playerId)
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

    private void PlayerInfo(int playerId)
    {
        for (int i = 0; i < AmountOfPlayerCards.Length; i++)
        {
            if (playerId == i)
            {
                AmountOfPlayerCards[i].text= Cards.ToString();
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
        Image winUI = winUiImage.GetComponent<Image>();
        for (int i = 0; i < WinUI.Length; i++)
        {
            if (playerId == i)
            {
                winUI.sprite = WinUI[i];
            }
        }
    }
    
    private void SetPlayerInfo(int amountOfCards, int Playerlives)
    {
        Image cardImage = turnCards.GetComponent<Image>();
        cardImage.fillAmount = (float) (amountOfCards / 4.0);
        TurnLives.text = Playerlives.ToString();
        Cards = amountOfCards;
        Lives = Playerlives;
    }

    public void Click()
    {
       ChangeTurnUI(1);
       readyCheckButton.SetActive(false);
       //Start Game
    }
}