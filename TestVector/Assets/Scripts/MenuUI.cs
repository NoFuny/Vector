using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

//Класс описываюшьй поведения UI 
public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ArbitraryPlane arbitraryPlane;
    [SerializeField] private WayPoint wayPoint;
    [SerializeField] Barrier barrier;

    [SerializeField] InputField inputRangeGrid, inputSpeed;
    [SerializeField] InputField inputAxisXPoint, inputAxisYPoint;
    [SerializeField] InputField inputAxisXBarrier, inputAxisYBarrier;
    [SerializeField] GameObject panelTools,panelTools3D;
    [SerializeField] Text textTime,erroeBarrier;

    [SerializeField] private GameObject textPointPrefab, parentTextPointPrefa;

    //Выбор режима рисования вектора Х
    public void UIMenuAxisX() {gameManager.SetMode(1);}

    //Выбор режима рисования вектора У
    public void UIMenuAxisY() {gameManager.SetMode(2);}

    //Установка размерности системы координта
    public void UIGridLength()
    {
        int intRangeInput = 0;
        try { intRangeInput = Convert.ToInt32(inputRangeGrid.text); }
        catch { inputRangeGrid.text = ""; }
        arbitraryPlane.gridLength = intRangeInput;
        arbitraryPlane.SurfaceCalculation();
    }

    //Установка скорости движения объекта
    public void UISpeed()
    {
        float intSpeed = 0;
        try { intSpeed = Convert.ToSingle(inputSpeed.text); }
        catch { inputSpeed.text = ""; }
        if (intSpeed == 0)
        {
            inputSpeed.text = "1";
            intSpeed = 1;
        }
        gameManager.Speed = intSpeed;
    }

    //Добавления точки по введеным параметрам
    public void UIAddPoint()
    {
        if (!inputAxisXPoint.text.Equals("") && !inputAxisYPoint.text.Equals(""))
        {
            var x = Convert.ToSingle(inputAxisXPoint.text.Replace('.', ','));
            var y = Convert.ToSingle(inputAxisYPoint.text.Replace('.',','));
            wayPoint.AddPoint(new Vector2(x, y));
            UIAddPointText(x, y);
        }
    }

    //Добавления точки в список на панели UI
    public void UIAddPointText(float x, float y)
    {
        int number = wayPoint.CountPoint();
        var textPoint = Instantiate(textPointPrefab, parentTextPointPrefa.transform).GetComponent<Text>();
        textPoint.text = $"{number}. ({x};{y})";
        inputAxisXPoint.text = "";
        inputAxisYPoint.text = "";
    }

    //Добавления барьера по заданным координатам
    public void UIAddBarrier()
    {
        if (!inputAxisXBarrier.text.Equals("") && !inputAxisYBarrier.text.Equals(""))
        {
            var x = Convert.ToSingle(inputAxisXBarrier.text.Replace('.', ','));
            var y = Convert.ToSingle(inputAxisYBarrier.text.Replace('.', ','));
            barrier.AddBarrier(new Vector2(x, y));
            inputAxisYBarrier.text = "";
            inputAxisXBarrier.text = "";
        }
    }



    //Удаление всех точек с ситемы координат
        public void UICleanPoint()
    {
        Text[] array = parentTextPointPrefa.GetComponentsInChildren<Text>();
        foreach (var text in array)
        {
            Destroy(text.gameObject);
        }

        Point[] allPoint = FindObjectsOfType<Point>();
        foreach (var point in allPoint)
        {
            Destroy(point.gameObject);
        }
        wayPoint.CleanPoints();
    }


    //Установка таймера, после запуска
    public void TimeWalking(float time)   
    { 
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string miliisecond = timeSpan.ToString("fffffff");
        miliisecond = miliisecond.Substring(0, miliisecond.Length - 5);
        textTime.text = timeSpan.ToString("mm':'ss") + $":{miliisecond}";
    }

    public void UIRestart() { gameManager.Restart(); }
    public void UIErrorBarrie(bool error) {erroeBarrier.gameObject.SetActive(error);}
    public void UIPanelTools(bool value) { panelTools.SetActive(value); panelTools3D.SetActive(!value); }
    public void UIPanelTools3D(bool value) { panelTools3D.SetActive(value); }
    public void UICameraView3D(bool value) { gameManager.CameraView3D(value); }
    public void UICleanBarrier() { barrier.CleanBarrier(); }
}
