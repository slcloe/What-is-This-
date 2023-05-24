using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacedObject : MonoBehaviour
{

    [SerializeField]
    private GameObject Cube;
    private GameObject CubeSelected;

    public bool IsSelected
    {
        get =>  SelectedObject == this;
    }

    private static PlacedObject selectedObject;
    public static PlacedObject SelectedObject
    {
        get => selectedObject;
        set
        {
            if (selectedObject==value ) return;

            if (selectedObject != null) 
            {
                selectedObject.CubeSelected.SetActive(false);
            }

             selectedObject = value;
            if (value != null)
                {
                string ParentName= value.transform.parent.name;



                    value.Cube.SetActive(false);
                Destroy(value);
            }
        }
    }

    private void Awake()
    {
        CubeSelected.SetActive(false);
    }

}
