using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        public int id;              // id of the loan
        public int amount;          // How much money you were lent
        public float interestRate;  // How much interest you have to pay
        public int startDate;       // The day you took the loan
        public int endDate;         // The day when you need to settle the loan

        public Loan(int amnt, float inter, int end)
        {
            id = Economy.loanId++;
            amount = amnt;
            interestRate = inter;
            startDate = currentDay;
            endDate = end;
        }

        // You pay interest everyday, you pay the amount at the end
        public int GetInterest()
        {
            int interest;
            if (currentDay < endDate)
            {
                interest = Mathf.FloorToInt(amount * interestRate / 100f);
            }
            else
            {
                interest = amount;
            }
            return interest;
        }
    }

    public int money = 0;   // Your money (0 BECAUSE YOU START WITH NOTHING)
    public int reputation = 0;  // Your reputation (0 BECAUSE YOU START WITH NOTHING)

    public List<Loan> loans = new List<Loan>(); // Your loans
    public int totalMoneyBorrowed = 0;
    public int totalMoneyPaidBack = 0;

    public Loan[] offers = new Loan[loanOfferNb];

    public bool bankrupt = false;
    public int bankruptStartDate;

    public const int bankruptGracePeriod = 5; // You have 5 days to earn money or it's over

    // UI
    public Text ecoInfo;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Pay the interests of the day after the fight
    public void NextDay()
    {
        currentDay++;

        int totalInterest = 0;
        List<Loan> toDel = new List<Loan>();
        // We add the interests/amounts of all the loans
        foreach (Loan loan in loans)
        {
            totalInterest += loan.GetInterest();
            if (loan.endDate == currentDay)
                toDel.Add(loan);
        }

        foreach (Loan del in toDel)
        {
            loans.Remove(del);
            // Debug.Log("Removed loan id=" + del.id);
        }

        // Debug.Log(totalInterest);

        money -= totalInterest;
        UpdateInfo();

        if (money < 0)
        {
            if (!bankrupt)
            {
                // This is your first day of being bankrupt, you can't take loans anymore until you go positive again
                bankrupt = true;
                bankruptStartDate = currentDay;
            }
            else
            {
                if (currentDay - bankruptStartDate > bankruptGracePeriod)
                {
                    // You have been bankrupt for 5 days in a row, YOU LOSE

                }
            }
        }
        else
        {
            if (bankrupt)
            {
                // You are positive again ! Yay
                bankrupt = false;
            }
        }
    }

    // Create multiple loan offers of various durations/amounts/interests
    public void CreateOffers()
    {

    }

    // TEST
    public void TakeLoan()
    {
        loans.Add(new Loan(300, 2.5f, currentDay + 15));
        money += 300;
        UpdateInfo();
    }


    // Add or substract money and update the UI accordingly
    public void EarnMoney(int amount)
    {
        money += amount;
        UpdateInfo();
    }
    public void SpendMoney(int amount)
    {
        money -= amount;
        UpdateInfo();
    }
    // Same shit for reputation
    public void GainReputation(int amount)
    {
        reputation += amount;
        UpdateInfo();
    }
    public void LoseReputation(int amount)
    {
        reputation -= amount;
        UpdateInfo();
    }

    // Update the UI when you change the money
    public void UpdateInfo()
    {
        ecoInfo.text = "Money : " + money + '\n' + "Reputation : " + reputation;
    }
}
