namespace SimpleRefactor
{
    public class FruitEntitySpawner : EntitySpawner
    {
        public Arena arena;

        public override Entity Spawn(Cell cell)
        {
            Entity newEntity = new FruitEntity(arena);

            BindEntityToCell(newEntity, cell);
            CreateGraphicForEntity(newEntity);

            return newEntity;
        }
    }
}
