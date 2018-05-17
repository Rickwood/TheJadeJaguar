using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPersonCharacterController : MonoBehaviour {

    public float speed;

    public float itemsCollected = 0f;
    public float pressedTimer = 0f;
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

        if (itemsCollected < 5)
        {
            if (canPickUp == true && Input.GetKey("e"))
            {
                pressedTimer += Time.deltaTime;

                if (pressedTimer > 3)
                {
                    painting.SetActive(false);
                    canPickUp = false;
                    moneyCollected = moneyCollected + 100f;
                    itemsCollected++;
                }
            }
            else
                pressedTimer = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Painting"))
        {
            canPickUp = true;
            painting = other.gameObject;

		}else if(other.gameObject.CompareTag("EXIT")){
			SceneManager.LoadScene (2); //scene 2 is the victory screen
		}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Painting"))
            canPickUp = false;
    }
}
