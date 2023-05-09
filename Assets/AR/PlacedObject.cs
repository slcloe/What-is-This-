using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    [SerializeField]
    private GameObject CubeSelected;
    // Start is called before the first frame update

    public bool IsSelected
    {
        get => SelectedObject == this;
    }

    private static PlacedObject selectedObject;
    public static PlacedObject SelectedObject
    {
        get => selectedObject;
        set
        {
            if(selectedObject==value ) return;

            if (selectedObject != null) 
            {
                selectedObject.CubeSelected.SetActive(false); 
            }

             selectedObject = value;

             if (value != null)
                {
                    value.CubeSelected.SetActive(true);
                }
        }
    }

    private void Awake()
    {
        CubeSelected.SetActive(false);
    }

}
