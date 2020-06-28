using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] tiles;
    [SerializeField] Transform player;
    [SerializeField] float lengthOfPerTile = 20;
    [SerializeField] int numberOfInitialTiles = 5;
    [SerializeField] TextMeshProUGUI scoreText;

    // only for visualization
    [Header("Only for Config")]
    [SerializeField] private int c_score = 0;
    [SerializeField] private int c_tileCount = 1;

    private ObjectPooler objectPooler;

    
    void Start()
    {
        objectPooler = ObjectPooler.Instance;

        // the first one will always be tile 0
        SpawnTile(0);

        for (int i = 0; i < numberOfInitialTiles - 1; i++)
        {
            SpawnTile(UnityEngine.Random.Range(0, tiles.Length)); 
        }

        UpdateUIText();
    }

    void Update()
    {
        // there will always be (numberOfInitialTiles-1) tiles before the player
        if (player.position.z > (c_tileCount - numberOfInitialTiles + 1) * lengthOfPerTile)
        {
            SpawnTile(UnityEngine.Random.Range(0, tiles.Length));
        }
    }

    private void SpawnTile(int tileIndex)
    {
        var tag = "Tile_" + (tileIndex + 1).ToString();

        //GameObject newTile = Instantiate(tiles[tileIndex], Vector3.forward * _tileCount * lengthOfPerTile, Quaternion.identity);
        GameObject newTile = objectPooler.SpawnFromPool(tag, Vector3.forward * c_tileCount * lengthOfPerTile, Quaternion.identity);
        c_tileCount++;
        UpdateUIText();
    }

    private void UpdateUIText()
    {
        // _score = _tileCount - 5;
        c_score = Mathf.FloorToInt(player.position.z / lengthOfPerTile);
        c_score = (c_score <= 0)? 0: c_score;
        scoreText.text = "Score: " + c_score.ToString();
    }
}
