using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public float range;
    public ParticleSystem ps;
    public GameObject particleObject;
    public int bullets;
    private int hits;
    private int bulletsFired;
    private double acc;
    public TMP_Text ammoText;
    public TMP_Text hitText;
    public TMP_Text accuText;
    private Animator anim;
    public GameObject gun;
    // Start is called before the first frame update
    void Start()
    {
        hits = 0;
        bulletsFired = 0;
        accuText.text = "0" + " %";
        anim = gun.GetComponent<Animator>();
        StartCoroutine(nameof(accuracyCalc));
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = bullets.ToString();
        if(Input.GetMouseButtonDown(0) && bullets > 0){
            shoot();
        }
        if (bullets == 0 && Input.GetKeyDown(KeyCode.R)){
            anim.Play("GunReload");
            bullets = 10;
        }
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * range);
    }
    public void shoot(){

        bullets --;
        bulletsFired ++;
        ps.Play();
        anim.Play("GunShoot");
            
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range)){
            Debug.Log(hit.collider.name);
            Instantiate(particleObject, hit.point, Quaternion.identity);
            if(hit.collider.tag == "Target"){
                hit.collider.gameObject.GetComponent<Target>().health -= 1;
                hits ++;
                hitText.text = hits.ToString();

            }
        }
    }
    IEnumerator accuracyCalc(){
        while(true){
            
            if(bulletsFired == 0) {
                acc = 0;
                Debug.Log("accu:" + acc);
            }else {
                acc = (double)hits / bulletsFired * 100.0;
            }
            
            //accuText.text = acc.ToString() + " %";
            accuText.text = $"{acc:F1} %";
            yield return new WaitForSeconds(2.0f);
        }
       


    }
}
