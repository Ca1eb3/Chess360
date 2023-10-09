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
    public GamePiece RemovedPiece;
    public bool CheckBlack = false;
    public bool CheckWhite = false;
    public bool ValidMove = false;
    public List<GamePiece> AttackingWhite;
    public List<GamePiece> AttackingBlack;
    public PieceColor Turn = PieceColor.White;
    public bool Checkmate = false;

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
        ValidMove = false;
        CheckWhite = false;
        CheckBlack = false;
        // string to append move to move record
        string moveRecordStringAppend = "";
        if (MoveCounter % 2 == 0)
        {
            Turn = PieceColor.White;
        }
        else
        {
            Turn = PieceColor.Black;
        }
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
        // Null Reference Check
        if (SelectedPiece == null)
        {
            return;
        }
        // Set pieces next location
        SelectedPiece.NextLocation = NewTile;
        // Do move checks
        if (Turn == PieceColor.White && SelectedPiece.Color == PieceColor.Black || Turn == PieceColor.Black && SelectedPiece.Color == PieceColor.White)
        {
            return;
        }
        if (!SelectedPiece.ValidMoveGraph.ValidMoves.Contains(SelectedPiece.NextLocation))
        {
            return;
        }
        if (true)
        {
            // reset attacking piece
            AttackingBlack.Clear();
            AttackingWhite.Clear();

            // Destroy piece in next location if applicable
            if (NewTile.IsOccupied == true)
            {
                RemovePiece();
            }
            // Change piece position
            SelectedPiece.TileSector = SelectedPiece.NextLocation.TileSector;
            SelectedPiece.TileIndex = SelectedPiece.NextLocation.TileIndex;
            // Update piece location
            SelectedPiece.PreviousLocation = SelectedPiece.CurrentLocation;
            SelectedPiece.CurrentLocation = SelectedPiece.NextLocation;
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

            if ((CheckBlack == true || CheckWhite == true) && ValidMove == true)
            {
                CheckmateAnalyzer();
            }




            // replace
            // Destroy piece in next location if applicable
            // Change piece position
            SelectedPiece.TileSector = SelectedPiece.PreviousLocation.TileSector;
            SelectedPiece.TileIndex = SelectedPiece.PreviousLocation.TileIndex;
            // Update piece location
            SelectedPiece.CurrentLocation = SelectedPiece.PreviousLocation;
            SelectedPiece.PreviousLocation = null;
            // update original tile and new tile
            CurrentTile.OccupyingObject = NewTile.OccupyingObject;
            NewTile.OccupyingObject = null;
            if (RemovedPiece != null)
            {
                ReplacePiece();
                RemovedPiece = null;
            }
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

            // return if putting self in check
            if (ValidMove == false)
            {
                return;
            }
        }
        // return if putting self in check
        // add code for check


        // append new tile to move record string
        moveRecordStringAppend = moveRecordStringAppend + SelectedPiece.PieceType.ToString() + SelectedPiece.NextLocation.TileSector.ToString() + SelectedPiece.NextLocation.TileIndex.ToString();
        // The below code should only execute if all checks have passed
        // update move counter
        MoveCounter++;
        // Destroy piece in next location if applicable
        if (NewTile.IsOccupied == true)
        {
            
            string pieceDestroyed = "x";
            if (NewTile.OccupyingObject.PieceType == PieceString.O)
            {
                UpdateOverseers();
                pieceDestroyed = "#";
            }
            DestroyPiece();
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
            piece.UpdateValidMoveGraph();
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
        if (!IsOver)
        {
            if (CheckBlack == true || CheckWhite == true)
            {
                moveRecordStringAppend += "+";
            }
            CheckBlack = false;
            CheckWhite = false;
        }
        // Update Move Record
        MoveRecordText.text = MoveRecordText.text + moveRecordStringAppend;
    }

    public void DestroyPiece()
    {
        GamePieces.Remove(NewTile.OccupyingObject);
        Destroy(NewTile.OccupyingObject.gameObject);
        NewTile.OccupyingObject = null;
        NewTile.UpdateStatus(this);
    }

    public void RemovePiece()
    {
        RemovedPiece = NewTile.OccupyingObject;
        NewTile.OccupyingObject = null;
        NewTile.UpdateStatus(this);
    }

    public void ReplacePiece()
    {
        NewTile.OccupyingObject = RemovedPiece;
        NewTile.UpdateStatus(this);
    }

    public void CheckmateAnalyzer()
    {
        // Check if the overseer can move to a safe location, if the attacking piece can be destroyed or blocked by another piece
        // if turn is white analyze checkmate for black



        //if (Turn == PieceColor.White)
        //{
        //    List<TileBehaviour> whiteAttackMoves = new List<TileBehaviour>();
        //    foreach (var piece in WhiteGamePieces) 
        //    { 
        //        if (piece.PieceType != PieceString.B)
        //        {
        //            whiteAttackMoves = whiteAttackMoves.Concat(piece.ValidMoveGraph.ValidMoves).ToList();
        //        }
        //    }

        //    foreach (var move in OverseerBlack.ValidMoveGraph.ValidMoves)
        //    {
        //        if (whiteAttackMoves.Contains(move))
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //            if (OverseerBlack.OccupiedSpaceCheck(move))
        //            {
        //                // do stuff to check edge case
        //                continue;
        //            }
        //            else
        //            {
        //                return;
        //            }
        //        }
        //    }

        //}



        // if turn is black analyze checkmate for white
        if (Turn == PieceColor.Black)
        {
            List<TileBehaviour> blackAttackMoves = new List<TileBehaviour>();
            foreach (var piece in BlackGamePieces)
            {
                if (piece.PieceType != PieceString.B)
                {
                    blackAttackMoves = blackAttackMoves.Concat(piece.ValidMoveGraph.ValidMoves).ToList();
                }
            }

            // Steps 1 and 2 check if overseer can move
            foreach (var move in OverseerWhite.ValidMoveGraph.ValidMoves)
            {
                if (blackAttackMoves.Contains(move))
                {
                    continue;
                }
                else
                {
                    if (OverseerWhite.OccupiedSpaceCheck(move))
                    {
                        // do stuff to check edge case when overseer can capture a piece and move to an occupied space that would then become under fire by another piece
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            // Step 3 and 4 check if attacking piece can be destroyed and check if attacking piece can be blocked
            if (AttackingWhite.Count == 1)
            {
                List<TileBehaviour> whiteAttackMoves = new List<TileBehaviour>();
                foreach (var piece in WhiteGamePieces)
                {
                    if (piece.PieceType != PieceString.B)
                    {
                        whiteAttackMoves = whiteAttackMoves.Concat(piece.ValidMoveGraph.ValidMoves).ToList();
                    }
                }

                if (whiteAttackMoves.Contains(AttackingWhite[0].CurrentLocation))
                {
                    // do stuff to check edge case when moving a piece to attack the check piece results in another check
                    return;
                }


                // Step 4 check if attacking piece can be blocked
                // get the number of ways the attacking piece can attack the overseer
                int attacks = AttackingWhite[0].ValidMoveGraph.ValidMoves.Count(attack => attack == OverseerWhite.CurrentLocation);
                if (attacks > 1)
                {
                    Checkmate = true;
                    return;
                }
                else
                {
                    // do stuff to check block attack
                    List<TileBehaviour> whiteBarricadeMoves = new List<TileBehaviour>();
                    if (AttackingWhite[0].PieceType != PieceString.P)
                    {
                        foreach (var piece in WhiteGamePieces)
                        {
                            if (piece.PieceType == PieceString.B)
                            {
                                whiteBarricadeMoves = whiteBarricadeMoves.Concat(piece.ValidMoveGraph.ValidMoves).ToList();
                            }
                        }
                        whiteAttackMoves = whiteBarricadeMoves.Concat(whiteAttackMoves).ToList();
                    }
                    // add edge case check for moving a barricade away from soldier to move out of check
                    // find valid move node to determine direction and depth checks
                    ValidMoveGraphBasicNode attackNode = AttackingWhite[0].ValidMoveGraph.FindNode(OverseerWhite.CurrentLocation);
                    MoveDirection attackDirection = attackNode.MoveDirection;
                    int depth = attackNode.Depth;
                    int i = 1;
                    ValidMoveGraphBasicNode checkNode = AttackingWhite[0].ValidMoveGraph.FindNode(attackDirection);
                    while (i < depth)
                    {
                        if (whiteAttackMoves.Contains(checkNode.PieceLocation))
                        {
                            return;
                        }
                        checkNode = checkNode.Node;
                        i++;
                    }
                    
                }
            }
        }
        Checkmate = true;
        return;
    }
}
