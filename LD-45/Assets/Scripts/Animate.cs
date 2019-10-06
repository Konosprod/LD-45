using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{

    public Animator animator;
    public float minTime = 0f;
    public float maxTime = 1f;
    public float speed = 1f;
 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        float time = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(time);
        animator.enabled = true;
        animator.speed = Random.Range(.2f, 1f);
    }
}
