using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Data.Common;
using System.Drawing;

public class Day3
{
    static void Main()
    {
        
        int row = 0;
        Schematic schematic = new Schematic();
        try
        {
            using (StreamReader reader = new StreamReader("C:\\Users\\matluu\\Documents\\AdventOfCode\\Day3\\Day3\\input.txt"))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    try { schematic.plotSymbols(line, row); } catch { Console.WriteLine("Error in plotSymbols."); }
                    try { schematic.plotParts(line, row); } catch { Console.WriteLine("Error in plotParts at line: " + row); }
                    
                    row++;
                }
            }
            schematic.plotAdjacentPoints();
            //schematic.adjacentPointReport();
            //schematic.partNumberReport();
            schematic.sumParts();
            Console.WriteLine("Sum of Part Numbers: " + schematic.partSum);
        }
        catch 
        {
            Console.WriteLine("Error opening file.");
        }
    }
}

public class Point
{
    public int row { get; set;}
    public int col { get; set;}
    public Point(int x, int y)
    {
        row = x;
        col = y;
    }
}

public class Schematic
{ 
    public int partSum { get; set;}
    public HashSet<Tuple<int, int>> adjacentPoints { get; set;}
    public HashSet<Symbol> Symbols { get; set; }
    public HashSet<partNumber> partNumbers { get; set; }
    public Point testPoint { get; set;}
    public Schematic()
    {
        adjacentPoints = new HashSet<Tuple<int, int>>();
        Symbols = new HashSet<Symbol>();
        partNumbers = new HashSet<partNumber>();
        partSum = 0;
        testPoint = new Point(0, 26);
    }
    public void plotSymbols(string line, int row)
    {
        for (int column = 0; column < line.Length; column++)
        {
            if (line[column] != '.' && !char.IsDigit(line[column]))
            {
                Symbol newSymbol = new Symbol(line[column], row, column);
                Symbols.Add(newSymbol);
            }
        }
    }

    public void plotParts(string line, int row)
    {
        int column = 0;
        while (column < line.Length)
        {
            if (!(char.IsDigit(line[column])))
            {
                column++;
                continue;
            }
            else
            { 
                int startIndex = column;
                int endIndex = startIndex;

                while (endIndex < line.Length && char.IsDigit(line[endIndex]))
                {
                    endIndex++;
                }
                string value = line.Substring(startIndex, endIndex - startIndex);
                partNumber newPartNumber = new partNumber(int.Parse(value));
                newPartNumber.plotPoints(row, startIndex, endIndex);
                partNumbers.Add(newPartNumber);
                column = endIndex;

            }
        }
    }

    public void plotAdjacentPoints()
    { 
        foreach (Symbol symbol in Symbols) 
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    Tuple<int, int> tuple = new Tuple<int, int>(symbol.location.row + i, symbol.location.col + j);
                    adjacentPoints.Add(tuple);
                }
            }
        }
    }
    public void sumParts()
    {
        Console.WriteLine("Summing Parts");
        foreach (var part in partNumbers)
        {
            foreach (var point in part.points)
            {
                Tuple<int, int> tuple = new Tuple<int, int>(point.row, point.col);
                if (adjacentPoints.Contains(tuple) && !part.isAdded)
                {
                    partSum += part.value;
                    part.isAdded = true;
                }
            }
        }
    }

    public void adjacentPointReport()
    {
        foreach (var point in adjacentPoints)
        {
            Console.WriteLine("{0}, {1}", point.Item1, point.Item2);
        }
    }
    public void partNumberReport()
    {
        foreach (var part in partNumbers)
        {
            Console.WriteLine("Part: " + part.value);
            foreach (var point in part.points)
            {
                Console.WriteLine("{0}, {1}", point.row, point.col);
            }
        }
    }
}
public class partNumber
{
    public int value { get; set;}
    public HashSet<Point> points { get; set;}
    public bool isAdded { get; set;}
    public partNumber(int Value)
    {
        value = Value;
        isAdded = false;
        points = new HashSet<Point>();
    }
    public void plotPoints(int row, int start, int end)
    {
        for (int bias = 0; (start + bias) < end; bias++)
        {
            Point newPoint = new Point(row, start + bias);
            points.Add(newPoint);
        }
        
    }
}

public class Symbol
{ 
    public char symbolValue { get; set;}
    public Point location { get; set;}

    public Symbol(char SymbolValue, int Row, int Column)
    {
        symbolValue = SymbolValue;
        location = new Point(Row, Column);
    }
}