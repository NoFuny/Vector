using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

//����� ������������ ��� ������� � �������� ����� � ��������� �������
public class Saving : MonoBehaviour
{
    private string pathSaveFolder;
    private string pathLoadFolder;
    private Vector2[] arbitraryPoints;

    private WayPoint wayPoint;
    private MenuUI menuUI;
    private void Awake()
    {
        wayPoint = FindObjectOfType<WayPoint>();
        menuUI = FindObjectOfType<MenuUI>();
    }

    //��������� ����� � ��������� �����, ��������� ��� ������ "Points"
    public void SaveFolder()
    {
        arbitraryPoints = wayPoint.GetArbitrary�oordinatePoint();
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

    //��������� ���� "Point.txt" , �� ������ ���� ������ ������������� ����� � ������� ���������
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
            catch { Debug.Log("������ ��� �������� �����"); }

        }


    }
}
