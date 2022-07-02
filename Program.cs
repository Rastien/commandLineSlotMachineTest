
//Row One Slots
var rowOneSlotOne   = ("", 0.0);
var rowOneSlotTwo   = ("", 0.0);
var rowOneSlotThree = ("", 0.0); 

//Row Two Slots
var rowTwoSlotOne    = ("", 0.0);
var rowTwoSlotTwo    = ("", 0.0);
var rowTwoSlotThree  = ("", 0.0);

//Row Three Slots
var rowThreeSlotOne   = ("", 0.0);
var rowThreeSlotTwo   = ("", 0.0);
var rowThreeSlotThree = ("", 0.0);

//Row Four Slots
var rowFourSlotOne   = ("", 0.0);
var rowFourSlotTwo   = ("", 0.0);
var rowFourSlotThree = ("", 0.0);

//Slot Icons as Tuples
var apple = ("A", 0.4);
var banana = ("B", 0.6);
var pineapple = ("P", 0.8);
var wildCard = ("*", 0.0);

//Random Generator
Random rnd = new Random();

//initialised values
double balance =0;
double stake =0;
double winTotal = 0;
bool displayGameHeader = false;

var gameHeader = new[]
           {
                    @"    /$$$$$$$$ /$$$$$$$$        /$$$$$$                      /$$        ",
                    @"   | $$_____/|_____ $$        /$$__  $$                    | $$        ",
                    @"   | $$           /$$/       | $$  \__/  /$$$$$$   /$$$$$$$| $$$$$$$   ",
                    @"   | $$$$$       /$$/        | $$       |____  $$ /$$_____/| $$__  $$  ",
                    @"   | $$__/      /$$/         | $$        /$$$$$$$|  $$$$$$ | $$  \ $$  ",
                    @"   | $$        /$$/          | $$    $$ /$$__  $$ \____  $$| $$  | $$  ",
                    @"   | $$$$$$$$ /$$$$$$$$      |  $$$$$$/|  $$$$$$$ /$$$$$$$/| $$  | $$  ",
                    @"   |________/|________/       \______/  \_______/|_______/ |__/  |__/  ",
                    @"                                                                       ",
                    @"                                                                       ",
                    @"                                                                       ",
            };

gameSetup();
mainGame();

void gameSetup()
{
    if(displayGameHeader is false)
    {
        Console.WriteLine("\n\n");

        foreach (string line in gameHeader)
        {
            Console.WriteLine(line);
        }
        displayGameHeader = true;
    }
        try
        {
            Console.Write("Enter your deposit: ");
            balance = Convert.ToDouble(Console.ReadLine());
            balance = Math.Round(balance, 2);
        }

        catch(FormatException)
        {
        Console.WriteLine();
        Console.WriteLine("!!!Please enter a numeric value for deposit!!!");
            gameSetup();
        }

        catch(Exception)
        {
        Console.WriteLine("!!!Unexpected input occured please try again!!!");
        gameSetup();
        }
        
        Console.WriteLine();
}


//Main game loop
void mainGame()
{
    while (balance > 0)
    {
        winTotal = 0;
        Console.WriteLine($"Your current balance is: {balance}\n");
        enterStake();
        spinReels();

        balance = balance
            - stake
            + winTotal;
    }

    Console.WriteLine("You balance is 0, thank you for playing. Goodbye.\n Press any key to close...");
    Console.ReadLine();
}

void enterStake()
{
    var balanceCheck = false;

    while(balanceCheck is false)
    {
        try
        {
            Console.Write("Please enter your stake amount: ");
            stake = Convert.ToDouble(Console.ReadLine());
            stake = Math.Round(stake, 2);
            if (stake > balance)
            {
                Console.WriteLine("stake cannot be higher than balance.\n");
                balanceCheck = false;
            }
            else
            {
                balanceCheck = true;
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter a numeric value for stake.\n");
            enterStake();
        }
        catch (Exception)
        {
            Console.WriteLine("Unexpected input occured please try again\n");
            enterStake();
        }
    }

    

    Console.WriteLine();


}

void spinReels()
{
    rowOneSlotOne   = getSlotResult();
    rowOneSlotTwo   = getSlotResult();
    rowOneSlotThree = getSlotResult();
   
    rowTwoSlotOne   = getSlotResult();
    rowTwoSlotTwo   = getSlotResult();
    rowTwoSlotThree = getSlotResult();
   
    rowThreeSlotOne   = getSlotResult();
    rowThreeSlotTwo   = getSlotResult();
    rowThreeSlotThree = getSlotResult();

    rowFourSlotOne    = getSlotResult();
    rowFourSlotTwo    = getSlotResult();
    rowFourSlotThree  = getSlotResult();


    Console.WriteLine($"{rowOneSlotOne.Item1} {rowOneSlotTwo.Item1} {rowOneSlotThree.Item1}");
    Console.WriteLine($"{rowTwoSlotOne.Item1} {rowTwoSlotTwo.Item1} {rowTwoSlotThree.Item1}");
    Console.WriteLine($"{rowThreeSlotOne.Item1} {rowThreeSlotTwo.Item1} {rowThreeSlotThree.Item1}");
    Console.WriteLine($"{rowFourSlotOne.Item1} {rowFourSlotTwo.Item1} {rowFourSlotThree.Item1}");

    var rowOneCoefficient = winChecker(rowOneSlotOne.Item2, rowOneSlotTwo.Item2, rowOneSlotThree.Item2);
    var rowTwoCoefficient = winChecker(rowTwoSlotOne.Item2, rowTwoSlotTwo.Item2, rowTwoSlotThree.Item2);
    var rowThreeCoefficient = winChecker(rowThreeSlotOne.Item2, rowThreeSlotTwo.Item2, rowThreeSlotThree.Item2);
    var rowFourCoefficient = winChecker(rowFourSlotOne.Item2, rowFourSlotTwo.Item2, rowFourSlotThree.Item2);

    Console.WriteLine();

    if (rowOneCoefficient > 0 )  { Console.WriteLine("!!Row one has a winning combo!!\n" ); }
    if(rowTwoCoefficient > 0 )   { Console.WriteLine("!!Row two has a winning combo!!\n" ); }
    if(rowThreeCoefficient > 0 ) { Console.WriteLine("!!Row three has a winning combo!!\n" ); }
    if(rowFourCoefficient > 0 )  { Console.WriteLine("!!Row four has a winning combo!!\n"); }

    if (rowOneCoefficient <= 0 && rowTwoCoefficient <= 0 && rowThreeCoefficient <= 0 && rowFourCoefficient <= 0) { Console.WriteLine(":( No winning rows this time :(\n"); }

    var totalCoefficient = Math.Round(rowOneCoefficient + rowTwoCoefficient + rowThreeCoefficient + rowFourCoefficient, 4, MidpointRounding.AwayFromZero);

    Console.WriteLine(totalCoefficient.ToString() );

    winTotal = Math.Round(stake * totalCoefficient, 2);

    Console.WriteLine();
}


ValueTuple<string, double> getSlotResult()
{
    int result = rnd.Next(1, 101);

    if (result >= 56) return apple;

    else if (result < 56 && result >= 21) return banana;

    else if (result < 21 && result >= 6) return pineapple;

    else return wildCard;
}

double winChecker(double slotOne, double slotTwo, double slotThree)
{
    if((slotOne == apple.Item2 || slotOne == wildCard.Item2) && (slotTwo == apple.Item2 || slotTwo == wildCard.Item2) && (slotThree == apple.Item2 || slotThree == wildCard.Item2))
    {
        return Math.Round(slotOne+slotTwo+slotThree, 4, MidpointRounding.AwayFromZero);
    }

    if ((slotOne == banana.Item2 || slotOne == wildCard.Item2) && (slotTwo == banana.Item2 || slotTwo == wildCard.Item2) && (slotThree == banana.Item2 || slotThree == wildCard.Item2))
    {
        return Math.Round(slotOne + slotTwo + slotThree, 4, MidpointRounding.AwayFromZero);
    }

    if ((slotOne == pineapple.Item2 || slotOne == wildCard.Item2) && (slotTwo == pineapple.Item2 || slotTwo == wildCard.Item2) && (slotThree == pineapple.Item2 || slotThree == wildCard.Item2))
    {
        return Math.Round(slotOne + slotTwo + slotThree, 4, MidpointRounding.AwayFromZero);
    }

    if ((slotOne == wildCard.Item2) && (slotTwo == wildCard.Item2) && (slotThree == wildCard.Item2))
    {
        return Math.Round(slotOne + slotTwo + slotThree, 4, MidpointRounding.AwayFromZero);
    }

    return 0;
}
