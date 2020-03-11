namespace Tetris_NetFramework
{
    class Coord
    {
        public int x;
        public int y;

        public Coord()
        { 
        
        }

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Coord operator +(Coord c1, int c2) // переопределение оператора, чтобы удобно менять y
        {
            return new Coord (c1.x, c1.y+c2);
        }

        public bool IsInsideMap(int size_x, int size_y) // проверка находится ли координата внутри массива
        {
            if (x < 0 || x > size_x - 1) return false;
            if (y < 0 || y > size_y - 1) return false;
            return true;
        }



    }
}
