using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimpleRefactor
{
    public class Snake : MonoBehaviour
    {
        public GameManager gameManager;
        public float baseSpeed = 1;
        public float currentSpeed;
        public float speedupMultiplier;
        public Direction direction;
        public GameObject cameraGameObject;
        public List<SnakePartEntity> snakePartEntities;
        public SnakePartEntitySpawner snakePartSpawner;

        private void LateUpdate()
        {
            MoveCameraToHead();
        }

        public void Initialize(Cell startingCell)
        {
            snakePartEntities = new List<SnakePartEntity>();

            currentSpeed = baseSpeed;
            direction = Direction.Right;

            CreateSnakePart(startingCell, direction);
            Grow();
        }
        
        public void TryToAdvance()
        {
            SnakePartEntity head = GetHead();
            Cell headCell = head.occupiedCell;
            Cell nextCell = headCell.GetNeighbour(direction);

            nextCell.TouchedBySnake(this);
            if (nextCell.CanSnakeEnter())
            {
                MoveTo(nextCell);
            }
        }
        
        public void SetDirection(Direction newDirection)
        {
            SnakePartEntity head = GetHead();
            Direction headDirection = head.GetDirection();
            bool willTheSnakeTurnIntoItself = newDirection.IsOppositeTo(headDirection);
            
            if (!willTheSnakeTurnIntoItself)
                direction = newDirection;
        }
        
        public void ActivateSpeedup()
        {
            currentSpeed = baseSpeed * speedupMultiplier;
        }
        
        public void DeactivateSpeedup()
        {
            currentSpeed = baseSpeed;
        }
        
        public void Grow()
        {
            SnakePartEntity tail = GetTail();
            Direction tailDirection = tail.GetDirection();
            Cell newTailCell = tail.occupiedCell.GetNeighbour(tailDirection.Opposite());

            CreateSnakePart(newTailCell, tailDirection);
        }

        public void Die()
        {
            gameManager.SnakeDied(this);
        }
        
        public int GetLength()
        {
            return snakePartEntities.Count;
        }
        
        public float GetSpeed()
        {
            return currentSpeed;
        }
        
        public Direction GetDirection()
        {
            return direction;
        }
        
        public SnakePartEntity GetHead()
        {
            return snakePartEntities.First();
        }
        
        public SnakePartEntity GetTail()
        {
            return snakePartEntities.Last();
        }
        
        public SnakePartEntity GetSnakePart(int indexSinceHead)
        {
            return snakePartEntities[indexSinceHead];
        }
        
        private void MoveTo(Cell cell)
        {
            Cell nextCell = cell;
            Direction nextDirection = direction;

            foreach (var snakePart in snakePartEntities)
            {
                Cell currentCell = snakePart.occupiedCell;
                Direction currentDirection = direction;

                snakePart.MoveToCell(nextCell);
                snakePart.SetDirection(nextDirection);

                nextCell = currentCell;
                nextDirection = currentDirection;
            }
        }
        
        private void MoveCameraToHead()
        {
            SnakePartEntity head = GetHead();
            GameObject headGraphic = head.GetGraphic();
            Vector3 headPosition = headGraphic.transform.position;

            Vector3 oldCameraPosition = cameraGameObject.transform.position;
            Vector3 newCameraPosition = Vector3.Lerp(oldCameraPosition, headPosition, Time.deltaTime);

            cameraGameObject.transform.position = newCameraPosition;
        }

        private void CreateSnakePart(Cell cell, Direction direction)
        {
            SnakePartEntity newPart = (SnakePartEntity)snakePartSpawner.Spawn(cell);
            newPart.SetDirection(direction);
            snakePartEntities.Add(newPart);
        }
    }
}
