using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс описывающий путь (набор точек в системе координат)
public class WayPoint : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private GameObject parent;

    private List<Point> listPoint = new List<Point>();

    //Перерасчет положения точек в пространстве
    public void RefrashPoint()

    {
        foreach (var point in listPoint)
        {
            point.MovePoint();
        }
    }

    //Добавление точек в простанстве
    public void AddPoint(Vector2 pointСoordinates)
    {
        var point = Instantiate(pointPrefab, parent.transform).GetComponent<Point>();
        point.ArbitraryVector = pointСoordinates;
        listPoint.Add(point);
    }

    //Удаление всех точек из списка
    public void CleanPoints()   {listPoint.Clear();}

    //Возвращает количество точек
    public int CountPoint() {return listPoint.Count;}

    //Получает список АБСОЛЮТНЫХ координ точек
    public Vector2[] GetСoordinatePoint()
    {
        int count = CountPoint();
        Vector2[] array = new Vector2[count];
        for (int i=0; i<count;i++)
        {
            array[i] = listPoint[i].AbsoluteVector;
        }
        return array;
    }

    //Получает список ПРОИЗВОДНЫХ координ точек
    public Vector2[] GetArbitraryСoordinatePoint()
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
