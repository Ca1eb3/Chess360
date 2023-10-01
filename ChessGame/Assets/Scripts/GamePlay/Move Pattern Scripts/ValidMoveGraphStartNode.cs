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
}
