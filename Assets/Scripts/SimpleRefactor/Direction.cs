namespace SimpleRefactor
{
    public struct Direction
    {
        public int deltaX;
        public int deltaY;

        public Direction(int deltaX, int deltaY)
        {
            this.deltaX = deltaX;
            this.deltaY = deltaY;
        }

        public bool IsEqualTo(Direction otherDirection)
        {
            bool oppositeX = this.deltaX == otherDirection.deltaX;
            bool oppositeY = this.deltaY == otherDirection.deltaY;

            return oppositeX && oppositeY;
        }

        public bool IsOppositeTo(Direction otherDirection)
        {
            bool oppositeX = this.deltaX == -otherDirection.deltaX;
            bool oppositeY = this.deltaY == -otherDirection.deltaY;

            return oppositeX && oppositeY;
        }

        public Direction Opposite()
        {
            return new Direction(-this.deltaX, -this.deltaY);
        }

        public static Direction Up    => new Direction( 0,  1);
        public static Direction Down  => new Direction( 0, -1);
        public static Direction Left  => new Direction( 1,  0);
        public static Direction Right => new Direction(-1,  0);
    }
}
