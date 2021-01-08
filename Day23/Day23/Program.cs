using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            List<int> Cups = new List<int>() { 8,5,3,1,9,2,6,4,7 };

            int CurrentLabel = Cups[0];
            int CurrentCupIndex = 0;

            for (int i = 0; i < 10; i++)
            {
                CurrentCupIndex = Cups.IndexOf(CurrentLabel);
                List<int> NextCups = GetNextThreeCups(CurrentCupIndex);
                CurrentCupIndex = Cups.IndexOf(CurrentLabel);
                int DestinationCupIndex = GetDestinationCupIndex(Cups[CurrentCupIndex]);
                Cups.InsertRange(DestinationCupIndex+1, NextCups);

                if (Cups.IndexOf(CurrentLabel) + 1 >= Cups.Count)
                {
                    CurrentLabel = Cups[0];
                }
                else
                {
                    CurrentLabel = Cups[Cups.IndexOf(CurrentLabel) + 1];
                }

            }
            Console.Read();

            List<int> GetNextThreeCups(int Index)
            {
                List<int> Numbers = new List<int>();
                if (Index > Cups.Count - 4)
                {                    
                    int CupsFromEnd = Cups.Count - (Index + 1);
                    Numbers.AddRange(Cups.GetRange(Index+1, CupsFromEnd));
                    Numbers.AddRange(Cups.GetRange(0,3-CupsFromEnd));
                    Cups.RemoveRange(Index+1, CupsFromEnd);
                    Cups.RemoveRange(0, 3 - CupsFromEnd);
                    return Numbers;

                }
                else
                {
                    Numbers.AddRange(Cups.GetRange(Index+1, 3));
                    Cups.RemoveRange(Index+1, 3);
                    return Numbers;
                }
            }

            int GetDestinationCupIndex(int CurrentCupLabel)
            {
                int i = CurrentCupLabel-1;
                while (i > 0)
                {
                    if (Cups.IndexOf(i) == -1)
                    {
                        i--;
                    }
                    else
                    {
                        return Cups.IndexOf(i);
                    }
                }
                return Cups.IndexOf(Cups.Max());

            }

            */

            string Numbers = System.IO.File.ReadAllLines(@"E:\Advent Code\Day 23\Data.txt")[0];

            //Give in cup label, outputs next cup label
            Dictionary<int, int> CupToNextCup = new Dictionary<int, int>();

            //Populates dictionary
            int NumOfCups = 1000000;
            for (int i = 0; i < NumOfCups; i++)
            {
                if (i < Numbers.Length)
                {
                    if (i == Numbers.Length - 1)
                    {
                        if (NumOfCups <= Numbers.Length)
                        {
                            CupToNextCup.Add(Convert.ToInt32(Numbers[i].ToString()), Convert.ToInt32(Numbers[0].ToString()));
                        }
                        else
                        {
                            CupToNextCup.Add(Convert.ToInt32(Numbers[i].ToString()), i + 2);
                        }
                        
                    }
                    else
                    {
                        CupToNextCup.Add(Convert.ToInt32(Numbers[i].ToString()), Convert.ToInt32(Numbers[i+1].ToString()));
                    }
                }
                else
                {
                    if (i == NumOfCups - 1)
                    {
                        CupToNextCup.Add(i + 1, Convert.ToInt32(Numbers[0].ToString()));
                    }
                    else
                    {
                        CupToNextCup.Add(i+1,i+2);
                    }                    
                }
               
            }

            //Main Loop
            int CurrentCupLabel = Convert.ToInt32(Numbers[0].ToString());
            for (int i = 0; i< 10000000; i++)
            {
                //Next Three Cups
                int NextCupLabel1 = CupToNextCup[CurrentCupLabel];
                int NextCupLabel2 = CupToNextCup[NextCupLabel1];
                int NextCupLabel3 = CupToNextCup[NextCupLabel2];

                int DestinationCupLabel = GetDestinationCupLabel(NextCupLabel1,NextCupLabel2,NextCupLabel3);             


                CupToNextCup[CurrentCupLabel] = CupToNextCup[NextCupLabel3];
                CupToNextCup[NextCupLabel3] = CupToNextCup[DestinationCupLabel];
                CupToNextCup[DestinationCupLabel] = NextCupLabel1;

                CurrentCupLabel = CupToNextCup[CurrentCupLabel];
            }

            //Result calculaed here
            int Cup1 = CupToNextCup[1];
            int Cup2 = CupToNextCup[Cup1];
            long Result = Cup1 * Cup2;

            Console.WriteLine(Result);
            Console.Read();            

            int GetDestinationCupLabel(int BannedCup1, int BannedCup2, int BannedCup3)
            {
                int i = CurrentCupLabel - 1;
              
                while (true)
                {
                    if (i < 1) i = NumOfCups;
                    if (i != BannedCup1 && i != BannedCup2 && i != BannedCup3) return i;
                    i--;
                }                
            }

        }
    }
}
