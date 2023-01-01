// Caleb Smith
// 12/26/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChessGame;

namespace ChessGame
{
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

        public void UpdateStatus(GameData data)
        {
            UpdateOccupation();
            ButtonStatus(data);
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

        public void ButtonStatus(GameData data)
        {
            if (IsOccupied)
            {
                //if (OccupyingObject.Color == PieceColor.White && data.MoveCounter % 2 == 0 || OccupyingObject.Color == PieceColor.Black && data.MoveCounter % 2 == 1)
                //{
                Button button = this.gameObject.GetComponent("Button") as Button;
                button.interactable = false;
                //}
                //else
                //{
                //    Button button = this.gameObject.GetComponent("Button") as Button;
                //    button.interactable = true;
                //}
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
}
