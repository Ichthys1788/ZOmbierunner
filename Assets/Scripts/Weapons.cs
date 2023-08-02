using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 100f;
    string gametag = "";
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
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
