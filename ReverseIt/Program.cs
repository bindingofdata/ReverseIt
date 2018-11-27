using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseIt
{
    internal static class Program
    {
        /*
         * This is a game where users will try to sort a list of numbers.
         * Users provide a number from 1 to 9.
         * The specified number of digits have their order reversed.
         * This is continued until the numbers are sorted in ascending order.
         */

        private static void Main ( string[] args )
        {
            bool again = true;
            string userResponse = "";

            while (again)
            {
                userResponse = MainMenu();

                switch ( userResponse )
                {
                    case "N":
                    case "n":
                    case "1":
                        NewGame();
                        break;
                    case "H":
                    case "h":
                    case "2":
                    case "?":
                        DisplayHelp();
                        break;
                    case "E":
                    case "e":
                    case "Q":
                    case "q":
                    case "3":
                        again = false;
                        Console.Clear();
                        Console.WriteLine( "Thanks for playing!" );
                        Console.WriteLine( "press any key to continue..." );
                        Console.ReadKey();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input. Please try again.");
                        Console.Write( "press any key to continue..." );
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static string MainMenu ()
        {
            Console.Clear();
            Console.WriteLine("---=== Welcome to Reverse It! ===---\n");
            Console.WriteLine( "*** Main Menu ***" );
            Console.WriteLine( "1. [N]ew Game " );
            Console.WriteLine( "2. [H]elp " );
            Console.WriteLine( "3. [E]xit " );
            Console.Write( "-> " );

            return Console.ReadLine();
        }

        #region Game
        private static void NewGame ()
        {
            int[] sortedDigits = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, };
            int[] scrambledDigits = ScrambleDigits( sortedDigits );
            int turnsTaken = 0;

            while ( !CheckSort(scrambledDigits, sortedDigits) )
            {
                scrambledDigits = PlayerTurn( scrambledDigits );
                turnsTaken++;
            }

            Console.Clear();
            Console.WriteLine( "---=== Reverse It! ===---\n" );
            Console.WriteLine( "Congratulations!" );
            Console.WriteLine( $"You sorted the array in {turnsTaken} turns!" );
            Console.WriteLine();
            Console.Write( "press any key to continue..." );

            Console.ReadLine();
        }

        private static int[] PlayerTurn ( int[] scrambledDigits )
        {
            int digitsToReplace = 0;
            int[] reversedDigits;
            int[] updatedOrder = new int[scrambledDigits.Length];

            do
            {
                Console.Clear();
                DrawState( scrambledDigits );
                Console.WriteLine();
                Console.WriteLine( "How many digits would you like reversed?" );
                Console.Write( "-> " );

            } while ( !int.TryParse( Console.ReadLine(), out digitsToReplace ) );

            reversedDigits = new int[digitsToReplace];

            int index = digitsToReplace - 1;
            for ( int i = 0; i < digitsToReplace; i++ )
            {
                reversedDigits[index] = scrambledDigits[i];
                index--;
            }

            for ( int i = 0; i < updatedOrder.Length; i++ )
            {
                if ( i < digitsToReplace )
                    updatedOrder[i] = reversedDigits[i];
                else
                    updatedOrder[i] = scrambledDigits[i];
            }

            return updatedOrder;
        }

        private static void DrawState ( int[] scrambledDigits )
        {
            Console.Clear();

            StringBuilder gameState = new StringBuilder();

            gameState.Append( "---=== Reverse It! ===---\n\n" );
            gameState.Append( "Current order: \n" );

            foreach ( int digit in scrambledDigits )
            {
                gameState.Append( digit ).Append( " " );
            }

            Console.WriteLine( gameState );
        }

        private static bool CheckSort ( int[] scrambledDigits, int[] sortedDigits )
        {
            bool sorted = true;

            for ( int i = 0; i < scrambledDigits.Length; i++ )
            {
                if (scrambledDigits[i] != sortedDigits[i])
                {
                    sorted = false;
                    break;
                }
            }

            return sorted;
        }

        private static int[] ScrambleDigits ( int[] sortedDigits )
        {
            List<int> unusedDigits = sortedDigits.ToList<int>();
            int[] scrambledDigits = new int[sortedDigits.Length];
            int digitIndex = 0;
            Random random = new Random();

            while ( unusedDigits.Count > 0 )
            {
                int nextDigit = random.Next( unusedDigits.Count );
                scrambledDigits[digitIndex] = unusedDigits[nextDigit];
                unusedDigits.RemoveAt( nextDigit );
                digitIndex++;
            }

            return scrambledDigits;
        }
        #endregion

        private static void DisplayHelp ()
        {
            StringBuilder helpMessage = new StringBuilder();

            helpMessage.Append( "---=== Reverse It! ===---\n\n" );
            helpMessage.Append( "How To Play: \n" );
            helpMessage.Append( "Reverse It! is a puzzle game where you attempt to order digits in the fewest possible turns.\n" );
            helpMessage.Append( "You will be provided with a list of digits from 1 to 9 in a random order.\n" );
            helpMessage.Append( "Each turn, you can specify a number of digits to reverse the order of.\n\n" );
            helpMessage.Append( "For example, you may have the following numbers: \n\t5 2 8 3 1 4 9 5 6\n" );
            helpMessage.Append( "On your turn, you could choose to reverse 4 digits, giving you: \n\t3 8 2 5 1 4 9 5 6\n\n" );
            helpMessage.Append( "The game is over when you have ordered the digits from lowest to highest.\n" );
            helpMessage.Append( "press any key to continue..." );

            Console.Clear();
            Console.Write( helpMessage );
            Console.ReadKey();
        }
    }
}
