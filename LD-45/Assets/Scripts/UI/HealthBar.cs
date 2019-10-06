using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image health;
    public float speed = 1f;
    public float time = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar(float value)
    {
        StartCoroutine(decreaseHealth(value));
    }

    IEnumerator decreaseHealth(float value)
    {
        float elapsedTime = 0;

        while(elapsedTime < time)
        {
            health.fillAmount = Mathf.Lerp(health.fillAmount, value, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
