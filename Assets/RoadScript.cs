using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour
{
    //Road blocks
    public List<RoadParts> blocks;     //Road blocks collection
    public GameObject roadBlocksPrefab;

    //Player prefab
    public GameObject playerPrefab;
    public float playerVision;

    // Start is called before the first frame update
    void Start()
    {
        blocks.Add(roadBlocksPrefab.GetComponent<RoadParts>().InstantiateRoadPart(transform.position));
    }

    // Update is called once per frame
    //TODO: ����������� � ���������� � �������� � � ����� ��������
    void Update()
    {
        float playerZPosition = playerPrefab.GetComponent<PlayerScript>().transform.position.z;

        float lastRoadBlockZPosition = blocks[blocks.Count - 1].transform.position.z;

        if (playerZPosition > lastRoadBlockZPosition - playerVision) 
        {
            //Create new road block
            float newRoadBlockPosition = lastRoadBlockZPosition + blocks[blocks.Count - 1].GetBlockLenght();
            RoadParts block = roadBlocksPrefab.GetComponent<RoadParts>().InstantiateRoadPart(new Vector3(transform.position.x, transform.position.y, newRoadBlockPosition));
            blocks.Add(block); //���������� ����� � ���������

        }

        foreach (RoadParts block in blocks)
        {
            bool farEnough = block.GetComponent<RoadParts>().FarEnough(playerZPosition); //��������, ������� �� ����� ���� ����

            if (farEnough) //���� �������
            {
                blocks.Remove(block); //�������� ����� �� ���������
                block.GetComponent<RoadParts>().Delete(); //�������� ����� �� �����
            }
        }
    }

    public void ChangeSide(float z) 
    {
        foreach (RoadParts block in blocks) 
        {
            if ( z > block.transform.position.z && z < block.transform.position.z + block.RoadBlockLenght ) 
            {
                block.transform.Rotate(0, 0, 90);
            }
        }

    }

}
