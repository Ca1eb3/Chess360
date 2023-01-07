// Caleb Smith
// 01/07/2023
using ChessGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : GamePiece
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool MoveParameterCheck()
    {
        if (MovePatterns.SingleRadial(NextLocation, CurrentLocation) || MovePatterns.SingleForward(NextLocation, CurrentLocation) || MovePatterns.SingleDiagonal(NextLocation, CurrentLocation))
        {
            return true;
        }
        else if (MovePatterns.SoldierBarricadeJump(NextLocation, CurrentLocation))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
