using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip shoutingClip;
    public float speedDampTime = 0.01f;
    public float sensitivityX = 1.0f;
    public bool sneak;
    public bool run;

    private Animator anim;
    private HashIDs hash;
    private bool noBackMove = true;
    private float elapsedTime = 0;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.Find("gameController").GetComponent<HashIDs>();
        anim.SetLayerWeight(1, 1f);

    }

    private void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        float v = Input.GetAxis("Vertical");
        sneak = Input.GetButton("Sneak");
        float mousex = Input.GetAxis("Mouse X");
        float horizontal = Input.GetAxis("Horizontal");
        run = Input.GetButton("Run");
        Rotating(horizontal);
        MovementManager(v, sneak);


    }
    void Update()
    {
        bool shout = Input.GetButton("Attract");
        anim.SetBool(hash.shoutingBool, shout);
        AudioManagement(shout);
    }

    void MovementManager(float vertical, bool sneaking)
    {

        anim.SetBool(hash.runningBool, run);
        anim.SetBool(hash.sneakingBool, sneaking);
        if (vertical > 0)
        {
            anim.SetFloat(hash.speedFloat, 1.5f, speedDampTime, Time.deltaTime);
            anim.SetBool("Backwards", false);

        }

        if(vertical < 0 )
        {
            
            if (noBackMove == true)
            {
                elapsedTime = 0;
                noBackMove = false;

            }
            anim.SetFloat(hash.speedFloat, -1.5f, speedDampTime, Time.deltaTime);
            anim.SetBool("Backwards", true);


            Rigidbody ourBody = this.GetComponent<Rigidbody>();

            float movement = Mathf.Lerp(0f, -.025f, elapsedTime);
            Vector3 moveBack = new Vector3(0.0f, 0.0f, movement);
            moveBack = ourBody.transform.TransformDirection(moveBack);
            ourBody.transform.position += moveBack;


        }

        if(vertical == 0)
        {
            anim.SetFloat(hash.speedFloat, 0.01f);
            anim.SetBool(hash.backwardsBool, false);
        }
    }

    void Rotating(float mouseXInput)
    {
        Rigidbody ourBody = this.GetComponent<Rigidbody>();

        if (mouseXInput != 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, mouseXInput * sensitivityX, 0f);

            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }
    }

    void AudioManagement(bool shout)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().pitch = 0.27f;
                GetComponent<AudioSource>().Play();
            }

            else
            {
                GetComponent<AudioSource>().Stop();
            }
        }

        if (shout)
        {
            AudioSource.PlayClipAtPoint(shoutingClip, transform.position);
        }
    }
}

