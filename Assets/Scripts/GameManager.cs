using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameOverScreen GOS;
    int playerPoints = 0;

    public void GameOver()
    {
        GOS.Setup(playerPoints);
    }
}
