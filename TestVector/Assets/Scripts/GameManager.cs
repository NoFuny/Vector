using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera camera3D;
    private Barrier barriers;

    private ArbitraryPlane arbitraryPlane;
    private WayPoint wayPoint;
    private MenuUI menuUI;
    private Player player;

    private Vector2 mouseWordPosicion;
    private ModeDrawing modeDrawing;
    private Vector2[] points;
    private float timeWalking;
    private int stepWalking = 0;
    private int stepCount = 0;
    private bool view3D;
    private float alfaText;

    [SerializeField] private GameObject Player3D, Player2D;
    private GameObject mainPlayer;

    public float Speed { get; set; } = 1;
    private void Awake()
    {
        modeDrawing = ModeDrawing.Expectation;
        arbitraryPlane = FindObjectOfType<ArbitraryPlane>();
        wayPoint = FindObjectOfType<WayPoint>();
        menuUI = FindObjectOfType<MenuUI>();
        barriers = FindObjectOfType<Barrier>();
        CameraView3D(false);

    }

    //������������, ������� ���������� �������� ������ � �����������.
    enum ModeDrawing
    {
        Expectation,
        ArbitraryVectorX,
        ArbitraryVectorY,
        MoveOnePoint,
        MoveAllPoint

    }

    void Update()
    {

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            mainCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel");
        }

        
        switch (modeDrawing)
        {
            //��������� � ������ ��������, ���������� ������� �� 3� ������
            case ModeDrawing.Expectation:
                if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = camera3D.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                        if (hit.transform.gameObject.tag == "Player")
                        {
                            SetMode(3);
                        }
               }
            }
                break;

            //��������� � ������ ��������� ������� �
            case ModeDrawing.ArbitraryVectorX:

                mouseWordPosicion = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                arbitraryPlane.ArrowAxisX(mouseWordPosicion);

                if (Input.GetMouseButtonDown(0))
                {
                    arbitraryPlane.ArbitraryVectorX = mouseWordPosicion;
                    arbitraryPlane.SurfaceCalculation();
                    barriers.CleanBarrier();
                    modeDrawing = ModeDrawing.Expectation;
                }
                break;

            //��������� � ������ ��������� ������� Y
            case ModeDrawing.ArbitraryVectorY:
                mouseWordPosicion = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                arbitraryPlane.ArrowAxisY(mouseWordPosicion);

                if (Input.GetMouseButtonDown(0))
                {
                    arbitraryPlane.ArbitraryVectorY = mouseWordPosicion;
                    arbitraryPlane.SurfaceCalculation();
                    barriers.CleanBarrier();
                    modeDrawing = ModeDrawing.Expectation;
                }
                break;

            //��������� � ������ �������� � ��������� �����
            case ModeDrawing.MoveOnePoint:
                MovePlayer();
                /*
                timeWalking += Time.deltaTime;
                menuUI.TimeWalking(timeWalking);
                if (stepWalking < stepCount)
                {
                    arbitraryPlane.PaintCoordinates(mainPlayer.transform.position);

                    if (view3D) AnimationText3D(stepWalking, mainPlayer.transform.position);

                    mainPlayer.transform.position = Vector3.MoveTowards(mainPlayer.transform.position, points[stepWalking], Time.deltaTime * Speed *  Vector2.Distance(arbitraryPlane.ArbitraryVectorX+arbitraryPlane.ArbitraryVectorY, Vector2.zero));
                    if (mainPlayer.transform.position == new Vector3(points[stepWalking].x, points[stepWalking].y, 0f))
                    {
                        stepWalking++;
                        modeDrawing= ModeDrawing.Expectation;
                    }
                }
                else if (stepWalking == stepCount) modeDrawing = ModeDrawing.Expectation;*/
                break;

            //��������� � ������ �������� �� ���� ��������
            case ModeDrawing.MoveAllPoint:
                MovePlayer();
               /* timeWalking += Time.deltaTime;
                menuUI.TimeWalking(timeWalking);
                if (stepWalking < stepCount)
                {
                    if (view3D) AnimationText3D(stepWalking, mainPlayer.transform.position);
                    mainPlayer.transform.position = Vector3.MoveTowards(mainPlayer.transform.position, points[stepWalking], Time.deltaTime * Speed);
                    if (mainPlayer.transform.position == new Vector3(points[stepWalking].x, points[stepWalking].y, 0f))
                    {
                        stepWalking++;
                    }
                }
                else if (stepWalking == stepCount) modeDrawing = ModeDrawing.Expectation; */
                break;


        }
    }

    public void MovePlayer()
    {
        timeWalking += Time.deltaTime;
        menuUI.TimeWalking(timeWalking);
        if (stepWalking < stepCount)
        {
            arbitraryPlane.PaintCoordinates(mainPlayer.transform.position);

            if (view3D) AnimationText3D(stepWalking, mainPlayer.transform.position);

            mainPlayer.transform.position = Vector3.MoveTowards(mainPlayer.transform.position, points[stepWalking], Time.deltaTime * Speed );
            if (mainPlayer.transform.position == new Vector3(points[stepWalking].x, points[stepWalking].y, 0f))
            {
                stepWalking++;
                if(modeDrawing==ModeDrawing.MoveOnePoint) modeDrawing = ModeDrawing.Expectation;
            }
        }
        else if (stepWalking == stepCount) modeDrawing = ModeDrawing.Expectation;
    }

    //����� ������ ������ � ����������
    public void SetMode(int modeInt)
    {
        if ((modeInt == ((int)ModeDrawing.MoveOnePoint) || modeInt == (int) ModeDrawing.MoveAllPoint))
        {
            Get�oordinatePoint();
            ErrorBarrier(false);
            if ( stepWalking == stepCount) return;
        }
        modeDrawing = (ModeDrawing)modeInt;
    }

    //������������ �� 3� ��� � �������
    public void CameraView3D(bool value)
    {
        view3D = value;
        barriers.gameObject.SetActive(value);
        Player3D.SetActive(value);
        camera3D.gameObject.SetActive(value);


        mainCamera.gameObject.SetActive(!value);
        Player2D.SetActive(!value);

        if (value)
        {
            mainPlayer = Player3D;
            player = FindObjectOfType<Player>();
        }
        else mainPlayer = Player2D;

    }

    //���������� ����� ����, ����� ��� ������������ ������ ��������
    private void Get�oordinatePoint()
    {
        points = wayPoint.Get�oordinatePoint();
        stepCount = points.Length;
    }

    //����� ������ � �����������
    public void ErrorBarrier(bool error)
    {
        menuUI.UIErrorBarrie(error);
    }

    //����� ��������, ����������� ������� � ������ ���������
    public void Restart()
    {
        timeWalking = 0;
        menuUI.TimeWalking(timeWalking);
        stepWalking = 0;
        mainPlayer.transform.position = new Vector3(0, 0, 0);
        menuUI.UIErrorBarrie(false);
        if (player!= null) player.TextPlayerOff();
    }

    //���������� �������� ���������� �������� (���������� ��� ������� �������� �� ��������� ����� �������)
    private float MiddleStepProcent(Vector2 start, Vector2 end, Vector2 position)
    {
        var centr = (start + end)/2;
        var result= 1- Vector2.Distance(position, centr) / Vector2.Distance(start,centr);
        return result;
    }

    //�������� �������� ��������� ������ ��������
    private void AnimationText3D(int step, Vector2 position)
    {
        if (step == 0)
        {
            alfaText = MiddleStepProcent(Vector2.zero, points[step], position);
            player.TextPlayer(stepWalking + 1, alfaText);
        }
        else
        {
            alfaText = MiddleStepProcent(points[step - 1], points[step], position);
            player.TextPlayer(stepWalking + 1, alfaText);
        }

    }
}



