// Caleb Smith
// 01/04/2022
using ChessGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilot : GamePiece
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
        if (MovePatterns.DiagonalMovePilot(NextLocation, CurrentLocation))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
