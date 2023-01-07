// Caleb Smith
// 12/28/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessGame;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    // properties
    [Header("Data")]
    public GamePiece SelectedPiece;
    public TileBehaviour CurrentTile;
    public TileBehaviour NewTile;
    public int MoveCounter = 0;
    public List<GamePiece> GamePieces = new List<GamePiece>();
    public Overseer OverseerBlack;
    public Overseer OverseerWhite;
    public bool IsOver = false;
    public PieceColor Winner = PieceColor.None;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] PieceObjects = GameObject.FindGameObjectsWithTag("Pieces");
        foreach (GameObject o in PieceObjects)
        {
            GamePiece piece = o.GetComponent("GamePiece") as GamePiece;
            GamePieces.Add(piece);
        }
        Overseer[] Overseers = GameObject.FindObjectsOfType<Overseer>();
        foreach (Overseer o in Overseers)
        {
            if (o.Color == PieceColor.White)
            {
                OverseerWhite = o;
            }
            if (o.Color == PieceColor.Black)
            {
                OverseerBlack = o;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void MakeMove()
    {
        // Null Reference Check
        if (SelectedPiece == null)
        {
            return;
        }
        // Set pieces next location
        SelectedPiece.NextLocation = NewTile;
        // Do move checks
        if (MoveCounter % 2 == 0 && SelectedPiece.Color == PieceColor.Black || MoveCounter % 2 == 1 && SelectedPiece.Color == PieceColor.White)
        {
            return;
        }
        if (SelectedPiece.MoveParameterCheck() == false)
        {
            return;
        }
        // update move counter
        MoveCounter++;
        // Destroy piece in next location if applicable
        if (NewTile.IsOccupied == true)
        {
            GamePieces.Remove(NewTile.OccupyingObject);
            if (NewTile.OccupyingObject == OverseerWhite)
            {
                OverseerWhite = null;
            }
            if (NewTile.OccupyingObject == OverseerBlack)
            {
                OverseerBlack = null;
            }
            Destroy(NewTile.OccupyingObject.gameObject);
            NewTile.OccupyingObject = null;
            NewTile.UpdateStatus(this);
        }
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
        CurrentTile.UpdateStatus(this);
        NewTile.UpdateStatus(this);
        foreach (var piece in GamePieces)
        {
            piece.CurrentLocation.UpdateStatus(this);
            piece.UpdateButtonStatus(this);
            piece.UpdateSceneStatus(this);
        }
        // Check win condition
        if (OverseerBlack == null)
        {
            IsOver = true;
            Winner = PieceColor.White;
            LoadMenu();
        }
        if (OverseerWhite == null)
        {
            IsOver = true;
            Winner = PieceColor.Black;
            LoadMenu();
        }
        if (GamePieces.Count == 14)
        {
            IsOver = true;
            Winner = PieceColor.None;
            LoadMenu();
        }
    }
}
