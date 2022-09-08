using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ����������� ���� (����� ����� � ������� ���������)
public class WayPoint : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private GameObject parent;

    private List<Point> listPoint = new List<Point>();

    //���������� ��������� ����� � ������������
    public void RefrashPoint()

    {
        foreach (var point in listPoint)
        {
            point.MovePoint();
        }
    }

    //���������� ����� � �����������
    public void AddPoint(Vector2 point�oordinates)
    {
        var point = Instantiate(pointPrefab, parent.transform).GetComponent<Point>();
        point.ArbitraryVector = point�oordinates;
        listPoint.Add(point);
    }

    //�������� ���� ����� �� ������
    public void CleanPoints()   {listPoint.Clear();}

    //���������� ���������� �����
    public int CountPoint() {return listPoint.Count;}

    //�������� ������ ���������� ������� �����
    public Vector2[] Get�oordinatePoint()
    {
        int count = CountPoint();
        Vector2[] array = new Vector2[count];
        for (int i=0; i<count;i++)
        {
            array[i] = listPoint[i].AbsoluteVector;
        }
        return array;
    }

    //�������� ������ ����������� ������� �����
    public Vector2[] GetArbitrary�oordinatePoint()
    {
        int count = CountPoint();
        Vector2[] array = new Vector2[count];
        for (int i = 0; i < count; i++)
        {
            array[i] = listPoint[i].ArbitraryVector;
        }
        return array;
    }
}
