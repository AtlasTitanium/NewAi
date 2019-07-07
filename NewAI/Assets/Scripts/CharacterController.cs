using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

    [Header("Player stats")]
    public float speed = 10.0f;
    public float jumpSpeed = 5.0f;
    public float attackSpeed = 0.1f;
    public int hp = 100;
    public int knockBackStrenght = 4;
    public int punchEnergy = 10;
    public int amountOfESources = 0;
    public int energy = 100;

    [Header("UI control")]
    public Slider healthBar;
    public Slider energyBar;
    public Text hpCounter;
    public Text energyCounter;

    [Header("Others")]
    public ParticleSystem punchParticle;
    public ParticleSystem circleOfEnergy;
    public LayerMask enemyLayer;
    public GameObject energySourcePrefab;

    private float translation;
    private float straffe;
    private Rigidbody rb;
    private bool attack1;
    private float curTime;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
    }
	
	void Update () {
        //Move around
        translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(straffe, 0, translation);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space)) {
            if(Physics.Raycast(transform.position, -transform.up, (transform.localScale.y/2) + 1)){
                rb.AddForce(transform.up * (jumpSpeed * 100) * Time.deltaTime, ForceMode.Impulse);
            }
        }

        //Normal attack
        if (Input.GetMouseButtonDown(0) && !attack1 && energy > 0){
            curTime = Time.time + attackSpeed;
            attack1 = true;

            Slash();
        }

        if(attack1){
            if(curTime <= Time.time){
                attack1 = false;
            }
            transform.Translate(0,0,(speed/2) * Time.deltaTime);
        }

        //Throw attack
        if (Input.GetMouseButtonDown(1) && amountOfESources >= 1){
            ThrowItem();
        }

        //set the punch particle at the right position
        punchParticle.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 6;

        //If Hp is empty, game over
        if(hp <= 0){
            GetComponent<MeshRenderer>().enabled = false;
            Debug.Log("game over!!");
            this.enabled = false;
        }

        //Show radiation Circle
        ParticleSystem.ShapeModule sm = circleOfEnergy.shape;
        sm.radius = GetComponent<EnergySource>().energyStrenght * 10;

        //Send right HUD stats
        healthBar.value = hp;
        hpCounter.text = hp + "/100";   
        energyBar.value = energy;
        energyCounter.text = energy + "/100"; 
    }

    public void TakeDagame(int damage, GameObject damager){
        hp -= damage;

        GetComponent<Rigidbody>().AddForce(damager.transform.forward * knockBackStrenght, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(transform.up * knockBackStrenght/2, ForceMode.Impulse);
    }

    public void Slash(){
        energy -= 10;

        punchParticle.Play();
        GetComponent<EnergySource>().energyStrenght = punchEnergy*2;

        Collider[] colliders = Physics.OverlapSphere(Camera.main.transform.position + Camera.main.transform.forward * 6, 2, enemyLayer);   
        foreach(Collider c in colliders){
            c.gameObject.GetComponent<EnemyController>().TakeDagame(10, knockBackStrenght);
        } 
    }

    public void ThrowItem(){
        amountOfESources--;
        GameObject source = Instantiate(energySourcePrefab, transform.position + transform.forward * 2, Quaternion.identity);
        source.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 15, ForceMode.Impulse);
    }

    public void GetFlower(Flower flower){
        switch(flower.flowerType){
            case FlowerType.Energy:
                energy += flower.getEnergy;
            break;
            case FlowerType.Heal:
                hp += flower.healHp;
            break;
            case FlowerType.Source:
                amountOfESources += flower.amountOfSources;
            break;
        }
    }

    //Debug draw gimos
    void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Camera.main.transform.position + Camera.main.transform.forward * 6, 2);
    }
}
