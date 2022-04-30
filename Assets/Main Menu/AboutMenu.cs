using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class AboutTheGorillas : MonoBehaviour
{
     public string randGorillaFact()
     {
          System.Random rnd = new System.Random();

          int rndIndex = rnd.Next(0, 6);

          return GlobalGorillas.gorillaFacts[rndIndex];
     }
}


public static class GlobalGorillas
{
     public static string[] gorillaFacts = {"Gorilla population is around 100 to 200 thousand",
          "Gorillas share 98.3% of their DNA with Humans", "Only 17% of the gorilla population currently lives in protected regions",
          "Gorillas live in family groups of usually five to 10", "Gorillas weigh up to 440 pounds", "Gorillas can grow up to 6 feet tall"};
}


