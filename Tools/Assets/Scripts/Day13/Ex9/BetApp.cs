using System.Collections.Generic;
using UnityEngine;


public class BetApp : MonoBehaviour
{

    string[] teams = { "A", "B", "C", "D", "E" };
    int numberOfTeams = 5;
    int NumberOfPosib = 3;
    int allposib;
    // Start is called before the first frame update
    void Start()
    {
        BetRecursions(5, 3);
    }



    void P2ws()
    {
        for (int a = 0; a < 3; a++)
        {
            for (int b = 0; b < 3; b++)
            {
                for (int c = 0; c < 3; c++)
                {
                    for (int d = 0; d < 3; d++)
                    {
                        for (int e = 0; e < 3; e++)
                        {
                            //Debug.Log(e);
                            Debug.Log(getTheResult(a) + getTheResult(b) + getTheResult(c) + getTheResult(d) + getTheResult(e));
                        }
                    }
                }
            }
        }
    }

    int BetRecursion(int numberOFTeams, int Posibl)
    {
        if (numberOFTeams == 0)
        {
            return 1;
        }
        return Posibl * BetRecursion(numberOFTeams - 1, Posibl);
    }

    void BetRecursions(int numberOFTeams, int Posibl)
    {
        int teams = Posibl - 1;
        for (int i = 0; i < Posibl; i++)
        {
            if (numberOFTeams == 0)
            {
                return;
            }
            if (numberOFTeams == 1)
            {
                //Debug.Log(getTheResult(teams));
                Debug.Log(i); // 0 ,  0,  0,1,2 // 0,  1,  0,1,2 // 0,  2,  0,1,2
                //Debug.Log(getTheResult(teams) + getTheResult(teams) + getTheResult(teams) + getTheResult(teams ) + getTheResult(teams));
            }
            teams -= 1;
            BetRecursions(numberOFTeams - 1, Posibl);
        }
    }

    private void diceRolls(int teams, List<int> chosen)
    {
        if (teams == 0)
        {
            string str = "";
            for (int column = 0; column <= 4; column++)
            {
                //str += getTheResult(vs[column]);
            }
            Debug.Log(str);
            // Debug.Log(getTheResult(vs[0]) + getTheResult(vs[1]) + getTheResult(vs[2]) + getTheResult(vs[0]) + getTheResult(vs[0])); 
            //} 
            //else 
            //{ 
            //for (int i = 0; i <= 2; i++) 
            //{ chosen.Add(i); 
            // choose diceRolls(teams - 1, chosen); 
            // explore chosen.Remove(chosen[chosen.Count-1]); // un-choose 
            //} 
        }
    }



    string getTheResult(int a)
    {
        string retun22 = " ";
        if (a == 0)
        {
            retun22 += "w" + " ";

        }
        if (a == 1)
        {
            retun22 += "d" + " ";

        }
        if (a == 2)
        {
            retun22 += "l" + " ";

        }
        return retun22;
    }
}
