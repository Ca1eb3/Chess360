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
        ValidMoveGraphBasicNode node = new ValidMoveGraphBasicNode();

        switch (MoveDirection)
        {
            case MoveDirection.Forward:
                node.PieceLocation = MoveDirectionOperations.Forward(PieceLocation);
                node.MoveDirection = MoveDirection.Forward;
                break;
            case MoveDirection.Backward:
                node.PieceLocation = MoveDirectionOperations.Backward(PieceLocation);
                node.MoveDirection = MoveDirection.Backward;
                break;
            case MoveDirection.Clockwise:
                node.PieceLocation = MoveDirectionOperations.Clockwise(PieceLocation);
                node.MoveDirection = MoveDirection.Clockwise;
                break;
            case MoveDirection.CounterClockwise:
                node.PieceLocation = MoveDirectionOperations.CounterClockwise(PieceLocation);
                node.MoveDirection = MoveDirection.CounterClockwise;
                break;
            case MoveDirection.DCounterClockwiseForward:
                node.PieceLocation = MoveDirectionOperations.DCounterClockwiseForward(PieceLocation);
                node.MoveDirection = MoveDirection.DCounterClockwiseForward;
                break;
            case MoveDirection.DClockwiseForward:
                node.PieceLocation = MoveDirectionOperations.DClockwiseForward(PieceLocation);
                node.MoveDirection = MoveDirection.DClockwiseForward;
                break;
            case MoveDirection.DClockwiseBackward:
                node.PieceLocation = MoveDirectionOperations.DClockwiseBackward(PieceLocation);
                node.MoveDirection = MoveDirection.DClockwiseBackward;
                break;
            case MoveDirection.DCounterClockwiseBackward:
                node.PieceLocation = MoveDirectionOperations.DCounterClockwiseBackward(PieceLocation);
                node.MoveDirection = MoveDirection.DCounterClockwiseBackward;
                break;
        }

        if (node.PieceLocation == null)
        {
            return;
        }
        node.Piece = Piece;
        node.Depth = Depth + 1;
        node.MoveParameterChecker();
        node.ContinueChecker();
        node.UpdateListValidMoves();
        if (node.MoveParameterCheck == false && node.ContinueChecks == false)
        {
            return;
        }
        Node = node;
        Node.TryNextMove();
    }
}
