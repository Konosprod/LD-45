using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [HideInInspector]
    public static int loanId = 0; // internal id for loans to be able to search/remove them from the list

    public const int loanOfferNb = 3;   // Number of loans you are offered each day

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

        // Next day interest for forecast
        public int GetNextDayInterest()
        {
            int interest;
            if (currentDay + 1 < endDate)
            {
                interest = Mathf.FloorToInt(amount * interestRate / 100f);
            }
            else
            {
                interest = amount;
            }
            return interest;
        }

        // Remaining debt (all interests + payment)
        public int GetRemainingDebt()
        {
            int debt = 0;
            debt += (endDate - currentDay - 1) * Mathf.FloorToInt(amount * interestRate / 100f); // Remaining interest that you will pay (-1 because no interest on last day)
            debt += amount;
            return debt;
        }
    }

    public int money = 0;   // Your money (0 BECAUSE YOU START WITH NOTHING)
    public int reputation = 0;  // Your reputation (0 BECAUSE YOU START WITH NOTHING)

    public List<Loan> loans = new List<Loan>(); // Your loans
    public int totalMoneyPaidBack = 0;

    public Loan[] offers = new Loan[loanOfferNb];

    public bool bankrupt = false;
    public int bankruptStartDate;

    public const int bankruptGracePeriod = 5; // You have 5 days to earn money or it's over

    // UI
    [Header("UI")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI reputationText;
    public TextMeshProUGUI dayText;


    [Header("Loan offers")]
    [Header("Loan1")]
    public TextMeshProUGUI loanOffer1AmountText;
    public TextMeshProUGUI loanOffer1RateText;
    public TextMeshProUGUI loanOffer1EndDateText;
    [Header("Loan2")]
    public TextMeshProUGUI loanOffer2AmountText;
    public TextMeshProUGUI loanOffer2RateText;
    public TextMeshProUGUI loanOffer2EndDateText;
    [Header("Loan3")]
    public TextMeshProUGUI loanOffer3AmountText;
    public TextMeshProUGUI loanOffer3RateText;
    public TextMeshProUGUI loanOffer3EndDateText;

    [Header("Debt")]
    public Text nextDayPaymentText;
    public Text moneyPaidBackText;
    public Text remainingDebtText;
    public Slider debtSlider;


    // Start is called before the first frame update
    void Start()
    {
        // UI setup for preparation phase
        UpdateDayText();
        UpdateMoneyText();
        UpdateReputationText();
        UpdateNextDayInterest();
        // Create the first loan offers
        CreateOffers();
        UpdateOffers();

        UpdateMoneyPaidBackText();
        UpdateRemainingDebtText();
        UpdateMoneyBorrowedSlider();
    }

    // Pay the interests of the day after the fight
    public void NextDay()
    {
        currentDay++;
        UpdateDayText();
        CreateOffers();
        UpdateOffers();

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
        totalMoneyPaidBack += totalInterest;
        UpdateMoneyPaidBackText();
        UpdateMoneyBorrowedSlider();
        SpendMoney(totalInterest);

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
        int amount;
        float interest;
        int duration;

        // Reputation bonus : 1 + Sqrt(reputation)% amount increased / interest decreased
        float repBonus = 1f + (Mathf.Sqrt(reputation) / 100f);

        // 1st loan : balanced (average amount, interest and duration)
        amount = Mathf.CeilToInt(Random.Range(400, 600) * repBonus);
        interest = Random.Range(3f, 7f) / repBonus;
        duration = Mathf.CeilToInt(Random.Range(8, 13) * 5f / interest);
        offers[0] = new Loan(amount, interest, currentDay + duration);

        // 2nd loan : high-interest (high amount, interest, short duration)
        amount = Mathf.CeilToInt(Random.Range(900, 1300) * repBonus);
        interest = Random.Range(8f, 20f) / repBonus;
        duration = Mathf.CeilToInt(Random.Range(4, 8) * 14f / interest);
        offers[1] = new Loan(amount, interest, currentDay + duration);

        // 3rd loan : long-duration (small amount, average interest, long duration)
        amount = Mathf.CeilToInt(Random.Range(250, 425) * repBonus);
        interest = Random.Range(2f, 4f) / repBonus;
        duration = Mathf.CeilToInt(Random.Range(20, 30) * 3f / interest);
        offers[2] = new Loan(amount, interest, currentDay + duration);
    }


    public void TakeLoanOffer(int offer)
    {
        loans.Add(offers[offer]);
        EarnMoney(offers[offer].amount);
        UpdateMoneyBorrowedSlider();
        UpdateRemainingDebtText();
    }


    // Add or substract money and update the UI accordingly
    public void EarnMoney(int amount)
    {
        money += amount;
        UpdateMoneyText();
        UpdateNextDayInterest(); // Not always usefull
    }
    public void SpendMoney(int amount)
    {
        money -= amount;
        UpdateMoneyText();
        UpdateNextDayInterest(); // Not always usefull
    }
    // Same shit for reputation
    public void GainReputation(int amount)
    {
        reputation += amount;
        UpdateReputationText();
    }
    public void LoseReputation(int amount)
    {
        reputation -= amount;
        UpdateReputationText();
    }

    // Update the UI when you change the money
    public void UpdateMoneyText()
    {
        moneyText.text = money + " q";
    }
    public void UpdateReputationText()
    {
        reputationText.text = "REP : " + reputation;
    }
    public void UpdateDayText()
    {
        dayText.text = "Day " + currentDay;
    }

    public void UpdateNextDayInterest()
    {
        int totalInterest = 0;
        // We add the interests/amounts of all the loans
        foreach (Loan loan in loans)
        {
            totalInterest += loan.GetNextDayInterest();
        }

        nextDayPaymentText.text = totalInterest + " q";
    }

    public void UpdateOffers()
    {
        loanOffer1AmountText.text = "+ " + offers[0].amount + " q";
        loanOffer1RateText.text = offers[0].interestRate.ToString("F1") + '%';
        loanOffer1EndDateText.text = "Lasts " + (offers[0].endDate - currentDay) + " days";

        loanOffer2AmountText.text = "+ " + offers[1].amount + " q";
        loanOffer2RateText.text = offers[1].interestRate.ToString("F1") + '%';
        loanOffer2EndDateText.text = "Lasts " + (offers[1].endDate - currentDay) + " days";

        loanOffer3AmountText.text = "+ " + offers[2].amount + " q";
        loanOffer3RateText.text = offers[2].interestRate.ToString("F1") + '%';
        loanOffer3EndDateText.text = "Lasts " + (offers[2].endDate - currentDay) + " days";
    }

    public void UpdateMoneyPaidBackText()
    {
        moneyPaidBackText.text = totalMoneyPaidBack + " q";
    }

    public void UpdateMoneyBorrowedSlider()
    {
        int remainingDebt = 0;
        foreach (Loan loan in loans)
        {
            remainingDebt += loan.GetRemainingDebt();
        }
        int totalDebt = totalMoneyPaidBack + remainingDebt;
        float ratio = totalMoneyPaidBack / (totalDebt != 0f ? totalDebt : 1f) * 100f;
        debtSlider.value = ratio;
    }

    public void UpdateRemainingDebtText()
    {
        int remainingDebt = 0;
        foreach (Loan loan in loans)
        {
            remainingDebt += loan.GetRemainingDebt();
        }

        remainingDebtText.text = remainingDebt + " q";
    }
}
