using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ������������ ��� �������� ���������� � ���������� ��������
public class Barrier : MonoBehaviour
{
    [SerializeField] GameObject obstaclesPrefab;
    [SerializeField] GameObject parentBarrier;
    [SerializeField] private ArbitraryPlane arbitraryPlane;


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
