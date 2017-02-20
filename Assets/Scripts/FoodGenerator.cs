using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour {
    public GameObject Head;
    public GameObject BorderUp;
    public GameObject BorderDown;
    public GameObject BorderLeft;
    public GameObject BorderRight;
    public GameObject Food;
    public GameObject poison;
    int poisonCounter = 0;
    int poisonTime;
    float FoodDistance;
    float HeadDistance;
    GameObject GeneratedPoison;
    GameObject GeneratedFood;

    // Use this for initialization
    void Start () {
        GenerateFood();
        poisonTime= Random.Range(0,5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void GenerateFood() {
    GeneratedFood=    Instantiate(Food, new Vector3(Random.Range(BorderLeft.transform.position.x+2f, BorderRight.transform.position.x-2f), Random.Range(BorderUp.transform.position.y-2f, BorderDown.transform.position.y+2f), 0), Quaternion.identity) as GameObject;
        if (poisonTime==poisonCounter) {
            poisonTime = Random.Range(0, 5);
            poisonCounter = 0;
            Destroy(GeneratedPoison);
            do
            {
                Destroy(GeneratedPoison);
                GeneratedPoison = Instantiate(poison, new Vector3(Random.Range(BorderLeft.transform.position.x + 2f, BorderRight.transform.position.x - 2f), Random.Range(BorderUp.transform.position.y - 2f, BorderDown.transform.position.y + 2f), 0), Quaternion.identity) as GameObject;
                HeadDistance = Vector3.Distance(GeneratedPoison.transform.position,Head.transform.position);
                 FoodDistance = Vector3.Distance(GeneratedPoison.transform.position, GeneratedFood.transform.position);


            } while (HeadDistance<5f|| FoodDistance<5f);
            GeneratedPoison.GetComponent<SphereCollider>().enabled = true;
            }
        poisonCounter++;
        if (poisonCounter>=5) {
            poisonCounter = 0;

        }
    }
}
