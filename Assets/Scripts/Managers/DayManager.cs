using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    private float elapsedTime = 0f;
    public int currentDay = 1;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeMultiText;
    public ObjectsManager objectsManager;
    private float timeMultiplier;

    void Update()
    {
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
        currentDay++;
        objectsManager.DeWaterAll();
        Debug.Log("Day: " + currentDay);
    }

    void HandleSpeedUpInput()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GlobalVariables.timeMultiplier = 1f; 
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            GlobalVariables.timeMultiplier = 8f; 
        }
        
    }

    public void SkipDay()
    {
        AdvanceDay();
        GlobalVariables.TimeCounter = currentDay * 120;
    }
}
