using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyWhenReload : MonoBehaviour
{
    public static DontDestroyWhenReload Instance;

    public bool isRestart = false;
    public string PlayerName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
