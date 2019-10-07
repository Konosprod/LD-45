using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectRobot : MonoBehaviour
{
    public string robotVal = "";
    public GameObject choosePanel;
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        Button b = GetComponent<Button>();
        b.onClick.AddListener(ChooseRobot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChooseRobot()
    {
        choosePanel.SetActive(false);
        mainMenu.SetActive(true);
        GameManager._instance.RobotModel = robotVal;
    }
}
