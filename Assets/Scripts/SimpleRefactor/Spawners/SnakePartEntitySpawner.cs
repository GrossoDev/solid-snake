namespace SimpleRefactor
{
    public class SnakePartEntitySpawner : EntitySpawner
    {
        public override Entity Spawn(Cell cell)
        {
            Entity newEntity = new SnakePartEntity();

            BindEntityToCell(newEntity, cell);
            CreateGraphicForEntity(newEntity);

            return newEntity;
        }
    }
}
