using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Класс предназначен для построения поверхности с произвольными координатами
public class ArbitraryPlane : MonoBehaviour
{
    [SerializeField] private GameObject ArbitraryAxisX, ArbitraryAxisY;
    [SerializeField] private Material MaterialAxisX, MaterialAxisY, MaterialCoordinates;
    [SerializeField]  private WayPoint wayPoint;
    private float distanceAxisX, distanceAxisY;

    Vector2 ArbitraryZero = Vector2.zero;
    public Vector2 ArbitraryVectorX { get; set; }
    public Vector2 ArbitraryVectorY { get; set; }
    public int gridLength = 3;

    LineRenderer linePlayerX, linePlayerY;

    private void Start()
    {

         linePlayerX = CreateLine(MaterialAxisX);
    }
    private void Awake()
    {
      

        
    }
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

    public Vector2 GetAbsolutePoint(Vector2 poit)
    {
        //var x = (poit - ArbitraryVectorY)/ArbitraryVectorX;
        // var y = (poit - ArbitraryVectorX) / ArbitraryVectorY;
        var x = poit.x  /( ArbitraryVectorX.x+ArbitraryVectorY.x);
        var y = poit.y / (ArbitraryVectorX.y + ArbitraryVectorY.y);
        Vector2 result = new Vector2 (x,y);
        Debug.Log($"{poit/(ArbitraryVectorX+ArbitraryVectorY)}");
        return result;
    }

    public void PaintCoordinates(Vector2 position)
    {

        if(linePlayerX ==null) linePlayerX= CreateLine(MaterialCoordinates);
        if (linePlayerY == null) linePlayerY = CreateLine(MaterialCoordinates);

         var paintX = Intersection(position,position-(ArbitraryVectorX*gridLength),ArbitraryZero,ArbitraryVectorY*gridLength);
         var paintY = Intersection(position, position - (ArbitraryVectorY * gridLength), ArbitraryZero, ArbitraryVectorX * gridLength);

        Debug.Log(paintX);

        linePlayerX.SetPosition(0, position);
        linePlayerX.SetPosition(1, paintX);

        linePlayerY.SetPosition(0, position);
        linePlayerY.SetPosition(1, paintY);
    }

     public Vector2 Intersection(Vector2 A, Vector2 B, Vector2 C, Vector2 D)
    {
        float xo = A.x, yo = A.y;
        float p = B.x - A.x, q = B.y - A.y;

        float x1 = C.x, y1 = C.y;
        float p1 = D.x - C.x, q1 = D.y - C.y;

        float x = (xo * q * p1 - x1 * q1 * p - yo * p * p1 + y1 * p * p1) /
            (q * p1 - q1 * p);
        float y = (yo * p * q1 - y1 * p1 * q - xo * q * q1 + x1 * q * q1) /
            (p * q1 - p1 * q);

        return new Vector2(x, y);
    }

}
