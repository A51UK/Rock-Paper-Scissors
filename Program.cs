using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Rock_Paper_Scissors
{

    public enum Move
    {
        Rock = 0,
        Paper = 1,
        Scissors = 2,
    }

    public enum Win
    {
        Player = 0,
        Computer = 1,
        Draw = 2,
    }

    public class PayerStatus
    {
        public PayerStatus()
        {
        }

        public int payerScore { get; set; } = 0;
        public Move move { get; set; } = Move.Paper;
    }

    public class Payers
    {
        public Payers() { }

        public PayerStatus Human { get; set; } = new PayerStatus();

        public PayerStatus Bot { get; set; } = new PayerStatus();
    }

    internal class Program
    {

        public static Random random = new Random();

        public static Payers gamePayers;



        static void Main(string[] args)
        {
          
            Win win;

            char keyenter = ' ';

            do
            {
                System.Console.WriteLine("Do you want to play? Y/N");
                keyenter = System.Console.ReadKey().KeyChar;
                System.Console.WriteLine();

            } while (keyenter != 'Y' && keyenter != 'N');

            gamePayers = new Payers();

            if (keyenter == 'Y')
            {
                do
                {

                  GameLoop();

                } while (!EndGame());
            }
        
        }

        static void GameLoop()
        {
            UserMove();
            ComputerMove();
            WinnerOutput(WhoWin(gamePayers));
        }


        static void WinnerOutput(Win win)
        {
            System.Console.WriteLine();
            System.Console.WriteLine($"The winner is {win}");

            if (win == Win.Player)
            {
                gamePayers.Human.payerScore += 1;
            }
            else if (win == Win.Computer)
            {
                gamePayers.Bot.payerScore += 1;
            }

            System.Console.WriteLine();
            System.Console.WriteLine($"Human Score: {gamePayers.Human.payerScore}");
            System.Console.WriteLine($"Bot Score: {gamePayers.Bot.payerScore}");
        }

        static void UserMove()
        {
            
            int move = 0;
            string? line = string.Empty;

            do
            {
                do
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine("Your Move: 0 = Rock, 1 = Paper, 2 =  Scissors");
                    line = System.Console.ReadLine();

                } while (int.TryParse(line, out move) == false);

            } while (move >= 3 || move < 0);

            gamePayers.Human.move = GetMove(move);

            System.Console.WriteLine();
            System.Console.WriteLine($"You have play {gamePayers.Human.move}");

        }

        static void ComputerMove()
        {
            int randomMove = random.Next(0, 3);
            gamePayers.Bot.move= GetMove(randomMove);

            System.Console.WriteLine();
            System.Console.WriteLine($"Bot have play {gamePayers.Bot.move}");
        }

        static bool EndGame()
        {

            char readkey = ' ';

            do
            {
                System.Console.WriteLine();
                System.Console.WriteLine("Do you want to play again: Y/N");

                readkey = System.Console.ReadKey().KeyChar;

                if (readkey == 'Y')
                {
                    return false;
                }
                else if (readkey == 'N')
                {
                    return true;
                }
                else
                {
                    return EndGame();
                }

            } while (readkey != 'Y' && readkey != 'N');
        }

        static Win WhoWin(Payers player) => player switch
        {
            { Human.move: Move.Paper, Bot.move: Move.Rock } => Win.Player,
            { Human.move: Move.Scissors, Bot.move: Move.Paper } => Win.Player,
            { Human.move: Move.Rock, Bot.move: Move.Scissors } => Win.Player,
            { Human.move: Move.Paper, Bot.move: Move.Scissors } => Win.Computer,
            { Human.move: Move.Scissors, Bot.move: Move.Rock } => Win.Computer,
            { Human.move: Move.Rock, Bot.move: Move.Paper } => Win.Computer,
            { Human.move: Move.Paper, Bot.move: Move.Paper } => Win.Draw,
            { Human.move: Move.Rock, Bot.move: Move.Rock } => Win.Draw,
            { Human.move: Move.Scissors, Bot.move: Move.Scissors } => Win.Draw,
            _ => throw new NotImplementedException()
        };

        static Move GetMove(int move) => move switch
        {
            0 => Move.Rock,
            1 => Move.Paper,
            2 => Move.Scissors,
            _ => throw new NotImplementedException()
        };

    }
}