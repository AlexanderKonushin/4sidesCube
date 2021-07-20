using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    public bool Fetch(float z) //��������, �������� �� ������ ������ ���� ���� �� ����������� ����������
    {
        bool result = false;

        if (z > transform.position.z + 100f)
        {
            result = true; //���� ������ �������� �� 100f �� �����, �� ������������ true
        }

        return result;
    }

    public void Delete()
    {
        Destroy(gameObject); //�������� �����
    }
}
