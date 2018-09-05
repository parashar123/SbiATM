using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SbiATM
{
    class Program
    {

        static void Main(string[] args)
        {


            Console.WriteLine("SBI Welcomes you!");
            Console.WriteLine("Please enter the amount you want to withdraw");


            WithDrawal();

            //Logical method for the denomination calculation.

        }

        public static void WithDrawal()
        {
        WithDrawalAmount:
            int WithdrawalAmount;
            if (int.TryParse(Console.ReadLine(), out WithdrawalAmount))
            {
                //nothing required as of now. Trying to validate.
                if (WithdrawalAmount == 0)
                {
                    Console.WriteLine("Please provide some real amount");
                    goto WithDrawalAmount;
                }
            }
            else
            {
                Console.WriteLine("Please provide some real amount");
                goto WithDrawalAmount;
            }

            NoteCalculation(WithdrawalAmount);

        }

        public static void NoteCalculation(int UsersAmount)
        {
            try
            {
                List<Note> Notee = new List<Note>();

                Notee.Add(new Note { Denominator = 5 });
                Notee.Add(new Note { Denominator = 10 });
                Notee.Add(new Note { Denominator = 50 });
                Notee.Add(new Note { Denominator = 100 });
                Notee.Add(new Note { Denominator = 500 });

                int LeastDenomination = Notee.Min(c => c.Denominator);

                int WithdrawalAmount = UsersAmount;


                //1.Trying to check if the amount is dispensable with Least denomination.

                if (UsersAmount % LeastDenomination != 0)
                {

                    Console.WriteLine("Invalid Amount!! \n");
                    Console.WriteLine("Sorry!! We cannot process this amount");
                    Console.WriteLine("Please try with amount ending with 5 or 0 next time");
                    WithDrawal();

                }

                //2. Calculating AllNotes in order  to dispense them.
                List<Note> AllNotes = new List<Note>();

                do
                {
                    //If we have the calculation on the basis of highest denomination first it will be easier to get the number of notes.

                    Notee = Notee.OrderByDescending(c => c.Denominator).ToList();

                    int totalCounts = 0;
                    int sum = 0;
                    //Check if highest count denom can cover the amount.
                    if (Notee[0].Denominator <= UsersAmount)
                    {

                        while (sum < UsersAmount)
                        {
                            sum += Notee[0].Denominator;
                            if (sum <= UsersAmount)
                            {
                                totalCounts++;
                            }


                        }
                        AllNotes.Add(new Note { Denominator = Notee[0].Denominator, NoteCount = totalCounts });
                        UsersAmount = (UsersAmount - (Notee[0].Denominator) * totalCounts);

                    }
                    else
                    {
                        //Simply removing that denominator related notes which are not nequired.
                        Notee.RemoveAt(0);
                    }


                } while (UsersAmount > 0);


                Console.WriteLine("We have your amount " + WithdrawalAmount + ", in the following denomination:");
                foreach (var item in AllNotes)
                {
                    Console.WriteLine("Denominator: {0} * {1}", item.Denominator, item.NoteCount);

                   
                }
                WithDrawal(); 
            }
            catch (Exception ex)
            { var message = ex.Message; }
        }
    }

    public class Note
    {

        public int Denominator { get; set; }
        public int NoteCount { get; set; }
    }




}

