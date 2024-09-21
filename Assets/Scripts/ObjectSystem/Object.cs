using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object : MonoBehaviour
{
    public Vector3 ObjectPosition;
    public ObjectData ObjectData;
    public int ObjectHealth;
    public Image HealthBarFill;
    public GameObject HealthBar;
    public ExperientManager ExperientManager;
    public GameObject Canvas;
    public GameObject XPPopup;
    private float lastHealthChangeTime;
    public float hideDelay = 1f;  

    void Start()
    {
        ExperientManager = FindAnyObjectByType<ExperientManager>();
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        ObjectHealth = ObjectData.Health;
        UpdateHealthBar();
        lastHealthChangeTime = Time.time;
        HealthBar.gameObject.SetActive(false);

    }

    void Update()
    {
        UpdateHealthBar();

        if (Time.time - lastHealthChangeTime > hideDelay)
        {
            HealthBar.gameObject.SetActive(false);  
        }


    }

    public void ModifyHealth(int damage)
    {
        ObjectHealth -= damage;
        lastHealthChangeTime = Time.time;
        if (HealthBar != null)
        {
            HealthBar.gameObject.SetActive(true);

        }
    }

    private void UpdateHealthBar()
    {
        HealthBarFill.fillAmount = GlobalVariables.UpdateFillBar(ObjectHealth, ObjectData.Health);
    }

    private void OnDestroy()
    {
        if (ExperientManager != null)
        {
            ExperientManager.Experient += ObjectData.Experience;
        }

        Vector3 mousePosition = Input.mousePosition;

        if (Canvas != null && XPPopup != null)
        {
            GameObject popup = Instantiate(XPPopup, ObjectPosition, Quaternion.identity, Canvas.transform);
            TextPopup goldPopup = popup.GetComponent<TextPopup>();
            goldPopup.isAdd = false;
            goldPopup.isGold = false;
            goldPopup.goldPopup = ObjectData.Experience;
        }
    }

}
