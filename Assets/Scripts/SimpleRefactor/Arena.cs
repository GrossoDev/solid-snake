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
        public int minAmountOfWalls = 50;
        public int maxAmountOfWalls = 6;
        public GameObject groundplaneGraphic;
        public WallEntitySpawner wallSpawner;
        public FruitEntitySpawner fruitSpawner;
        public Cell[,] cells;

        public void Initialize()
        {
            CreateCellGrid();
            SpawnEdgeWalls();
            SpawnInnerWalls();
            SpawnFruit();
            StretchGroundplaneToFitArena();
        }

        public Cell GetRandomFreeCell()
        {
            Cell cell;
            int iterations = 0;

            do
            {
                int row = Random.Range(0, width);
                int column = Random.Range(0, height);

                cell = cells[row, column];
                iterations++;

                if (iterations > MaxIterationsToFindFreeCell)
                {
                    throw new Exception($"Couldn't find a random free cell after {iterations} iterations.");
                }
            }
            while (AreThereAnyWallsAround(cell));

            return cell;
        }

        public int GetAmountOfWallsToCreate()
        {
            return Random.Range(minAmountOfWalls, maxAmountOfWalls + 1);
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

        public bool AreThereAnyWallsAround(Cell centerCell)
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
                    bool cellHasWall = DoesCellHaveAWall(cell);
                    
                    if (cellHasWall)
                        return true;
                }
            }

            return false;
        }

        private bool DoesCellHaveAWall(Cell cell)
        {
            if (cell.IsOccupied())
            {
                Entity occupee = cell.GetOccupyingEntity();
                bool isWall = occupee is WallEntity;

                return isWall;
            }
            else
            {
                return false;
            }
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

            for (int spawned = 0; spawned <= amountToSpawn; spawned++)
            {
                Cell randomCell = GetRandomFreeCell();
                wallSpawner.Spawn(randomCell);
            }
        }

        public void SpawnFruit()
        {
            Cell randomCell = GetRandomFreeCell();
            fruitSpawner.Spawn(randomCell);
        }

        private void StretchGroundplaneToFitArena()
        {

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