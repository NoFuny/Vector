using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Класс 3д объекта, для отслеживания столновений и движения 
    [SerializeField] private GameManager gameManager;
    [SerializeField] Transform Player3D;
    [SerializeField] TextMesh textMesh; 



    //Отслеживание столкновений с барьерами
    private void OnTriggerEnter(Collider other)
    {
        gameManager.SetMode(0);
        gameManager.ErrorBarrier(true);
    }


    public void TextPlayerOff()
    {
        textMesh.gameObject.SetActive(false);
    }

    public void TextPlayer(int step, float gamma)
    {
        if (!textMesh.gameObject.activeSelf) textMesh.gameObject.SetActive(true);
        textMesh.text = step.ToString();
        textMesh.color = new Color(1f, 1f, 1f, gamma); ;
    }
}
