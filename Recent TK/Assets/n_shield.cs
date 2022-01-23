using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class n_shield : MonoBehaviour
{

    public Transform Shield;
    public int _small_shields = 10;

    public float distance_from_player = 10;

    public GameObject Player;
    private int generatedShieldCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("defend"))
        {
            createshield();
        }

    }

    void createshield()
    {
        for (int i = 0; i < _small_shields; i++)
        {
            string objectName = "Sheild" + generatedShieldCount;
            Transform new_shield;
            new_shield = Instantiate(Shield) as Transform;
            new_shield.name = objectName;

            new_shield.transform.Translate(new Vector3(distance_from_player, 5, 0));

        }
    }
}
