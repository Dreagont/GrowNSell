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

    private float lastHealthChangeTime;
    public float hideDelay = 1f;  

    void Start()
    {
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
        HealthBar.gameObject.SetActive(true);
    }

    private void UpdateHealthBar()
    {
        HealthBarFill.fillAmount = GlobalVariables.UpdateFillBar(ObjectHealth, ObjectData.Health);
    }
}
