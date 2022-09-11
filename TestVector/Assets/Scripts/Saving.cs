using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

//Класс предназначен для импорта и експорта точек в текстовом формате
public class Saving : MonoBehaviour
{
    [SerializeField] private WayPoint wayPoint;
    [SerializeField] private MenuUI menuUI;

    private string pathSaveFolder;
    private string pathLoadFolder;
    private Vector2[] arbitraryPoints;


    //Сохраняет точки в указанную папку, сохраняет под именем "Points"
    public void SaveFolder()
    {
        arbitraryPoints = wayPoint.GetArbitraryСoordinatePoint();
        pathSaveFolder = EditorUtility.OpenFolderPanel("Save ", "", "");
        if (!pathSaveFolder.Equals(""))
        {
            pathSaveFolder += "/Points.txt";
            File.Create(pathSaveFolder).Close();
            ;
            foreach (var point in arbitraryPoints)
            {
                File.AppendAllText(pathSaveFolder, $"{point.x};{point.y}" + "\n");
            }

        }

        Debug.Log(pathSaveFolder);
    }

    //Загружает файл "Point.txt" , на основе него заного перестраивает точки в системе координат
    public void LoadFolder()
    {
        pathLoadFolder = EditorUtility.OpenFilePanel("Load2", "", "txt");
        if (!pathLoadFolder.Equals(""))
        {
            var lines = File.ReadAllLines(pathLoadFolder);
            int counPoint = lines.Length;
            int i = 0;
            arbitraryPoints = new Vector2[counPoint];

            try
            {
                foreach (var line in lines)
                {
                    var vector = line.Split(';');
                    arbitraryPoints[i] = new Vector2(Convert.ToSingle(vector[0]), Convert.ToSingle(vector[1]));
                    i++;
                }
                menuUI.UICleanPoint();
                foreach (var newVetror in arbitraryPoints)
                {
                    wayPoint.AddPoint(newVetror);
                    menuUI.UIAddPointText(newVetror.x, newVetror.y);
                }
            }
            catch { Debug.Log("Ошибка при загрузке файла"); }

        }


    }
}
