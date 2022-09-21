using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveSystem : MonoBehaviour
{
    private string SavePath => $"{Application.persistentDataPath}/binarySave.txt";

    [ContextMenu("Save")]
    private void Save()
    {
        var state = LoadBinaryFile();
        CaptureState(state);
        SaveBinaryFile(state);
    }

    [ContextMenu("Load")]
    private void Load()
    {
        var state = LoadBinaryFile();
        RestoreState(state);
    }

    private void SaveBinaryFile(object state)
    {
        using (var stream = File.Open(SavePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }

    public Dictionary<string, object> LoadBinaryFile()
    {
        if(!File.Exists(SavePath))
        {
            return new Dictionary<string, object>();
        }

        using(FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    private void CaptureState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.Id] = saveable.CaptureState();
        }
    }

    private void RestoreState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            if(state.TryGetValue(saveable.Id, out object value))
            {
                saveable.RestoreState(value);
            }
        }
    }
}
