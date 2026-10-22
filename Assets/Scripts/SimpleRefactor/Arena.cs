using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace SimpleRefactor
{
    public class Arena : MonoBehaviour
    {
        public const int MaxIterationsToFindFreeCell = 100;
        public int width = 40;
        public int height = 40;
        public int minAmountOfWalls = 6;
        public int maxAmountOfWalls = 50;
        public GameObject groundplaneGraphic;
        public WallEntitySpawner wallSpawner;
        public FruitEntitySpawner fruitSpawner;
        public Cell[,] cells;

        public void Initialize()
        {
            CreateCellGrid();
            SpawnEdgeWalls();
            SpawnInnerWalls();
            SpawnRandomFruit();
            StretchGroundplaneToFitArena();
        }

        public Cell GetRandomFreeCell()
        {
            Cell cell;
            int iterations = 0;

            do
            {
                int row = Random.Range(0, height);
                int column = Random.Range(0, width);

                cell = cells[row, column];
                iterations++;

                if (iterations > MaxIterationsToFindFreeCell)
                {
                    throw new Exception($"Couldn't find a random free cell after {iterations} iterations.");
                }
            }
            while (AreTheSurroundingsOfCellFree(cell) == false);

            return cell;
        }

        public Cell GetAdjacentCell(Cell cell, Direction direction)
        {
            Vector2Int cellPosition = cell.GetPosition();
            int cellRow = cellPosition.y;
            int cellColumn = cellPosition.x;

            int neighbourRow = cellRow + direction.deltaY;
            int neighbourColumn = cellColumn + direction.deltaX;

            Cell neighbour = cells[neighbourRow, neighbourColumn];

            return neighbour;
        }
        
        public void SpawnRandomWall()
        {
            Cell randomCell = GetRandomFreeCell();
            wallSpawner.Spawn(randomCell);
        }

        public void SpawnRandomFruit()
        {
            Cell randomCell = GetRandomFreeCell();
            fruitSpawner.Spawn(randomCell);
        }

        private int GetAmountOfWallsToCreate()
        {
            return Random.Range(minAmountOfWalls, maxAmountOfWalls + 1);
        }

        private bool AreTheSurroundingsOfCellFree(Cell centerCell)
        {
            Vector2Int centerCellPosition = centerCell.GetPosition();
            int centerCellRow = centerCellPosition.y;
            int centerCellColumn = centerCellPosition.x;

            int rowLowerBound = Math.Max(centerCellRow - 1, 0);
            int rowUpperBound = Math.Min(centerCellRow + 1, height - 1);
            int columnLowerBound = Math.Max(centerCellColumn - 1, 0);
            int columnUpperBound = Math.Min(centerCellColumn + 1, width - 1);

            for (int row = rowLowerBound; row <= rowUpperBound; row++)
            {
                for (int column = columnLowerBound; column <= columnUpperBound; column++)
                {
                    Cell cell = cells[row, column];
                    
                    if (cell.IsOccupied())
                        return false;
                }
            }

            return true;
        }

        private void CreateCellGrid()
        {
            cells = new Cell[height, width];

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    Vector2Int position = new Vector2Int(column, row);
                    Cell newCell = new Cell(this);
                    newCell.SetPosition(position);

                    cells[row, column] = newCell;
                }
            }
        }

        private void SpawnEdgeWalls()
        {
            for (int row = 0; row < height; row++)
            {
                Cell leftEdgeCell = cells[row, 0];
                Cell rightEdgeCell = cells[row, width - 1];

                wallSpawner.Spawn(leftEdgeCell);
                wallSpawner.Spawn(rightEdgeCell);
            }

            for (int column = 0; column < width; column++)
            {
                Cell upperEdgeCell = cells[0, column];
                Cell lowerEdgeCell = cells[height - 1, column];
                
                wallSpawner.Spawn(upperEdgeCell);
                wallSpawner.Spawn(lowerEdgeCell);
            }
        }

        private void SpawnInnerWalls()
        {
            int amountToSpawn = GetAmountOfWallsToCreate();

            for (int spawned = 0; spawned < amountToSpawn; spawned++)
            {
                SpawnRandomWall();
            }
        }

        private void StretchGroundplaneToFitArena()
        {
            Vector3 position = new Vector3(width / 2, 0, -height / 2);
            Vector3 scale = new Vector3(width, 1, height);

            groundplaneGraphic.transform.position = position;
            groundplaneGraphic.transform.localScale = scale;
        }

        public void Dispose()
        {
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    cells[row, column].Dispose();
                }
            }
        }
    }
}