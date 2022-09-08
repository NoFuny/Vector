using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Класс 3д объекта, для отслеживания столновений и движения 
    private GameManager gameManager;
    [SerializeField] Transform Player3D;
    [SerializeField] TextMesh textMesh; 
 
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    //Добавляет текст с номером точки, к которой движется объект
    public void TextPlayer(int step,float gamma)
    {
        if(!textMesh.gameObject.activeSelf) textMesh.gameObject.SetActive(true);
        textMesh.text = step.ToString();
        textMesh.color = new Color(1f, 1f, 1f, gamma); ;
    }

    //Отслеживание столкновений с барьерами
    private void OnTriggerEnter(Collider other)
    {
        gameManager.SetMode(0);
        gameManager.ErrorBarrier(true);
    }

    //Отключение текста с номером точки, к которой движется объект
    public void TextPlayerOff()
    {
        textMesh.gameObject.SetActive(false);
    }
}
