// Caleb Smith
// 12/26/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessGame;

public class GamePiece : MonoBehaviour
{
    // class variables
    [Header("Test")]
    public TileBehaviour CurrentLocation;
    public TileBehaviour NextLocation;


    // properties
    [Header("Status")]
    public Sector TileSector;
    public int TileIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGameData(GameData gameData)
    {
        gameData.CurrentTile = CurrentLocation;
        gameData.SelectedPiece = this;
    }

    public virtual void ChangePosition()
    {
        //TileSector = 
        //TileIndex = 
    }
}
