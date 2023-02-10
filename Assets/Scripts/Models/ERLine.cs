using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ERLine
{
    public string Name { get; set; }
    public string Node1Name { get; set; }
    public string Node2Name { get; set; }
    public string Cardinality { get; set; }
    public bool EnabledCardinality { get; set; }

    public ERLine(string node1, string node2, bool enabledCardinality = false, string cardinality = "")
    {
        Name = CreateName(node1, node2);
        Node1Name = node1;
        Node2Name = node2;
        Cardinality = cardinality;
        EnabledCardinality = enabledCardinality;
    }

    // Check if a node is linked by this ER_LineModel
    public bool IsNodeLinked(string nodeName)
    {
        return nodeName == Node1Name || nodeName == Node2Name;
    }

    // Check if 2 nodes are linked by this ER_LineModel
    public bool AreNodesLinked(string node1, string node2)
    {
        // Checks both orders
        return (node1 == Node1Name && node2 == Node2Name) || (node2 == Node1Name && node1 == Node2Name);
    }

    public static string CreateName(string node1, string node2)
    {
        return String.Compare(node1, node2) < 0 ? "erline_" + node1 + "_" + node2 : "erline_" + node2 + "_" + node1;
    }
}
