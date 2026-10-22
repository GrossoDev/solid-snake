using UnityEngine;

namespace SimpleRefactor
{
    public abstract class EntitySpawner : MonoBehaviour
    {
        public Transform graphicsParent;
        public GameObject graphicPrefab;

        public abstract Entity Spawn(Cell cell);

        protected void CreateGraphicForEntity(Entity entity)
        {
            Vector2Int positionInGrid = entity.occupiedCell.GetPosition();
            Vector3 positionInSpace = new Vector3(positionInGrid.x, 0, -positionInGrid.y);
            Quaternion rotation = Quaternion.identity;
            GameObject graphic = Instantiate(graphicPrefab, positionInSpace, rotation, graphicsParent);

            entity.SetGraphic(graphic);
        }

        protected void BindEntityToCell(Entity entity, Cell cell)
        {
            entity.SetOccupiedCell(cell);
            cell.SetOccupyingEntity(entity);
        }
    }
}
