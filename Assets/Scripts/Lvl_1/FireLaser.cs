using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireLaser : MonoBehaviour
{
    private bool targetHit;
    public bool cloneIsDead = false;
    [SerializeField] bool playerIsDead = false;
    [SerializeField]bool PuzzleResolved = false;
    [SerializeField] ParticleSystem flashFire;
    [SerializeField] ParticleSystem ImpactHit;
    [SerializeField] Transform raycastOrigin;
    [SerializeField] private TrailRenderer _tracereffect;
    public ObjectFollowMiror objectFollowMirorscript;

    private Ray _ray;
    private RaycastHit hitInfo;
    
    void Start()
    {
    }

    
    void FixedUpdate()
    {
        if (playerIsDead == true )
        {
            //gameover
            //reload la scene
            SceneManager.LoadScene("Lvl_1_test");
        }
        else if (cloneIsDead == true && playerIsDead == false)
        {
            //Win
            PuzzleResolved = true;
        }
    }

    public void FiringBullet()
    {
      
       flashFire.Play();

       _ray.origin = raycastOrigin.position;
       if (objectFollowMirorscript.OnMirrorSide == true)
       {
           _ray.direction = -raycastOrigin.forward;
       }
       else
       {
           _ray.direction = raycastOrigin.forward;
       }
     

       var tracer = Instantiate(_tracereffect, _ray.origin, Quaternion.identity);
       tracer.AddPosition(_ray.origin);
       if (Physics.Raycast(_ray,out hitInfo))
       {
          // Debug.DrawLine(_ray.origin,hitInfo.point,Color.red,1.0f);
          Debug.Log("A TOUCHE" + hitInfo.collider.gameObject.name);
         targetHit = true;
           ImpactHit.transform.position = hitInfo.point;
           ImpactHit.transform.forward = hitInfo.normal;
           ImpactHit.Play();

           tracer.transform.position = hitInfo.point;
           
           if (targetHit = true && hitInfo.collider.gameObject.tag == "Clone")
           {
               cloneIsDead = true;
              Debug.Log("WIN");
               
           }
           else if (targetHit = true && hitInfo.collider.gameObject.tag == "Player")
           {
               playerIsDead = true;
               Debug.Log("lost");
           }
       }


       

    }

        
    
}
