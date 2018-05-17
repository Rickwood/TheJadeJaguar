using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstPersonCharacterController : MonoBehaviour {

    public Text inventoryText;
    public Text scoreText;

    private float inventoryMax = 8f;
    private float speed;
    private float itemsCollected = 0f;
    private float pressedTimer = 3f;
    private float moneyCollected = 0f;
    private bool canPickUp = false;
    private GameObject painting;

    public Image timerBar;

	// Use this for initialization
	void Start () {
        speed = 3.0f;
        Cursor.lockState = CursorLockMode.Locked;
        inventoryText.text = "Inventory: " + itemsCollected + "/" + inventoryMax;
        scoreText.text = "Dolla's!: $" + moneyCollected;
        timerBar.enabled = false;
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

        if (itemsCollected < inventoryMax)
        {
            if (canPickUp == true && Input.GetKey("e"))
            {
                timerBar.enabled = true;
                pressedTimer -= Time.deltaTime;
                timerBar.fillAmount = pressedTimer / 3;

                if (pressedTimer <= 0)
                {
                    painting.SetActive(false);
                    canPickUp = false;
                    timerBar.enabled = false;
                    moneyCollected = moneyCollected + 100f;
                    itemsCollected++;
                    updateInventory();
                    updateScore();
                }
            }
            else
            {
                timerBar.enabled = false;
                pressedTimer = 3f;
            }
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

    void updateInventory()
    {
        inventoryText.text = "Inventory: " + itemsCollected + "/" + inventoryMax;
    }

    void updateScore()
    {
        scoreText.text = "Dolla's!: $" + moneyCollected;
    }
}
