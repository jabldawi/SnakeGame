using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading;


class Program

{

    static void Main()

    {
        Console.WindowHeight = 18;

        Console.WindowWidth = 32;

        Console.CursorVisible = false; //Ukrycie znaku zachęty

        ConsoleKey info = ConsoleKey.RightArrow;

        int screenwidth = Console.WindowWidth;

        int screenheight = Console.WindowHeight - 2; //-2 żeby okno się nie przewijało

        int score = 0, prevX, prevY;

        bool loop = true;

        List<Pixel> teljePositie = new List<Pixel>();

        Pixel head = new Pixel();
        teljePositie.Add(head);

        Obstacle food = new Obstacle(screenwidth, screenheight);

        //Rysowanie okna gry:
        for (int i = 0; i < screenwidth; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.Write("■");
            Console.SetCursorPosition(i, screenheight - 1);
            Console.Write("■");
        }


        for (int i = 0; i < screenheight; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("■");
            Console.SetCursorPosition(screenwidth - 1, i);
            Console.Write("■");
        }


        while (loop)

        {
            ////Poprzednia pozycja głowy
            prevX = teljePositie[^1].xPos;
            prevY = teljePositie[^1].yPos;

            //Rysuje Pokarm
            food.DrawPixel();


            //Rysuje Węza       
            foreach (Pixel item in teljePositie)
            {
                item.DrawPixel();
            }


            //Rysuje Punktacje
            Console.SetCursorPosition(0, screenheight - 1);

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("\nScore: " + score);

            Console.Write("H");


            //Ruch automatyczny po wciśnięciu klawisza  
            Thread.Sleep(500);

            if (Console.KeyAvailable)
                info = Console.ReadKey().Key;

            //Ruch ogona
            for (int i = teljePositie.Count - 1; i > 0; i--)
            {
                teljePositie[i].xPos = teljePositie[i - 1].xPos;
                teljePositie[i].yPos = teljePositie[i - 1].yPos;
            }

            //Ruch głowy
            switch (info)
            {
                case ConsoleKey.UpArrow:

                    head.yPos--;
                    break;

                case ConsoleKey.DownArrow:

                    head.yPos++;
                    break;

                case ConsoleKey.LeftArrow:

                    head.xPos--;
                    break;

                case ConsoleKey.RightArrow:

                    head.xPos++;
                    break;
            }


            //Kolizja z pokarmem
            if (head.xPos == food.xPos && head.yPos == food.yPos)
            {
                score++;
                food = new Obstacle(screenwidth, screenheight);
                teljePositie.Add(new Pixel(prevX, prevY));
            }

            //Kolizja z ramką 
            if (head.xPos == 0 || head.xPos == screenwidth - 1 || head.yPos == 0 || head.yPos == screenheight - 1)
            {
                loop = false;
            }


            //Czyszczenie okna:  
            for (int i = 1; i < screenheight - 1; i++)
            {
                for (int j = 1; j < screenwidth - 1; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write(" ");
                }
            }
        }

        //Podsumowanie i zakończenie:
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Red;

        Console.SetCursorPosition(screenwidth / 5, screenheight / 2);

        Console.WriteLine("Game Over");

        Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);

        Console.WriteLine("Dein Score ist: " + score);

        Environment.Exit(0);

    }

}


public class Pixel

{

    public int xPos { get; set; }

    public int yPos { get; set; }

    public ConsoleColor schermKleur { get; set; }

    public string karacter { get; set; }

    public Pixel()
    {
        xPos = Console.WindowWidth / 2;
        yPos = (Console.WindowHeight - 2) / 2;
        schermKleur = ConsoleColor.Yellow;
        karacter = "@";
    }

    public Pixel(int x, int y)
    {
        xPos = x;
        yPos = y;
        schermKleur = ConsoleColor.White;
        karacter = "■";
    }

    public void DrawPixel()
    {
        Console.ForegroundColor = schermKleur;
        Console.SetCursorPosition(xPos, yPos);
        Console.Write(karacter);
    }

}

public class Obstacle : Pixel
{
    public Random random = new Random();

    public Obstacle(int x, int y)
    {
        xPos = random.Next(1, x - 1);
        yPos = random.Next(1, y - 1);
        karacter = "*";
        schermKleur = ConsoleColor.Cyan;
    }
}
