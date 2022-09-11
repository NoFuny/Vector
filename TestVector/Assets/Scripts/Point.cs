using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс описывающий точку в простанстве
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

    //Перемещение точки в АБСОЛЮТНЫЕ координаты
    public void MovePoint()
    {
        absoluteVector = arbitraryPlane.GetArbitraryPoint(ArbitraryVector);
        transform.position = absoluteVector;
    }

    //Возвращает координаты в виде текста
    private void TetxPoin()
    {
        textMesh.text = $"({ArbitraryVector.x} ; {ArbitraryVector.y})";
    }

}
