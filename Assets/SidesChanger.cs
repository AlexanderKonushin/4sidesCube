using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidesChanger : MonoBehaviour
{
    private enum ESides : int
    {  
        bottom = 1,
        left = 2,
        right = 3,
        top = 4
    }

    
    [SerializeField] private ESides currentSide;

    //����������, �� ������� ����� ���������� ������
    [SerializeField] private Vector3 bottomSideCoordinates;
    [SerializeField] private Vector3 leftSideCoordinates;
    [SerializeField] private Vector3 rightSideCoordinates;
    [SerializeField] private Vector3 topSideCoordinates;

    //����� ������ ����� �������
    [SerializeField] private float changeSideCooldownSec;
    [SerializeField] private float fullSideChangeTime;


    private Vector3 moveTowardsCoordinates; //����������, � ������� ��������� �����
    private float moveTowardsStep;          //��� �� �������
    private float moveTowardsAngle;         //�������  �� �������
    private float nextSideChange;           //����� ����� ����� ����� �������� �������
    private float AngleLeft;                //���� ���������� ��� ��������

    private const float kRotateAngle = -90; //���� ��������

    //TODO: ������� ��������� ���������� � ������������ � ��������� ��������
    // Start is called before the first frame update
    private void Start()
    {
        moveTowardsCoordinates = bottomSideCoordinates;
    }

    // Update is called once per frame
    private void Update() {

        if (Input.GetButton("Fire1") && Time.time > nextSideChange)
        {
            nextSideChange = Time.time + changeSideCooldownSec;
            ChangeSide();
        }

        if (Vector3.Distance(transform.position, moveTowardsCoordinates) > 0.0001)
        {
            //���������� ������ �� ����� ����� � �������
            transform.position = Vector3.MoveTowards(transform.position, moveTowardsCoordinates,
                moveTowardsStep * Time.deltaTime);
            transform.Rotate(0, 0, moveTowardsAngle * Time.deltaTime);
            AngleLeft -= moveTowardsAngle * Time.deltaTime;
        }
        else
        {   
            //��� ����, ����� ���� ��� ������ � ����� ��������
            if (AngleLeft != 0)
            {
                transform.Rotate(0, 0, AngleLeft);
                AngleLeft = 0;
            }
        }

    }

    public void ChangeSide()
    {
        switch (currentSide) 
        {
            case (ESides.bottom): 
                {
                    currentSide = ESides.left;
                    moveTowardsCoordinates = leftSideCoordinates;
                    break;
                }
            case (ESides.left):
                {
                    currentSide = ESides.top;
                    moveTowardsCoordinates = topSideCoordinates;
                    break;
                }
            case (ESides.top):
                {
                    currentSide = ESides.right;
                    moveTowardsCoordinates = rightSideCoordinates;
                    break;
                }
            case (ESides.right):
                {
                    currentSide = ESides.bottom;
                    moveTowardsCoordinates = bottomSideCoordinates;
                    break;
                }
        }

        moveTowardsStep = Vector3.Distance(transform.position, moveTowardsCoordinates) / fullSideChangeTime;
        moveTowardsAngle = kRotateAngle / fullSideChangeTime;
        AngleLeft = -90;

    }
}
