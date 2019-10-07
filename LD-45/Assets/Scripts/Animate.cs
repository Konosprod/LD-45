using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{

    public Animator animator;
    public float minTime = 0f;
    public float maxTime = 1f;
    public float speed = 1f;

    [Header("Actions")]
    public bool isDone = true;
    public GameObject laser;
    public float laserSpeed = 20f;
    public GameObject shield;
    public float shieldDuration = 2f;
    public float attackSpeed = 20f;

    private Vector3 startingPos;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForAnimation());
        startingPos = transform.position;
    }

    public void attackAnimation(int direction)
    {
        SoundManager._instance.PlaySFX(SFXType.Attack);
        isDone = false;
        StartCoroutine(doAttack(direction));
    }

    public void LaserAnimation(int direction)
    {
        SoundManager._instance.PlaySFX(SFXType.Laser);
        isDone = false;
        StartCoroutine(shootLaser(direction));
    }

    public void ShieldAnimation()
    {
        SoundManager._instance.PlaySFX(SFXType.Shield);
        isDone = false;
        StartCoroutine(doShield());
    }

    private IEnumerator WaitForAnimation()
    {
        float time = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(time);
        animator.enabled = true;
        animator.speed = Random.Range(.2f, 1f);
    }

    private IEnumerator shootLaser(int direction)
    {
        laser.SetActive(true);
        Vector3 straingPos = laser.transform.position;
        yield return new WaitForEndOfFrame();
        Renderer r = laser.GetComponent<Renderer>();
        while(r.isVisible)
        {
            laser.transform.position += new Vector3(direction * 1 * laserSpeed * Time.deltaTime, 0, 0);
            yield return new WaitForEndOfFrame();
        }
        isDone = true;
        laser.SetActive(false);
        laser.transform.position = straingPos;
    }

    private IEnumerator doShield()
    {
        shield.SetActive(true);
        yield return new WaitForEndOfFrame();
        float elapsedTime = 0f;

        while(elapsedTime < shieldDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        isDone = true;
        shield.SetActive(false);
    }

    private IEnumerator doAttack(int direction)
    {
        SpriteRenderer r = GetComponent<SpriteRenderer>();

        while(r.isVisible)
        {
            transform.position += new Vector3(direction * 1 * attackSpeed * Time.deltaTime, 0, 0);
            yield return new WaitForEndOfFrame();
        }

        isDone = true;
        transform.position = startingPos;
    }

    public void ResetAnimations()
    {
        laser.SetActive(false);
        shield.SetActive(false);
        //transform.position = startingPos;
        isDone = true;
    }
}
