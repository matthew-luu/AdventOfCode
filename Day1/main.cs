//We need to find the first and last digit in a line
//If there is one digit, it is the first and last
//If there are no digits, it is 0

using System;
using System.IO;
class HelloWorld {
  static void Main() {
    int sum = 0;
    try
    {
        //Open file
        using (StreamReader reader = new StreamReader("input.txt"))
        {
            string line;
            //Read line
            while ((line = reader.ReadLine()) != null)
                {
                    int forward;
                    int backward;
                    char? first = null;
                    char? last = null;
                    //Check to see if digit
                    for(forward = 0; forward < line.Length; forward++)
                    {
                        //If digit set first to digit
                        if (char.IsDigit(line[forward]))
                        {
                            first = line[forward];
                            break;
                        }
                    }
                    if (first == null)  break;
                    for (backward = line.Length - 1; backward > forward; backward--)
                    {
                        if (char.IsDigit(line[backward]))
                        {
                            last = line[backward];
                            break;
                        }
                    }
                    if (last == null)   last = first;
                    string key = first.ToString() + last.ToString();
                    sum += int.Parse(key);
                    Console.WriteLine(sum);
                }
                Console.WriteLine("Final total: " + sum);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occured");
    }
  }
}