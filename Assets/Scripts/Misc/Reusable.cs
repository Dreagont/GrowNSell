using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reusable : MonoBehaviour
{
    public GameObject GameObjectToggle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleGameObject(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }
}
