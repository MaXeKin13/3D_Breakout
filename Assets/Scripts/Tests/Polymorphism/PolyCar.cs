using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyCar
{
    public int HP;
    public string Color;

    public PolyCar(int _hp, string _color)
    {
        HP = _hp;
        Color = _color;
    }

    public void ShowDetails()
    {
        Console.WriteLine(("HP: "+ HP + " color:" + Color));
    }

    public virtual void Repair()
    {
        Console.WriteLine("Car was repaired");
    }
}
