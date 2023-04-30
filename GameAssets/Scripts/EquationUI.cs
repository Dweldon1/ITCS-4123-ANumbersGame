using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EquationUI : MonoBehaviour
{
    private TextMeshProUGUI equation;

    public int x;

    public int y;

    public string eq;

   

    // Start is called before the first frame update
    void Start()
    {
        equation = GetComponent<TextMeshProUGUI>();
        x = Random.Range(0, 9);
        y = Random.Range(0, 9);
        setEquation();
    }

    void setEquation() {
        eq = x.ToString();
        eq = string.Concat(eq, " + ");
        eq = string.Concat(eq, y.ToString());

        equation.text = eq;
    }
}
