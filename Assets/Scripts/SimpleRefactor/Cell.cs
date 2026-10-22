using System.Collections.Generic;
using UnityEngine;

namespace SimpleRefactor
{
    public class Cell
    {
        private Arena arena;
        private bool isOccupied;
        private Entity occupyingEntity;
        private Vector2Int position;

        public Cell(Arena arena)
        {
            this.arena = arena;
        }

        public bool IsOccupied()
        {
            return isOccupied;
        }

        public Vector2Int GetPosition()
        {
            return position;
        }

        public void SetPosition(Vector2Int newPosition)
        {
            position = newPosition;
        }

        public Entity GetOccupyingEntity()
        {
            return occupyingEntity;
        }

        public void SetOccupyingEntity(Entity newEntity)
        {
            isOccupied = true;
            occupyingEntity = newEntity;
        }

        public void RemoveOccupyingEntity()
        {
            isOccupied = false;
            occupyingEntity = null;
        }

        public Cell GetNeighbour(Direction direction)
        {
            return arena.GetAdjacentCell(this, direction);
        }

        public bool CanSnakeEnter()
        {
            if (isOccupied)
                return occupyingEntity.CanBeDestroyedByASnake();
            else
                return true;
        }

        public void TouchedBySnake(Snake snake)
        {
            if (isOccupied)
                occupyingEntity.TouchedBySnake(snake);
        }

        public void Dispose()
        {
            if (isOccupied)
                occupyingEntity.DisposeGraphic();
        }
    }
}
