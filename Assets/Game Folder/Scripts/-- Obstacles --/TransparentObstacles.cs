using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObstacles : MonoBehaviour
{
    private float detectionRange;
    private PlayerAttack playerAttack;

    [SerializeField] private LayerMask obstacleMask;

   private Dictionary<GameObject, Color> originalColors = new Dictionary<GameObject, Color>();

    void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Start()
    {
        detectionRange = playerAttack.GetAttackRange();
    }

    void Update()
    {
        DetectObstacle();
    }

    public void DetectObstacle()
    {
        Collider[] obstaclesInRange = Physics.OverlapSphere(transform.position, detectionRange, obstacleMask);

        HashSet<GameObject> currentObstacles = new HashSet<GameObject>();

        foreach (Collider obstacle in obstaclesInRange)
        {
            GameObject obj = obstacle.gameObject;
            currentObstacles.Add(obj);

            Renderer renderer = obj.GetComponent<Renderer>();
            Material mat = renderer.material;

            if (!originalColors.ContainsKey(obj))
            {
                originalColors[obj] = mat.color;
            }

            ChangeTransparent(mat, 0.5f);
        }

        List<GameObject> objectsToRevert = new List<GameObject>();

        foreach (GameObject obj in originalColors.Keys)
        {
            if (!currentObstacles.Contains(obj))
            {
                objectsToRevert.Add(obj);
            }
        }

        foreach (GameObject obj in objectsToRevert)
        {
            RevertTransparency(obj);
        }
    }

    public void ChangeTransparent(Material mat, float alphaVal)
    {
        Color col = mat.color;
        Color newCol = new Color(col.r, col.g, col.b, alphaVal);
        mat.SetColor("_Color", newCol);
    }

    public void RevertTransparency(GameObject obj)
    {
        if (originalColors.ContainsKey(obj))
        {
            Material mat = obj.GetComponent<Renderer>().material;
            mat.SetColor("_Color", originalColors[obj]);
            originalColors.Remove(obj);
        }
    }
}
