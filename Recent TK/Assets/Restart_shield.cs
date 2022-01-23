using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart_shield : MonoBehaviour
{

    public Transform Player;
    public Transform Shield;
    public int _small_shields = 5;

    public float distance_from_player = .5f;

    private float shield_left = 0;
    private float shield_top = 1;
   

    private bool shield_up = false;
    private bool destory_shield = false;
    public float timeLeft ;
    private const float _rotationSpeed = 20f;
    // public Text startText; // used for showing countdown from 3, 2, 1 



    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
   
        if (Input.GetButton("defend"))
        {
            createshield();
           
        }

        if(shield_up)
        {
            GameObject[] shields = GameObject.FindGameObjectsWithTag("jack");
            foreach (GameObject shield in shields)
            {
                float xSpin = Random.Range(0, 360);
                float ySpin = Random.Range(0, 360);
                float zSpin = Random.Range(0, 360);
                shield.transform.Rotate(_rotationSpeed * Time.deltaTime, _rotationSpeed * Time.deltaTime, _rotationSpeed * Time.deltaTime);
            }
        }

        if(Input.GetButton("destroy"))
        {

           if(shield_up)
            {
                GameObject[] shields = GameObject.FindGameObjectsWithTag("jack");
                foreach (GameObject shield in shields) 
                {
                    shield.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    shield.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    shield.transform.rotation = Random.rotation;
                   
                }
                timeLeft = 3.0f;
                destory_shield = true;
                shield_up = false;
            }
          
        }

        if(destory_shield)
        { 
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                GameObject[] shields = GameObject.FindGameObjectsWithTag("jack");
                foreach (GameObject shield in shields)
                {
                    GameObject.Destroy(shield);
                }
                timeLeft = 3.0f;
                destory_shield = false;
               // shield_up = false;
            }
        }


        
    }

    void createshield()
    {
        if(shield_up)
        {
            return;
        }

        float x = shield_left;
        int shieldcount = 0;
        for (int i = 0; i < _small_shields; i++)
        {
            float y = shield_top;

            for (int k = 0; k < _small_shields; k++)
            {
                string objectName = "Shield"+shieldcount;
                Transform new_shield;
                new_shield = Instantiate(Shield, Player.transform.position, Player.transform.rotation) as Transform;
                new_shield.name = objectName;
                new_shield.tag = "jack";
                new_shield.transform.Translate(new Vector3(x - .5f ,y,distance_from_player));
                y -= 0.25f;

                new_shield.transform.SetParent(Player);
                new_shield.gameObject.GetComponent<Rigidbody>().useGravity = false;
                new_shield.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                float xSpin = Random.Range(0, 360);
                float ySpin = Random.Range(0, 360);
                float zSpin = Random.Range(0, 360);
                new_shield.transform.rotation = Quaternion.Euler(xSpin, ySpin, zSpin);
                shieldcount++;
            }
            x += 0.2f;
     
        }
        shield_up = true;
    }
}
