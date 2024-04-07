using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{

    static int score = 0;
    static bool restart = true;
    static bool start = true ;
    static int button = 1;
    static string filePath = "/Users/rashid/Projects/fds1/fds1/RecordTable.txt";
    

    

    //static ConsoleKeyInfo cki;

    //function for controll area ( is it free )
    static bool AreaControl(char[][] field, int wallx, int wally, int wallLength, int emptySpace, int direction)
    {

        // controll for vertical walls (dir=1)
        if (direction == 1)
        {
            for (int i = wallx - 1; i <= wallx + wallLength + emptySpace; i++)
            {
                for (int j = wally - 1; j <= wally + 1; j++)
                {
                    if (i >= 0 && i < 23 && j >= 0 && j < 53)
                    {
                        if (field[i][j] == '#')
                        {
                            return false; // If a wall is detected in the neighborhood, the area is not free
                        }
                    }
                }
            }
            return true;
        }

        // controll for horizontal walls (dir=0)
        else
        {
            for (int i = wallx - 1; i <= wallx + 1; i++)
            {
                for (int j = wally - 1; j <= wally + wallLength + emptySpace; j++)
                {
                    if (i >= 0 && i < 23 && j >= 0 && j < 53)
                    {
                        if (field[i][j] == '#')
                        {
                            return false; // If a wall is detected in the neighborhood, the area is not free
                        }
                    }
                }
            }
            return true;
        }

    }

    //function for moving and smashing numbers 
    static bool numMove(char[][] field, int direction, int cursorx, int cursory)
    {
        Random random = new Random();
        int numx; int numy;

        switch (direction)
        {
            //MOVE UP
            case 0:
                //Consecutive Control (check if next num < that our)
                if (field[cursory - 3][cursorx] <= field[cursory - 2][cursorx])
                {

                    if (field[cursory - 3][cursorx] == '0' || field[cursory - 3][cursorx] == '1' ||
                        field[cursory - 3][cursorx] == '2' || field[cursory - 3][cursorx] == '3' ||
                        field[cursory - 3][cursorx] == '4' || field[cursory - 3][cursorx] == '5' ||
                        field[cursory - 3][cursorx] == '6' || field[cursory - 3][cursorx] == '7' ||
                        field[cursory - 3][cursorx] == '8' || field[cursory - 3][cursorx] == '9')
                    {
                        //recursion for movement of more than one number
                        if (numMove(field, direction, cursorx, cursory - 1))
                        {
                            field[cursory - 3][cursorx] = field[cursory - 2][cursorx];
                            field[cursory - 2][cursorx] = ' ';

                            return true;
                        }
                        return false;
                    }
                    //smashing
                    else if (field[cursory - 3][cursorx] == '#')
                    {
                        //controll of last number between player and wall
                        if (field[cursory - 1][cursorx] == 'X')
                        {
                            return false;
                        }

                        //giving scores
                        if (field[cursory - 2][cursorx] == '5' || field[cursory - 2][cursorx] == '6' ||
                           field[cursory - 2][cursorx] == '7' || field[cursory - 2][cursorx] == '8' ||
                           field[cursory - 2][cursorx] == '9')
                        {
                            score += 1;
                        }
                        else if (field[cursory - 2][cursorx] == '1' || field[cursory - 2][cursorx] == '2' ||
                                 field[cursory - 2][cursorx] == '3' || field[cursory - 2][cursorx] == '4')
                        {
                            score += 2;
                        }
                        else if (field[cursory - 2][cursorx] == '0')
                        {
                            score += 20;
                        }

                        //create new numbers on the board after smashing 
                        do
                        {
                            numy = random.Next(1, 22); numx = random.Next(1, 52);
                        } while (!(field[numy][numx] == ' '));

                        field[numy][numx] = Convert.ToChar(random.Next(53, 58));
                        return true;
                    }
                    //simple move
                    else
                    {

                        field[cursory - 3][cursorx] = field[cursory - 2][cursorx];
                        field[cursory - 2][cursorx] = ' ';
                        return true;

                    }

                }
                return false;

            //MOVE RIGHT  
            case 1:
                //Consecutive Control (check if next num < that our)
                if (field[cursory - 2][cursorx + 1] <= field[cursory - 2][cursorx])
                {

                    if (field[cursory - 2][cursorx + 1] == '0' || field[cursory - 2][cursorx + 1] == '1' ||
                        field[cursory - 2][cursorx + 1] == '2' || field[cursory - 2][cursorx + 1] == '3' ||
                        field[cursory - 2][cursorx + 1] == '4' || field[cursory - 2][cursorx + 1] == '5' ||
                        field[cursory - 2][cursorx + 1] == '6' || field[cursory - 2][cursorx + 1] == '7' ||
                        field[cursory - 2][cursorx + 1] == '8' || field[cursory - 2][cursorx + 1] == '9')
                    {
                        //recursion for movement of more than one number
                        if (numMove(field, direction, cursorx + 1, cursory))
                        {
                            field[cursory - 2][cursorx + 1] = field[cursory - 2][cursorx];
                            field[cursory - 2][cursorx] = ' ';

                            return true;
                        }

                        return false;
                    }
                    //smashing
                    else if (field[cursory - 2][cursorx + 1] == '#')
                    {
                        //controll of last number between player and wall
                        if (field[cursory - 2][cursorx - 1] == 'X')
                        {
                            return false;
                        }

                        //giving scores
                        if (field[cursory - 2][cursorx] == '5' || field[cursory - 2][cursorx] == '6' ||
                           field[cursory - 2][cursorx] == '7' || field[cursory - 2][cursorx] == '8' ||
                           field[cursory - 2][cursorx] == '9')
                        {
                            score += 1;
                        }
                        else if (field[cursory - 2][cursorx] == '1' || field[cursory - 2][cursorx] == '2' ||
                                 field[cursory - 2][cursorx] == '3' || field[cursory - 2][cursorx] == '4')
                        {
                            score += 2;
                        }
                        else if (field[cursory - 2][cursorx] == '0')
                        {
                            score += 20;
                        }

                        //create new numbers on the board after smashing 
                        do
                        {
                            numy = random.Next(1, 22); numx = random.Next(1, 52);
                        } while (!(field[numy][numx] == ' '));

                        field[numy][numx] = Convert.ToChar(random.Next(53, 58));
                        return true;
                    }
                    //simple move
                    else
                    {

                        field[cursory - 2][cursorx + 1] = field[cursory - 2][cursorx];
                        field[cursory - 2][cursorx] = ' ';
                        return true;
                    }
                }
                return false;

            //MOVE DOWN 
            case 2:
                //Consecutive Control (check if next num < that our)
                if (field[cursory - 1][cursorx] <= field[cursory - 2][cursorx])
                {
                    if (field[cursory - 1][cursorx] == '0' || field[cursory - 1][cursorx] == '1' ||
                        field[cursory - 1][cursorx] == '2' || field[cursory - 1][cursorx] == '3' ||
                        field[cursory - 1][cursorx] == '4' || field[cursory - 1][cursorx] == '5' ||
                        field[cursory - 1][cursorx] == '6' || field[cursory - 1][cursorx] == '7' ||
                        field[cursory - 1][cursorx] == '8' || field[cursory - 1][cursorx] == '9')
                    {
                        //recursion for movement of more than one number
                        if (numMove(field, direction, cursorx, cursory + 1))
                        {
                            field[cursory - 1][cursorx] = field[cursory - 2][cursorx];
                            field[cursory - 2][cursorx] = ' ';

                            return true;
                        }
                        return false;
                    }
                    //smashing 
                    else if (field[cursory - 1][cursorx] == '#')
                    {
                        //controll of last number between player and wall
                        if (field[cursory - 3][cursorx] == 'X')
                        {
                            return false;
                        }

                        //giving scores
                        if (field[cursory - 2][cursorx] == '5' || field[cursory - 2][cursorx] == '6' ||
                           field[cursory - 2][cursorx] == '7' || field[cursory - 2][cursorx] == '8' ||
                           field[cursory - 2][cursorx] == '9')
                        {
                            score += 1;
                        }
                        else if (field[cursory - 2][cursorx] == '1' || field[cursory - 2][cursorx] == '2' ||
                                 field[cursory - 2][cursorx] == '3' || field[cursory - 2][cursorx] == '4')
                        {
                            score += 2;
                        }
                        else if (field[cursory - 2][cursorx] == '0')
                        {
                            score += 20;
                        }

                        //create new numbers on the board after smashing 
                        do
                        {
                            numy = random.Next(1, 22); numx = random.Next(1, 52);
                        } while (!(field[numy][numx] == ' '));

                        field[numy][numx] = Convert.ToChar(random.Next(53, 58));
                        return true;
                    }
                    //simple move
                    else
                    {
                        field[cursory - 1][cursorx] = field[cursory - 2][cursorx];
                        field[cursory - 2][cursorx] = ' ';
                        return true;

                    }
                }
                return false;


            //MOVE LEFT
            case 3:
                //Consecutive Control (check if next num < that our)
                if (field[cursory - 2][cursorx - 1] <= field[cursory - 2][cursorx])
                {
                    if (field[cursory - 2][cursorx - 1] == '0' || field[cursory - 2][cursorx - 1] == '1' ||
                    field[cursory - 2][cursorx - 1] == '2' || field[cursory - 2][cursorx - 1] == '3' ||
                    field[cursory - 2][cursorx - 1] == '4' || field[cursory - 2][cursorx - 1] == '5' ||
                    field[cursory - 2][cursorx - 1] == '6' || field[cursory - 2][cursorx - 1] == '7' ||
                    field[cursory - 2][cursorx - 1] == '8' || field[cursory - 2][cursorx - 1] == '9')
                    {
                        //recursion for movement of more than one number
                        if (numMove(field, direction, cursorx - 1, cursory))
                        {
                            field[cursory - 2][cursorx - 1] = field[cursory - 2][cursorx];
                            field[cursory - 2][cursorx] = ' ';

                            return true;
                        }
                        return false;
                    }
                    //smashing 
                    else if (field[cursory - 2][cursorx - 1] == '#')
                    {
                        //controll of last number between player and wall
                        if (field[cursory - 2][cursorx + 1] == 'X')
                        {
                            return false;
                        }

                        //giving scores
                        if (field[cursory - 2][cursorx] == '5' || field[cursory - 2][cursorx] == '6' ||
                           field[cursory - 2][cursorx] == '7' || field[cursory - 2][cursorx] == '8' ||
                           field[cursory - 2][cursorx] == '9')
                        {
                            score += 1;
                        }
                        else if (field[cursory - 2][cursorx] == '1' || field[cursory - 2][cursorx] == '2' ||
                                 field[cursory - 2][cursorx] == '3' || field[cursory - 2][cursorx] == '4')
                        {
                            score += 2;
                        }
                        else if (field[cursory - 2][cursorx] == '0')
                        {
                            score += 20;
                        }

                        //create new numbers on the board after smashing 
                        do
                        {
                            numy = random.Next(1, 22); numx = random.Next(1, 52);
                        } while (!(field[numy][numx] == ' '));

                        field[numy][numx] = Convert.ToChar(random.Next(53, 58));
                        return true;
                    }
                    //simple move
                    else
                    {
                        field[cursory - 2][cursorx - 1] = field[cursory - 2][cursorx];
                        field[cursory - 2][cursorx] = ' ';
                        return true;
                    }
                }
                return false;
        }
        return false;

    }

    //function that print Intro 
    static void StartAnimation()
    {
        Console.ResetColor();
        //Game end animation (in progress)
        string[] textt = {@"________/\\\\\\\\\__________________________________________________________/\\\\\\\\\\\\__________________________________________________          ",
                          @" _____/\\\////////__________________________________________________________\/\\\////////\\\________________________________________________         ",
                          @"  ___/\\\/_________________________________________________________/\\\______\/\\\______\//\\\_______________________________________________        ",
                          @"   __/\\\_________________/\\\\\_____/\\\____/\\\__/\\/\\\\\\____/\\\\\\\\\\\_\/\\\_______\/\\\_____/\\\\\_____/\\____/\\___/\\__/\\/\\\\\\___       ",
                          @"    _\/\\\_______________/\\\///\\\__\/\\\___\/\\\_\/\\\////\\\__\////\\\////__\/\\\_______\/\\\___/\\\///\\\__\/\\\__/\\\\_/\\\_\/\\\////\\\__      ",
                          @"     _\//\\\_____________/\\\__\//\\\_\/\\\___\/\\\_\/\\\__\//\\\____\/\\\______\/\\\_______\/\\\__/\\\__\//\\\_\//\\\/\\\\\/\\\__\/\\\__\//\\\_     ",
                          @"      __\///\\\__________\//\\\__/\\\__\/\\\___\/\\\_\/\\\___\/\\\____\/\\\_/\\__\/\\\_______/\\\__\//\\\__/\\\___\//\\\\\/\\\\\___\/\\\___\/\\\_    ",
                          @"       ____\////\\\\\\\\\__\///\\\\\/___\//\\\\\\\\\__\/\\\___\/\\\____\//\\\\\___\/\\\\\\\\\\\\/____\///\\\\\/_____\//\\\\//\\\____\/\\\___\/\\\_   ",
                          @"        _______\/////////_____\/////______\/////////___\///____\///______\/////____\////////////________\/////________\///__\///_____\///____\///__  ",
                            "",
                            "",
                            "",
                            "",
                            "",
                           "                                                                       Press Enter  " };



        //ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Magenta
        ConsoleColor[] colorss = { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Magenta };

        bool animationRunningg = true; // Animation starts immediately

        while (animationRunningg)
        {
            int centerXX = Console.WindowWidth / 2 - textt[0].Length / 2;
            int centerYY = Console.WindowHeight / 2 - textt.Length / 2;
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                animationRunningg = false; // Stop the animation on Enter press
            }

            Console.Clear();
            for (int j = 0; j < textt.Length; j++)
            {

                for (int i = 0; i < textt[j].Length - 1; i++)
                {
                    Console.SetCursorPosition(centerXX + i, centerYY);

                    // Display text in different colors
                    Console.ForegroundColor = colorss[(i + Environment.TickCount / 200) % colorss.Length];
                    Console.Write(textt[j][i]);
                }
                centerYY++;
                Console.WriteLine();
            }

            Thread.Sleep(100);
        }
        GameMenu();
    }

    //function that print screen when player die
    static void EndAnimation()
    {
        //Console.ResetColor();
        //Game end animation (in progress)
        string[] textt = {"▓██   ██▓ ▒█████   █    ██    ▓█████▄  ██▓▓█████ ▓█████▄   ",
                          "  ▒██  ██▒▒██▒  ██▒ ██  ▓██▒   ▒██▀ ██▌▓██▒▓█   ▀ ▒██▀ ██▌ ",
                          "   ▒██ ██░▒██░  ██▒▓██  ▒██░   ░██   █▌▒██▒▒███   ░██   █▌ ",
                          "   ░ ▐██▓░▒██   ██░▓▓█  ░██░   ░▓█▄   ▌░██░▒▓█  ▄ ░▓█▄   ▌ ",
                          "   ░ ██▒▓░░ ████▓▒░▒▒█████▓    ░▒████▓ ░██░░▒████▒░▒████▓  ",
                          "    ██▒▒▒ ░ ▒░▒░▒░ ░▒▓▒ ▒ ▒     ▒▒▓  ▒ ░▓  ░░ ▒░ ░ ▒▒▓  ▒  ",
                          "  ▓██ ░▒░   ░ ▒ ▒░ ░░▒░ ░ ░     ░ ▒  ▒  ▒ ░ ░ ░  ░ ░ ▒  ▒  ",
                          "  ▒ ▒ ░░  ░ ░ ░ ▒   ░░░ ░ ░     ░ ░  ░  ▒ ░   ░    ░ ░  ░  ",
                          "  ░ ░         ░ ░     ░           ░     ░     ░  ░   ░     ",
                          "  ░ ░                           ░                  ░       ",
                            "",
                            "",
                            "",
                          "                 Press Enter to continue "};



        //ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Magenta
        ConsoleColor[] colorss = { ConsoleColor.Red, ConsoleColor.DarkRed };

        bool animationRunningg = true; // Animation starts immediately
        int centerXX = 0;
        int centerYY = 0;
        while (animationRunningg)
        {
            centerXX = Console.WindowWidth / 2 - textt[0].Length / 2;
            centerYY = Console.WindowHeight / 2 - textt.Length / 2;
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                animationRunningg = false; // Stop the animation on Enter press
            }

            Console.Clear();
            for (int j = 0; j < textt.Length; j++)
            {

                for (int i = 0; i < textt[j].Length - 1; i++)
                {
                    Console.SetCursorPosition(centerXX + i, centerYY);

                    // Display text in different colors
                    Console.ForegroundColor = colorss[(i + Environment.TickCount / 200) % colorss.Length];
                    Console.Write(textt[j][i]);
                }
                centerYY++;
                Console.WriteLine();
            }
            

            Thread.Sleep(100);
        }

        // SAVE NAME AND RESULT
        //Console.SetCursorPosition(centerXX + 23, centerYY - 1);
        //string name = Console.ReadLine();
        SaveRecord();
        //GameMenu();

    }

    //function that create walls in the field
    static void WallCreating(char[][] field, int direction, int wall11x, int wall11y, int wall7x, int wall7y, int wall3x, int wall3y)
    {
        Random random = new Random();

        // direction of wall 0 = horizontall , 1 = vertical

        // creating 3x 11 unit wall
        for (int z = 0; z < 3; z++)
        {

            direction = random.Next(0, 2);
            do
            {
                if (direction == 1)
                {
                    wall11x = random.Next(2, 11);
                    wall11y = random.Next(2, 51);
                }
                else if (direction == 0)
                {
                    wall11x = random.Next(2, 21);
                    wall11y = random.Next(2, 41);
                }


                //control is area free
            } while (!AreaControl(field, wall11x, wall11y, 11, 1, direction));



            for (int i = 0; i < 11; i++)
            {
                //creating vertical walls
                if (direction == 1)
                {
                    field[wall11x + i][wall11y] = '#';
                }

                //creating horizontall walls
                else if (direction == 0)
                {
                    field[wall11x][wall11y + i] = '#';
                }
            }
        }

        // creating 5x 7 unit wall
        for (int z = 0; z < 5; z++)
        {
            direction = random.Next(0, 2);
            do
            {
                if (direction == 1)
                {
                    wall11x = random.Next(2, 15);
                    wall11y = random.Next(2, 51);
                }
                else if (direction == 0)
                {
                    wall11x = random.Next(2, 21);
                    wall11y = random.Next(2, 45);
                }
                //control is area free
            } while (!AreaControl(field, wall11x, wall11y, 7, 1, direction));

            for (int i = 0; i < 7; i++)
            {
                //creating vertical walls
                if (direction == 1)
                {
                    field[wall11x + i][wall11y] = '#';
                }

                //creating horizontall walls
                else if (direction == 0)
                {
                    field[wall11x][wall11y + i] = '#';
                }
            }
        }

        // creating 20x 3 unit wall
        for (int z = 0; z < 20; z++)
        {
            direction = random.Next(0, 2);
            do
            {
                if (direction == 1)
                {
                    wall11x = random.Next(2, 19);
                    wall11y = random.Next(2, 51);
                }
                else if (direction == 0)
                {
                    wall11x = random.Next(2, 21);
                    wall11y = random.Next(2, 49);
                }
                //control is area free
            } while (!AreaControl(field, wall11x, wall11y, 3, 1, direction));

            for (int i = 0; i < 3; i++)
            {
                //creating vertical walls
                if (direction == 1)
                {
                    field[wall11x + i][wall11y] = '#';
                }

                //creating horizontall walls
                else if (direction == 0)
                {
                    field[wall11x][wall11y + i] = '#';
                }
            }
        }

    }

    //function that create our main field 
    static void FieldCreating(char[][] field)
    {
        // Initializing each inner array with 53 elements
        for (int i = 0; i < 23; i++)
        {
            field[i] = new char[53];
        }

        //creating external walls and empty field 
        for (int i = 0; i < 53; i++)
        {
            field[0][i] = '#';
            field[22][i] = '#';
        }
        for (int i = 1; i < 22; i++)
        {
            field[i][0] = '#';
            field[i][52] = '#';

            for (int j = 1; j < 52; j++)
            {
                field[i][j] = ' ';
                field[i][j] = ' ';
            }
        }
    }

    //function that print HI-Score part in menu
    static void HiScoreList()
    {
        Console.ResetColor();
        Console.Clear();

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            string[][] nickScores = new string[lines.Length][];
            

            for (int i = 0; i < lines.Length; i++)
            {
                nickScores[i] = lines[i].Split(' ');
            }
            // buble sort
            for (int i = 0; i < nickScores.Length - 1; i++)
            {
                for (int j = 0; j < nickScores.Length - i - 1; j++)
                {
                    if (int.Parse(nickScores[j][1]) < int.Parse(nickScores[j + 1][1]))
                    {
                        string tempScore = nickScores[j][1];
                        nickScores[j][1] = nickScores[j + 1][1];
                        nickScores[j + 1][1] = tempScore;

                        string tempName = nickScores[j][0];
                        nickScores[j][0] = nickScores[j + 1][0];
                        nickScores[j + 1][0] = tempName;
                    }
                }
            }

            int centerXX = Console.WindowWidth / 2 - 8;
            int centerYY = Console.WindowHeight / 2 - 6;


            if (nickScores.Length > 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.SetCursorPosition(centerXX, centerYY);
                    Console.WriteLine(i + 1 + ". " + nickScores[i][0] + "  " + nickScores[i][1]);
                    Console.WriteLine();
                    centerYY += 2;
                }
            }
            else
            {
                for (int i = 0; i < nickScores.Length; i++)
                {
                    Console.SetCursorPosition(centerXX, centerYY);
                    Console.WriteLine(i + 1 + ". " + nickScores[i][0] + "  " + nickScores[i][1]);
                    Console.WriteLine();
                    centerYY += 2;
                }
            }
            
            


            Console.SetCursorPosition(centerXX + 6, centerYY );
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" BACK ");

            ConsoleKeyInfo cki;
            while (true)

            {
                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    GameMenu();
                }
            }
        }
        else
        {
            ConsoleKeyInfo cki;
            while (true)

            {
                Console.WriteLine("Sorry, there seems to be some kind of error here, press Enter to return to the menu. ");

                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {

                    GameMenu();
                }
            }
           
        }
    }

    //function that print Menu
    static void GameMenu()
    {
        Console.ResetColor();
        
        
        string[] textt = {"  NEW GAME  ",
                          "  HI-SCORE  ",
                          "    QUIT    " };



        while (true)
        {

            int centerXX = Console.WindowWidth / 2 - 6;
            int centerYY = Console.WindowHeight / 2 - 2;



            if (button == 1)
            {
                Console.Clear();
                for (int i = 0; i < textt.Length; i++)
                {
                    if (i == 0)
                    {
                        Console.SetCursorPosition(centerXX, centerYY);
                        ConsoleColor prevCol = Console.BackgroundColor;
                        Console.BackgroundColor = ConsoleColor.White;
                        ConsoleColor prevColor = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(textt[i]);
                        Console.BackgroundColor = prevCol;
                        Console.ForegroundColor = prevColor;

                    }
                    else
                    {
                        Console.SetCursorPosition(centerXX, centerYY);
                        Console.WriteLine(textt[i]);

                    }
                    centerYY += 2;
                }

            }
            else if (button == 2)
            {
                Console.Clear();
                for (int i = 0; i < textt.Length; i++)
                {
                    if (i == 1)
                    {
                        Console.SetCursorPosition(centerXX, centerYY);
                        ConsoleColor prevCol = Console.BackgroundColor;
                        Console.BackgroundColor = ConsoleColor.White;
                        ConsoleColor prevColor = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(textt[i]);
                        Console.BackgroundColor = prevCol;
                        Console.ForegroundColor = prevColor;
                    }
                    else
                    {
                        Console.SetCursorPosition(centerXX, centerYY);
                        Console.WriteLine(textt[i]);

                    }
                    centerYY += 2;
                }

            }
            else if (button == 3)
            {
                Console.Clear();

                for (int i = 0; i < textt.Length; i++)
                {
                    if (i == 2)
                    {
                        Console.SetCursorPosition(centerXX, centerYY);
                        ConsoleColor prevCol = Console.BackgroundColor;
                        Console.BackgroundColor = ConsoleColor.White;
                        ConsoleColor prevColor = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(textt[i]);
                        Console.BackgroundColor = prevCol;
                        Console.ForegroundColor = prevColor;

                    }
                    else
                    {
                        Console.SetCursorPosition(centerXX, centerYY);
                        Console.WriteLine(textt[i]);

                    }
                    centerYY += 2;
                }

            }
            ConsoleKeyInfo cki;

            cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.UpArrow && button > 1)
            {
                button--;
            }
            else if (cki.Key == ConsoleKey.DownArrow && button < 3)
            {
                button++;
            }
            else if (cki.Key == ConsoleKey.DownArrow && button == 3)
            {
                button = 1;
            }
            else if (cki.Key == ConsoleKey.UpArrow && button == 1)
            {
                button = 3;
            }

            
            //Navigation in menu
            if (cki.Key == ConsoleKey.Enter)
            {
                if (button == 1)
                {
                    restart = true;
                    Main();
                }
                else if (button == 2)
                {
                    HiScoreList();
                }
                else if (button == 3)
                {
                    Environment.Exit(0);
                }
            }

            Thread.Sleep(50);
        }



    }

    //function that save player's name and score in record txt file
    static void SaveRecord()
    {
        

        Console.Clear();
        Console.ResetColor();
        string name = "";

        string[] textt = {"╔╗╔╔═╗╔╦╗╔═╗  ╦═╗╔═╗╔═╗╦╔═╗╔╦╗╦═╗╔═╗╔╦╗╦╔═╗╔╗╔ ",
                          "║║║╠═╣║║║║╣   ╠╦╝║╣ ║ ╦║╚═╗ ║ ╠╦╝╠═╣ ║ ║║ ║║║║ ",
                          "╝╚╝╩ ╩╩ ╩╚═╝  ╩╚═╚═╝╚═╝╩╚═╝ ╩ ╩╚═╩ ╩ ╩ ╩╚═╝╝╚╝ "
        };
        int centerXX = Console.WindowWidth / 2 - textt[0].Length / 2;
        int centerYY = Console.WindowHeight / 6;

        for (int i = 0; i < 3; i++)
        {
            Console.SetCursorPosition(centerXX, centerYY);
            Console.Write(textt[i]);
            centerYY++;
        }

        centerXX = Console.WindowWidth / 2 - 8;
        Console.SetCursorPosition(centerXX - 2, centerYY + 3);
        Console.Write("YOUR SCORE: ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(score);
        Console.ResetColor();

        Console.SetCursorPosition(centerXX, centerYY + 6);
        Console.Write("NAME: ");

        //string name = Console.ReadLine();
        centerXX = Console.WindowWidth / 2 - 23;
        int counter = 0;
        string[] alph = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "." };

        int cursorX = centerXX;
        int cursorY = centerYY;
        ConsoleColor prevCol = Console.BackgroundColor;
        ConsoleColor prevColor = Console.ForegroundColor;
        int pozitionX = 0;
        int pozitionY = 0;
        while (true)
        {
            
            int letCount = 1;
           

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.ForegroundColor = prevColor;
                    Console.BackgroundColor = prevCol;

                    Console.SetCursorPosition(centerXX, centerYY + 8);
                    Console.WriteLine(" ____ ");

                    if (cursorX == centerXX && cursorY == centerYY && letCount == 1)
                    {                     
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        letCount++;
                    }
                    else
                    {
                        Console.ForegroundColor = prevColor;
                        Console.BackgroundColor = prevCol;
                    }
                    
                    Console.SetCursorPosition(centerXX, centerYY + 9);
                    Console.WriteLine($"||{alph[counter]} ||");
                    Console.SetCursorPosition(centerXX, centerYY + 10);
                    Console.WriteLine("||__||");
                    Console.SetCursorPosition(centerXX, centerYY + 11);
                    Console.WriteLine(@"|/__\|");
                    counter++;
                    centerXX += 5;
                    
                }
                centerYY += 4;
                centerXX = Console.WindowWidth / 2 - 23;
            }
            
            counter = 0;
            centerXX = Console.WindowWidth / 2 - 23;
            centerYY = Console.WindowHeight / 6 + 3;
            ConsoleKeyInfo cki;

            cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.UpArrow )
            {
                cursorY -= 4;
                pozitionY--;

                if (pozitionY == -1)
                {
                    pozitionY = 2;
                    cursorY += 12;
                }
            }
            else if (cki.Key == ConsoleKey.DownArrow )
            {
                cursorY += 4;
                pozitionY++;

                if (pozitionY == 3)
                {
                    pozitionY = 0;
                    cursorY -= 12;
                }
            }
            else if (cki.Key == ConsoleKey.RightArrow )
            {
                cursorX += 5;
                pozitionX++;

                if (pozitionX == 9)
                {
                    pozitionX = 0;
                    cursorX -= 45;
                }
            }
            else if (cki.Key == ConsoleKey.LeftArrow )
            {
                cursorX -= 5;
                pozitionX--;

                if (pozitionX == -1)
                {
                    pozitionX = 8;
                    cursorX += 45;
                }
            }
            
            if (cki.Key == ConsoleKey.Spacebar)
            {
                if (name.Length < 10 )
                {
                    if (pozitionY == 0) name += alph[pozitionX];

                    else if (pozitionY == 1) name += alph[pozitionX + 9];

                    else if (pozitionY == 2) name += alph[pozitionX + 18];

                }
                
            }
            Console.SetCursorPosition(centerXX + 21, centerYY + 6);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(name);

            
            //Navigation in menu
            if (cki.Key == ConsoleKey.Enter)
            {
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine(name + " " + score);
                }
                GameMenu();
            }

            Thread.Sleep(50);
        }
        //Console.ReadLine();
    }


    
    static void Main()
    {

        //SaveRecord();

        if (start)
        {
            start = false;
            StartAnimation();
            
        }
        

        while (restart)
        {

           

            Console.ResetColor();



            //SoundPlayer player = new SoundPlayer(soundPath);
            Random random = new Random();

            //Сreating our field
            char[][] field = new char[23][];
            FieldCreating(field);

            //Сreating interior walls
            int direction = 2;
            int wall11x = 0; int wall7x = 0; int wall3x = 0;
            int wall11y = 0; int wall7y = 0; int wall3y = 0;
            WallCreating(field, direction, wall11x, wall11y, wall7x, wall7y, wall3x, wall3y);

            // TIME
            int time = 0;
            int sleepCounter = 0;
            int counter15s = 0;

            // LIFE
            int life = 1;

            //70 random numbers
            int numx; int numy;
            for (int i = 0; i < 70; i++)
            {
                do
                {
                    numy = random.Next(1, 22); numx = random.Next(1, 52);
                } while (!(field[numy][numx] == ' '));

                field[numy][numx] = Convert.ToChar(random.Next(48, 58));

            }


            // position of player
            int cursorx = 1, cursory = 3;
            // taking random coordinates X and Y for player
            do
            {
                cursorx = random.Next(1, 52);
                cursory = random.Next(3, 22);

            } while (field[cursory - 2][cursorx] != ' ');
            field[cursory - 2][cursorx] = 'X';

            // required for readkey
            ConsoleKeyInfo cki;

            // Main Game loop
            while (life > 0)
            {


                //All code for player movement 
                while (Console.KeyAvailable)
                {   // true: there is a key in keyboard buffer
                    cki = Console.ReadKey(true);

                    // PLAYER MOVE RIGHT
                    if (cki.Key == ConsoleKey.RightArrow && cursorx < 51)
                    {   // key and boundary control
                        if (field[cursory - 2][cursorx + 1] == ' ')
                        {
                            field[cursory - 2][cursorx + 1] = 'X';
                            field[cursory - 2][cursorx] = ' ';
                            //Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            //Console.WriteLine(" ");
                            cursorx++;
                        }
                        //////////////////////////////////////////////////////////////////////////////////////////////////field[cursory - 2][cursorx + 1]<48
                        else if ((field[cursory - 2][cursorx + 1] == '0' || field[cursory - 2][cursorx + 1] == '1' ||
                                  field[cursory - 2][cursorx + 1] == '2' || field[cursory - 2][cursorx + 1] == '3' ||
                                  field[cursory - 2][cursorx + 1] == '4' || field[cursory - 2][cursorx + 1] == '5' ||
                                  field[cursory - 2][cursorx + 1] == '6' || field[cursory - 2][cursorx + 1] == '7' ||
                                  field[cursory - 2][cursorx + 1] == '8' || field[cursory - 2][cursorx + 1] == '9')
                                  && numMove(field, 1, cursorx, cursory))
                        {
                            field[cursory - 2][cursorx + 1] = 'X';
                            field[cursory - 2][cursorx] = ' ';
                            //Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            //Console.WriteLine(" ");
                            cursorx++;
                        }

                    }
                    // PLAYER MOVE LEFT
                    if (cki.Key == ConsoleKey.LeftArrow && cursorx > 1)
                    {
                        if (field[cursory - 2][cursorx - 1] == ' ')
                        {
                            field[cursory - 2][cursorx - 1] = 'X';
                            field[cursory - 2][cursorx] = ' ';
                            //Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            //Console.WriteLine(" ");
                            cursorx--;
                        }
                        else if ((field[cursory - 2][cursorx - 1] == '0' || field[cursory - 2][cursorx - 1] == '1' ||
                                  field[cursory - 2][cursorx - 1] == '2' || field[cursory - 2][cursorx - 1] == '3' ||
                                  field[cursory - 2][cursorx - 1] == '4' || field[cursory - 2][cursorx - 1] == '5' ||
                                  field[cursory - 2][cursorx - 1] == '6' || field[cursory - 2][cursorx - 1] == '7' ||
                                  field[cursory - 2][cursorx - 1] == '8' || field[cursory - 2][cursorx - 1] == '9')
                                  && numMove(field, 3, cursorx, cursory))
                        {
                            field[cursory - 2][cursorx - 1] = 'X';
                            field[cursory - 2][cursorx] = ' ';
                            //Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            //Console.WriteLine(" ");
                            cursorx--;
                        }
                    }
                    // PLAYER MOVE UP
                    if (cki.Key == ConsoleKey.UpArrow && cursory > 3)
                    {
                        if (field[cursory - 3][cursorx] == ' ')
                        {
                            field[cursory - 3][cursorx] = 'X';
                            field[cursory - 2][cursorx] = ' ';
                            //Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            //Console.WriteLine(" ");
                            cursory--;
                        }
                        else if ((field[cursory - 3][cursorx] == '0' || field[cursory - 3][cursorx] == '1' ||
                                  field[cursory - 3][cursorx] == '2' || field[cursory - 3][cursorx] == '3' ||
                                  field[cursory - 3][cursorx] == '4' || field[cursory - 3][cursorx] == '5' ||
                                  field[cursory - 3][cursorx] == '6' || field[cursory - 3][cursorx] == '7' ||
                                  field[cursory - 3][cursorx] == '8' || field[cursory - 3][cursorx] == '9')
                                  && numMove(field, 0, cursorx, cursory))
                        {
                            field[cursory - 3][cursorx] = 'X';
                            field[cursory - 2][cursorx] = ' ';
                            //Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            //Console.WriteLine(" ");
                            cursory--;
                        }
                    }
                    // PLAYER MOVE DOWN
                    if (cki.Key == ConsoleKey.DownArrow && cursory < 23)
                    {
                        // if place is empty 
                        if (field[cursory - 1][cursorx] == ' ')
                        {
                            field[cursory - 1][cursorx] = 'X';
                            field[cursory - 2][cursorx] = ' ';
                            //Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            //Console.WriteLine(" ");
                            cursory++;
                        }
                        // if there is a count 
                        else if ((field[cursory - 1][cursorx] == '0' || field[cursory - 1][cursorx] == '1' ||
                                  field[cursory - 1][cursorx] == '2' || field[cursory - 1][cursorx] == '3' ||
                                  field[cursory - 1][cursorx] == '4' || field[cursory - 1][cursorx] == '5' ||
                                  field[cursory - 1][cursorx] == '6' || field[cursory - 1][cursorx] == '7' ||
                                  field[cursory - 1][cursorx] == '8' || field[cursory - 1][cursorx] == '9')
                                  && numMove(field, 2, cursorx, cursory))
                        {
                            field[cursory - 1][cursorx] = 'X';
                            field[cursory - 2][cursorx] = ' ';
                            //Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            //Console.WriteLine(" ");
                            cursory++;
                        }
                    }
                }



                // clear Console for rewriting our game screen
                Console.Clear();

                int centerXX = Console.WindowWidth / 2 - 50 / 2;
                int centerYY = Console.WindowHeight / 2 - 25 / 2;
                Console.SetCursorPosition(centerXX, centerYY - 2);

                //Printing Time and Lifes
                ConsoleColor prColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($" Time  :   {time}");
                Console.WriteLine($"      Life  :    {life}       Score :    {score}");
                Console.WriteLine();
                Console.ForegroundColor = prColor;

                //Printing whole game field 
                for (int i = 0; i < field.Length; i++)
                {
                    for (int j = 0; j < field[i].Length; j++)
                    {
                        Console.SetCursorPosition(centerXX + j, centerYY);
                        if (field[i][j] == '0')
                        {
                            ConsoleColor prevColor = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(field[i][j]);
                            Console.ForegroundColor = prevColor;
                        }
                        else if (field[i][j] == '#')
                        {
                            ConsoleColor prevColor = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(field[i][j]);
                            Console.ForegroundColor = prevColor;
                        }
                        else if (field[i][j] == 'X')
                        {
                            ConsoleColor prevColor = Console.ForegroundColor;
                            ConsoleColor prevCol = Console.BackgroundColor;
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(field[i][j]);
                            Console.BackgroundColor = prevCol;
                            Console.ForegroundColor = prevColor;
                        }
                        else if (field[i][j] == '1' || field[i][j] == '2' || field[i][j] == '3' || field[i][j] == '4')
                        {
                            ConsoleColor prevColor = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(field[i][j]);
                            Console.ForegroundColor = prevColor;
                        }
                        else if (field[i][j] == '5' || field[i][j] == '6' || field[i][j] == '7' || field[i][j] == '8' || field[i][j] == '9')
                        {
                            ConsoleColor prevColor = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(field[i][j]);
                            Console.ForegroundColor = prevColor;
                        }

                        else
                            Console.Write(field[i][j]);

                    }
                    centerYY++;
                    Console.WriteLine();
                }





                // TIMERS
                sleepCounter++;
                //Console.WriteLine($"50ms has passed {sleepCounter} times");

                //increase timer every second 
                if (sleepCounter % 20 == 0)
                {
                    time++;
                }


                // decreasing all numbers by one every 15 seconds
                int tempValue;

                if (sleepCounter % 300 == 0)
                {
                    counter15s++;
                    for (int i = 1; i < 22; i++)
                    {
                        for (int j = 1; j < 52; j++)
                        {
                            //if it count from 2 to 9 decrease by 1 
                            if (field[i][j] == '2' || field[i][j] == '3' || field[i][j] == '4' ||
                                field[i][j] == '5' || field[i][j] == '6' || field[i][j] == '7' ||
                                field[i][j] == '8' || field[i][j] == '9')
                            {
                                tempValue = Convert.ToInt16(field[i][j]);
                                tempValue--;
                                field[i][j] = Convert.ToChar(tempValue);
                            }
                            //if it is 1 change to 0 with 3% probality
                            else if (field[i][j] == '1')
                            {
                                int three = random.Next(1, 101);
                                if (three == 1 || three == 2 || three == 3)
                                {
                                    tempValue = Convert.ToInt16(field[i][j]);
                                    tempValue--;
                                    field[i][j] = Convert.ToChar(tempValue);
                                }
                            }
                        }
                    }
                }

                // ZERO MOVES
                int dir = 0;
                bool switcher = true;
                if (sleepCounter % 20 == 0)
                {
                    // loops for finding 0s
                    for (int i = 1; i < 22; i++)
                    {
                        for (int j = 1; j < 52; j++)
                        {
                            if (field[i][j] == '0')
                            {
                                //loop for taking random direction for 0 move until this move is aviliable 
                                do
                                {
                                    // 0 = UP | 1 = RIGHT | 2 = DOWN | 3 = LEFT
                                    dir = random.Next(0, 4);

                                    //MOVE UP
                                    if (dir == 0 && (field[i - 1][j] == ' ' || field[i - 1][j] == 'X'))
                                    {
                                        if (field[i - 1][j] == 'X')
                                        {
                                            switcher = false;
                                            life--;

                                        }
                                        else
                                        {
                                            field[i - 1][j] = 'A';
                                            field[i][j] = ' ';
                                            switcher = false;
                                        }

                                    }
                                    //MOVE RIGHT
                                    else if (dir == 1 && (field[i][j + 1] == ' ' || field[i][j + 1] == 'X'))
                                    {
                                        if (field[i][j + 1] == 'X')
                                        {
                                            switcher = false;
                                            life--;

                                        }
                                        else
                                        {
                                            field[i][j + 1] = 'A';
                                            field[i][j] = ' ';
                                            switcher = false;
                                        }

                                    }
                                    //MOVE DOWN
                                    else if (dir == 2 && (field[i + 1][j] == ' ' || field[i + 1][j] == 'X'))
                                    {
                                        if (field[i + 1][j] == 'X')
                                        {
                                            switcher = false;
                                            life--;

                                        }
                                        else
                                        {
                                            field[i + 1][j] = 'A';
                                            field[i][j] = ' ';
                                            switcher = false;
                                        }
                                    }
                                    //MOVE LEFT
                                    else if (dir == 3 && (field[i][j - 1] == ' ' || field[i][j - 1] == 'X'))
                                    {
                                        if (field[i][j - 1] == 'X')
                                        {
                                            switcher = false;
                                            life--;

                                        }
                                        else
                                        {
                                            field[i][j - 1] = 'A';
                                            field[i][j] = ' ';
                                            switcher = false;
                                        }
                                    }
                                    //stand still because movement is impossible
                                    else if (field[i - 1][j] != ' ' && field[i][j + 1] != ' ' && field[i + 1][j] != ' ' && field[i][j - 1] != ' ')
                                    {
                                        switcher = false;
                                    }

                                } while (switcher);

                                switcher = true;

                            }


                        }
                    }
                    //in loop before, 0 changes to 'A' after moving, this loop for changing 'A' to 0 back
                    for (int i = 1; i < 22; i++)
                    {
                        for (int j = 1; j < 52; j++)
                        {
                            if (field[i][j] == 'A')
                            {
                                field[i][j] = '0';
                            }
                        }
                    }

                }

                //stopping code execution every 50ms (player max speed)
                Thread.Sleep(50);
            }


            EndAnimation();


        }
        
    }

}
