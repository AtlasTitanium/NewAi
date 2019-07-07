using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidInfected : MonoBehaviour
{
    public int specialDamage = 25;
    public int shieldAlliesRange = 10;
    public GameObject shieldPrefab;
    public LayerMask enemies;
    public bool isIntangable = false;
    public bool hasTeleported;

    private bool attack = false;
    private bool isFalling;
    private CharacterController cc;
    private float startTime;
    private float telportStartTime;
    private float intangableTime;
    private float teleportCooldown;
    void Update(){
        if(attack){
            if(GetComponent<Rigidbody>().velocity.y <= 0){
                isFalling = true;
            }
            if(isFalling){
                if(Physics.Raycast(transform.position, -transform.up, (transform.localScale.y/2) + 1)){
                    if(Vector3.Distance(cc.transform.position, transform.position) <= 5){
                        cc.TakeDagame(specialDamage, this.gameObject);
                    }
                    cc = null;
                    attack = false;
                    isFalling = false;
                }
            }
        }

        if(isIntangable){
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            if(startTime + intangableTime <= Time.time){
                IntangableOff();
                if(startTime + intangableTime*3 <= Time.time){
                    isIntangable = false;
                }
            }
        }

        if(hasTeleported){
            if(telportStartTime + teleportCooldown <= Time.time){
                hasTeleported = false;
            }
        }
    }

    //Abilities
    public void IntangableOn(float _intangableTime){
        Debug.Log("e");
        intangableTime = _intangableTime;
        startTime = Time.time;
        isIntangable = true;
        Debug.Log("f");

        Renderer r = GetComponent<Renderer>();
        r.material.color = new Color(r.material.color.r, r.material.color.g, r.material.color.b, 0.2f);
        GetComponent<EnemyController>().enabled = false;
    }

    public void IntangableOff(){
        Renderer r = GetComponent<Renderer>();
        r.material.color = new Color(r.material.color.r, r.material.color.g, r.material.color.b, 1);
        GetComponent<EnemyController>().enabled = true;
    }

    public void ShieldSelf(){
        GameObject shield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        shield.transform.parent = transform;

        GetComponent<EnemyController>().GetShield(50, shield);
    }

    public void ShieldAllies(){
        Collider[] otherEnemies = Physics.OverlapSphere(transform.position, shieldAlliesRange, enemies);
        foreach(Collider c in otherEnemies){
            GameObject shield = Instantiate(shieldPrefab, c.transform.position, Quaternion.identity);
            shield.transform.parent = c.transform;

            c.GetComponent<EnemyController>().GetShield(25, shield);
        }
    }

    public void Teleport(int _teleportCooldown){
        hasTeleported = true;
        teleportCooldown = _teleportCooldown;
        telportStartTime = Time.time;
    }


    //Attack
    public void Attack(CharacterController _cc){
        cc = _cc;
        GetComponent<Rigidbody>().AddForce(transform.forward * 2, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(transform.up * 6, ForceMode.Impulse);
        attack = true;
    }
}
