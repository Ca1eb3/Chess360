// Caleb Smith
// 12/28/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessGame;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UIElements;

public class GameData : MonoBehaviour
{
    // properties
    [Header("Data")]
    public GamePiece SelectedPiece;
    public TileBehaviour CurrentTile;
    public TileBehaviour NewTile;
    public int MoveCounter = 0;
    public List<GamePiece> GamePieces = new List<GamePiece>();
    public List<GamePiece> WhiteGamePieces = new List<GamePiece>();
    public List<GamePiece> BlackGamePieces = new List<GamePiece>();
    public Overseer OverseerBlack;
    public Overseer OverseerWhite;
    public bool IsOver = false;
    public PieceColor Winner = PieceColor.None;
    public TextMeshProUGUI MoveRecordText;
    public GameObject MoveRecordContent;

    // Start is called before the first frame update
    void Start()
    {
        UpdateGamePieces();
        MoveRecordContent = GameObject.Find("MoveRecordContent");
        MoveRecordText = MoveRecordContent.GetComponent<TextMeshProUGUI>().ConvertTo<TextMeshProUGUI>();
        UpdateOverseers();
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateGamePieces()
    {
        GamePieces.Clear();
        WhiteGamePieces.Clear();
        BlackGamePieces.Clear();
        GameObject[] PieceObjects = GameObject.FindGameObjectsWithTag("Pieces");
        foreach (GameObject o in PieceObjects)
        {
            GamePiece piece = o.GetComponent("GamePiece") as GamePiece;
            GamePieces.Add(piece);
            if (piece.Color == PieceColor.White)
            {
                WhiteGamePieces.Add(piece);
            }
            else
            {
                BlackGamePieces.Add(piece);
            }
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void UpdateOverseers()
    {
        OverseerBlack = null;
        OverseerWhite = null;
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

    public void MakeMove()
    {
        // string to append move to move record
        string moveRecordStringAppend = "";
        if (MoveCounter % 2 == 0)
        {
            if (MoveCounter == 0)
            {
                int moveNumber = MoveCounter / 2 + 1;
                moveRecordStringAppend = moveNumber + ". ";
            }
            else
            {
                int moveNumber = MoveCounter / 2 + 1;
                moveRecordStringAppend = " " + moveNumber + ". ";
            }
        }
        else
        {
            moveRecordStringAppend = " ";
        }
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
        if (SelectedPiece.MoveParameterCheckCurrentState() == false)
        {
            return;
        }
        // append new tile to move record string
        moveRecordStringAppend = moveRecordStringAppend + SelectedPiece.PieceType.ToString() + SelectedPiece.NextLocation.TileSector.ToString() + SelectedPiece.NextLocation.TileIndex.ToString();
        // update move counter
        MoveCounter++;
        // Destroy piece in next location if applicable
        if (NewTile.IsOccupied == true)
        {
            string pieceDestroyed = "x";
            GamePieces.Remove(NewTile.OccupyingObject);
            if (NewTile.OccupyingObject.PieceType == PieceString.O)
            {
                UpdateOverseers();
                pieceDestroyed = "#";
            }
            Destroy(NewTile.OccupyingObject.gameObject);
            NewTile.OccupyingObject = null;
            NewTile.UpdateStatus(this);
            moveRecordStringAppend = moveRecordStringAppend + pieceDestroyed;
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
        }
        if (OverseerWhite == null)
        {
            IsOver = true;
            Winner = PieceColor.Black;
        }
        if (GamePieces.Count == 14)
        {
            IsOver = true;
            Winner = PieceColor.None;
        }
        // Update Move Record
        MoveRecordText.text = MoveRecordText.text + moveRecordStringAppend;
    }

    public void CheckmateAnalyzer()
    {

    }
}
