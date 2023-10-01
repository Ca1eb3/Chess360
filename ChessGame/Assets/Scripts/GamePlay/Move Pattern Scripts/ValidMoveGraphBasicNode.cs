// Caleb Smith
// 09/24/2023
using ChessGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ValidMoveGraphBasicNode
{
    // properties
    public TileBehaviour PieceLocation;
    public ValidMoveGraphBasicNode Node;
    public bool MoveParameterCheck = false;
    public bool ContinueChecks = false;
}
