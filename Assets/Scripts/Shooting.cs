using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Shooting : MonoBehaviour
{
    [SerializeField]
    private Camera FpsCamera;
    public float fireRate=0.5f;
    public float timer=0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        if(timer>0.5f)
        {
            if(Input.GetButton("Fire1"))
            {
                RaycastHit _hit;
                Ray ray=FpsCamera.ViewportPointToRay(new Vector3(0.5f,0.5f));
                if(Physics.Raycast(ray,out _hit,100))
                {
                    Debug.Log(_hit.collider.gameObject.name);
                    if(_hit.collider.gameObject.CompareTag("Player")&&!_hit.collider.GetComponent<PhotonView>().IsMine)
                    {
                        _hit.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamage",RpcTarget.AllBuffered,5f);
                    }
                }
                timer=0f; 
            }

                
        }
    }
   
}
