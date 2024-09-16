using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    private float elapsedTime = 0f;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeMultiText;
    public ObjectsManager objectsManager;
    private float timeMultiplier;
    public int currentDay;

    void Update()
    {
        currentDay = GlobalVariables.currentDay;
        UpdateTime();
        HandleSpeedUpInput();
        UpdateUIText();

    }

    public void UpdateTime()
    {
        timeMultiplier = GlobalVariables.timeMultiplier;
        elapsedTime += Time.deltaTime * timeMultiplier;
        GlobalVariables.TimeCounter += Time.deltaTime * timeMultiplier;

        if (elapsedTime >= GlobalVariables.DayDuration)
        {
            AdvanceDay();
        }
    }

    public void UpdateUIText()
    {
        timeText.text = GlobalVariables.FormatTime(elapsedTime);
        timeMultiText.text = timeMultiplier.ToString() + "X";
    }
    void AdvanceDay()
    {
        elapsedTime = 0f;
        GlobalVariables.currentDay++;
        GlobalVariables.TimeCounter = GlobalVariables.currentDay * 120f;
        objectsManager.UpdateSeedSpriteAndState();
        objectsManager.DeWaterAll();
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
        AdvanceDay();
    }
}
