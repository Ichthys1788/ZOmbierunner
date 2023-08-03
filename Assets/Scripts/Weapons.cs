using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Weapons : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 100f;
     AudioSource Gunshot;



    string gametag = "";
    void Start()
    {
        Gunshot = GetComponent<AudioSource>();
        Gunshot.Stop();
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {   
            
            Play();
            Shoot();
            
        }
    }

    private void Play()
    {
        Gunshot.Play();
    }

    private void Shoot()
    {        
        Gunshot = GetComponent<AudioSource>();

        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        { 
           gameObject.tag = hit.transform.name;
           Debug.Log(gameObject.tag);
           
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            target.Damagetaken(damage);
        }
        else
        {
            return;
        }
    }


 
}
