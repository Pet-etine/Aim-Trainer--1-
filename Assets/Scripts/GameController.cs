using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    private bool startRun;
    private bool timerStarted;
    public bool childTrigger;
    public float range;
    public ParticleSystem ps;
    public GameObject particleObject;
    public GameObject finishLine;
    public int bullets;
    private int hits;
    private int bulletsFired;
    private double acc;
    public TMP_Text ammoText;
    public TMP_Text hitText;
    public TMP_Text accuText;
    public TMP_Text timeText;
    public TMP_Text FinishText;
    public TMP_Text FinishTextScore;
    private Animator anim;
    public GameObject gun;
    public GameObject hitMarkers;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        hitMarkers.gameObject.SetActive(false);
        FinishText.enabled = false;
        FinishTextScore.enabled = false;
        timeElapsed = 0;
        Time.timeScale = 0;
        childTrigger = false;
        startRun = false;
        timerStarted = false;
        hits = 0;
        bulletsFired = 0;
        accuText.text = "0" + " %";
        anim = gun.GetComponent<Animator>();
        StartCoroutine(nameof(accuracyCalc));
        anim = gun.GetComponent<Animator>();

        // Check if the Animator component was found
        if (anim == null)
        {
            Debug.LogError("Animator component not found on the gun GameObject!");
        }

    }
    // Update is called once per frame
    void Update()
    {
        ammoText.text = bullets.ToString();
        if (Input.GetMouseButtonDown(0) && bullets > 0)
        {
            shoot();
        }
        if (Input.GetMouseButtonDown(0) && bullets > 0 && !EventSystem.current.IsPointerOverGameObject()) // Add UI check
        {
            shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.Play("GunReload");
            bullets = 10;
        }
        if (startRun == false && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("RUN STARTED!");
            Time.timeScale = 1;
            timerStarted = true;
            StartCoroutine(nameof(updateRunTimer));
            startRun = true;
        }
        if (childTrigger == true)
        {
            StopCoroutine(nameof(updateRunTimer));
            FinishText.enabled = true;
            FinishTextScore.enabled = true;
            FinishTextScore.text = "Your time was: " + timeElapsed.ToString("0.0") + " seconds!";
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * range);
    }
    public void shoot()
    {

        bullets--;
        bulletsFired++;
        ps.Play();
        anim.Play("GunShoot");

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            //Debug.Log(hit.collider.name);
            Instantiate(particleObject, hit.point, Quaternion.identity);
            if (hit.collider.tag == "Target")
            {
                StartCoroutine(nameof(hitmarks));
                hit.collider.gameObject.GetComponent<Target>().health -= 1;
                hits++;
                hitText.text = hits.ToString() + " / 28";
                //hitMarkers.gameObject.SetActive(true);

            }
        }
    }
    IEnumerator hitmarks()
    {
        hitMarkers.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitMarkers.SetActive(false);
    }
    IEnumerator accuracyCalc()
    {
        while (true)
        {

            if (bulletsFired == 0)
            {
                acc = 0;
                Debug.Log("accu:" + acc);
            }
            else
            {
                acc = (double)hits / bulletsFired * 100.0;
            }

            //accuText.text = acc.ToString() + " %";
            accuText.text = $"{acc:F1} %";
            yield return new WaitForSeconds(2.0f);
        }

    }
    IEnumerator updateRunTimer()
    {
        for (; ; )
        {
            float interval = 0.1f;
            timeText.text = timeElapsed.ToString("0.0");
            yield return new WaitForSeconds(interval);
            timeElapsed += interval;
        }
    }
}
