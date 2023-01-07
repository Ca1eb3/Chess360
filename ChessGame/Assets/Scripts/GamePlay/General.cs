// Caleb Smith
// 12/31/2022
using ChessGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class General : GamePiece
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
        if (MovePatterns.SingleForward(NextLocation, CurrentLocation))
        {
            return true;
        }
        else if (MovePatterns.Radial(NextLocation, CurrentLocation))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
