using System;
using System.Threading;

namespace Tetris_NetFramework
{
    class Game
    {
        public int size_x;
        public int size_y;
        public const int gameSpeedConst = 500;
        public const int sizeGlobalMap_X = 29;
        public const int sizeGlobalMap_Y = 30;
        public const int sizeGameMap_X = 12;
        public const int sizeGameMap_Y = 22;

        public int gameSpeed = gameSpeedConst;
        public const int gameSpeedBoost = 70;
        public Map map;
        public GameMap gameMap = new GameMap(sizeGameMap_X, sizeGameMap_Y);
        Coord figCoord { get; set; }
        Coord figStartCoord { get; }
        public int figMax_X { get; set; }
        public int figMin_X { get; set; }
        public int score { get; set; }
        public bool isLeftClear { get; set; }
        public bool isRightClear { get; set; }
        public bool isRotatable { get; set; }
        public bool isDownClear { get; set; }
        public int figVer { get; set; } // 1 - J, 2 - O, 3 - L, 4 - I, 5 - S, 6 - Z, 7 - T       
        public bool isSkipped = false;
        public bool isSettings = false;
        public bool isRestart = false;
        public bool pressLeft = false;
        public bool pressRight = false;
        public bool pressUp = false;
        public bool pressDown = false;

        public string space = " ";
        public string mainBackgroundSym = ".";
        public string gameBackgroundSym = " ";
        public string mainCounturSym = "#";
        public string gameCounturSym = "#";
        public string figSym = "O";

        ConsoleKeyInfo ConsKeyInfo { get; set; }
        public Game(int size_x, int size_y)
        {
            this.size_x = size_x;
            this.size_y = size_y;
            isRotatable = true;
            score = 0;
            figStartCoord = new Coord(6, 1);
            map = new Map(size_x, size_y);
            Console.SetWindowSize(65, 32);
            Console.CursorVisible = false;
            Console.Title = "Console Tetris by Melnikov A.V.";
            figCoord = figStartCoord;

        }
        public void Settings()
        {
            Console.Clear();

            Console.WriteLine("Введите символ пробела между символами:");
            string input = Console.ReadLine();
            space = input;

            Console.WriteLine("\nВведите символ для основного бэкграунда:");
            input = Console.ReadLine();
            mainBackgroundSym = input;

            Console.WriteLine("\nВведите символ для игрового бэкграунда:");
            input = Console.ReadLine();
            gameBackgroundSym = input;

            Console.WriteLine("\nВведите символ для основного контура:");
            input = Console.ReadLine();
            mainCounturSym = input;

            Console.WriteLine("\nВведите символ для игрового контура:");
            input = Console.ReadLine();
            gameCounturSym = input;

            Console.WriteLine("\nВведите символ для фигур:");
            input = Console.ReadLine();
            figSym = input;

            isSettings = false;
        }
        public void ReBuild()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Coord xy;

            for (int y = 0; y < size_y; y++)
            {
                for (int x = 0; x < size_x; x++)
                {
                    Console.CursorVisible = false;
                    xy = new Coord(x, y);
                    Console.Write(map.Get(xy) + space.ToString());
                    Console.CursorVisible = false;
                    if (x == size_x - 1)
                    {
                        Console.CursorVisible = false;
                        Console.WriteLine();
                        Console.CursorVisible = false;
                    }
                    Console.CursorVisible = false;
                }
            }

        }
        public void Fall()
        {
            while (!isRestart)
            {
                if (isDownClear)
                {
                    figCoord = new Coord(figCoord.x, figCoord.y + 1);
                    Thread.Sleep(gameSpeed);
                    isDownClear = false;
                }
            }
        }
        public bool StartGame()
        {
            Console.Clear();
            Coord xy;
            figCoord = figStartCoord;
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            gameMap.FillMap(gameBackgroundSym);
            gameMap.FillContour(gameCounturSym);
            map.FillMap(mainBackgroundSym);
            map.FillContour(mainCounturSym);
            Map.InsertMatrix(gameMap.map, map.map, 2, 3);

            for (int y = 0; y < size_y; y++)  // анимация intro
            {
                for (int x = 0; x < size_x; x++)
                {
                    xy = new Coord(x, y);
                    Console.Write(map.Get(xy) + space);
                    Console.CursorVisible = false;
                    Thread.Sleep(5);                                                    ///////////////////////////////////
                    if (x == size_x - 1)
                        Console.WriteLine();
                    if (isSkipped)
                        return true;
                }
                if (isSkipped)
                    return true;
            } // INTRO

            Console.SetCursorPosition(7, 8);
            Console.Write("Welcome  to  Game");

            Console.SetCursorPosition(10, 11);
            Console.Write("Use arrows");

            Console.SetCursorPosition(9, 12);
            Console.Write("to   control");

            Console.SetCursorPosition(8, 14);
            Console.Write("\"Esc\" to START");


            Console.SetCursorPosition(9, 21);
            Console.Write("S - settings");

            Console.SetCursorPosition(9, 22);
            Console.Write("R - restart");

            figCoord = figStartCoord;

            while (!isSkipped)
            {
                for (int y = 0; y < 1; y++)  // анимация intro
                {
                    Console.SetCursorPosition(0, 0);
                    Console.CursorVisible = false;
                    for (int x = 0; x < 1; x++)
                    {
                        Console.CursorVisible = false;
                        xy = new Coord(x, y);
                        Console.Write(map.Get(xy) + space);

                        Thread.Sleep(5);                                                    ///////////////////////////////////
                        if (x == size_x - 1)
                            Console.WriteLine();
                        if (isSkipped)
                            return true;
                    }

                    if (isSkipped)
                        return true;
                } // INTRO
            }         

            return false;
        }
        public void EndGame()
        {
            Console.Clear();
            Console.SetCursorPosition(10, 9);
            Console.Write("The End");
            Console.SetCursorPosition(7, 11);
            Console.Write($"Your Score is {score}");

            Thread.Sleep(2000);

            Console.Clear();
            Console.SetCursorPosition(0, 0);

            string[] str = { "\n"+
"\n                        ##############" +
"\n                    ####              #####" +
"\n               #####                       #####"+
"\n           ####                                 ###"+
"\n         ###      ####                  ####      ###"+
"\n       ###       ######                ######       ###"+
"\n     ###        ########              ########        ###"+
"\n    ###        ##########            ##########         ###"+
"\n   ###         ##########            ##########          ###"+
"\n  ###          ##########            ##########           ###"+
"\n ###           ##########            ##########            ###"+
"\n###            ##########            ##########            ###"+
"\n###             ########              ########             ###"+
"\n###              ######                ######              ###"+
"\n###                ##                    ##                ###"+
"\n ###     ####                                    ####     ###"+
"\n  ###  ## ##                                      ## ##  ###"+
"\n   ###     ##                                    ##     ###"+
"\n    ###     ###                                 ##     ###"+
"\n     ###     ###                             ###      ###"+
"\n       ###     ######                   #######      ###"+
"\n         ####      #########     ###########      ####"+
"\n            ####       #################       ####"+
"\n                #####                     #####"+
"\n                    #######################"+
""};
            foreach (string s in str)
                Console.Write(s);

            while (!isRestart)
            {
                Console.SetCursorPosition(23, 28);
                Console.Write("Press R to RESTART");
                Thread.Sleep(500);
                Console.SetCursorPosition(23, 28);
                Console.Write("      R                   ");
                Thread.Sleep(500);
            }

        }
        public void MoveHero()
        {
            Console.CursorVisible = false;
            while (true)
            {
                ConsKeyInfo = Console.ReadKey(true);
                Thread.Sleep(10);

                if (ConsKeyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (isLeftClear)
                    {
                        pressLeft = true;
                    }

                } // LeftArrow
                else if (ConsKeyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (isRightClear)
                    {
                        pressRight = true;
                    }
                } // RightArrow
                else if (ConsKeyInfo.Key == ConsoleKey.UpArrow)
                {
                    pressUp = true;

                } // UpArrow
                else if (ConsKeyInfo.Key == ConsoleKey.DownArrow)
                {
                    pressDown = true;
                }
                else if (ConsKeyInfo.Key == ConsoleKey.R)
                {
                    score = 0;
                    gameSpeed = gameSpeedConst;
                    isRestart = true;
                }// R                
                else if (ConsKeyInfo.Key == ConsoleKey.Escape)
                {
                    isSkipped = true;
                } // Escape
                else if (ConsKeyInfo.Key == ConsoleKey.S)
                {
                    isSkipped = true;
                    isSettings = true;
                }


            } // end while
        }
        public void MainGame()
        {

            Thread thrSpeed = new Thread(Fall); // падение фигуры
            thrSpeed.Start();
            thrSpeed.IsBackground = true;
            thrSpeed.Priority = ThreadPriority.Lowest;


            int size_x = this.size_x;
            int size_y = this.size_y;            
            int line = 0;

            figVer = 1;

            int figVerNext = figVer;
            int figVerTemp = figVer;

            Map map = this.map;
            GameMap gameMap = this.gameMap;
            GameMap tempMap = new GameMap(sizeGameMap_X, sizeGameMap_Y);
            GameMap miniMap = new GameMap(6, 6);
            Coord xyMini = new Coord(18, 5);
            int sp = gameSpeed;
            Random rand = new Random();
            int tempRnd = rand.Next(1, 8);
            int nextRnd = rand.Next(1, 8);
                                   
            map.FillMap(mainBackgroundSym);
            map.FillContour(mainCounturSym);
            gameMap.FillMap(gameBackgroundSym);
            miniMap.FillMap(mainBackgroundSym);
            gameMap.FillContour(gameCounturSym);
            miniMap.FillContour(mainCounturSym);

            Map.InsertMatrix(gameMap.map, map.map, 2, 3);
            Map.InsertMatrix(gameMap.map, tempMap.map, 0, 0);

            bool isTouched = false;
            bool touch = false;

            figCoord = figStartCoord;

            while (!isRestart)
            {
                touch = false;
                isRightClear = true;
                isLeftClear = true;
                isRotatable = true;

                if (!isTouched)
                {
                    gameMap.FillMap(gameBackgroundSym);
                }
                else
                {
                    Map.InsertMatrix(tempMap.map, gameMap.map, 0, 0);
                }

                { // модуль движения
                    if (pressLeft)
                    {
                        figCoord.x = figCoord.x - 1;
                        pressLeft = false;
                    }

                    if (pressRight)
                    {
                        figCoord.x = figCoord.x + 1;
                        pressRight = false;
                    }

                    if (pressDown)
                    {
                        gameSpeed = gameSpeedBoost;
                        pressDown = false;
                    }


                } // модуль движения             
                               
                Coord[] figArray;
                Coord[] nextFigArray;

                figVerTemp = figVer;

                if (figVer == 4)
                    figVerNext = 1;
                else
                    figVerNext = figVer + 1;

                nextFigArray = gameMap.PutFig(figCoord, figSym, tempRnd, figVerNext, false);

                figArray = gameMap.PutFig(figCoord, figSym, tempRnd, figVerTemp, true);

                miniMap.FillMap(gameBackgroundSym);
                miniMap.FillContour(gameCounturSym);


                if (nextRnd == 1 || nextRnd == 3) // вставка следующей фигуры в миниатюрке
                    miniMap.PutFig(new Coord(3, 2), figSym, nextRnd, 1, true);
                else
                    miniMap.PutFig(new Coord(3, 3), figSym, nextRnd, 1, true);


                Map.InsertMatrix(miniMap.map, map.map, xyMini.x, xyMini.y);

                int max = 0;
                int min = size_x;
                int max_Y = 0;

                foreach (Coord xy in figArray) 
                {
                    if (max <= xy.x)
                        max = xy.x;

                    if (min >= xy.x)
                        min = xy.x;

                    if (max_Y <= xy.y)
                        max_Y = xy.y;

                    figMax_X = max;
                    figMin_X = min;
                } 

                gameMap.FillContour(gameCounturSym);

                Map.InsertMatrix(gameMap.map, map.map, 2, 3);

                foreach (Coord xy in nextFigArray) // проверка для вращения фигуры (чтобы при следующем положении не вылететь за пределы)
                {
                    if (max <= xy.x)
                        max = xy.x;
                    if (min >= xy.x)
                        min = xy.x;

                    if (min < 1)
                        isRotatable = false;
                    if (max > 10)
                        isRotatable = false;
                }

                if (pressUp && isRotatable)
                {
                    if (figVer == 4)
                    {
                        figVer = 1;
                        pressUp = false;
                    }
                    else
                    {
                        figVer = figVer + 1;
                        pressUp = false;
                    }
                }

                isDownClear = true;

                foreach (Coord xy in figArray) // проверка нет ли помех по сторонам для движения
                {
                    if (tempMap.Get(new Coord(xy.x - 1, xy.y)).Contains(figSym) || tempMap.Get(new Coord(xy.x - 1, xy.y)).Contains(gameCounturSym))
                        isLeftClear = false;

                    if (tempMap.Get(new Coord(xy.x + 1, xy.y)).Contains(figSym) || tempMap.Get(new Coord(xy.x + 1, xy.y)).Contains(gameCounturSym))
                        isRightClear = false;

                    if (tempMap.Get(new Coord(xy.x, xy.y + 1)).Contains(gameCounturSym) && xy.y > 1 || tempMap.Get(new Coord(xy.x, xy.y + 1)).Contains(figSym) && xy.y > 1)
                        isDownClear = false;

                }

                foreach (Coord xy in figArray) // проверка на косание дна
                {
                    if (!isDownClear)
                    {
                        Map.InsertMatrix(gameMap.map, tempMap.map, 0, 0);
                        figCoord = figStartCoord;

                        isTouched = true;
                        touch = true;

                        if (xy.y < 2)
                        {
                            EndGame();
                            return;
                        }
                    }

                }

                if (touch) 
                {
                    tempRnd = nextRnd;
                    nextRnd = rand.Next(1, 8);

                    
                    if (gameSpeed > 10)
                    {
                        sp -= 3;
                        gameSpeed = sp;
                    }
                }

                line = 0;

                for (int y = tempMap.size_y; y > 1; y--) // удаление заполненного ряда
                {
                    line = 0;
                    for (int x = 1; x < tempMap.size_x; x++)
                    {
                        if (tempMap.Get(new Coord(tempMap.size_x - x, y)).Contains(figSym))
                        {
                            line++;
                        }

                        if (line == 10)
                        {
                            score++;

                            for (int yy = y; yy > 1; yy--)
                            {
                                for (int xx = 1; xx < tempMap.size_x; xx++)
                                {
                                    tempMap.Copy(new Coord(xx, yy - 1), new Coord(xx, yy));

                                }
                            }
                        }

                    } // for x

                } // for y
                Console.SetCursorPosition(sizeGlobalMap_X - 4, sizeGlobalMap_Y);
                Console.WriteLine($"Score: {score}");
                
                ReBuild();

            } // end while        
                                                  
            thrSpeed.Abort();
        }

        static void Main()
        {
            Game g = new Game(sizeGlobalMap_X, sizeGlobalMap_Y);

            while (!g.isRestart)
            {                
                Thread thr = new Thread(g.MoveHero); // чтение кавиатуры           
                thr.Start();
                Thread thr2 = new Thread(g.MoveHero); // чтение кавиатуры           

                if (g.StartGame())
                {
                    if (g.isSettings)
                    {
                        thr.Abort();
                        g.Settings();
                        thr2.Start();
                        g.MainGame();
                    }
                    else
                    {
                        g.MainGame();
                    }

                    if (g.isSkipped)
                    {
                        g.MainGame();
                    }
                }
                thr.Abort();
                thr2.Abort();
                g.isRestart = false;
            }

        }
    }
}
