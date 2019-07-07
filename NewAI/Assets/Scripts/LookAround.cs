using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour {
    public CharacterController characterController;
    public LayerMask enemyLayer;
    public Camera cam;
    public float camSwitchSpeed;

    [SerializeField]
    public float sensitivity = 5.0f;
    [SerializeField]
    public float smoothing = 2.0f;
 
    public GameObject player;
    private Vector2 mouseLook;
    private Vector2 smoothness;
    private bool pos;
    private float startTime;

    private Vector3 startMarker;
    private Vector3 endMarker;

    private EnemyController enemyInFront;
    public Flower currentFlower;

	void Start () {
        startTime = Time.time;
        player = this.transform.parent.gameObject;
        startMarker = cam.transform.localPosition;
        endMarker = cam.transform.localPosition;
	}
	
	void Update () {
        Vector2 mouseData = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseData = Vector2.Scale(mouseData, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
      
        smoothness.x = Mathf.Lerp(smoothness.x, mouseData.x, 1f / smoothing);
        smoothness.y = Mathf.Lerp(smoothness.y, mouseData.y, 1f / smoothing);
        mouseLook += smoothness;
        mouseLook.y = Mathf.Clamp(mouseLook.y, -60, 90);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);

        //change cam pos
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if(pos){
                startTime = Time.time;
                startMarker = new Vector3(1, cam.transform.localPosition.y, cam.transform.localPosition.z);
                endMarker = new Vector3(-1, cam.transform.localPosition.y, cam.transform.localPosition.z);
                pos = false;
            } else {
                startTime = Time.time;
                startMarker = new Vector3(-1, cam.transform.localPosition.y, cam.transform.localPosition.z);
                endMarker = new Vector3(1, cam.transform.localPosition.y, cam.transform.localPosition.z);
                pos = true;
            }
        }
    
        float distCovered = (Time.time - startTime) * camSwitchSpeed;
        float fracJourney = distCovered / 1.6f;
        cam.transform.localPosition = Vector3.Lerp(startMarker, endMarker, fracJourney);

        //if croshair is over enemy, show it's health
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 10, enemyLayer)){
            if(hit.transform.gameObject.GetComponent<EnemyController>()){
                if(enemyInFront != null){
                    enemyInFront.HUD.SetActive(false);
                }

                enemyInFront = hit.transform.gameObject.GetComponent<EnemyController>();
                enemyInFront.HUD.SetActive(true);
            }
        } else {
            if(enemyInFront != null){
                enemyInFront.HUD.SetActive(false);
            }
            enemyInFront = null;
        }

        //PickupItem
        if (Input.GetKeyDown(KeyCode.F) && currentFlower != null){
            PickUpItem();
        }

        //if croshair is over flower, get it's stats
        RaycastHit hitFlower;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitFlower, 10)){
            if(hitFlower.transform.gameObject.GetComponent<Flower>()){
                currentFlower = hitFlower.transform.gameObject.GetComponent<Flower>();
                currentFlower.HUD.SetActive(true);
            } else {
                if(currentFlower != null){
                    currentFlower.HUD.SetActive(false);
                }
                currentFlower = null;
            }
        } else {
            if(currentFlower != null){
                currentFlower.HUD.SetActive(false);
            }
            currentFlower = null;
        }

         //Quit game
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    void PickUpItem(){
        characterController.GetFlower(currentFlower);
        Destroy(currentFlower.gameObject);
        currentFlower = null;
    }
}
