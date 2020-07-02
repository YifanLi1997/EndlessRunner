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
    [SerializeField] TextMeshProUGUI numberOfTilesText;

    // only for visualization
    [Header("Only for Config")]
    //[SerializeField] private int c_score = 0; // no longer used
    [SerializeField] private int c_tileCount = 0;

    private ObjectPooler objectPooler;

    
    void Start()
    {
        objectPooler = ObjectPooler.Instance;

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
        objectPooler.SpawnFromPool(tag, Vector3.forward * c_tileCount * lengthOfPerTile, Quaternion.identity);
        c_tileCount++;
        UpdateUIText();
    }

    // TODO: better make another script to handle the UI text
    private void UpdateUIText()
    {
        #region no longer used
        // TODO: bug - the score will jump from 0 directly to 2
        //c_score = c_tileCount - 5;
        //c_score = Mathf.FloorToInt(player.position.z / lengthOfPerTile);
        //c_score = (c_score <= 0) ? 0 : c_score;
        //numberOfTilesText.text = "Score: " + c_score.ToString();
        #endregion

        numberOfTilesText.text = "Tiles: " + c_tileCount.ToString();
    }
}
