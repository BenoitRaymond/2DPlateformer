using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour, ISaveable
{
    [SerializeField] private string _name = "toto";
    [SerializeField] private int _level = 1;

    public object CaptureState()
    {
        return new SaveData
        {
            name = _name,
            level = _level
        };
    }
    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        _level = saveData.level;
        _name = saveData.name;
    }

    [Serializable]
    private struct SaveData
    {
        public string name;
        public int level;
        //public Transform position;
    }
}
