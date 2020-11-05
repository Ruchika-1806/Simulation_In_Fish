using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School : MonoBehaviour
{
	public GameObject Prefab; //Creating a reference to the prefab
	// A prefab is a template for creating new instances of object in the scene


	public GameObject Prefab_for_Goal;
	static int FishCount = 12; //Total number of fish in school
	public static GameObject[] AllFish = new GameObject[FishCount]; //To access each fish later on

	public static float Dimensions = 5.3f; 

	public	static	Vector3	Target = Vector3.zero; //for defining the position of the goal for fish.
	//Generating random behaviour in fish
	//Initially it is set in the middle of the tank


    // Start is called before the first frame update
    //It instantiates the Prefab at the beginning 
    void Start()//creating the flock of the fishes 
    {
        for(int i=0; i<FishCount; i++)//looping through each fish
    	{
    		Vector3 Position = new Vector3(Random.Range(-Dimensions,Dimensions),
    			Random.Range(-Dimensions,Dimensions), Random.Range(-Dimensions,Dimensions));//creating a position for the fish. 
    		//Basically the position would be in cube of dimensions 10*10

    		AllFish[i] = (GameObject) Instantiate(Prefab, Position, Quaternion.identity); //Instanting each fish
    	}
    }

    // Update is called once per frame
    void Update()
    {
    	if(Random.Range(0, 10000) < 30)// For changing the position of the target randomly.
    	//Fish move towards the target. This number can be increased or decreased.
        {
        	Target = new Vector3(Random.Range(-Dimensions,Dimensions),
        		Random.Range(-Dimensions,Dimensions), Random.Range(-Dimensions,Dimensions));

        	Prefab_for_Goal.transform.position = Target;
        }
        
    }
}
