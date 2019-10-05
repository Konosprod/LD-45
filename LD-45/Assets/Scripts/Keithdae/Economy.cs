using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy : MonoBehaviour
{
    // Singleton
    public static Economy _instance;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }


    public static int currentDay = 1;

    public static int loanId = 0; // internal id for loans to be able to search/remove them from the list
    public const int loanOfferNb = 3;   // Number of loan you are offered each day

    public class Loan
    {
        public int id;             // id of the loan
        public int amount;          // How much money you were lent
        public float interestRate;  // How much interest you have to pay
        public int startDate;       // The day you took the loan
        public int endDate;         // The day when you need to settle the loan
        public int interestFreq;    // How often you need to pay interest (every 2-3-4-etc days)

        public Loan(int amnt, float inter, int start, int end, int freq)
        {
            id = Economy.loanId++;
            amount = amnt;
            interestRate = inter;
            startDate = start;
            endDate = end;
            interestFreq = freq;
        }

        public int CheckInterest()
        {
            int interest = 0;

            if ((Economy.currentDay - startDate) % interestFreq == 0)
            {
                interest = Mathf.FloorToInt(amount * interestRate);
            }

            return interest;
        }
    }

    public int money = 0;   // Your money (0 BECAUSE YOU START WITH NOTHING)
    public int reputation = 0;  // Your reputation (0 BECAUSE YOU START WITH NOTHING)
    public List<Loan> loans = new List<Loan>(); // Your loans

    public Loan[] offers = new Loan[loanOfferNb];

    public bool bankrupt = false;
    public int bankruptStartDate;

    public const int bankruptGracePeriod = 5; // You have 5 days to earn money or it's over

    // Start is called before the first frame update
    void Start()
    {

    }

    public void NextDay()
    {
        currentDay++;

        int totalInterest = 0;
        // We add the interests of all the loans
        foreach (Loan loan in loans)
        {
            totalInterest += loan.CheckInterest();
        }

        money -= totalInterest;

        if(money < 0)
        {
            if(!bankrupt)
            {
                // This is your first day of being bankrupt, you can't take loans anymore until you go positive again
                bankrupt = true;
                bankruptStartDate = currentDay;
            }
            else
            {
                if(currentDay - bankruptStartDate > bankruptGracePeriod)
                {
                    // You have been bankrupt for 5 days in a row, YOU LOSE

                }
            }
        }
        else
        {
            if(bankrupt)
            {
                // You are positive again ! Yay
                bankrupt = false;
            }
        }
    }

    public void CreateOffers()
    {

    }
}
