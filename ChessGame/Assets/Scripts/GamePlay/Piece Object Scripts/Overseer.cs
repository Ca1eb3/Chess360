// Caleb Smith
// 01/07/2023
using ChessGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overseer : GamePiece
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
        if (depth <= 1)
        {
            if (MovePatterns.Radial(nextLocation, currentLocation) || MovePatterns.Forward(nextLocation, currentLocation) || MovePatterns.Diagonal(nextLocation, currentLocation))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
