using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    private float elapsedTime = 0f;
    public int currentDay = 1;
    public float timeMultiplier = 1f;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeMultiText;
    public ObjectsManager objectsManager;


    void Update()
    {
        UpdateTime();
        HandleSpeedUpInput();
        UpdateUIText();

    }

    public void UpdateTime()
    {
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
            timeMultiplier = 1f; 
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            timeMultiplier = 8f; 
        }
        
    }

    public void SkipDay()
    {
        AdvanceDay();
        GlobalVariables.TimeCounter = currentDay * 120;
    }
}
