using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public partial class Rectangle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] private Image image;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Canvas canvas;

    [SerializeField] private float offset;

    private Collider2D collider2DRect;
    private Collider2D[] collider2Ds;

    private List<DataLine> dataLines = new List<DataLine>();

    private bool isMove;
    private float radious;


    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();

        collider2DRect = rectTransform.GetComponent<Collider2D>();
    }

    private void Start()
    {
        SetColor();

        float x = collider2DRect.bounds.size.x / 2;
        float y = collider2DRect.bounds.size.y / 2;

        radious = Mathf.Sqrt(x * x + y * y);
    }

    private void SetColor()
    {
        image.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        collider2DRect.enabled = false;

        GetAllBoxColliders();

        isMove = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        CheckDistance();

        if (isMove)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

            MoveConnection();
        }

    }

    private void CheckDistance()
    {
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if (Vector3.Distance(transform.position, collider2Ds[i].transform.position) < radious * 2)
            {
                isMove = false;
                transform.position += (transform.position - collider2Ds[i].transform.position) * 0.01f;
                return;
            }
        }
    }

    private void MoveConnection()
    {
        for (int i = 0; i < dataLines.Count; i++)
        {
            LineRenderer lineRenderer = dataLines[i].lineRenderer;

            if (lineRenderer == null)
                dataLines.RemoveAt(i);
            else
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(transform.position);
                lineRenderer.SetPosition(dataLines[i].index, pos);
            }

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        collider2DRect.enabled = true;
    }

    private void GetAllBoxColliders()
    {
        collider2Ds = Physics2D.OverlapBoxAll(transform.position, rectTransform.rect.size * 20f, 0f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (eventData.clickCount == 2)
        {
            DeleteAllConnection();

            Destroy(gameObject);
        }
    }

    public void AddDataLineInList(DataLine dataLine)
    {
        dataLines.Add(dataLine);
    }

    public void ClearAllDataLineList()
    {
        dataLines.Clear();
    }

    private void DeleteAllConnection()
    {
        for (int i = 0; i < dataLines.Count; i++)
        {
            if (dataLines[i].lineRenderer != null)
                Destroy(dataLines[i].lineRenderer.gameObject);
        }

        ClearAllDataLineList();

        collider2Ds = null;
    }
}