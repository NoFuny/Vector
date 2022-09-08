using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ������������ ��� �������� ���������� � ���������� ��������
public class Barrier : MonoBehaviour
{
    [SerializeField] GameObject obstaclesPrefab;
    [SerializeField] GameObject parentBarrier;
    private ArbitraryPlane arbitraryPlane;

    private void Awake()
    {
        arbitraryPlane = FindObjectOfType<ArbitraryPlane>();
    }

    //�������� ����� ����� �� �����������
    public void AddBarrier(Vector2 positionBarier)
    {
        if (Vector2.Distance(arbitraryPlane.ArbitraryVectorX,arbitraryPlane.ArbitraryVectorY) !=0)
        {
            var coordinates = arbitraryPlane.GetArbitraryPoint(positionBarier);
            var a = Instantiate(obstaclesPrefab, parentBarrier.transform);
            a.transform.position = coordinates;
        }
        
    }

    //������� ��� ������� � �����������
    public void CleanBarrier()
    {
        var childens = parentBarrier.GetComponentsInChildren<Rigidbody>();
        foreach (var child in childens)
        {
            Destroy(child.gameObject);
        }
    }



}
