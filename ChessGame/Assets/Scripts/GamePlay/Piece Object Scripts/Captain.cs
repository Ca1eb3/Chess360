// Caleb Smith
// 12/31/2022
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChessGame;

public class Captain : GamePiece
{
    public override void SetBitValue()
    {
        BitValue = new BitArray(5);
        BitValue.Set(0, true);
        if (Color == PieceColor.Black)
        {
            BitValue.Set(1, true);
        }
        BitValue.Set(3, true);
        BitValue.Set(4, true);
    }

    public override bool MoveParameterCheck(TileBehaviour nextLocation, TileBehaviour currentLocation, int depth)
    {
        if (depth <= 1 && MovePatterns.Radial(nextLocation, currentLocation))
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
        else if (MovePatterns.Forward(nextLocation, currentLocation))
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
