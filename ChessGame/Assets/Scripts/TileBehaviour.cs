// Caleb Smith
// 12/26/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChessGame;

public class TileBehaviour : MonoBehaviour
{
    // class variables
    public GamePiece OccupyingObject = null;

    // properties
    [Header("Status")]
    public bool IsOccupied;
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

    public void UpdateStatus()
    {
        UpdateOccupation();
        ButtonStatus();
    }

    public void UpdateOccupation()
    {
        if (OccupyingObject == null)
        {
            IsOccupied = false;
        }
        else
        {
            IsOccupied = true;
        }
    }
    
    public void ButtonStatus()
    {
        if (IsOccupied)
        {
            Button button = this.gameObject.GetComponent("Button") as Button;
            button.interactable = false;
        }
        else
        {
            Button button = this.gameObject.GetComponent("Button") as Button;
            button.interactable = true;
        }
    }

    public bool UpdateOccupyingObject(GamePiece piece)
    {
        if (!IsOccupied)
        {

            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateGameData(GameData gameData)
    {
        gameData.NewTile = this;
    }
}
