namespace SimpleRefactor
{
    public class WallEntitySpawner : EntitySpawner
    {
        public override Entity Spawn(Cell cell)
        {
            Entity newEntity = new WallEntity();

            BindEntityToCell(newEntity, cell);
            CreateGraphicForEntity(newEntity);

            return newEntity;
        }
    }
}
