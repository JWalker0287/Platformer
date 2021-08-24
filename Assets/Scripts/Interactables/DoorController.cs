using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : Interactable
{
    public int doorID;
    public int destinationID;
    public string scene;

    public override void Interact()
    {
        SceneLoader.OpenDoor(destinationID, scene);
    }
}
