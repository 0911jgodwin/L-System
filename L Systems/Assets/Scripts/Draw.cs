using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Draw : MonoBehaviour
{
    private Dictionary<char, string> ruleset;
    string axiom;
    int iterations;
    float length;
    public GameObject Tree;
    public GameObject currentTree;
    public float angle;
    private Stack<Location> locationStack;
    public Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
    public Camera mainCamera;

    [SerializeField] private GameObject treeParent;
    [SerializeField] private GameObject branch;
    [SerializeField] private GameObject leaf;

    public void MakeTree()
    {
        if (currentTree) { Destroy(currentTree); }
        currentTree = Instantiate(Tree);
        bounds = new Bounds(Vector3.zero, Vector3.zero);
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        DrawTree(CreateString(axiom, iterations));
    }

    public void SetVariables(Dictionary<char, string> ruleset_, string axiom_, int iterations_, float angle_, float length_)
    {
        ruleset = ruleset_;
        axiom = axiom_;
        iterations = iterations_;
        angle = angle_;
        length = length_;
    }

    private string CreateString(string axiom, int iterations)
    {
        string currentString = axiom;
        StringBuilder newString = new StringBuilder();

        for (int i = 0; i < iterations; i++)
        {
            foreach (char c in currentString)
            {
                newString.Append(ruleset.ContainsKey(c) ? ruleset[c] : c.ToString());
            }

            currentString = newString.ToString();
            newString = new StringBuilder();
        }

        return currentString;
    }

    private void DrawTree(string treeString)
    {
        Vector3 startPosition = Vector3.zero;
        locationStack = new Stack<Location>();
        GameObject parentBranch = currentTree;

        for (int i = 0; i < treeString.Length; i++)
        {
            switch (treeString[i])
            {
                case 'F':
                    startPosition = transform.position;
                    transform.Translate(Vector3.up * length / 4);

                    GameObject branchLine = Instantiate(branch);
                    branchLine.transform.SetParent(parentBranch.transform);
                    branchLine.GetComponent<LineRenderer>().startWidth = 1;
                    branchLine.GetComponent<LineRenderer>().endWidth = 1;
                    branchLine.GetComponent<LineRenderer>().SetPosition(0, startPosition);
                    branchLine.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    parentBranch = branchLine;
                    UpdateBounds(branchLine.GetComponent<LineRenderer>().bounds);
                    break;
                case 'X':
                    Instantiate(leaf, transform.position, transform.rotation, parentBranch.transform);
                    break;
                case '+':
                    transform.Rotate(Vector3.back * angle);
                    break;
                case '-':
                    transform.Rotate(Vector3.forward * angle);
                    break;
                case '[':
                    locationStack.Push(new Location(transform.rotation, transform.position, parentBranch));
                    break;
                case ']':
                    Location loc = locationStack.Pop();
                    transform.SetPositionAndRotation(loc.GetPosition(), loc.GetRotation());
                    parentBranch = loc.GetParent();
                    break;
                default:
                    throw new InvalidOperationException("That character is not valid");
            }
        }
        CalculateViewport();
    }

    private void CalculateViewport()
    {
        mainCamera.orthographicSize = bounds.size.x > bounds.size.y ? (bounds.size.x / 2) : (bounds.size.y / 2) * 1.1f;
        mainCamera.transform.position = new Vector3(Mathf.Round(mainCamera.orthographicSize * 0.75f), mainCamera.orthographicSize, -10);
    }

    private void UpdateBounds(Bounds branchBounds)
    {
        bounds.Encapsulate(branchBounds); 
    }

    
}
