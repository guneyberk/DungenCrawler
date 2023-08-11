using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrapPlacer : MonoBehaviour
{
    [SerializeField] Trap _trapPrefab;
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo,Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                if (hitInfo.collider.GetComponentInChildren<Trap>() != null)
                    return;


                var placementPoint = hitInfo.collider.transform.position + Vector3.up;
                Trap trap=Instantiate(_trapPrefab,placementPoint,Quaternion.identity);


                trap.transform.SetParent(hitInfo.collider.transform);
            }

        }
    }
}
