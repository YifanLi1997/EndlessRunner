using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver = false;
    public static bool gameStarted = false;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameStartPanel;

    void Start()
    {
        gameOver = false;
        gameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputController.tap == true)
        {
            gameStarted = true;
            gameStartPanel.SetActive(false);
        }

        if (gameOver)
        {
            gameOverPanel.SetActive(true);
        }
    }
}
