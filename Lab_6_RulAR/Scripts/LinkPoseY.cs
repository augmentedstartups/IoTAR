using UnityEngine;
using System.Collections;

public class LinkPoseY : MonoBehaviour
{

    public GameObject GamePose1, GamePose2;
    public Vector3 position2, position1;
    public Vector3 pos_tempY;
    Vector3 position_difference, size_temp;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        position_difference = transform.position;
        size_temp = transform.localScale;
        position1 = GamePose1.GetComponent<Pose>().pos;
        position2 = GamePose2.GetComponent<Pose2>().pos2;

        float distance = position1.y - position2.y;
        size_temp.z = distance / 2;
        pos_tempY = position2;

        transform.position = pos_tempY;
        transform.localScale = size_temp;
    }


}
