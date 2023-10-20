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

            node.MoveDirection = (MoveDirection)i;
            node.PieceLocation = MoveDirectionOperations.MoveOperator(PieceLocation, node.MoveDirection);

            if (node.PieceLocation == null)
            {
                continue;
            }
            node.Piece = Piece;
            node.Depth = Depth + 1;
            node.MoveParameterChecker(PieceLocation);
            node.ContinueChecker();
            node.UpdateListValidMoves();
            if (node.ContinueChecks == false)
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

    public ValidMoveGraphBasicNode FindNode(TileBehaviour NodeLocation)
    {
        for (int i = 0; i < Nodes.Count; i++) 
        { 
            if (Nodes[i].PieceLocation == NodeLocation)
            {
                return Nodes[i];
            }
            ValidMoveGraphBasicNode node = Nodes[i];
            while (node.Node != null)
            {
                node = node.Node;
                if (node.PieceLocation == NodeLocation)
                {
                    return node;
                }
            }
        }
        return null;
    }

    public ValidMoveGraphBasicNode FindNode(MoveDirection Direction)
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            if (Nodes[i].MoveDirection == Direction)
            {
                return Nodes[i];
            }
        }
        return null;
    }
}
