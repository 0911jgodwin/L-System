using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_InputField Axiom;
    [SerializeField] TMP_InputField XField;
    [SerializeField] TMP_InputField FField;
    [SerializeField] Slider Iterations;
    [SerializeField] Slider Angle;
    [SerializeField] Slider Length;
    [SerializeField] Slider Example;
    [SerializeField] Toggle customTree;
    private Draw draw;
    [SerializeField] GameObject drawObject;

    void Start()
    {
        draw = drawObject.GetComponent<Draw>();
    }

    public void drawTrees()
    {
        if (customTree.isOn)
        {
            draw.SetVariables(new Dictionary<char, string>
            {
                { 'F', FField.text },
                { 'X', XField.text }
            }, Axiom.text, (int)Iterations.value, Angle.value, Length.value);
        }
        else
        {
            switch (Example.value)
            {
                case 1:
                    exampleRulesetOne();
                    SetFields("F[+F]F[-F]F", "","F", 3, 25.7f, 10f);
                    break;
                case 2:
                    exampleRulesetTwo();
                    SetFields("F[+F]F[-F][F]", "", "F", 3, 20f, 10f);
                    break;
                case 3:
                    exampleRulesetThree();
                    SetFields("FF-[-F+F+F]+[+F-F-F]", "", "F", 3, 22.5f, 10f);
                    break;
                case 4:
                    exampleRulesetFour();
                    SetFields("FF", "F[+X]F[-X]+X", "X", 5, 20f, 10f);
                    break;
                case 5:
                    exampleRulesetFive();
                    SetFields("FF", "F[+X][-X]FX", "X", 5, 25.7f, 10f);
                    break;
                case 6:
                    exampleRulesetSix();
                    SetFields("FF", "F-[[X]+X]+F[+FX]-X", "X", 5, 22.5f, 10f);
                    break;
                case 7:
                    exampleRulesetSeven();
                    SetFields("F[+FF][-FF]F[-F][+F]F", "", "F", 4, 35f, 10f);
                    break;
                case 8:
                    exampleRulesetEight();
                    SetFields("F", "F[-FX]+FX", "FX", 4, 40f, 50f);
                    break;
            }
        }
        draw.MakeTree();
    }

    private void SetFields(string FField_, string XField_, string axiom_, int iterations_, float angle_, float length_)
    {
        FField.text = FField_;
        XField.text = XField_;
        Axiom.text = axiom_;
        Iterations.value = iterations_;
        Angle.value = angle_;
        Length.value = length_;
    }

    private void exampleRulesetOne()
    {
        draw.SetVariables(new Dictionary<char, string>
        {
            { 'F', "F[+F]F[-F]F" }
        }, "F", 3, 25.7f, 10f);
    }

    private void exampleRulesetTwo()
    {
        draw.SetVariables(new Dictionary<char, string>
        {
            { 'F', "F[+F]F[-F][F]" }
        }, "F", 3, 20f, 10f);
    }

    private void exampleRulesetThree()
    {
        draw.SetVariables(new Dictionary<char, string>
        {
            { 'F', "FF-[-F+F+F]+[+F-F-F]" }
        }, "F", 3, 22.5f, 10f);
    }

    private void exampleRulesetFour()
    {
        draw.SetVariables(new Dictionary<char, string>
        {
            { 'X', "F[+X]F[-X]+X" },
            { 'F', "FF" }
        }, "X", 5, 20f, 10f);
    }

    private void exampleRulesetFive()
    {
        draw.SetVariables(new Dictionary<char, string>
        {
            { 'X', "F[+X][-X]FX" },
            { 'F', "FF" }
        }, "X", 5, 25.7f, 10f);
    }

    private void exampleRulesetSix()
    {
        draw.SetVariables(new Dictionary<char, string>
        {
            { 'X', "F-[[X]+X]+F[+FX]-X" },
            { 'F', "FF" }
        }, "X", 5, 22.5f, 10f);
    }

    private void exampleRulesetSeven()
    {
        draw.SetVariables(new Dictionary<char, string>
        {
            { 'F', "F[+FF][-FF]F[-F][+F]F" }
        }, "F", 4, 35f, 10f);
    }

    private void exampleRulesetEight()
    {
        draw.SetVariables(new Dictionary<char, string>
        {
            { 'X', "F[-FX]+FX" },
            { 'F', "F" }
        }, "FX", 4, 40f, 50f);
    }
}
