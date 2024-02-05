using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolymorphismC
{

    static void Main(string[] args)
    {
        var cars = new List<PolyCar>
        {
            //can create lists of deriving classes instead of bas class
            new PolyAudi(200, "blue", "A4"),
            new PolyBMW(250, "red", "M3")
        };

        foreach (var car in cars)
        {
            //this calls the BASE class
            car.Repair();
        }

        Console.ReadKey();
    }
    
}
