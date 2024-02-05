using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyAudi:PolyCar
{
    private string brand = "BMW";
    
    public string Model { get; set; }

    public PolyAudi(int hp, string color, string model):base(hp, color)
    {
        
        this.Model = model;
    }

    public void ShowDetails()
    {
        Console.WriteLine(("Brand: " + brand + " HP: "+ HP + " color:" + Color));
    }

    public override void Repair()
    {
        Console.WriteLine("The BMW {0} was repaired", Model);
    }
}
