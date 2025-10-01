using UnityEngine;
using System.Collections;


public class HandColorChanger : MonoBehaviour
{
    public SkinnedMeshRenderer handRenderer;
    private Coroutine delayCoroutine;

    public Material normalMaterial;        // red 
    public Material YellowMaterial;      

    public Material GreenMaterial;  

    private bool isInBox = false;  // yellow 
    private bool isInGreen = false;


    void Start()
    {
        if (handRenderer == null)
        {
            Debug.LogError("Hand Renderer is not assigned!");
            return;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.LogError("hit something!");
        if (other.CompareTag("Box")) // Yellow = box
        {
            isInBox = true;
            handRenderer.material = YellowMaterial;
        }
        if (other.CompareTag("green")) 
        {
            isInGreen = true;
            handRenderer.material = GreenMaterial;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            Debug.LogError("exit box!");
            isInBox = false;

        }
        if (other.CompareTag("green"))
        {
            Debug.LogError("exit box!");
            isInGreen = false;

        }

        if (!isInBox && !isInGreen)
            {
                
                if (delayCoroutine != null)
                {
                    StopCoroutine(delayCoroutine);
                }
                delayCoroutine = StartCoroutine(ChangeToNormalAfterDelay(2f)); 
            }
        }

            IEnumerator ChangeToNormalAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            if (!isInBox && !isInGreen)
            {
                handRenderer.material = normalMaterial;
                Debug.Log("Changed to normal (red) after delay");
            }
        }

}
