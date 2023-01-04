// Caleb Smith
// 12/26/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChessGame;

public abstract class GamePiece : MonoBehaviour
{
    // class variables
    public TileBehaviour CurrentLocation;
    public TileBehaviour NextLocation;


    // properties
    [Header("Status")]
    public PieceColor Color;
    public Sector TileSector;
    public int TileIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGameData(GameData data)
    {
        data.CurrentTile = CurrentLocation;
        data.SelectedPiece = this;
    }

    public virtual void UpdateSceneStatus(GameData data)
    {
        if (Color == PieceColor.White && data.MoveCounter % 2 == 0 || Color == PieceColor.Black && data.MoveCounter % 2 == 1)
        {
            this.gameObject.transform.SetAsLastSibling();
        }
        else
        {
            this.gameObject.transform.SetAsFirstSibling();
        }
    }

    public void UpdateButtonStatus(GameData data)
    {
        if (Color == PieceColor.White && data.MoveCounter % 2 == 0 || Color == PieceColor.Black && data.MoveCounter % 2 == 1)
        {
            Button button = this.gameObject.GetComponent("Button") as Button;
            button.interactable = true;
        }
        else
        {
            Button button = this.gameObject.GetComponent("Button") as Button;
            button.interactable = false;
        }
    }

    public abstract bool MoveParameterCheck();
}
