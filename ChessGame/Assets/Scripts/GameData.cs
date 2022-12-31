// Caleb Smith
// 12/28/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessGame;

public class GameData : MonoBehaviour
{
    // properties
    [Header("Data")]
    public GamePiece SelectedPiece;
    public TileBehaviour CurrentTile;
    public TileBehaviour NewTile;
    public int MoveCounter = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MakeMove()
    {
        // Set pieces next location
        SelectedPiece.NextLocation = NewTile;
        // Do move checks
        if (MoveCounter % 2 == 0 && SelectedPiece.Color == PieceColor.Black || MoveCounter % 2 == 1 && SelectedPiece.Color == PieceColor.White)
        {
            return;
        }
        // update move counter
        MoveCounter++;
        // Change piece position
        SelectedPiece.gameObject.transform.position = SelectedPiece.NextLocation.gameObject.transform.position;
        SelectedPiece.TileSector = SelectedPiece.NextLocation.TileSector;
        SelectedPiece.TileIndex = SelectedPiece.NextLocation.TileIndex;
        // Update piece location
        SelectedPiece.CurrentLocation = SelectedPiece.NextLocation;
        SelectedPiece.NextLocation = null;
        // update original tile and new tile
        NewTile.OccupyingObject = CurrentTile.OccupyingObject;
        CurrentTile.OccupyingObject = null;
        CurrentTile.UpdateStatus();
        NewTile.UpdateStatus();
    }
}
