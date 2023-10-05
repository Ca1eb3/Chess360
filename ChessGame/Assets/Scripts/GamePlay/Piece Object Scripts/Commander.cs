// Caleb Smith
// 01/04/2023
using ChessGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : GamePiece
{
    public override bool MoveParameterCheck(TileBehaviour nextLocation, TileBehaviour currentLocation, int depth)
    {
        if (!OccupiedSpaceCheck(nextLocation))
        {
            return false;
        }
        if (MovePatterns.Forward(nextLocation, currentLocation) || MovePatterns.Radial(nextLocation, currentLocation) || MovePatterns.Diagonal(nextLocation, currentLocation))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
