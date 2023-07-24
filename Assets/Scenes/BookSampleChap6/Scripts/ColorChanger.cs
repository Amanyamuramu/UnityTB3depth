using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour {
    public Button colorChangeButton;
    private Renderer rend;

    // Start is called before the first frame update
    void Start() {
        rend = GetComponent<Renderer>();
        colorChangeButton.onClick.AddListener(ChangeColor);
    }

    void ChangeColor() {
        rend.material.color = new Color(Random.value, Random.value, Random.value);
    }
}

