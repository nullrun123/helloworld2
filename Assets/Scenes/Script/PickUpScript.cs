using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
   
    public float throwForce = 500f; 
    public float pickUpRange = 5f; 
    private GameObject heldObj; 
    private Rigidbody heldObjRb;
    private bool canDrop = true; 
    private int LayerNumber;

   
    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer"); 
        
    }

    public void OnPickUpButtonClick()
    {
        if (heldObj == null) 
        {
            
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
            {
               
                if (hit.transform.gameObject.tag == "canPickUp")
                {
                    
                    PickUpObject(hit.transform.gameObject);
                }
            }
        }
        else
        {
            if (canDrop == true)
            {
                StopClipping();  
                DropObject();
            }
        }

        if (heldObj != null) 
        {
            MoveObject(); 
        }
    }

  
    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
        {
            heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
            heldObj.layer = LayerNumber; //change the object layer to the holdLayer
           
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    void DropObject()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
    }
    void MoveObject()
    {
        //keep object position the same as the holdPosition position
        heldObj.transform.position = holdPos.transform.position;
    }
    
    void ThrowObject()
    {
        
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }
   void StopClipping() //function only called when dropping/throwing
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position);
        
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
       
        if (hits.Length > 1)
        {
           
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); 
        }
    }
    public void OnThrowButtonClick()
    {
        if (heldObj != null && canDrop == true)
        {
            StopClipping();
            ThrowObject();
        }
    }
}

   