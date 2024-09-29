using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public int Gold = 1000;
    public int GoalGold = 5000;
    public int DayCheck = 5;

    public TextMeshProUGUI GoalDayText;
    public TextMeshProUGUI WinMessage;

    public Text GoldText;
    public Text GoalGoldText;
    public Text GoalGoldText1;

    public GameObject WinGameWindow;
    public GameObject LoseGameWindow;
    public GameObject GamePenaty;

    public GameObject goldAddText;
    public Canvas Canvas;
    void Start()
    {
        Canvas = FindAnyObjectByType<Canvas>();

    }

    void Update()
    {
        UpdateTextUI();
        CompleteGoal();

        if (Gold >= GoalGold)
        {
            GoalGoldText.color = Color.green;
        }
        else
        {
            GoalGoldText.color = Color.red;
        }
    }


    public void UpdateTextUI()
    {
        if (Gold < 100000)
        {
            GoldText.text = Gold.ToString();
        }
        else
        {
            GoldText.text = GlobalVariables.FormatNumber(Gold);

        }
        if (GoalGold < 100000)
        {
            GoalGoldText.text = GoalGold.ToString();
            GoalGoldText1.text = GoalGold.ToString();
        }
        else
        {
            GoalGoldText.text = GlobalVariables.FormatNumber(GoalGold);
            GoalGoldText1.text = GlobalVariables.FormatNumber(GoalGold);

        }
        GoalDayText.text = "in Day " + (DayCheck + 2).ToString();
    }
    public bool CanAffordItem(int itemPrice)
    {
        return Gold - itemPrice >= 0;
    }
    public void CompleteGoal()
    {
        if (GlobalVariables.currentDay == DayCheck + 3)
        {
            GamePenaty.SetActive(true);
            if (Gold < GoalGold)
            {
                LoseGame();

            }
            else
            {
                WinGame();

            }
        }

    }
    private void LoseGame()
    {
        LoseGameWindow.SetActive(true);
        GlobalVariables.CanAction = false;
    }

    private void WinGame()
    {
        WinGameWindow.SetActive(true);
        DayCheck += 7;
        GoalGold *= 4;
        WinMessage.text = "Continue with " + GoalGold.ToString() + " Goal gold.";

    }

    public void RestartGame()
    {
        GamePenaty.SetActive(false);
        GlobalVariables.currentDay = 0;
        GlobalVariables.CanAction = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SpawnGoldText(int price, bool isAdd, int quantity)
    {
        Gold = Gold + price * quantity;
        Vector3 mousePosition = Input.mousePosition;
        GameObject popup = Instantiate(goldAddText, mousePosition, Quaternion.identity, Canvas.transform);
        TextPopup goldPopup = popup.GetComponent<TextPopup>();
        goldPopup.isAdd = isAdd;
        goldPopup.isGold = true;
        goldPopup.goldPopup = (price * quantity);
    }
}
