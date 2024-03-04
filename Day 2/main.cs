using System;
using System.IO;

class Day2
{
    static void Main()
    {
        int idSum = 0;
        int powerSum = 0;
        try
        {
            //Read the file
            using (StreamReader reader = new StreamReader("input.txt"))
            {
                string line;
                //Read the line
                while ((line = reader.ReadLine()) != null)
                {
                    //Parse the line to get the game number
                    int colonIndex = line.IndexOf(':');
                    int gameNumber = int.Parse(line.Substring(5, colonIndex - 5).Trim());
                    
                    //Create a new game
                    Game currentGame = new Game(gameNumber);
                    
                    //Store the rest of the line
                    currentGame.Rounds = line.Substring(colonIndex + 1).Trim();
                    
                    //Process the line
                    currentGame.Play();
                    
                    //Add to powerSum
                    powerSum += currentGame.gamePower();
                    
                    //Add ID to idSum if possible
                    if (currentGame.isPossible())   idSum += gameNumber;
                }
                Console.WriteLine("Possible ID Sum: " + idSum);
                Console.WriteLine("Power Sum: " + powerSum);
            }
        }
        catch
        {
            Console.WriteLine("Unable to open file.");
        }
    }
}

public class Game 
{
    public int ID {get; set;}
    public int Red {get; set;}
    public int Green {get; set;}
    public int Blue {get; set;}
    public string Rounds {get; set;}
    
    //Constructor
    public Game()
    {
        ID = 0;
        Red = 0;
        Green = 0;
        Blue = 0;
    }
    
    //Constructor
    public Game(int id)
    {
        ID = id;
    }
    
    //Line processor
    public void Play()
    {
        while (!(Rounds == "" || Rounds == null))
        {
            int turnEnd = Rounds.IndexOf(";");
            if (turnEnd == -1)
            {
                //Final Round
                Round(Rounds);
                Rounds = "";
            }
            else
            {
                Round(Rounds.Substring(0, turnEnd).Trim());
                Rounds = Rounds.Substring(turnEnd + 1).Trim();
            }
        }
        return;
    }
    
    //Go through the round and get the cube entry
    public void Round(string round)
    {
        string cubes = round;
        while (!(cubes == "" || cubes == null))
        {
            int nextCube = cubes.IndexOf(",");
            if (nextCube == -1)
            {
                Update(cubes);
                return;
            }
            else
            {
                Update(cubes.Substring(0, nextCube).Trim());
                cubes = cubes.Substring(nextCube + 1).Trim();
            }
        }
    }
    
    //Process the cube color and amount
    public void Update(string cube)
    {
        
        string[] parts = cube.Split(' ');
        int value = int.Parse(parts[0]);
        string color = parts[1].Trim();
        
        if (color == "red")
        {
            Red = Math.Max(Red, value);
        }
        
        if (color == "green")
        {
            Green = Math.Max(Green, value);
        }
        
        if (color == "blue")
        {
            Blue = Math.Max(Blue, value);
        }
    }
    
    //Calculate the power of the set
    public int gamePower()
    {
        return Red * Green * Blue;
    }
    
    //Check if Game is possible
    public bool isPossible()
    {
        return (Red <= 12 && Green <= 13 && Blue <= 14);
    }
}
