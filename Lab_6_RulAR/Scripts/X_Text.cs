using UnityEngine;
using System.Collections;

public class X_Text : MonoBehaviour
{

    public GameObject GamePose1, GamePose2;
    public GameObject LenthX;
    public Vector3 position2, position1;
    Vector3 position_difference, pos_temp, size_temp;
    float C, B;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        position_difference = transform.position;
        pos_temp = transform.position;
        position1 = GamePose1.GetComponent<Pose>().pos;
        position2 = GamePose2.GetComponent<Pose2>().pos2;

        float distance = position2.x - position1.x;
        //distance = Mathf.RoundToInt(distance);
        GetComponent<TextMesh>().text = distance.ToString("f1");

        pos_temp = LenthX.GetComponent<LinkPoseX>().pos_tempX;
        pos_temp.x = pos_temp.x + distance/2;
        transform.position = pos_temp;

    }


}
