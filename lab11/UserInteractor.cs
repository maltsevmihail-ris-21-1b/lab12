using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

public class UserInteractor
{
    public static int GetFromUser(string? msg = "", bool isMinus = false)
    {
        Regex regPuls = new Regex("^[\\d]+$");
        Regex regMinus = new Regex("^-[\\d]+$");
        int toReturn = -1;
        string? buf;
        bool isFinished = false;
        Console.WriteLine(msg);
        do
        {
            buf = Console.ReadLine();
            if (isMinus)
            {
                isFinished = regMinus.IsMatch(buf);
            }
            else
            {
                isFinished = regPuls.IsMatch(buf);
            }
            if (!isFinished)
            {
                Console.WriteLine("Try again");
            }
        } while (!isFinished);
        toReturn = int.Parse(buf);
        return toReturn; ;
    }
}


    