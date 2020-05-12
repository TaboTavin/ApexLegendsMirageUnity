using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMovementScript : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public Animator cloneAnim;
      
    //Timer
    public float lifeTime = 10.0f;

    //Animator
    const string NOT_WALKING = "NotWalking";

    private void Start()
    {
        cloneAnim.SetBool(NOT_WALKING, false);
        
    }
    private void Update()
    {
        if (lifeTime < 8f)
        {
            movementSpeed = 0f;

            cloneAnim.SetBool(NOT_WALKING, true);

        }

        else if (lifeTime > 8f)
        {
            this.gameObject.GetComponent<Animator>().Play("Run");
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        }
        lifeTime -= Time.deltaTime;
        Destroy(this.gameObject, 20f);

    }
}
