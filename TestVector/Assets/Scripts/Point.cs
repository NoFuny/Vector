using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ����������� ����� � �����������
public class Point : MonoBehaviour
{

    [SerializeField] TextMesh textMesh;
    private ArbitraryPlane arbitraryPlane;
    private Vector2 arbitraryVector;
    private Vector2 absoluteVector;
    public Vector2 ArbitraryVector 
    { 
        get 
        { 
            return arbitraryVector; 
        }
        set 
        {
            arbitraryVector = value;
            MovePoint();
            TetxPoin();
        }
    }
    public Vector2 AbsoluteVector {get { return absoluteVector;}}
    
    private void Awake()
    {
        arbitraryPlane = FindObjectOfType<ArbitraryPlane>();
    }

    //����������� ����� � ���������� ����������
    public void MovePoint()
    {
        absoluteVector = arbitraryPlane.GetArbitraryPoint(ArbitraryVector);
        transform.position = absoluteVector;
    }

    //���������� ���������� � ���� ������
    private void TetxPoin()
    {
        textMesh.text = $"({ArbitraryVector.x} ; {ArbitraryVector.y})";
    }

}
