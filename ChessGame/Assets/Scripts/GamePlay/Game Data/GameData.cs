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
using System.Linq;
using System.Threading;

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
    public TextMeshProUGUI EvaluationRecordText;
    public GamePiece RemovedPiece;
    public bool CheckBlack = false;
    public bool CheckWhite = false;
    public bool ValidMove = false;
    public bool PieceDestroyed = false;
    public List<GamePiece> AttackingWhite;
    public List<GamePiece> AttackingBlack;
    public PieceColor Turn = PieceColor.White;
    public bool Checkmate = false;
    public List<Tuple<TileBehaviour, GamePiece>> GlobalInvalidMoves = new List<Tuple<TileBehaviour, GamePiece>>();
    public List<Tuple<TileBehaviour, GamePiece>> GlobalValidMoves = new List<Tuple<TileBehaviour, GamePiece>>();
    public BoardStateAnalyzer BoardStateAnalyzer = new BoardStateAnalyzer();

    // Start is called before the first frame update
    void Start()
    {
        UpdateGamePieces();
        MoveRecordText = GameObject.Find("MoveRecordContent").GetComponent<TextMeshProUGUI>();
        EvaluationRecordText = GameObject.Find("BoardEvaluationContent").GetComponent<TextMeshProUGUI>();
        UpdateOverseers();
        BoardStateAnalyzer.SetUp(GamePieces);
        UpdateBoardEvaluation();
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
        if (MoveCounter % 2 == 0)
        {
            Turn = PieceColor.White;
        }
        else
        {
            Turn = PieceColor.Black;
        }
        // Null Reference Check
        if (SelectedPiece == null)
        {
            return;
        }
        // Set pieces next location
        SelectedPiece.NextLocation = NewTile;
        // Do move checks
        if (Turn != SelectedPiece.Color)
        {
            return;
        }
        if (!SelectedPiece.ValidMoveGraph.ValidMoves.Contains(SelectedPiece.NextLocation))
        {
            return;
        }

        // Move into probable state to continue move checks
        NewPieceState(NewTile, CurrentTile, SelectedPiece);

        // The game is in the probable next game state
        foreach (var piece in BlackGamePieces)
        {
            if (RemovedPiece != null && piece.Equals(RemovedPiece))
            {
                continue;
            }
            if (piece.AttackOverseerCheck(OverseerWhite))
            {
                CheckWhite = true;
                AttackingWhite.Add(piece);
            }
        }
        foreach (var piece in WhiteGamePieces)
        {
            if (RemovedPiece != null && piece.Equals(RemovedPiece))
            {
                continue;
            }
            if (piece.AttackOverseerCheck(OverseerBlack))
            {
                CheckBlack = true;
                AttackingBlack.Add(piece);
            }
        }
        if (CheckBlack && Turn == PieceColor.Black || CheckWhite && Turn == PieceColor.White)
        {
            ValidMove = false;
        }
        else
        {
            ValidMove = true;
        }

        // replace state
        ReplacePieceState(NewTile, CurrentTile, SelectedPiece);

        // return if putting self in check
        if (ValidMove == false)
        {
            ResetStatus();
            return;
        }

        // The below code should only execute if all checks have passed
        // update move counter
        MoveCounter++;
        // Destroy piece in next location if applicable
        if (NewTile.IsOccupied == true)
        {
            PieceDestroyed = true;
            DestroyPiece();
        }
        // Change piece position
        SelectedPiece.gameObject.transform.position = SelectedPiece.NextLocation.gameObject.transform.position;
        SelectedPiece.TileSector = SelectedPiece.NextLocation.TileSector;
        SelectedPiece.TileIndex = SelectedPiece.NextLocation.TileIndex;
        // Update piece location
        SelectedPiece.PreviousLocation = SelectedPiece.CurrentLocation;
        SelectedPiece.CurrentLocation = SelectedPiece.NextLocation;
        SelectedPiece.NextLocation = null;
        // update original tile and new tile
        NewTile.OccupyingObject = CurrentTile.OccupyingObject;
        CurrentTile.OccupyingObject = null;
        CurrentTile.UpdateStatus(this);
        NewTile.UpdateStatus(this);
        UpdateGamePieces();
        foreach (var piece in GamePieces)
        {
            piece.CurrentLocation.UpdateStatus(this);
            piece.UpdateButtonStatus(this);
            piece.UpdateSceneStatus(this);
            piece.UpdateValidMoveGraph();
        }
        // Check win condition
        // Call Checkmate Analyzer to determine Checkmate
        if (Turn == PieceColor.White && CheckBlack == true)
        {
            CheckmateAnalyzer(PieceColor.Black);
        }
        if (Turn == PieceColor.Black && CheckWhite == true)
        {
            CheckmateAnalyzer(PieceColor.White);
        }
        if (Checkmate == true)
        {
            IsOver = true;
            Winner = Turn;
        }
        if (GamePieces.Count == 14)
        {
            IsOver = true;
            Winner = PieceColor.None;
        }
        // Prepare control bit values and calculate board state
        BitArray CheckControl = new BitArray(3);
        if (Checkmate == true)
        {
            if (Winner == PieceColor.White)
            {
                CheckControl.Set(0, true);
            }
            else
            {
                CheckControl.Set(0, true);
                CheckControl.Set(2, true);
            }
        }
        else if (CheckBlack == true)
        {
            CheckControl.Set(1, true);
            CheckControl.Set(2, true);
        }
        else if (CheckWhite == true)
        {
            CheckControl.Set(1, true);
        }
        else if (IsOver == true)
        {
            CheckControl.Set(2, true);
        }

        BoardStateAnalyzer.UpdateBoardState(SelectedPiece, CheckControl);
        // Update Move Record
        UpdateMoveRecord();
        UpdateBoardEvaluation();
        ResetStatus();
    }

    public void ResetStatus()
    {
        CheckBlack = false;
        CheckWhite = false;
        ValidMove = false;
        PieceDestroyed = false;
        // reset attacking pieces
        AttackingBlack.Clear();
        AttackingWhite.Clear();
    }

    public void UpdateBoardEvaluation()
    {
        EvaluationRecordText.text = "Piece Evaluation: " + BoardStateAnalyzer.PieceEvaluation.ToString() +"\r\n\r\nBoard Evaluation: " + BoardStateAnalyzer.BoardEvaluation.ToString();
    }

    public void UpdateMoveRecord()
    {
        // string to append move to move record
        string moveRecordStringAppend = "";
        // append move number
        if (Turn == PieceColor.White)
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

        // append piece type to move record string
        moveRecordStringAppend += SelectedPiece.PieceType.ToString();

        // append captured piece status if applicable
        if (PieceDestroyed)
        {
            moveRecordStringAppend += "x";
        }

        // append new tile to move record string
        moveRecordStringAppend += SelectedPiece.CurrentLocation.TileSector.ToString().ToLower() + SelectedPiece.CurrentLocation.TileIndex.ToString();

        // append check or checkmate status if applicable
        if (Checkmate == true)
        {
            moveRecordStringAppend += "#";
        }
        else if (CheckBlack || CheckWhite)
        {
            moveRecordStringAppend += "+";
        }

        // update text
        MoveRecordText.text = MoveRecordText.text + moveRecordStringAppend;
    }

    public void DestroyPiece()
    {
        GamePieces.Remove(NewTile.OccupyingObject);
        DestroyImmediate(NewTile.OccupyingObject.gameObject);
        NewTile.OccupyingObject = null;
        NewTile.UpdateStatus(this);
    }

    public void NewPieceState(TileBehaviour CheckTile, TileBehaviour PieceLocation, GamePiece Piece)
    {
        // Destroy piece in next location if applicable
        if (CheckTile.IsOccupied == true)
        {
            RemovedPiece = CheckTile.OccupyingObject;
            CheckTile.OccupyingObject = null;
            CheckTile.UpdateStatus(this);
        }
        // Change piece position
        Piece.TileSector = CheckTile.TileSector;
        Piece.TileIndex = CheckTile.TileIndex;
        // Update piece location
        Piece.PreviousLocation = Piece.CurrentLocation;
        Piece.CurrentLocation = CheckTile;
        // update original tile and new tile
        CheckTile.OccupyingObject = PieceLocation.OccupyingObject;
        PieceLocation.OccupyingObject = null;
        PieceLocation.UpdateStatus(this);
        CheckTile.UpdateStatus(this);
        UpdateGamePieces();
        foreach (var piece in GamePieces)
        {
            piece.CurrentLocation.UpdateStatus(this);
            piece.UpdateButtonStatus(this);
            piece.UpdateSceneStatus(this);
            piece.UpdateValidMoveGraph();
        }
    }

    public void ReplacePieceState(TileBehaviour CheckTile, TileBehaviour PieceLocation, GamePiece Piece)
    {
        // replace
        // Destroy piece in next location if applicable
        // Change piece position
        Piece.TileSector = Piece.PreviousLocation.TileSector;
        Piece.TileIndex = Piece.PreviousLocation.TileIndex;
        // Update piece location
        Piece.CurrentLocation = Piece.PreviousLocation;
        Piece.PreviousLocation = null;
        // update original tile and new tile
        PieceLocation.OccupyingObject = CheckTile.OccupyingObject;
        CheckTile.OccupyingObject = null;
        if (RemovedPiece != null)
        {
            CheckTile.OccupyingObject = RemovedPiece;
            CheckTile.UpdateStatus(this);
            RemovedPiece = null;
        }
        PieceLocation.UpdateStatus(this);
        CheckTile.UpdateStatus(this);
        UpdateGamePieces();
        foreach (var piece in GamePieces)
        {
            piece.CurrentLocation.UpdateStatus(this);
            piece.UpdateButtonStatus(this);
            piece.UpdateSceneStatus(this);
            piece.UpdateValidMoveGraph();
        }
    }
    public void CheckmateAnalyzer(PieceColor NextMoveColor)
    {
        GlobalInvalidMoves.Clear();
        GlobalValidMoves.Clear();
        UpdateGamePieces();

        if (NextMoveColor == PieceColor.White)
        {
            foreach (var piece in WhiteGamePieces)
            {
                foreach (var move in piece.ValidMoveGraph.ValidMoves)
                {
                    GlobalValidMoves.Add(new Tuple<TileBehaviour, GamePiece>(move, piece));
                }
            }
        }
        else
        {
            foreach (var piece in BlackGamePieces)
            {
                foreach (var move in piece.ValidMoveGraph.ValidMoves)
                {
                    GlobalValidMoves.Add(new Tuple<TileBehaviour, GamePiece>(move, piece));
                }
            }
        }

        // remove duplicate elements
        GlobalValidMoves = GlobalValidMoves.Distinct().ToList();

        foreach (var moveTuple in GlobalValidMoves)
        {
            MoveAnalyzer(NextMoveColor, moveTuple.Item1, moveTuple.Item2.CurrentLocation, moveTuple.Item2);
        }

        foreach (var move in GlobalInvalidMoves)
        {
            if (GlobalValidMoves.Contains(move))
            {
                GlobalValidMoves.Remove(move);
            }
        }

        if (GlobalValidMoves.Count == 0)
        {
            Checkmate = true;
            return;
        }
    }
    public void MoveAnalyzer(PieceColor NextMoveColor, TileBehaviour CheckTile, TileBehaviour PieceLocation, GamePiece CheckPiece)
    {
        if (CheckTile == null || PieceLocation == null || CheckPiece == null)
        {
            return;
        }

        // Move into probable state to continue move checks
        NewPieceState(CheckTile, PieceLocation, CheckPiece);

        // The game is in the probable next game state
        if (NextMoveColor == PieceColor.White)
        {
            foreach (var piece in BlackGamePieces)
            {
                if (RemovedPiece != null && piece.Equals(RemovedPiece))
                {
                    continue;
                }
                if (piece.AttackOverseerCheck(OverseerWhite))
                {
                    GlobalInvalidMoves.Add(new Tuple<TileBehaviour, GamePiece>(CheckTile, CheckPiece));
                    break;
                }
            }
        }
        else
        {
            foreach (var piece in WhiteGamePieces)
            {
                if (RemovedPiece != null && piece.Equals(RemovedPiece))
                {
                    continue;
                }
                if (piece.AttackOverseerCheck(OverseerBlack))
                {
                    GlobalInvalidMoves.Add(new Tuple<TileBehaviour, GamePiece>(CheckTile, CheckPiece));
                    break;
                }
            }
        }

        // replace state
        ReplacePieceState(CheckTile, PieceLocation, CheckPiece);
    }
}
