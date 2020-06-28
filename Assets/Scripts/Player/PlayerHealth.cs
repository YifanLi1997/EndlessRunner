﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerHealth = 1;
    [SerializeField] TextMeshProUGUI healthText;

    [Header("Only for Config")]
    [SerializeField] GameObject c_hit;

    void Start()
    {
        UpdateUIText();
    }

    void Update()
    {
        GameOver();
    }

    private void GameOver()
    {
        if (playerHealth <= 0)
        {
            PlayerManager.gameOver = true;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            c_hit = hit.gameObject;
            playerHealth--;
            UpdateUIText();

            //do not destory the Obstacle! Set it inactive. please consider carefully with the ObjectPooler system
            hit.gameObject.SetActive(false);
        }
    }

    private void UpdateUIText()
    {
        healthText.text = "Health: " + playerHealth.ToString();
    }

}
