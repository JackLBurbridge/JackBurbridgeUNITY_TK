
using System.Collections;
using UnityEngine;
 
public class Pull : MonoBehaviour
{

    [Tooltip("Hand that is the target destination of the pull")]
    public Transform hand;

    [Tooltip("Tag that is used to determine if an object is pullable or not")]
    public string pullableTag;

    [Tooltip("Force modifier, tweak it to suit your needs")]
    public float modifier = 1.0f;

    [Tooltip("The direction of the force that is pulling the object")]
    Vector3 pullForce;

    [Tooltip("Once an object is in the hand, save it to this variable")]
    public Transform heldObject;

    [Tooltip("The distance threshold in which the object is considered pulled to the hand")]
    public float positionDistanceThreshold;

    [Tooltip("The distance threshold in which the object's velocity is set to maximum")]
    public float velocityDistanceThreshold;

    [Tooltip("The maximum velocity of the object being pulled")]
    public float maxVelocity;

    [Tooltip("The velocity at which the object is thrown")]
    public float throwVelocity;

    void Update()
    {

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag.Equals(pullableTag))
                {
                    StartCoroutine(PullObject(hit.transform));
                }
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            if (heldObject != null)
            {
                heldObject.transform.parent = null;
                heldObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                heldObject.GetComponent<Rigidbody>().velocity = transform.forward * throwVelocity;
                heldObject = null;
            }
        }
    }

    IEnumerator PullObject(Transform t)
    {
        Rigidbody r = t.GetComponent<Rigidbody>();
        while (true)
        {

            //  If the player right-clicks, stop pulling
            if (Input.GetMouseButtonDown(1))
            {
                break;
            }
            float distanceToHand = Vector3.Distance(t.position, hand.position);
            /*
                If the object is withihn the distance threshold, consider it pulled all the way and:
                1) Set the object's position to the hand position
                2) make it's parent be the hand object
                3) Constrain its movement, but not rotation
                4) Set its velocity to be the forward vector of the camera * the throw velocity
                5) Break out of the coroutine
            */
            if (distanceToHand < positionDistanceThreshold)
            {
                t.position = hand.position;
                t.parent = hand;
                r.constraints = RigidbodyConstraints.FreezePosition;
                heldObject = t;

                break;
            }

            //  Calculate the pull direction vector
            Vector3 pullDirection = hand.position - t.position;

            //  Normalize it and multiply by the force modifier
            pullForce = pullDirection.normalized * modifier;

            /*
                Check if the velocity magnitude of the object is less than the maximum velocity
                and
                check if the distance to hand is greater than the distance threshold
            */
            if (r.velocity.magnitude < maxVelocity && distanceToHand > velocityDistanceThreshold)
            {

                //  Add force that takes the object's mass into account
                r.AddForce(pullForce, ForceMode.Force);
            }
            else
            {

                // Set a constant velocity to the object
                r.velocity = pullDirection.normalized * maxVelocity;
            }

            yield return null;
        }
    }
}