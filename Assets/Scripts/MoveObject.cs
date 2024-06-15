using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float moveSpeed = 0.1f;

    private GameObject selectedObject;

    public void SetSelectedObject(GameObject obj)
    {
        selectedObject = obj;
    }

    public void MoveUp()
    {
        if (selectedObject != null)
        {
            selectedObject.transform.position += Vector3.up * moveSpeed;
        }
    }

    public void MoveDown()
    {
        if (selectedObject != null)
        {
            selectedObject.transform.position += Vector3.down * moveSpeed;
        }
    }

    public void MoveLeft()
    {
        if (selectedObject != null)
        {
            selectedObject.transform.position += Vector3.left * moveSpeed;
        }
    }

    public void MoveRight()
    {
        if (selectedObject != null)
        {
            selectedObject.transform.position += Vector3.right * moveSpeed;
        }
    }
}
