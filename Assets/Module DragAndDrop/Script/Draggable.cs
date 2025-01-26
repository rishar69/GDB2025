using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] private GameObject toppingPrefab;

    private Camera mainCamera;
    private GameObject currentTopping;
    private Vector2 mousePositionOffset;
    private Collider2D currentDropTarget;

    private void Awake()
    {
        mainCamera = Camera.main;
        ValidateMainCamera();
    }

    private void ValidateMainCamera()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found in the scene.");
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0;
        return mainCamera ? mainCamera.ScreenToWorldPoint(mousePosition) : Vector2.zero;
    }

    private void OnMouseDown()
    {
        SpawnTopping();
        StartDragging();
    }

    private void SpawnTopping()
    {
        if (currentTopping == null && toppingPrefab != null)
        {
            currentTopping = Instantiate(toppingPrefab, GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    private void StartDragging()
    {
        if (currentTopping != null)
        {
            mousePositionOffset = (Vector2)currentTopping.transform.position - GetMouseWorldPosition();
        }
    }

    private void OnMouseDrag()
    {
        UpdateToppingPosition();
    }

    private void UpdateToppingPosition()
    {
        if (currentTopping != null)
        {
            currentTopping.transform.position = GetMouseWorldPosition() + mousePositionOffset;
        }
    }

    private void OnMouseUp()
    {
        TryAddToppingToCup();
        CleanupTopping();
    }

    private void TryAddToppingToCup()
    {
        if (currentDropTarget != null)
        {
            EmptyCup emptyCup = currentDropTarget.GetComponent<EmptyCup>();
            if (emptyCup != null)
            {
                emptyCup.TryAddTopping(currentTopping.GetComponent<Toppings>());
            }
        }
    }

    private void CleanupTopping()
    {
        if (currentTopping != null)
        {
            Destroy(currentTopping);
            currentTopping = null;
        }
        currentDropTarget = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentDropTarget = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == currentDropTarget)
        {
            currentDropTarget = null;
        }
    }
}