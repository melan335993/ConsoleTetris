using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris_NetFramework
{
    class Map
    {
        public string fig { get; set; }
        public int size_x { get; }
        public int size_y { get; }
        public string[,] map { get; }


        public Map(int size_x, int size_y) 
        {
            this.size_x = size_x;
            this.size_y = size_y;
            map = new string[size_x, size_y];
        }

        public Map(int size) 
        {
            this.size_x = size;
            this.size_y = size;
            map = new string[size_x, size_y];
        }      

        public void Set(Coord xy, string sym)
        {
            if (xy.IsInsideMap(size_x, size_y))
                map[xy.x, xy.y] = sym;
        }

        public string Get(Coord xy)
        {
            if (xy.IsInsideMap(size_x, size_y))
                return map[xy.x, xy.y];

            return "?";
        }

        public void Copy(Coord from, Coord to)
        {
            Set(to, Get(from));
        }

        public void FillMap(string ch) // метод для заполнения массива символом
        {
            for (int y = 0; y < size_y; y++)
                for (int x = 0; x < size_x; x++)
                    Set(new Coord(x, y), ch);
        }

        public void FillContour(string ch) // метод для заполнения массива по периметру символом 
        {
            for (int y = 0; y < size_y; y++)
                for (int x = 0; x < size_x; x++)
                {
                    if (x == 0 || x == size_x - 1 || y == 0 || y == size_y - 1)
                        Set(new Coord(x, y), ch);
                }
        }        

        public static void InsertMatrix<T>(T[,] source, T[,] dest, int row, int col) 
        {
            // TODO: Проверка границ
            for (int r = 0; r < source.GetLength(0); r++)
            {
                for (int c = 0; c < source.GetLength(1); c++)
                {
                    dest[r + row, c + col] = source[r, c];
                }
            }
        }



    }
}
