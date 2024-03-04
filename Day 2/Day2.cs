using System;
using System.IO;

class Day2
{
    static void Main()
    {
        int sum = 0;
        try
        {
            using (StreamReader reader = new StreamReader("input.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //Parse the line and get the game #
                    int colonIndex = line.IndexOf(':');
                    int gameNumber = int.Parse(line.Substring(5, colonIndex - 5).Trim());
                    Game currentGame = new Game(gameNumber);
                    //Store the rest of the line for processing
                    currentGame.Rounds = line.Substring(colonIndex + 1).Trim();
                    ////Console.WriteLine("Game " + currentGame.ID + ": " + currentGame.Rounds);
                    currentGame.Play();
                    //Add ID to sum if possible
                    if (currentGame.isPossible())   sum += gameNumber;
                }
                Console.WriteLine("Final Sum: " + sum);
            }
        }
        catch (Exception ex)
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
    public Game()
    {
        ID = 0;
        Red = 0;
        Green = 0;
        Blue = 0;
    }
    
    public Game(int id)
    {
        ID = id;
    }
    
    public void Play()
    {
        while (!(Rounds == "" || Rounds == null))
        {
            ////Console.WriteLine("PLAY: " + Rounds);
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
    
    public void Round(string round)
    {
        string cubes = round;
        ////Console.WriteLine("ROUND: " + cubes);
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
    //Go through each round and add maxValues
    public void Update(string cube)
    {
        
        string[] parts = cube.Split(' ');
        try{
        ////Console.WriteLine("UPDATE: parts[0] = " + parts[0] + " parts[1] = " + parts[1]);
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
        catch (Exception update)
        {
            Console.WriteLine("Error in Update");
        }
    }
    
    //Check if isPossible
    public bool isPossible()
    {
        return (Red <= 12 && Green <= 13 && Blue <= 14);
    }
}
