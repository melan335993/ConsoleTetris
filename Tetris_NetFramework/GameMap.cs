using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris_NetFramework
{
    class GameMap
    {
        public string fig { get; set; }
        public int size_x { get; }
        public int size_y { get; }
        public string[,] map { get; }

        public GameMap(int size_x, int size_y) 
        {
            this.size_x = size_x;
            this.size_y = size_y;
            map = new string[size_x, size_y];
        }

        public GameMap(int size) 
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

        public Coord[] PutFig(Coord xy, string sym, int fig, int ver, bool drow)
        {
            Coord[] defultArray = { new Coord(0, 0) };
            
            switch (fig)
            {
                case 1: // J
                    switch (ver)
                    {
                        case 1:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][ ][ ]
                            //   [ ][O][*][O]
                            //   [ ][ ][ ][O]

                            if (drow)
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x + 1, xy.y), sym);
                                Set(new Coord(xy.x - 1, xy.y), sym);
                                Set(new Coord(xy.x + 1, xy.y + 1), sym);
                            }                           

                            Coord[] array1 = { new Coord(xy.x, xy.y), new Coord(xy.x + 1, xy.y), new Coord(xy.x - 1, xy.y), new Coord(xy.x + 1, xy.y + 1) };

                            return array1;

                        case 2:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][O][ ]
                            //   [ ][ ][O][ ]
                            //   [ ][O][*][ ]

                            if (drow)
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x - 1, xy.y), sym);
                                Set(new Coord(xy.x, xy.y - 1), sym);
                                Set(new Coord(xy.x, xy.y - 2), sym);
                            }
                               
                            Coord[] array2 = { new Coord(xy.x, xy.y), new Coord(xy.x - 1, xy.y), new Coord(xy.x, xy.y - 1), new Coord(xy.x, xy.y - 2) };

                            return array2;

                        case 3:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][ ][ ]
                            //   [ ][O][ ][ ]
                            //   [ ][O][*][O]

                            if (drow) 
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x + 1, xy.y), sym);
                                Set(new Coord(xy.x - 1, xy.y), sym);
                                Set(new Coord(xy.x - 1, xy.y - 1), sym);
                            }                                

                            Coord[] array3 = { new Coord(xy.x, xy.y), new Coord(xy.x + 1, xy.y), new Coord(xy.x - 1, xy.y), new Coord(xy.x - 1, xy.y - 1) };

                            return array3;

                        case 4:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][O][O]
                            //   [ ][ ][O][ ]
                            //   [ ][ ][*][ ]

                            if (drow)
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x, xy.y - 1), sym);
                                Set(new Coord(xy.x, xy.y - 2), sym);
                                Set(new Coord(xy.x + 1, xy.y - 2), sym);
                            }                                

                            Coord[] array4 = { new Coord(xy.x, xy.y), new Coord(xy.x, xy.y - 1), new Coord(xy.x, xy.y - 2), new Coord(xy.x + 1, xy.y - 2) };

                            return array4;

                        default: return defultArray;
                    }

                case 2: // O
                    switch (ver)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][ ][ ]
                            //   [ ][O][O][ ]
                            //   [ ][O][*][ ]

                            if (drow) 
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x, xy.y - 1), sym);
                                Set(new Coord(xy.x - 1, xy.y - 1), sym);
                                Set(new Coord(xy.x - 1, xy.y), sym);
                            }                                

                            Coord[] array1 = {new Coord(xy.x, xy.y), new Coord(xy.x, xy.y - 1),new Coord(xy.x-1, xy.y - 1),new Coord(xy.x -1, xy.y)};

                            return array1;

                        default: return defultArray;
                    }

                case 3: // L
                    switch (ver)
                    {
                        case 1:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][ ][ ]
                            //   [ ][O][*][O]
                            //   [ ][O][ ][ ]

                            if (drow)
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x + 1, xy.y), sym);
                                Set(new Coord(xy.x - 1, xy.y), sym);
                                Set(new Coord(xy.x - 1, xy.y + 1), sym);
                            }
                               
                            Coord[] array1 = {new Coord(xy.x, xy.y),new Coord(xy.x+1, xy.y), new Coord(xy.x-1, xy.y ), new Coord(xy.x - 1, xy.y +1)};

                            return array1;

                            case 2:
                            //   [ ][ ][ ][ ]
                            //   [ ][O][O][ ]
                            //   [ ][ ][O][ ]
                            //   [ ][ ][*][ ]

                            if (drow) 
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x, xy.y - 1), sym);
                                Set(new Coord(xy.x, xy.y - 2), sym);
                                Set(new Coord(xy.x - 1, xy.y - 2), sym);
                            }                                

                            Coord[] array2 = {new Coord(xy.x, xy.y),new Coord(xy.x, xy.y-1),new Coord(xy.x, xy.y-2 ),new Coord(xy.x - 1, xy.y -2) };

                            return array2;

                            case 3:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][ ][O]
                            //   [ ][O][*][O]

                            if (drow)
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x + 1, xy.y), sym);
                                Set(new Coord(xy.x - 1, xy.y), sym);
                                Set(new Coord(xy.x + 1, xy.y - 1), sym);
                            }                               

                            Coord[] array3 = {new Coord(xy.x, xy.y),new Coord(xy.x+1, xy.y),new Coord(xy.x-1, xy.y ), new Coord(xy.x +1, xy.y -1) };

                            return array3;

                            case 4:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][O][ ]
                            //   [ ][ ][O][ ]
                            //   [ ][ ][*][O]

                            if (drow) 
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x + 1, xy.y), sym);
                                Set(new Coord(xy.x, xy.y - 1), sym);
                                Set(new Coord(xy.x, xy.y - 2), sym);
                            }                                

                            Coord[] array4 = { new Coord(xy.x, xy.y),new Coord(xy.x+1, xy.y), new Coord(xy.x, xy.y-1 ), new Coord(xy.x, xy.y -2)};

                            return array4;

                        default: return defultArray;
                    }

                case 4:  // I
                    switch (ver)
                    {
                        case 1:
                        case 3:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][ ][ ]
                            //   [O][O][*][O]

                            if (drow) 
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x + 1, xy.y), sym);
                                Set(new Coord(xy.x - 1, xy.y), sym);
                                Set(new Coord(xy.x - 2, xy.y), sym);
                            }                                

                            Coord[] array1 = { new Coord(xy.x, xy.y), new Coord(xy.x+1, xy.y), new Coord(xy.x-1, xy.y), new Coord(xy.x-2, xy.y)};

                            return array1;

                            case 2:
                            case 4:
                            //   [ ][ ][O][ ]
                            //   [ ][ ][O][ ]
                            //   [ ][ ][O][ ]
                            //   [ ][ ][*][ ]

                            if (drow)
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x, xy.y - 1), sym);
                                Set(new Coord(xy.x, xy.y - 2), sym);
                                Set(new Coord(xy.x, xy.y - 3), sym);
                            }                              

                            Coord[] array2 = {new Coord(xy.x, xy.y),new Coord(xy.x, xy.y-1), new Coord(xy.x, xy.y-2), new Coord(xy.x, xy.y-3)};

                            return array2;

                        default: return defultArray;
                    }

                case 5: // S
                    switch (ver)
                    {
                        case 1:
                        case 3:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][O][O]
                            //   [ ][O][*][ ]

                            if (drow) 
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x - 1, xy.y), sym);
                                Set(new Coord(xy.x + 1, xy.y - 1), sym);
                                Set(new Coord(xy.x, xy.y - 1), sym);
                            }                                

                            Coord[] array1 = { new Coord(xy.x, xy.y),new Coord(xy.x-1, xy.y),new Coord(xy.x+1, xy.y-1),new Coord(xy.x, xy.y-1) };

                            return array1;

                        case 2:
                        case 4:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][O][ ]
                            //   [ ][ ][*][O]
                            //   [ ][ ][ ][O]
                            
                            if (drow)
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x + 1, xy.y), sym);
                                Set(new Coord(xy.x, xy.y - 1), sym);
                                Set(new Coord(xy.x + 1, xy.y + 1), sym);
                            }                               

                            Coord[] array2 = {  new Coord(xy.x, xy.y),new Coord(xy.x+1, xy.y), new Coord(xy.x, xy.y-1), new Coord(xy.x+1, xy.y+1) };

                            return array2;


                        default: return defultArray;
                    }

                case 6: // Z
                    switch (ver)
                    {
                        case 1:
                        case 3:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][ ][ ]
                            //   [ ][O][O][ ]
                            //   [ ][ ][*][O]

                            if (drow)
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x + 1, xy.y), sym);
                                Set(new Coord(xy.x, xy.y - 1), sym);
                                Set(new Coord(xy.x - 1, xy.y - 1), sym);
                            }                              

                            Coord[] array1 = {new Coord(xy.x, xy.y), new Coord(xy.x+1, xy.y), new Coord(xy.x, xy.y-1),  new Coord(xy.x-1, xy.y-1)};

                            return array1;

                            case 2:
                            case 4:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][ ][O]
                            //   [ ][ ][O][O]
                            //   [ ][ ][*][ ]

                            if (drow)
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x, xy.y - 1), sym);
                                Set(new Coord(xy.x + 1, xy.y - 1), sym);
                                Set(new Coord(xy.x + 1, xy.y - 2), sym);
                            }                              

                            Coord[] array2 = {new Coord(xy.x, xy.y), new Coord(xy.x, xy.y-1),new Coord(xy.x+1, xy.y-1), new Coord(xy.x+1, xy.y-2) };

                            return array2;

                        default: return defultArray;
                    }

                case 7: // T
                    switch (ver)
                    {
                        case 1:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][O][ ]
                            //   [ ][O][*][O]

                            if (drow) {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x, xy.y - 1), sym);
                                Set(new Coord(xy.x + 1, xy.y), sym);
                                Set(new Coord(xy.x - 1, xy.y), sym);
                            }                                

                            Coord[] array1 = {new Coord(xy.x, xy.y), new Coord(xy.x, xy.y-1), new Coord(xy.x+1, xy.y), new Coord(xy.x-1, xy.y)};

                            return array1;

                        case 2:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][O][ ]
                            //   [ ][ ][O][O]
                            //   [ ][ ][*][ ]

                            if (drow) {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x, xy.y - 1), sym);
                                Set(new Coord(xy.x + 1, xy.y - 1), sym);
                                Set(new Coord(xy.x, xy.y - 2), sym);
                            }                                

                            Coord[] array2 = { new Coord(xy.x, xy.y), new Coord(xy.x, xy.y - 1) , new Coord(xy.x + 1, xy.y - 1), new Coord(xy.x, xy.y - 2) };

                            return array2;

                            case 3:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][ ][ ]
                            //   [ ][O][O][O]
                            //   [ ][ ][*][ ]

                            if (drow)
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x, xy.y - 1), sym);
                                Set(new Coord(xy.x + 1, xy.y - 1), sym);
                                Set(new Coord(xy.x - 1, xy.y - 1), sym);
                            }                               

                            Coord[] array3 = {new Coord(xy.x, xy.y), new Coord(xy.x, xy.y-1), new Coord(xy.x+1, xy.y-1), new Coord(xy.x-1, xy.y-1)};

                            return array3;

                            case 4:
                            //   [ ][ ][ ][ ]
                            //   [ ][ ][O][ ]
                            //   [ ][O][O][ ]
                            //   [ ][ ][*][ ]

                            if (drow) 
                            {
                                Set(new Coord(xy.x, xy.y), sym);
                                Set(new Coord(xy.x, xy.y - 1), sym);
                                Set(new Coord(xy.x - 1, xy.y - 1), sym);
                                Set(new Coord(xy.x, xy.y - 2), sym);
                            }                                

                            Coord[] array4 = {new Coord(xy.x, xy.y), new Coord(xy.x, xy.y-1), new Coord(xy.x-1, xy.y-1), new Coord(xy.x, xy.y-2)};

                            return array4;

                        default: return defultArray;
                    }
                default: return defultArray;
            }
        }
    }
}
