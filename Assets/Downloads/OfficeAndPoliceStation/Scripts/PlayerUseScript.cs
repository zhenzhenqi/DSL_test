using UnityEngine;
using System.Collections;

public class PlayerUseScript : MonoBehaviour {

	public float interactDistance = 20f;
//	public int DSL_test_counter = 0;

//	public static System.Action<int> OnPigEnter;

	void Update () 
	{

       //Debug.Log(Input.GetAxis("PS4_RightJoystick_Horizontal") + "," +Input.GetAxis("PS4_RightJoystick_Verticle") );
      

        if(Input.GetButtonUp("Fire1"))
		{
			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, interactDistance))
			{
				if(hit.collider.CompareTag("Door"))
				{
                    Debug.Log("open door!");
					hit.collider.transform.parent.GetComponent<DoorScript>().ChangeDoorState();
				}
			}
			
		}
	}

//	void OnTriggerExit(Collider col){
//		if (col.gameObject.tag == "DSL_test") {
//			Debug.Log ("pig is triggered by "+col.gameObject.name+" "+col.gameObject.tag);
//			DSL_test_counter += 1;
//			if (OnPigEnter != null)
//				OnPigEnter (DSL_test_counter);
//		} else {
//		}
//	}
//		
}
