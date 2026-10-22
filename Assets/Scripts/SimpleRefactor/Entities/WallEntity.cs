namespace SimpleRefactor
{
    public class WallEntity : Entity
    {
        public override bool CanBeDestroyedByASnake() => false;

        public override void TouchedBySnake(Snake snake)
        {
            snake.Die();
        }
    }
}
