using UnityEngine;
using System.Collections;

public class Z_Text : MonoBehaviour
{

    public GameObject GamePose1, GamePose2;
    public GameObject LenthZ;
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

        float distance = position2.z - position1.z;
        GetComponent<TextMesh>().text = distance.ToString("f1");
        pos_temp = LenthZ.GetComponent<LinkPoseZ>().position1;
        pos_temp.z = pos_temp.z + distance / 2;
        transform.position = pos_temp;

    }


}
