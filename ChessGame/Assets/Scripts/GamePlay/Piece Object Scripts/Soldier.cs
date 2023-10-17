// Caleb Smith
// 01/07/2023
using ChessGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : GamePiece
{
    public override bool MoveParameterCheck(TileBehaviour nextLocation, TileBehaviour currentLocation, int depth)
    {
        if (depth <= 2 && (MovePatterns.Radial(nextLocation, currentLocation) || MovePatterns.Forward(nextLocation, currentLocation) || MovePatterns.Diagonal(nextLocation, currentLocation)))
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
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
}
