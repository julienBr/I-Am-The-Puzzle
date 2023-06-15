using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

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
    [SerializeField] private bool _canShoot = false;
    public GameObject ammo;
    [SerializeField] private XRSocketInteractor socket;
    [SerializeField] private bool ammoIsTrue = false;
    
    

    private Ray _ray;
    private RaycastHit hitInfo;
    
    void Start()
    {
    }

    
    void FixedUpdate()
    {
        StartCoroutine(DeathRespawnDelay());

        if (_canShoot == true )
        {
            // ammo.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //ammo.gameObject.GetComponent<Rigidbody>().useGravity = false;
            ammo.transform.Rotate(0f,10f,0f);
            ammo.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.green);
        }
        else if(_canShoot == false )
        {
            
            ammo.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
            ammo.transform.Rotate(0f,0f,0f);
            //ammo.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            //  ammo.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        
      
        

        
    }

    public void FiringBullet()
    {
        StartCoroutine(FireDelay());

    }

    IEnumerator FireDelay()
    {
        if (_canShoot == true && ammoIsTrue == true)
        {
            flashFire.Play();

            _ray.origin = raycastOrigin.position;
          /*  if (gameObject.gameObject.name == "PistolWithAmmoLVL2Mirror"  || gameObject.gameObject.name == "PistolWithAmmoLVL1" || gameObject.gameObject.name == "PistolWithoutAmmoMirror" )
          
            {
                _ray.direction = -raycastOrigin.forward;
            }
            else //if (  gameObject.gameObject.name == "PistolWithoutAmmo"  )
            {
                _ray.direction = raycastOrigin.forward;
            }*/
      
            _ray.direction = raycastOrigin.forward;

            var tracer = Instantiate(_tracereffect, _ray.origin, Quaternion.identity);
            tracer.AddPosition(_ray.origin);
            if (Physics.Raycast(_ray,out hitInfo))
            {
                Debug.DrawLine(_ray.origin,hitInfo.point,Color.red,1.0f);
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
            
            _canShoot = false;
            
            yield return new WaitForSeconds(0.7f);
            _canShoot = true;
          
        }

    }
    
    
    

    public void SocketAmmoActivated()
    {
        GameObject ammoprism = socket.selectTarget.gameObject;
        Debug.Log("le pile est la " + ammoprism.name);

        if (ammoprism.name == "Ammo")
        {
            _canShoot = true;
            ammoIsTrue= true;

        }
        else if (ammoprism.name == "AmmoTransparent")
        {
            _canShoot= false;
           ammoIsTrue = false;
        }
        
    }
    
    public void SocketAmmoDesactivated()
    {
        _canShoot = false;
       
    }

    IEnumerator DeathRespawnDelay()
    {
        if (playerIsDead == true )
        {
            //gameover
            //reload la scene
            yield return new WaitForSeconds(0.2f);
            SceneManager.LoadScene("Lvl_1_test");
        }
        else if (cloneIsDead == true && playerIsDead == false)
        {
            //Win
            PuzzleResolved = true;
        }   
    }

}   
