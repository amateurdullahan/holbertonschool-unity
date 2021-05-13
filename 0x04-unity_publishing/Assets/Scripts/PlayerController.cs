using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerBody;
    private Vector3 inputVector;
    public float speed = 100f;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Text winloseText;
    public Image winloseBG;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0, Input.GetAxisRaw("Vertical") * speed);
        playerBody.velocity = inputVector;
    }

    // gameplay
    void Update()
    {
        SetScoreText();
        SetHealthText();
        if (health == 0)
        {
            // Debug.Log("Game Over!");
            winloseBG.gameObject.SetActive(true);
            winloseBG.GetComponent<Image>().color = Color.red;
            winloseText.color = Color.white;
            winloseText.text = "Game Over!";
            StartCoroutine(LoadScene(3));
        }

        // Open menu with esc
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    SceneManager.LoadScene(0);
        //}
    }

    // scoring
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            score++;
            Object.Destroy(other.gameObject);
        }
        if (other.tag == "Trap")
        {
            health--;
        }
        if (other.tag == "Goal")
        {
            winloseBG.gameObject.SetActive(true);
            winloseBG.GetComponent<Image>().color = Color.green;
            winloseText.color = Color.black;
            winloseText.text = "You Win!";
            StartCoroutine(LoadScene(3));
        }
    }

    void SetScoreText()
    {
        scoreText.text = $"Score: {score}";
    }

    void SetHealthText()
    {
        healthText.text = $"Health: {health}";
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
