using UnityEngine;

namespace SimpleRefactor
{
    public class FruitEntity : Entity
    {
        private Arena arena;

        public FruitEntity(Arena arena)
        {
            this.arena = arena;
        }

        public override bool CanBeDestroyedByASnake() => true;

        public override void TouchedBySnake(Snake snake)
        {
            DestroyEntity();
            snake.Grow();
            arena.SpawnFruit();
        }
    }
}
