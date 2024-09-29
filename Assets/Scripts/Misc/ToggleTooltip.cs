using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTooltip : MonoBehaviour
{
    public TooltipManager manager;

    private void OnDisable()
    {
        manager.gameObject.SetActive(false);
    }

}
