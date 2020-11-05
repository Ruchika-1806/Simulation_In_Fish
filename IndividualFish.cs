using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualFish : MonoBehaviour
{
	public float Speed = 0.7001f; //speed of the fish
    float TurnSpeed = 0.40f; // how fast fish will turn
    Vector3 AverageHeading; //Flock should head towards the average heading of the group.
    Vector3 AveragePosition; //Average position of group
    float DistanceOfNeighbour = 3.0f; //Distance between two fish to become member of the flock

    bool Return = false;//This will set to true when the fish will reach the boundary of the space defined in Dimensions variable

    // Start is called before the first frame update
    void Start()
    {
        Speed = Random.Range(0.4f,1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, Vector3.zero) >= School.Dimensions)//If this is greater than the dimensions of the space, then the school should turn back
                Return = true;
        else
                Return = false;

        if(Return)
        {

            Vector3 Direction = Vector3.zero - transform.position;//Turn the other way

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                        Quaternion.LookRotation(Direction),
                                        TurnSpeed * Time.deltaTime);
            Speed = Random.Range(0.5f,0.9f);
        }

        else
        {
           // if(Random.Range(0,4) <1)//Not applying the rules frequentlty
                ApplyRules();
        }

        transform.Translate(0,0, Time.deltaTime * Speed);//This moves the fish forward
    }



    void ApplyRules()
    {
        GameObject[] GameObjects;
        GameObjects = School.AllFish;


        Vector3 GroupCentre = Vector3.zero;  //centre of group and alligning it ti the centre
        Vector3 Avoid = Vector3.zero;  //avoidance vector, fish must avoid collison with someone that is near
        Vector3 Target = School.Target;//Referring to the Target of the school

        float GroupSpeed = 0.25f; //group speed

        float Distance;

        int GroupMembers = 0;//Intially size of the group is zero. We keep on adding members later to the group depending upon the distance between the fishes

        foreach (GameObject ob in GameObjects)
        {
            if(ob != this.gameObject)
            {
                Distance = Vector3.Distance(ob.transform.position, this.transform.position);
                if(Distance <= DistanceOfNeighbour)
                {
                     GroupMembers += 1; //Make that fish member of the group
                     GroupCentre += ob.transform.position; //Adding up the centres when that fish becomes part of the group

                     if(Distance < 1.5f) // Setting minimum distance for collison
                         Avoid += (this.transform.position - ob.transform.position);//This vector will face in other direction to avoid collision
                    

                     //For calculating average group speed, we need to add the speed of the neighbouring fish to the group
                      IndividualFish Fish = ob.GetComponent<IndividualFish>();//Calculating the group speed by adding the speed of the neighbouring fish
                      GroupSpeed += Fish.Speed;            

                }
            }

        }


        if(GroupMembers > 0)
        {
            //We need to calculate the group centre
            GroupCentre = GroupCentre/GroupMembers + (Target - this.transform.position);
            Speed = GroupSpeed/GroupMembers;

             Vector3 Direction = (GroupCentre + Avoid) - transform.position;//For getting the position in which fish must turn
            
             if(Direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                        Quaternion.LookRotation(Direction),
                                        TurnSpeed * Time.deltaTime);
            
        }


    }
}
