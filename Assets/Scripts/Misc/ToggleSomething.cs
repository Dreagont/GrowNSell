using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSomething : MonoBehaviour
{
    public GameObject ToggleObject;

    public void ToggleIt()
    {
        if (ToggleObject.activeInHierarchy)
        {
            ToggleObject.SetActive(false);
        } else
        {
            ToggleObject.SetActive(true);
        }
    }

    public void ToggleAction()
    {
        GlobalVariables.CanAction = !GlobalVariables.CanAction;
    }
}
