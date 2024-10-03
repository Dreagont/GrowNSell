using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Tool System/Tool")]
public class ToolData : ScriptableObject
{
    public int ToolEnergy;
    public int ToolTier;
    public GameObject ToolAnimation;
}
