namespace SimpleRefactor
{
    public class SnakePartEntity : Entity
    {
        private Direction direction;

        public Direction GetDirection()
        {
            return direction;
        }

        public void SetDirection(Direction newDirection)
        {
            direction = newDirection;
        }

        public override bool CanBeDestroyedByASnake() => false;

        public override void TouchedBySnake(Snake snake)
        {
            snake.Die();
        }
    }
}
