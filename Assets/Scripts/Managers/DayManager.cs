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

    private void Start()
    {
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
        objectsManager.DeWaterAll();
        gameManager.RefillEnergy();
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
        StartCoroutine(FadeAndAdvanceDay());
    }

    private IEnumerator FadeAndAdvanceDay()
    {
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
    }

    private void SetFadeAlpha(float alpha)
    {
        Color color = SkipDayFade.color;
        color.a = alpha;
        SkipDayFade.color = color;
    }
}
