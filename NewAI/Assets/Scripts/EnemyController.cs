using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InfectedType {Light, Void}
public class EnemyController : MonoBehaviour
{
    public InfectedType infectedType;
    public GameObject HUD;
    public float HUDHeight;
    public Slider healthbar;
    public Slider shieldbar;
    public int shieldHp = 0;
    public int hp = 100;
    public int attackDamage = 5;


    [HideInInspector]
    public Transform closestEnergySource;
    [HideInInspector]
    public GameObject shield;

    void Start(){
        healthbar.maxValue = hp;
    }

    void Update(){
        HUD.transform.LookAt(Camera.main.transform.position);
        HUD.transform.position = new Vector3(transform.position.x, HUDHeight, transform.position.z);

        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);

        if(hp <= 0){
            Destroy(this.gameObject);
            Debug.Log("Killed enemy");
        }

        healthbar.value = hp;  
        
        shieldbar.value = shieldHp;
        if(shieldHp <= 0){
            Destroy(shield);
            shieldbar.gameObject.SetActive(false);
        } else {
            shieldbar.gameObject.SetActive(true);
        }
    }

    public void TakeDagame(int damage, int knockBackStrenght){
        GetComponent<Rigidbody>().AddForce(-HUD.transform.forward * knockBackStrenght, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(transform.up * knockBackStrenght/2, ForceMode.Impulse);

        if(shieldHp > 0){
            shieldHp -= damage;
        } else {
            hp -= damage;
        }
        

        if(infectedType == InfectedType.Light){
            GetComponent<LightInfected>().ChangeVisibility(true);
        }
    }

    public void GetEnergySource(Transform pos){
        if(closestEnergySource != null){
            if(Vector3.Distance(transform.position, closestEnergySource.position) > Vector3.Distance(transform.position, pos.position)){
                closestEnergySource = pos;
            }
        } else {
            closestEnergySource = pos;
        }
    }

    public void DestroyES(){
        Destroy(closestEnergySource.gameObject);
        closestEnergySource = null;
    }

    public void Attack(int damage){
        if(closestEnergySource.GetComponent<CharacterController>()){
            closestEnergySource.GetComponent<CharacterController>().TakeDagame(damage,this.gameObject);
        }   
    }

    public void SpecialAttack(){
        switch(infectedType){
            case InfectedType.Light:
                GetComponent<LightInfected>().Attack(closestEnergySource.GetComponent<CharacterController>());
            break;
            case InfectedType.Void:
                GetComponent<VoidInfected>().Attack(closestEnergySource.GetComponent<CharacterController>());
            break;
        }
    }

    public void Heal(int heal){
        StartCoroutine(HealHP(heal));
    }

    public void GetShield(int shieldHeal, GameObject _shield){
        shieldHp += shieldHeal;

        if(shieldHp >= 100){
            shieldHp = 100;
        }

        shield = _shield;
    }

    IEnumerator HealHP(int heal){
        yield return new WaitForSeconds(5);
        hp += heal;

        if(infectedType == InfectedType.Light){
            GetComponent<LightInfected>().ChangeVisibility(true);
        }

        StopAllCoroutines();
    }
}