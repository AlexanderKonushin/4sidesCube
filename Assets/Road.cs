using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public List<GameObject> blocks; //��������� ���� �������� ������
    public GameObject Car; //�����
    public GameObject roadPrefab; //������ ��������� �����
    public GameObject carPrefab; //������ ������ NPC
    public GameObject coinPrefab; //������ ������
    public int i = 0;
    private System.Random rand = new System.Random(); //��������� ��������� �����

    // Start is called before the first frame update
    void Start()
    {
        var block = Instantiate(roadPrefab, new Vector3(0, 0, 50), Quaternion.identity);
        blocks.Add(block);
    }

    // Update is called once per frame
    void Update()
    {
        float z = Car.GetComponent<CarMoving>().rb.position.z; //��������� ��������� ������

        var last = blocks[blocks.Count - 1]; //����� ��������� �����, ������� ������ ���� �� ������

        if (z > last.transform.position.z - 5 * 50f) //���� ����� �������� � ���������� ����� �����, ��� �� 10 ������
        {
            //��������������� ������ �����
            var block = Instantiate(roadPrefab, new Vector3(last.transform.position.x, last.transform.position.y, last.transform.position.z + 50f), Quaternion.identity);
            block.transform.SetParent(gameObject.transform); //����������� ����� � ������ Road
            blocks.Add(block); //���������� ����� � ���������

        }

        foreach (GameObject block in blocks)
        {
            bool fetched = block.GetComponent<RoadBlock>().Fetch(z); //��������, ������� �� ����� ���� ����

            if (fetched) //���� �������
            {
                blocks.Remove(block); //�������� ����� �� ���������
                block.GetComponent<RoadBlock>().Delete(); //�������� ����� �� �����
            }
        }
    }
}