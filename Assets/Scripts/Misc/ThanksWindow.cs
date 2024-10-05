using TMPro;
using UnityEngine;

public class ThanksWindow : MonoBehaviour
{
    public GameManager GameManager;
    public ActionManager ActionManager;
    public TMP_InputField InputField;
    public int MaxCharacters = 20;

    void Start()
    {
    }
    private void Awake()
    {
        ActionManager = FindAnyObjectByType<ActionManager>();
        if (DontDestroyWhenReload.Instance.isRestart)
        {
            gameObject.SetActive(false);
            ActionManager.Pickup();
        }
        else
        {
            setLimmit();

        }
    }

    public void setLimmit()
    {
        GlobalVariables.CanInput = false;
        GameManager.canAction = false;
        InputField.characterLimit = MaxCharacters;
        InputField.onValueChanged.AddListener(ValidateInput);
    }
    void Update()
    {

    }

    public void StartButtonPressed()
    {
        GlobalVariables.CanInput = true;
        GameManager.canAction = false;


        if (InputField.text == "UnknowPlayer" || InputField.text == "")
        {
            GameManager.PlayerName = "UnknowPlayer";
        }
        else
        {
            GameManager.PlayerName = InputField.text;
        }
        ActionManager.Pickup();
        DontDestroyWhenReload.Instance.PlayerName = GameManager.PlayerName;
        DontDestroyWhenReload.Instance.isRestart = true;    
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        InputField.onValueChanged.RemoveListener(ValidateInput);
    }

    private void ValidateInput(string input)
    {
        string validInput = "";
        foreach (char c in input)
        {
            if (char.IsLetterOrDigit(c))
            {
                validInput += c;
            }
        }

        InputField.text = validInput;
    }
}
