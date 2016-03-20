using UnityEngine;
using System.Collections;

public class ScrollZoneSetBackground : MonoBehaviour {

    [SerializeField]
    private Material[] backgroundMaterials;
    [SerializeField]
    private MeshRenderer _renderer;

    private Material curMaterial;

	public void SetBackground (float sizeX, float sizeY) {
        _renderer.materials[0] = backgroundMaterials[0];
        _renderer.material.mainTextureScale = new Vector2(sizeX/20, sizeY/20);
    }
}
