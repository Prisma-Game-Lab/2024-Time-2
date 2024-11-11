using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void OnDoorInteraction() 
    {
        LevelManager.Instance.changeScene(sceneName);
    }
}
