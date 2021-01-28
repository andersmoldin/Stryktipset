using System;
using System.Linq;
using StryktipsetCore;

//for (int i = 1; i <= 13; i++)
//{
//    var random = new Random().Next(3);

//    Console.WriteLine($"{i, 2}: {(random == 0 ? "X" : random)}");
//}

var omgång1 = Read.GetOmgångs().First();

Console.WriteLine($"{omgång1.Id}, {omgång1.Vecka}, {omgång1.RättRad}, {omgång1.Omsättning}, {omgång1.Utdelning13}{null}{null}");
Console.ReadLine();