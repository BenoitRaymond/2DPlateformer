using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;

    public PlayerData(PlayerController playerController)
    {
        position = new float[2];
        position[0] = playerController.transform.position.x;
        position[1] = playerController.transform.position.y; 
    }
}
