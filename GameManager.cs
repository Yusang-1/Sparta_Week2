using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Card firstCard;
    public Card secondCard;

    public TextMeshProUGUI timeTxt;
    public GameObject endTxt;

    public int cardCount = 0;
    float time = 30.0f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if(time <= 0.0f)
        {
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }    
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if(cardCount == 0)
            {
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }
}
