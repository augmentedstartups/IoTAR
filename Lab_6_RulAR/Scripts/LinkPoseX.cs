using UnityEngine;
using System.Collections;

public class LinkPoseX : MonoBehaviour
{

    public GameObject GamePose1, GamePose2;
    public Vector3 position2, position1;
    public Vector3 pos_tempX;
    Vector3 position_difference, size_temp;
    float C, B;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        position_difference = transform.position;
        size_temp = transform.localScale;
        pos_tempX = transform.position;
        position1 = GamePose1.GetComponent<Pose>().pos;
        position2 = GamePose2.GetComponent<Pose2>().pos2;

        float distance = position2.x - position1.x;
        size_temp.z = distance / 2;
        pos_tempX.z = position2.z;
        pos_tempX.x = position1.x;
        pos_tempX.y = position1.y;
        transform.position = pos_tempX;
        transform.localScale = size_temp;
    }


}
