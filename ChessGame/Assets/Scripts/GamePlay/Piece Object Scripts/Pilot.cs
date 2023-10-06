// Caleb Smith
// 01/04/2023
using ChessGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilot : GamePiece
{
    public override bool MoveParameterCheck(TileBehaviour nextLocation, TileBehaviour currentLocation, int depth)
    {
        if (OccupiedSpaceCheck(nextLocation))
        {
            if (CanAttackCheck(nextLocation))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (MovePatterns.Diagonal(nextLocation, currentLocation))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
