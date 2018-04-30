using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCharacterController : MonoBehaviour {

    public float speed;

    private float moneyCollected = 0f;
    private bool canPickUp = false;
    private GameObject painting;

	// Use this for initialization
	void Start () {
        speed = 4.0f;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        float translation = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation);

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        if (canPickUp == true)
        {
            if (Input.GetKeyDown("e"))
            {
                painting.SetActive(false);
                moneyCollected = moneyCollected + 100f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Painting"))
        {
            canPickUp = true;
            painting = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Painting"))
            canPickUp = false;
    }
}
