using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private SpawnController spawnController;
    [SerializeField] private LineController lineController;

    private Rectangle currentRectangle;

    private int clickCount = 0;

    private void Update()
    {
        ClickMouseButtonDownLeft();
        ClickMouseButtonDownRight();
    }

    private void ClickMouseButtonDownRight()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (spawnController.IsRaycastHitRectangle(Input.mousePosition))
            {
                clickCount++;

                if (clickCount == 1)
                {
                    lineController.CreateLine();

                    currentRectangle = spawnController.GetRectangle(Input.mousePosition);

                    currentRectangle.AddDataLineInList(
                        new DataLine
                        {
                            lineRenderer = lineController.Currentline,
                            index = 0
                        });
                }

                if (clickCount == 2)
                {
                    if (currentRectangle == spawnController.GetRectangle(Input.mousePosition))
                    {
                        clickCount--;
                        return;
                    }

                    if (currentRectangle != null)
                        lineController.AddPointToLine(Camera.main.ScreenToWorldPoint(currentRectangle.transform.position));
                    else
                    {
                        clickCount = 0;
                        return;
                    }

                    currentRectangle = spawnController.GetRectangle(Input.mousePosition);

                    currentRectangle.AddDataLineInList(
                        new DataLine
                        {
                            lineRenderer = lineController.Currentline,
                            index = 1
                        });

                    lineController.AddPointToLine(Camera.main.ScreenToWorldPoint(currentRectangle.transform.position));

                    lineController.UpdatePositionLine();

                    clickCount = 0;
                }
            }
        }
    }

    private void ClickMouseButtonDownLeft()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spawnController.SpawnRectangle(Input.mousePosition);
        }
    }

}
