using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    int points = 0;
    public void GameOver()
    {
        GameOverScreen.Setup(points);
    }
}
