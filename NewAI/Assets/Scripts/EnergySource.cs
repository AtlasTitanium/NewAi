using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySource : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float energyStrenght;
    public bool isPlayer;
    public Vector2 pulseStrenghtBetween;
    public ParticleSystem circle;

    private float startTime;
    private bool sw;
    void Start(){
        startTime = Time.time;
    }
    void Update(){
        if(isPlayer){
            if(energyStrenght <= Random.Range(pulseStrenghtBetween.x-2 , pulseStrenghtBetween.x+2)){
                sw = false;
            }
            if(energyStrenght >= Random.Range(pulseStrenghtBetween.y-2 , pulseStrenghtBetween.y+2)){
                sw = true;
            }

            if(sw){
                energyStrenght -= Time.deltaTime / 2;
            } else {
                energyStrenght += Time.deltaTime / 2;
            }

        } else {
            if(energyStrenght <= 0){
                energyStrenght = 0;
            } else {
                energyStrenght -= Mathf.Pow((Time.time - startTime)/(energyStrenght+20), 2.0f) * Time.deltaTime;
            }
        }
        

        if(Physics.CheckSphere(transform.position, energyStrenght, enemyLayer)){
            Collider[] colliders = Physics.OverlapSphere(transform.position, energyStrenght, enemyLayer);
            foreach(Collider c in colliders){
                if(c.GetComponent<EnemyController>()){
                    c.GetComponent<EnemyController>().GetEnergySource(transform);
                }
            }
        }

        //Show radiation Circle
        ParticleSystem.ShapeModule sm = circle.shape;
        sm.radius = energyStrenght * 10;

        //make circle even
        circle.transform.eulerAngles = new Vector3(-90,0,0);
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, energyStrenght);
    }
}
