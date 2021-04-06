using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class TakingDamage : MonoBehaviourPunCallbacks
{
    public float health;
    public Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        health=100;
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    [PunRPC]
    public void TakeDamage(float _damage)
    {
        health-=_damage;
        healthBar.fillAmount=health*0.01f;
        Debug.Log(health+" "+photonView.Owner.NickName);
        if(health<=0f)
        {
            Die();
            //die
        }
    }
    void Die()
    {
        //this function will be called for all the users in the photon room
        if(photonView.IsMine)
        {
            PixelGunGameManager.instance.LeaveRoom();
        }
    }
}
