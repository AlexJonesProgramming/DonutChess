using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{

    public GameObject piece;
    public Transform showcase;
    public GameObject board;

    public float sensitivity, zoom;
    float previousX, previousY, boardX, boardY;

    bool started = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.Mouse2))
        {
            
            if (!started)
            {
                started = true;
                previousX = Input.mousePosition.x;
                previousY = Input.mousePosition.y;
                boardX = board.transform.rotation.eulerAngles.x;
                boardY = board.transform.rotation.eulerAngles.z;
            }
            else
            {
                float newX = (Input.mousePosition.x - previousX) * -sensitivity;
                float newY = (Input.mousePosition.y - previousY) * -sensitivity;

                board.transform.RotateAround(Vector3.up, newX);
                board.transform.RotateAround(Vector3.left, newY);

                previousX = Input.mousePosition.x;
                previousY = Input.mousePosition.y;
            }
            
        }
        else
        {
            started = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            print("click");


            if (piece == null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag != "Board")
                    {
                        piece = hit.transform.gameObject;
                        piece.transform.parent = null;

                        piece.transform.position = showcase.position;
                        piece.transform.rotation = Quaternion.Euler(-90, 0, 0);
                    }
                }
            }
            else
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    piece.transform.position = hit.point;

                    piece.transform.forward = hit.normal;
                    /*float x = Vector3.Angle(Vector3.right, hit.normal);
                    float y = Vector3.Angle(Vector3.forward, hit.normal);
                    float z = Vector3.Angle(Vector3.up, hit.normal);

                    piece.transform.rotation = Quaternion.Euler(x - 90, y, z);
                    print(x + " " + y + " " + z);
                    */

                    piece.transform.parent = board.transform;
                    piece = null;
                }
            }
        }


        if (Input.GetMouseButtonUp(1))
        {
            print("click");


            if (piece == null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag != "Board")
                    {
                        Destroy( hit.transform.gameObject);
                    }
                }
            }
        }
    }
}
