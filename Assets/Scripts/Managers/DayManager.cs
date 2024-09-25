using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    private float elapsedTime = 0f;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeMultiText;
    public ObjectsManager objectsManager;
    private float timeMultiplier;
    public int currentDay;
    private GameManager gameManager;
    public Text DayCounter;
    public Image SkipDayFade;
    private ShopUIController shopUIController;
    public bool canPassDay;
    private void Start()
    {
        shopUIController = FindAnyObjectByType<ShopUIController>();
        SkipDayFade.gameObject.SetActive(true);
        gameManager = GetComponentInParent<GameManager>();
        SkipDayFade.color = new Color(SkipDayFade.color.r, SkipDayFade.color.g, SkipDayFade.color.b, 0); 
    }

    void Update()
    {
        UpdateTime();
        HandleSpeedUpInput();
        UpdateUIText();
        if (Input.GetKeyDown(KeyCode.K))
        {
            SkipDay();
        }
    }

    public void UpdateTime()
    {
        timeMultiplier = GlobalVariables.timeMultiplier;
        GlobalVariables.TimeCounter += Time.deltaTime * timeMultiplier;
    }

    public void UpdateUIText()
    {
        timeText.text = GlobalVariables.FormatTime(elapsedTime);
        timeMultiText.text = timeMultiplier.ToString() + "X";
        DayCounter.text = "Day " + GlobalVariables.currentDay.ToString();
    }

    void AdvanceDay()
    {
        elapsedTime = 0f;
        GlobalVariables.currentDay++;
        GlobalVariables.TimeCounter = GlobalVariables.currentDay * 120f;
        objectsManager.UpdateSeedSpriteAndState();
        objectsManager.WaterCheck();
        objectsManager.DeWaterAll();
        gameManager.RefillEnergy();
        gameManager.ShopRollCost = 10;
        shopUIController.RollNewItemsFree();
        Debug.Log("Day: " + GlobalVariables.currentDay);
    }

    void HandleSpeedUpInput()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GlobalVariables.timeMultiplier = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            GlobalVariables.timeMultiplier = 10f;
        }
    }

    public void SkipDay()
    {
        if (canPassDay)
        {
            StartCoroutine(FadeAndAdvanceDay());

        }
    }
    private IEnumerator FadeAndAdvanceDayDelay()
    {
        yield return new WaitForSeconds(1);
        canPassDay = true;

    }
    private IEnumerator FadeAndAdvanceDay()
    {
        canPassDay = false;
        for (float t = 0; t < 0.5f; t += Time.deltaTime)
        {
            float normalizedTime = t / 0.5f;
            SetFadeAlpha(Mathf.Lerp(0, 1, normalizedTime));
            yield return null;
        }
        SetFadeAlpha(1); 

        AdvanceDay(); 

        for (float t = 0; t < 0.5f; t += Time.deltaTime)
        {
            float normalizedTime = t / 0.5f;
            SetFadeAlpha(Mathf.Lerp(1, 0, normalizedTime));
            yield return null;
        }
        SetFadeAlpha(0); 
        StartCoroutine(FadeAndAdvanceDayDelay());   
    }

    private void SetFadeAlpha(float alpha)
    {
        Color color = SkipDayFade.color;
        color.a = alpha;
        SkipDayFade.color = color;
    }
}
