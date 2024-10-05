using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryFrame : MonoBehaviour
{
    public GameObject MenuIcon;

    private void OnDisable()
    {
        MenuIcon.SetActive(true);
    }

    private void OnEnable()
    {
        MenuIcon.SetActive(false);
    }
}
