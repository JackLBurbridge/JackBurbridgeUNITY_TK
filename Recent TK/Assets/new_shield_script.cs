using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_shield_script : MonoBehaviour
{

    
    public Transform shield;
    public Transform start_shield;
    public Transform  end_shield;

    private bool _move = false;
    private GameObject new_shield;
    private bool shield_create = false ;
    public float speed = .1f;



    public int _small_shields = 10;

    public float distance_from_player = 10;

    public GameObject Player;
    private int generatedShieldCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Vector3 target = GameObject.Find("end_shield").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        if (Input.GetButton("defend"))
        {
            createshield();
        }

   

    }

    void createshield()
    {
        for( int i = 0; i < _small_shields; i++)
        {
            string objectName = "Sheild" + generatedShieldCount;
            Transform new_shield;
            new_shield = Instantiate(shield, end_shield) as Transform;
            new_shield.name = objectName;

            new_shield.transform.Translate(new Vector3(distance_from_player, 5, 0));

        }
    }
}
