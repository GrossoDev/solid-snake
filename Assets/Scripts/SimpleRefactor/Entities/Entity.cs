using System.Collections.Generic;
using UnityEngine;

namespace SimpleRefactor
{
    public abstract class Entity
    {
        public Cell occupiedCell;
        public GameObject graphic;

        public GameObject GetGraphic()
        {
            return graphic;
        }

        public void SetGraphic(GameObject newGraphic)
        {
            graphic = newGraphic;
        }

        public Cell GetOccupiedCell()
        {
            return occupiedCell;
        }

        public void SetOccupiedCell(Cell cell)
        {
            occupiedCell = cell;
        }

        public void MoveToCell(Cell newCell)
        {
            occupiedCell.RemoveOccupyingEntity();
            newCell.SetOccupyingEntity(this);

            occupiedCell = newCell;
            UpdateGraphicPosition();
        }

        public void DestroyEntity()
        {
            DisposeGraphic();
        }

        public abstract bool CanBeDestroyedByASnake();

        public abstract void TouchedBySnake(Snake snake);

        private void UpdateGraphicPosition()
        {
            Vector2Int cellPositionInArena = occupiedCell.GetPosition();

            int graphicPositionX = cellPositionInArena.x;
            int graphicPositionY = 0;
            int graphicPositionZ = -cellPositionInArena.y;

            graphic.transform.position = new Vector3Int(graphicPositionX, graphicPositionY, graphicPositionZ);
        }

        public void DisposeGraphic()
        {
            GameObject.Destroy(graphic);
        }
    }
}
