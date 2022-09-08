using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Класс предназначен для построения поверхности с произвольными координатами
public class ArbitraryPlane : MonoBehaviour
{
    [SerializeField] private GameObject ArbitraryAxisX, ArbitraryAxisY;
    [SerializeField] private Material MaterialAxisX, MaterialAxisY;
    [SerializeField]  private WayPoint wayPoint;
    private float distanceAxisX, distanceAxisY;

    Vector2 ArbitraryZero = Vector2.zero;
    public Vector2 ArbitraryVectorX { get; set; }
    public Vector2 ArbitraryVectorY { get; set; }
    public int gridLength = 3;

    LineRenderer linePlayerX, linePlayerY;

    //Получение произвольной координаты Х
    public void ArrowAxisX(Vector2 point)
    {
        if (!ArbitraryAxisX.activeSelf) ArbitraryAxisX.SetActive(true);
        var angle = Mathf.Atan2(point.x, point.y) * Mathf.Rad2Deg;
        ArbitraryAxisX.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, -angle);
        ArbitraryAxisX.GetComponent<SpriteRenderer>().size = new Vector2(0.5f,Vector2.Distance(ArbitraryZero,point));
    }

    //Получение произвольной координаты У
    public void ArrowAxisY(Vector2 point)
    {
        if (!ArbitraryAxisY.activeSelf) ArbitraryAxisY.SetActive(true);
        var angle = Mathf.Atan2(point.x, point.y) * Mathf.Rad2Deg;
        ArbitraryAxisY.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, -angle);
        ArbitraryAxisY.GetComponent<SpriteRenderer>().size = new Vector2(0.5f, Vector2.Distance(ArbitraryZero, point));
    }

    //Функция, создающая линии, для отрисовки поверхности
    LineRenderer CreateLine(Material materiaAxis)
    {
        LineRenderer line;
        line = new GameObject("Line").AddComponent<LineRenderer>();
        line.material = materiaAxis;
        line.positionCount = 2;
        line.startWidth = 0.017f;
        line.endWidth = 0.017f;
        return line;
    }

    //Полный расчет поверхности и построение сетки координат по данным
    public void SurfaceCalculation()
    {
        distanceAxisX = Vector2.Distance(ArbitraryZero, ArbitraryVectorX);
        distanceAxisY = Vector2.Distance(ArbitraryZero, ArbitraryVectorY);

        if (distanceAxisX!=0 && distanceAxisY != 0)
        {
            CleanSurfaceCalculation();;
            wayPoint.RefrashPoint();

            for (int i = -gridLength; i<=gridLength; i++)
            {
                var lineX = CreateLine(MaterialAxisX);
                lineX.SetPosition(0, gridLength*(-ArbitraryVectorX)+ ArbitraryVectorY*i);
                lineX.SetPosition(1, gridLength*ArbitraryVectorX+ ArbitraryVectorY*i);

                var lineY= CreateLine(MaterialAxisY);
                lineY.SetPosition(0, gridLength * (-ArbitraryVectorY) + ArbitraryVectorX * i);
                lineY.SetPosition(1, gridLength * ArbitraryVectorY + ArbitraryVectorX * i);
            }
        }
    }

    //Очистка поверхности, удаление сетки координат
    private void CleanSurfaceCalculation()
    {
        LineRenderer[] allLine = FindObjectsOfType<LineRenderer>();
        foreach (var a in allLine)
        {
            Destroy(a.gameObject);
        }    
    }

    //Преобразование ПРОИЗВОДНЫХ координак в АБСОЛЮТНЫЕ (в системе Unity)
    public Vector2 GetArbitraryPoint(Vector2 poit)
    {
        var x = poit.x* ArbitraryVectorX;
        var y = poit.y * ArbitraryVectorY;
        Vector2 ArbitrarySumm = x + y;
        return ArbitrarySumm;
    }

   /* public Vector2 GetAbsolutePoint(Vector2 poit)
    {
        var x = new Vector2(poit.x / ArbitraryVectorX.x, poit.y / ArbitraryVectorX.y);
        var y = new Vector2(poit.y / ArbitraryVectorY.x, poit.y / ArbitraryVectorY.y);
        Vector2 ArbitrarySumm = x;
        return ArbitrarySumm;
    }*/


}
