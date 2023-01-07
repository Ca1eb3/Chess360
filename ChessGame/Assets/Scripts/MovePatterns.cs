// Caleb Smith
// 01/04/2023
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessGame
{
    public static class MovePatterns
    {
        public static bool SingleForward(TileBehaviour targetLocation, TileBehaviour currentLocation)
        {
            if (targetLocation.TileSector == currentLocation.TileSector && targetLocation.TileIndex == currentLocation.TileIndex - 1 || targetLocation.TileSector == currentLocation.TileSector && targetLocation.TileIndex == currentLocation.TileIndex + 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool SingleRadial(TileBehaviour targetLocation, TileBehaviour currentLocation)
        {
            if (Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Subtract(currentLocation.TileSector) && targetLocation.TileIndex == currentLocation.TileIndex || Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Add(currentLocation.TileSector) && targetLocation.TileIndex == currentLocation.TileIndex)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool SingleDiagonal(TileBehaviour targetLocation, TileBehaviour currentLocation)
        {
            if (Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Subtract(currentLocation.TileSector) && targetLocation.TileIndex == currentLocation.TileIndex - 1 || Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Add(currentLocation.TileSector) && targetLocation.TileIndex == currentLocation.TileIndex - 1 || Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Subtract(currentLocation.TileSector) && targetLocation.TileIndex == currentLocation.TileIndex + 1 || Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Add(currentLocation.TileSector) && targetLocation.TileIndex == currentLocation.TileIndex + 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Radial(TileBehaviour targetLocation, TileBehaviour currentLocation)
        {
            if (targetLocation.TileIndex == currentLocation.TileIndex)
            {
                int n = SectorOperations.Subtract(currentLocation.TileSector);
                Sector i = (Sector)n;
                while (i != targetLocation.TileSector)
                {
                    string objectName = i.ToString();
                    objectName += Convert.ToString(currentLocation.TileIndex);
                    GameObject o = GameObject.Find($"{objectName}");
                    TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                    if (tile.IsOccupied == true)
                    {
                        break;
                    }
                    i = (Sector)SectorOperations.Subtract(i);
                }
                if (i.Equals(targetLocation.TileSector))
                {
                    return true;
                }
                else
                {
                    int l = SectorOperations.Add(currentLocation.TileSector);
                    Sector j = (Sector)l;
                    while (j != targetLocation.TileSector)
                    {
                        string objectName = j.ToString();
                        objectName += Convert.ToString(currentLocation.TileIndex);
                        GameObject o = GameObject.Find($"{objectName}");
                        TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                        if (tile.IsOccupied == true)
                        {
                            break;
                        }
                        j = (Sector)SectorOperations.Add(j);
                    }
                    if (j.Equals(targetLocation.TileSector))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        public static bool Forward(TileBehaviour targetLocation, TileBehaviour currentLocation)
        {
            if (targetLocation.TileSector == currentLocation.TileSector)
            {
                if (targetLocation.TileIndex < currentLocation.TileIndex)
                {
                    int i = currentLocation.TileIndex - 1;
                    while (i > targetLocation.TileIndex)
                    {
                        string objectName = currentLocation.TileSector.ToString();
                        objectName += Convert.ToString(i);
                        GameObject o = GameObject.Find($"{objectName}");
                        TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                        if (tile.IsOccupied == true)
                        {
                            return false;
                        }
                        i--;
                    }
                    return true;
                }
                if (targetLocation.TileIndex > currentLocation.TileIndex)
                {
                    int i = currentLocation.TileIndex + 1;
                    while (i < targetLocation.TileIndex)
                    {
                        string objectName = currentLocation.TileSector.ToString();
                        objectName += Convert.ToString(i);
                        GameObject o = GameObject.Find($"{objectName}");
                        TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                        if (tile.IsOccupied == true)
                        {
                            return false;
                        }
                        i++;
                    }
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
        public static bool Diagonal(TileBehaviour targetLocation, TileBehaviour currentLocation)
        {
            bool[] IsValidMove = new bool[4] { true, true, true, true };
            GameObject tempObject = new GameObject();
            tempObject.AddComponent<TileBehaviour>();
            TileBehaviour temp = tempObject.GetComponent<TileBehaviour>();
            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;
            temp.TileIndex = temp.TileIndex + 1;
            temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
            while (!(temp.TileSector.Equals(targetLocation.TileSector) && temp.TileIndex.Equals(targetLocation.TileIndex)))
            {
                if (temp.TileIndex > 8 || temp.TileIndex < 1)
                {
                    IsValidMove[0] = false;
                    break;
                }
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true)
                {
                    IsValidMove[0] = false;
                    break;
                }
                temp.TileIndex = temp.TileIndex + 1;
                temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
            }

            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;
            temp.TileIndex = temp.TileIndex - 1;
            temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
            while (!(temp.TileSector.Equals(targetLocation.TileSector) && temp.TileIndex.Equals(targetLocation.TileIndex)))
            {
                if (temp.TileIndex > 8 || temp.TileIndex < 1)
                {
                    IsValidMove[1] = false;
                    break;
                }
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true)
                {
                    IsValidMove[1] = false;
                    break;
                }
                temp.TileIndex = temp.TileIndex - 1;
                temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
            }

            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;
            temp.TileIndex = temp.TileIndex - 1;
            temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
            while (!(temp.TileSector.Equals(targetLocation.TileSector) && temp.TileIndex.Equals(targetLocation.TileIndex)))
            {
                if (temp.TileIndex > 8 || temp.TileIndex < 1)
                {
                    IsValidMove[2] = false;
                    break;
                }
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true)
                {
                    IsValidMove[2] = false;
                    break;
                }
                temp.TileIndex = temp.TileIndex - 1;
                temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
            }

            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;
            temp.TileIndex = temp.TileIndex + 1;
            temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
            while (!(temp.TileSector.Equals(targetLocation.TileSector) && temp.TileIndex.Equals(targetLocation.TileIndex)))
            {
                if (temp.TileIndex > 8 || temp.TileIndex < 1)
                {
                    IsValidMove[3] = false;
                    break;
                }
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true)
                {
                    IsValidMove[3] = false;
                    break;
                }
                temp.TileIndex = temp.TileIndex + 1;
                temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
            }

            if (IsValidMove[0] || IsValidMove[1] || IsValidMove[2] || IsValidMove[3])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DiagonalMovePilot(TileBehaviour targetLocation, TileBehaviour currentLocation)
        {
            bool[] IsValidMove = new bool[4] { true, true, true, true };
            GameObject tempObject = new GameObject();
            tempObject.AddComponent<TileBehaviour>();
            TileBehaviour temp = tempObject.GetComponent<TileBehaviour>();
            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;
            temp.TileIndex = temp.TileIndex + 1;
            temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
            while (!(temp.TileSector.Equals(targetLocation.TileSector) && temp.TileIndex.Equals(targetLocation.TileIndex)))
            {
                if (temp.TileIndex > 8 || temp.TileIndex < 1)
                {
                    IsValidMove[0] = false;
                    break;
                }
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true && !(tile.OccupyingObject.TryGetComponent(out Barricade barricade)))
                {
                    IsValidMove[0] = false;
                    break;
                }
                temp.TileIndex = temp.TileIndex + 1;
                temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
            }

            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;
            temp.TileIndex = temp.TileIndex - 1;
            temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
            while (!(temp.TileSector.Equals(targetLocation.TileSector) && temp.TileIndex.Equals(targetLocation.TileIndex)))
            {
                if (temp.TileIndex > 8 || temp.TileIndex < 1)
                {
                    IsValidMove[1] = false;
                    break;
                }
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true && !(tile.OccupyingObject.TryGetComponent(out Barricade barricade)))
                {
                    IsValidMove[1] = false;
                    break;
                }
                temp.TileIndex = temp.TileIndex - 1;
                temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
            }

            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;
            temp.TileIndex = temp.TileIndex - 1;
            temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
            while (!(temp.TileSector.Equals(targetLocation.TileSector) && temp.TileIndex.Equals(targetLocation.TileIndex)))
            {
                if (temp.TileIndex > 8 || temp.TileIndex < 1)
                {
                    IsValidMove[2] = false;
                    break;
                }
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true && !(tile.OccupyingObject.TryGetComponent(out Barricade barricade)))
                {
                    IsValidMove[2] = false;
                    break;
                }
                temp.TileIndex = temp.TileIndex - 1;
                temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
            }

            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;
            temp.TileIndex = temp.TileIndex + 1;
            temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
            while (!(temp.TileSector.Equals(targetLocation.TileSector) && temp.TileIndex.Equals(targetLocation.TileIndex)))
            {
                if (temp.TileIndex > 8 || temp.TileIndex < 1)
                {
                    IsValidMove[3] = false;
                    break;
                }
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true && !(tile.OccupyingObject.TryGetComponent(out Barricade barricade)))
                {
                    IsValidMove[3] = false;
                    break;
                }
                temp.TileIndex = temp.TileIndex + 1;
                temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
            }

            if (IsValidMove[0] || IsValidMove[1] || IsValidMove[2] || IsValidMove[3])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool SoldierBarricadeJump(TileBehaviour targetLocation, TileBehaviour currentLocation)
        {
            GameObject tempObject = new GameObject();
            tempObject.AddComponent<TileBehaviour>();
            TileBehaviour temp = tempObject.GetComponent<TileBehaviour>();
            temp.TileIndex = currentLocation.TileIndex;
            temp.TileSector = currentLocation.TileSector;

            // forward
            if (targetLocation.TileSector == currentLocation.TileSector && targetLocation.TileIndex == currentLocation.TileIndex - 2)
            {
                temp.TileIndex = temp.TileIndex - 1;
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied)
                {
                    if (tile.OccupyingObject.TryGetComponent(out Barricade barricade))
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
            else if (targetLocation.TileSector == currentLocation.TileSector && targetLocation.TileIndex == currentLocation.TileIndex + 2)
            {
                temp.TileIndex = temp.TileIndex + 1;
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied)
                {
                    if (tile.OccupyingObject.TryGetComponent(out Barricade barricade))
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
            // radial
            else if (Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Subtract((Sector)SectorOperations.Subtract(currentLocation.TileSector)) && targetLocation.TileIndex == currentLocation.TileIndex)
            {
                temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied)
                {
                    if (tile.OccupyingObject.TryGetComponent(out Barricade barricade))
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
            else if (Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Add((Sector)SectorOperations.Add(currentLocation.TileSector)) && targetLocation.TileIndex == currentLocation.TileIndex)
            {
                temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied)
                {
                    if (tile.OccupyingObject.TryGetComponent(out Barricade barricade))
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
            // diagonal
            else if (Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Subtract((Sector)SectorOperations.Subtract(currentLocation.TileSector)) && targetLocation.TileIndex == currentLocation.TileIndex - 2)
            {
                temp.TileIndex = temp.TileIndex - 1;
                temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied)
                {
                    if (tile.OccupyingObject.TryGetComponent(out Barricade barricade))
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
            else if (Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Add((Sector)SectorOperations.Add(currentLocation.TileSector)) && targetLocation.TileIndex == currentLocation.TileIndex - 2)
            {
                temp.TileIndex = temp.TileIndex - 1;
                temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied)
                {
                    if (tile.OccupyingObject.TryGetComponent(out Barricade barricade))
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
            else if (Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Subtract((Sector)SectorOperations.Subtract(currentLocation.TileSector)) && targetLocation.TileIndex == currentLocation.TileIndex + 2)
            {
                temp.TileIndex = temp.TileIndex + 1;
                temp.TileSector = (Sector)SectorOperations.Subtract(temp.TileSector);
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied)
                {
                    if (tile.OccupyingObject.TryGetComponent(out Barricade barricade))
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
            else if (Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Add((Sector)SectorOperations.Add(currentLocation.TileSector)) && targetLocation.TileIndex == currentLocation.TileIndex + 2)
            {
                temp.TileIndex = temp.TileIndex + 1;
                temp.TileSector = (Sector)SectorOperations.Add(temp.TileSector);
                string objectName = $"{temp.TileSector}{temp.TileIndex}";
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied)
                {
                    if (tile.OccupyingObject.TryGetComponent(out Barricade barricade))
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
            else
            {
                return false;
            }
        }
    }
}