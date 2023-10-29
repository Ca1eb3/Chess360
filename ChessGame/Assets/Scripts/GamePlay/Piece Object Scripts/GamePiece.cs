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
    public TileBehaviour PreviousLocation;
    public ValidMoveGraphStartNode ValidMoveGraph = new ValidMoveGraphStartNode();
    public bool AttacksOverseer = false;


    // properties
    [Header("Status")]
    public PieceColor Color;
    public PieceString PieceType;
    public Sector TileSector;
    public int TileIndex;
    public BitArray BitValue;

    // Start is called before the first frame update
    void Start()
    {
        CurrentLocation = GameObject.Find(TileSector.ToString() + TileIndex.ToString()).GetComponent<TileBehaviour>();
        ValidMoveGraph.Piece = this;
        UpdateValidMoveGraph();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void SetBitValue();

    public void UpdateGameData(GameData data)
    {
        data.CurrentTile = CurrentLocation;
        data.SelectedPiece = this;
        UpdateValidMoveGraph();
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

    public bool AttackOverseerCheck(Overseer overseer)
    {
        return this.ValidMoveGraph.ValidMoves.Contains(overseer.CurrentLocation);
    }

    public abstract bool MoveParameterCheck(TileBehaviour nextLocation, TileBehaviour currentLocation, int depth);


    public bool OccupiedSpaceCheck(TileBehaviour nextLocation)
    {
        if (nextLocation.IsOccupied == true)
        {
            return true;
        }
        return false;
    }

    public bool CanAttackCheck(TileBehaviour nextLocation)
    {
        if (nextLocation.OccupyingObject.Color == Color || nextLocation.OccupyingObject.PieceType == PieceString.B)
        {
            return false;
        }
        return true;
    }

    public virtual void UpdateValidMoveGraph()
    {
        ValidMoveGraph.PieceLocation = CurrentLocation;
        ValidMoveGraph.ClearValidMoves();
        ValidMoveGraph.CreateNodes();
    }
}
