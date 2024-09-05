using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SaveGameManager : MonoBehaviour
{
    public static SaveData data;
    public GameObject SaveLoadPanel;
    private void Awake()
    {
        if (data == null)
        {
            data = new SaveData();
        }

        SaveLoad.OnLoadGame += LoadData;
        SaveLoadPanel.gameObject.SetActive(false);
    }

    public void ToggleSaveLoad()
    {
        if (SaveLoadPanel.gameObject.activeInHierarchy)
        {
            SaveLoadPanel.SetActive(false);
        }
        else
        {
            SaveLoadPanel.SetActive(true);
        }
    }

    public static void SaveData()
    {
        if (data == null)
        {
            data = new SaveData();
        }

        SaveLoad.Save(data);
    }

    public void DeleteSave()
    {
        SaveLoad.DeleteSaveData();
    }

    public static void LoadData(SaveData _data)
    {
        data = _data;
    }

    public static void TryLoadData()
    {
        SaveLoad.Load();
    }
}
