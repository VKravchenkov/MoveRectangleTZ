using System.Collections.Generic;
using UnityEngine;


public class LineController : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRendererPref;

    private List<Vector2> listLinePos = new List<Vector2>();
    private LineRenderer currentline;

    public LineRenderer Currentline => currentline;

    public void CreateLine()
    {
        currentline = Instantiate(lineRendererPref, Vector3.zero, Quaternion.identity);

        currentline.enabled = false;
    }

    public void UpdatePositionLine()
    {
        currentline.enabled = true;
        currentline.SetPosition(0, listLinePos[0]);
        currentline.SetPosition(1, listLinePos[1]);
        listLinePos.Clear();
    }

    public void DeleteLine()
    {
        Destroy(currentline.gameObject);
    }

    public void AddPointToLine(Vector2 position)
    {
        listLinePos.Add(position);
    }
    public void ClearListPoints()
    {
        listLinePos.Clear();
    }
}

