using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class Head : MonoBehaviour {
    public Text ScoreText;
    public GameObject GameOverScreen;
    public GameObject FoodGeneratorObj;
    public GameObject RedScreen;
    public GameObject[] snakePartsArray = new GameObject[2];
    public GameObject MyChild;
    public GameObject partPrefab;
    public AudioSource DeadSound;
    public AudioSource FoodSound;
    public ArrayList SnakeParts = new ArrayList();
    int score = 0;
    int VerticalSign = 1;
    int HorizontalSign = 1;
    int randomStartDir;
    float VerticalSpeed = 0;
    float HorizontalSpeed = 0;
    float speed;
    bool VerticalCheck = false;
    bool HorizontalCheck = false;
    GameObject GeneratedPart;
    FoodGenerator FoodGeneratorScript;

    // Use this for initialization
    void Start() {
        FoodGeneratorScript = FoodGeneratorObj.GetComponent<FoodGenerator>();
        for (int i = 0; i < snakePartsArray.Length; i++) {
            SnakeParts.Add(snakePartsArray[i]);
        }
        randomStartDir = Random.Range(0, 5);
        if (randomStartDir == 1)
        {
            HorizontalSpeed = GameManger.speed;

        }
        else if (randomStartDir == 2)
        {
            HorizontalSpeed = -GameManger.speed;

        }
        else if (randomStartDir == 3)
        {
            VerticalSpeed = GameManger.speed;

        }
        else if (randomStartDir == 4)
        {
            VerticalSpeed = -GameManger.speed;

        }
        else {
            HorizontalSpeed = GameManger.speed;

        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (GameManger.snakeDead == false) {
            this.transform.position = new Vector3(this.transform.position.x + (VerticalSpeed * VerticalSign), this.transform.position.y + (HorizontalSpeed * HorizontalSign), this.transform.position.z);
        }
        if (Input.GetKey("up") && HorizontalCheck == false && !Input.GetKey("right") && !Input.GetKey("left")) {
            HorizontalSign = 1;
            HorizontalSpeed = GameManger.speed;
            VerticalSpeed = 0;
            VerticalCheck = false;
            HorizontalCheck = true;
        } else if (Input.GetKey("down") && HorizontalCheck == false && !Input.GetKey("right") && !Input.GetKey("left")) {
            HorizontalSign = -1;
            HorizontalSpeed = GameManger.speed;
            VerticalSpeed = 0;
            VerticalCheck = false;
            HorizontalCheck = true;
        } else if (Input.GetKey("right") && VerticalCheck == false && !Input.GetKey("up") && !Input.GetKey("down")) {
            VerticalSign = 1;
            VerticalSpeed = GameManger.speed;
            HorizontalSpeed = 0;
            VerticalCheck = true;
            HorizontalCheck = false;
        } else if (Input.GetKey("left") && VerticalCheck == false && !Input.GetKey("up") && !Input.GetKey("down")) {
            VerticalSign = -1;
            VerticalSpeed = GameManger.speed;
            HorizontalSpeed = 0;
            VerticalCheck = true;
            HorizontalCheck = false;
        }



    }
    void AddPart() {
        GeneratedPart = Instantiate(partPrefab, snakePartsArray[snakePartsArray.Length - 1].transform.position, snakePartsArray[snakePartsArray.Length - 1].transform.rotation) as GameObject;
        snakePartsArray = (GameObject[])SnakeParts.ToArray(typeof(GameObject));
        GeneratedPart.gameObject.GetComponent<SnakPart>().MainTarget = snakePartsArray[snakePartsArray.Length - 1];
        SnakeParts.Add(GeneratedPart);

    }
    IEnumerator StartRedScreen()
    {
        RedScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        RedScreen.SetActive(false);
        GameOverScreen.SetActive(true);

    }
    public void replay() {
        SceneManager.LoadScene(0);
    }
    void GameOverBehave() {
        DeadSound.Play();
        StartCoroutine(StartRedScreen());
        Camera.main.GetComponent<CameraShake>().ShakeCamera(1f, 0.05f);
        GameManger.snakeDead = true;
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Collider>().tag=="food"){
            FoodSound.Play();
			Destroy (other.gameObject);
			AddPart ();
            score++;
            ScoreText.text = "Score:"+score;
            FoodGeneratorScript.GenerateFood();

        }
        else if(other.gameObject.GetComponent<Collider>().tag=="part"){
            GameOverBehave();          
			Debug.Log ("part");
		}else if(other.gameObject.GetComponent<Collider>().tag=="poison"){
            GameOverBehave();
            Debug.Log ("poison");
		}
        else if (other.gameObject.GetComponent<Collider>().tag == "Border")
        {
            GameOverBehave();
            Debug.Log("border");
        }
    }

}
