using UnityEngine;
using System.Collections;

public class HandColorChanger2 : MonoBehaviour
{
    public SkinnedMeshRenderer handRenderer;
    public Material normalMaterial;
    public Material YellowMaterial;
    public Material GreenMaterial;

    public MeshCollider yellowCollider1;
    public MeshCollider yellowCollider2;
    public MeshCollider greenCollider1;
    public MeshCollider greenCollider2;

    private Coroutine delayCoroutine;

    private bool isInYellow = false;
    private bool isInGreen = false;

    void Start()
    {
        if (handRenderer == null)
        {
            Debug.LogError("Hand Renderer is not assigned!");
        }
    }

    void Update()
    {
        Vector3 handPosition = transform.position;

        bool inYellowNow = IsPointInsideMesh(handPosition, yellowCollider1) || IsPointInsideMesh(handPosition, yellowCollider2);
        bool inGreenNow = IsPointInsideMesh(handPosition, greenCollider1) || IsPointInsideMesh(handPosition, greenCollider2);

        if (inYellowNow && !isInYellow)
        {
            isInYellow = true;
            handRenderer.material = YellowMaterial;
        }
        else if (!inYellowNow && isInYellow)
        {
            isInYellow = false;
        }

        if (inGreenNow && !isInGreen)
        {
            isInGreen = true;
            handRenderer.material = GreenMaterial;
        }
        else if (!inGreenNow && isInGreen)
        {
            isInGreen = false;
        }

        if (!isInYellow && !isInGreen)
        {
            if (delayCoroutine == null)
            {
                delayCoroutine = StartCoroutine(ChangeToNormalAfterDelay(10f));
            }
        }
        else
        {
            if (delayCoroutine != null)
            {
                StopCoroutine(delayCoroutine);
                delayCoroutine = null;
            }
        }
    }

    IEnumerator ChangeToNormalAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!isInYellow && !isInGreen)
        {
            handRenderer.material = normalMaterial;
            Debug.Log("Changed to normal material after delay");
        }

        delayCoroutine = null;
    }

    bool IsPointInsideMesh(Vector3 point, MeshCollider meshCol)
    {
        if (meshCol == null) return false;

        Vector3 direction = Vector3.up;
        Ray ray = new Ray(point, direction);
        RaycastHit[] hits = Physics.RaycastAll(ray, 100f);

        int hitCount = 0;
        foreach (var hit in hits)
        {
            if (hit.collider == meshCol)
                hitCount++;
        }

        return hitCount % 2 == 1;
    }
}
