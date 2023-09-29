using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class FireLaser : MonoBehaviour
{
    private bool targetHit;
  //  public bool cloneIsDead = false;
  //[SerializeField] bool playerIsDead = false;
    [SerializeField] ParticleSystem flashFire;
    [SerializeField] ParticleSystem ImpactHit;
    [SerializeField] Transform raycastOrigin;
    [SerializeField] private TrailRenderer _tracereffect;
    public ObjectFollowMiror objectFollowMirorscript;
    [SerializeField] private bool _canShoot = false;
    public GameObject ammo;
    [SerializeField] private XRSocketInteractor socket;
    [SerializeField] private bool ammoIsTrue = false;
    [SerializeField] private Lvl1Manager _lvl1Manager;

    [SerializeField] private AudioSource gunsound;
   [SerializeField] private AudioSource impactsound;

    private Ray _ray;
   private RaycastHit hitInfo;

    void Start()
    {
    }


    void FixedUpdate()
    {
        //StartCoroutine(DeathRespawnDelay());

        if (_canShoot == true)
        {
            // ammo.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //ammo.gameObject.GetComponent<Rigidbody>().useGravity = false;
            ammo.transform.Rotate(0f, 10f, 0f);
            ammo.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.green);
        }
        else if (_canShoot == false)
        {

            ammo.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
            ammo.transform.Rotate(0f, 0f, 0f);
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
            gunsound.Play();
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
            if (Physics.Raycast(_ray, out hitInfo))
            {
              //  Debug.DrawLine(_ray.origin,hitInfo.point,Color.red,5f);
             //   Debug.Log("A TOUCHE" + hitInfo.collider.gameObject.name);
            Debug.Log(hitInfo.collider.name);
                ImpactHit.transform.position = hitInfo.point;
                ImpactHit.transform.forward = hitInfo.normal;
                ImpactHit.Play();

                tracer.transform.position = hitInfo.point;
                _canShoot = false;

                yield return new WaitForSeconds(0.7f);
                _canShoot = true;

              
                if ( hitInfo.collider.gameObject.tag == "Player" )
                {
                    //playerIsDead = true;
                    _lvl1Manager.playerIsDead = true;
                    Debug.Log("plauerdead");
                    StartCoroutine(_lvl1Manager.ReloadScene());
                }
                if ( hitInfo.collider.gameObject.tag == "Clone")
                {
                   // cloneIsDead = true;
                   _lvl1Manager.cloneIsDead = true;
                   Debug.Log("clonedead");
                   _canShoot = false;
                   objectFollowMirorscript._objectToFollow.GetComponent<FireLaser>()._canShoot = false;
                   StartCoroutine(_lvl1Manager.ReloadScene());
                   
                }

               // StartCoroutine(_lvl1Manager.ReloadScene());

                //  StartCoroutine(DeathRespawnDelay());

            }

          

           

        }

    }




    public void SocketAmmoActivated()
    {
        GameObject ammoprism = socket.selectTarget.gameObject;
        Debug.Log("le pile est la " + ammoprism.name);

       
        if (ammoprism.name == "Ammo")
        {
            _canShoot = true;
            ammoIsTrue = true;

        }
        else if (ammoprism.name == "AmmoTransparent")
        {
            objectFollowMirorscript.GetComponent<FireLaser>()._canShoot = true;
            objectFollowMirorscript.GetComponent<FireLaser>().ammoIsTrue = true;
            _canShoot = false;
            ammoIsTrue = false;
        }

    }

    public void SocketAmmoDesactivated()
    {
        
        _canShoot = false;
        if (objectFollowMirorscript != null)
        {
            objectFollowMirorscript.GetComponent<FireLaser>()._canShoot = false;
            objectFollowMirorscript.GetComponent<FireLaser>().ammoIsTrue = false;
        }
       
        
     
    }
    
    
    public void SocketAmmoHover()
    {
       
       // ammoprism1.GetComponent<Collider>().enabled = false;
       
     //  ammo.GetComponentInParent<XRGrabInteractable>().enabled = false;
       

    }
    public void SocketAmmoHoverExited()
    {
       
        // ammoprism1.GetComponent<Collider>().enabled = false;
       
      //  ammo.GetComponentInParent<XRGrabInteractable>().enabled = true;
       

    }

  /*  IEnumerator DeathRespawnDelay()
    {
        if (playerIsDead == true && cloneIsDead == true || playerIsDead == true && cloneIsDead == false)
        {
            //gameover
            //reload la scene
            yield return new WaitForSeconds(0f);
            SceneManager.LoadScene("Lvl_1_test");
            Debug.Log("lost");
        }
        else if (cloneIsDead == true && playerIsDead == false)
        {
            //WIN
            yield return new WaitForSeconds(0f);
            _lvl1Manager.EnigmeFinished();
            _canShoot = false;
            objectFollowMirorscript._objectToFollow.GetComponent<FireLaser>()._canShoot = false;
            Debug.Log("WIN");
        }

    }*/
  
  
  /* IEnumerator DeathRespawnDelay()
    {
        if (_lvl1Manager.playerIsDead == true && _lvl1Manager.cloneIsDead == true || _lvl1Manager.playerIsDead == true && _lvl1Manager.cloneIsDead == false)
        {
            //gameover
            //reload la scene
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Lvl_1_test");
            Debug.Log("lost");
        }
        else if (_lvl1Manager.cloneIsDead == true && _lvl1Manager.playerIsDead == false)
        {
            //WIN
            yield return new WaitForSeconds(1f);
            _lvl1Manager.EnigmeFinished();
            _canShoot = false;
            objectFollowMirorscript._objectToFollow.GetComponent<FireLaser>()._canShoot = false;
            Debug.Log("WIN");
        }

    */

}
