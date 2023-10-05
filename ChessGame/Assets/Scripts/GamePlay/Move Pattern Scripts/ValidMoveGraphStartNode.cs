// Caleb Smith
// 09/24/2023
using ChessGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ValidMoveGraphStartNode
{
    // properties
    public TileBehaviour PieceLocation;
    public List<ValidMoveGraphBasicNode> Nodes;
    public List<TileBehaviour> ValidMoves;
    public GamePiece Piece;
    public int Depth = 0;

    public void CreateNodes()
    {
        for (int i = 0 ; i < 8; i++) 
        {
            ValidMoveGraphBasicNode node = new ValidMoveGraphBasicNode();

            switch (i)
            {
                case 0 :
                    node.PieceLocation = MoveDirectionOperations.Forward(PieceLocation);
                    node.MoveDirection = MoveDirection.Forward;
                    break;
                case 1 :
                    node.PieceLocation = MoveDirectionOperations.Backward(PieceLocation);
                    node.MoveDirection = MoveDirection.Backward;
                    break;
                case 2:
                    node.PieceLocation = MoveDirectionOperations.Clockwise(PieceLocation);
                    node.MoveDirection = MoveDirection.Clockwise;
                    break;
                case 3:
                    node.PieceLocation = MoveDirectionOperations.CounterClockwise(PieceLocation);
                    node.MoveDirection = MoveDirection.CounterClockwise;
                    break;
                case 4:
                    node.PieceLocation = MoveDirectionOperations.DCounterClockwiseForward(PieceLocation);
                    node.MoveDirection = MoveDirection.DCounterClockwiseForward;
                    break;
                case 5:
                    node.PieceLocation = MoveDirectionOperations.DClockwiseForward(PieceLocation);
                    node.MoveDirection = MoveDirection.DClockwiseForward;
                    break;
                case 6:
                    node.PieceLocation = MoveDirectionOperations.DClockwiseBackward(PieceLocation);
                    node.MoveDirection = MoveDirection.DClockwiseBackward;
                    break;
                case 7:
                    node.PieceLocation = MoveDirectionOperations.DCounterClockwiseBackward(PieceLocation);
                    node.MoveDirection = MoveDirection.DCounterClockwiseBackward;
                    break;
            }

            if (node.PieceLocation == null)
            {
                continue;
            }
            node.Piece = Piece;
            node.Depth = Depth + 1;
            node.MoveParameterChecker();
            node.ContinueChecker();
            node.UpdateListValidMoves();
            if (node.MoveParameterCheck == false && node.ContinueChecks == false)
            {
                continue;
            }
            Nodes.Add(node);
            node.TryNextMove();
        }
    }

    public void ClearValidMoves()
    {
        Nodes.Clear();
        ValidMoves.Clear();
    }
}
