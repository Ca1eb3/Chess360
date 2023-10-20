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
        if (MoveParameterCheck)
        {
            Piece.ValidMoveGraph.ValidMoves.Add(PieceLocation);
        }
    }

    public void MoveParameterChecker(TileBehaviour PrevLocation)
    {
        MoveParameterCheck = Piece.MoveParameterCheck(PieceLocation, PrevLocation, Depth);
    }

    public void ContinueChecker()
    {
        if (MoveParameterCheck == true)
        {
            if (Piece.OccupiedSpaceCheck(PieceLocation))
            {
                if (Piece.CanAttackCheck(PieceLocation))
                {
                    ContinueChecks = false;
                    return;
                }
            }
            if (Piece.PieceType == PieceString.S)
            {
                ContinueChecks = false;
                return;
            }
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
        ValidMoveGraphBasicNode node = new ValidMoveGraphBasicNode();

        node.MoveDirection = MoveDirection;
        node.PieceLocation = MoveDirectionOperations.MoveOperator(PieceLocation, node.MoveDirection);

        if (node.PieceLocation == null)
        {
            return;
        }
        node.Piece = Piece;
        node.Depth = Depth + 1;
        node.MoveParameterChecker(PieceLocation);
        node.ContinueChecker();
        node.UpdateListValidMoves();
        if (node.ContinueChecks == false)
        {
            return;
        }
        Node = node;
        Node.TryNextMove();
    }
}
