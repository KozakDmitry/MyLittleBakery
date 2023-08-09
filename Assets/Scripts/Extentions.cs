using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extentions 
{
    public static int Pow(this int bas, int exp)
    {
        return Enumerable
              .Repeat(bas, exp)
              .Aggregate(1, (a, b) => a * b);
    }

}
