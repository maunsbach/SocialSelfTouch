using System.Data;
using UnityEngine;

public class ColliderConnector : MonoBehaviour
{
    public DataHand Hand = new DataHand();

    void Awake()
    {
        GameObject[] sphereColliderObjects = GameObject.FindGameObjectsWithTag("SphereCollider");

        DataJoints joint;
        DataRow row;
        DataColumn column;

        for (int i = 0; i < sphereColliderObjects.Length; i++)
        {
            SphereID iD = sphereColliderObjects[i].GetComponent<SphereID>();


            joint = Hand[iD.HandLimb];
            row = joint[iD.HandJoint];
            if (row.ContainsKey(iD.Row))
            {
                column = row[iD.Row];
            }
            else
            {
                row.Add(iD.Row, new DataColumn());
                column = row[iD.Row];
            }
            column.Add(iD.Column, sphereColliderObjects[i]);
        }
    }

    public Vector3 GetColliderPosition(HandLimbs handLimb, HandJoints handJoints, int row, int column)
    {
        return Hand[handLimb][handJoints][row][column].transform.position;
    }

    public GameObject GetColliderGameObject(HandLimbs handLimb, HandJoints handJoints, int row, int column)
    {
        return Hand[handLimb][handJoints][row][column];
    }
}
