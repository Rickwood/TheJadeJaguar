using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCharacterController : MonoBehaviour {

    public float speed;

    private float pressedTimer = 0f;
    private float moneyCollected = 0f;
    private bool canPickUp = false;
    private GameObject painting;

	// Use this for initialization
	void Start () {
        speed = 3.0f;
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

        if (canPickUp == true && Input.GetKey("e"))
        {
            pressedTimer += Time.deltaTime;

            if (pressedTimer >= 3)
            {
                painting.SetActive(false);
                moneyCollected = moneyCollected + 100f;
            }
        }
        else
            pressedTimer = 0f;
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
