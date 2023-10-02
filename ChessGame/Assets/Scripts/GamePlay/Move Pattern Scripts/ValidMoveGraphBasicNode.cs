// Caleb Smith
// 09/24/2023
using ChessGame;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ValidMoveGraphBasicNode
{
    // properties
    [Serialize]
    public TileBehaviour PieceLocation;
    public ValidMoveGraphBasicNode Node;
    public bool MoveParameterCheck = false;
    public bool ContinueChecks = false;
    public MoveDirection MoveDirection;
    public GamePiece Piece;
    public int Depth;

    public void UpdateListValidMoves()
    {
        Piece.ValidMoveGraph.ValidMoves.Add(PieceLocation);
    }

    public void MoveParameterChecker()
    {
        MoveParameterCheck = Piece.MoveParameterCheck(PieceLocation, Piece.CurrentLocation);
    }

    public void ContinueChecker()
    {
        if (MoveParameterCheck == true)
        {
            ContinueChecks = true;
            return;
        }
        if (PieceLocation.IsOccupied == true && PieceLocation.OccupyingObject.PieceType == PieceString.B)
        {
            if ((Piece.PieceType == PieceString.S && Depth == 1) || Piece.PieceType == PieceString.P) 
            { 
                ContinueChecks = true;
                return;
            }
        }
        ContinueChecks = false;
    }

    public void TryNextMove()
    {
        // Add code to recursively check for the next valid move until parameter checks fails
    }
}
