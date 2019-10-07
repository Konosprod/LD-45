using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoanListElement : MonoBehaviour
{
    public Text loanName;
    public Text loanAmount;
    public Text loanEndDate;
    public Text loanRate;
    public Text loanInterest;

    public Button payBackButton;

    public int loanId;

    public RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupLoanElement(string name, int amount, int endDate, float rate, int interest, int id)
    {
        loanName.text = name;
        loanAmount.text = amount + " q";
        loanEndDate.text = "day " + endDate;
        loanRate.text = rate.ToString("F2") + "%";
        loanInterest.text = interest + " q";

        loanId = id;

        rectTransform = GetComponent<RectTransform>();
    }

    public void Click()
    {
        Economy._instance.PayLoanById(loanId);
    }
}
