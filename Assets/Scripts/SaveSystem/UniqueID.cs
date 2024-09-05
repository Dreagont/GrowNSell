using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]
public class UniqueID : MonoBehaviour
{
    [ReadOnly, SerializeField] private string id = Guid.NewGuid().ToString();

    [SerializeField] private static SerializableDictionary<string, GameObject> idDatabse = new SerializableDictionary<string, GameObject>();

    public string ID => id;

    private void OnValidate()
    {
        if (idDatabse.ContainsKey(id))
        {
            Generate();

        } else
        {
            idDatabse.Add(id, this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (idDatabse.ContainsKey(id)) { idDatabse.Remove(id); }
    }

    private void Generate()
    {
        id = Guid.NewGuid().ToString();
        idDatabse.Add(id, this.gameObject);
    }
}
