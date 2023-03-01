//Console.WriteLine("Hello, World!");

using System;

// © Slavkovic Filip

// Definition eines Knotens im Baum
class Node
{
    public int value;   // Der Wert des Knotens
    public Node left, right;   // Die beiden Kinder des Knotens

    // Konstruktor, der einen neuen Knoten mit dem angegebenen Wert erstellt
    public Node(int val)
    {
        value = val;
        left = null;
        right = null;
    }
}

// def eines binären Suchbaums
class BinaryTree
{
    public Node root;   // Wurzel des Baums

    // leerer baum
    public BinaryTree()
    {
        root = null;
    }

    // neues element zum baum dings da
    public void Add(int val)
    {
        root = AddHelper(root, val);
    }

    // neues element an der richtigen stelle im baum
    private Node AddHelper(Node current, int val)
    {
        // wenn aktuellerr Knoten null dann neuer Knoten mit ang. Wert
        if (current == null)
        {
            return new Node(val);
        }
        
        //wenn wert kleiner als wert vom aktuellen knoten, dann element im linken teilbaum adden
        if (val < current.value)
        {
            current.left = AddHelper(current.left, val);
        }
        // andernfalls bei rechtem baum einfügen
        else
        {
            current.right = AddHelper(current.right, val);
        }

        // aktuellen knoten zurückgeben
        return current;
    }

    
    //inorder traversierung und aktion für jedes element ausführen
    private void InOrderTraversal(Node node, Action<int> action)
    {
        if (node == null)
        {
            return;
        }

        InOrderTraversal(node.left, action);
        action(node.value);
        InOrderTraversal(node.right, action);
    }

    // Methode, die ein sortiertes Array der Elemente des Baums zurückgibt
    public int[] ToArray()
    {
        int count = Count(root);  // Zählt die Anzahl der Knoten im Baum
        int[] arr = new int[count];  // Erstellt ein leeres Array der Größe des Baums
        int i = 0;

        // Führt die Inorder-Traversierung des Baums aus und fügt jedes Element dem Array hinzu
        InOrderTraversal(root, (val) => {
            arr[i++] = val;
        });

        // Gibt Array zurück
        return arr;
    }

    // Zählt die Anzahl der Knoten im Baum rekursiv
    private int Count(Node node)
    {
        if (node == null)
        {
            return 0;
        }

        return Count(node.left) + Count(node.right) + 1;
    }

    // sortiertes Array in der Mitte aufteilen und kleineren Elemente links und die größeren Elemente rechts platziert
    public void Rebuild()
    {
        int[] sortedArr = ToArray();   // Ruft ToArray-Methode auf, um das sortierte Array der Elemente des Baums zu erhalten
        root = BuildHelper(sortedArr, 0, sortedArr.Length - 1);   // Ruft Hilfsmethode BuildHelper auf, um den Baum neu aufzubauen
    }
    
    // Hilfsmethode, die den Baum anhand des sortierten Arrays und der Anfangs- und Endposition neu aufsetzt
    private Node BuildHelper(int[] arr, int start, int end)
    {
        // Wenn die Startposition größer als die Endposition ist, gibt es keine weiteren Elemente zu platzieren, daher geben wir null zurück
        if (start > end)
        {
            return null;
        }

        // Berechnet Mitte des Arrays und erstellt einen neuen Knoten mit dem Wert des Elements in der Mitte
        int mid = (start + end) / 2;
        Node node = new Node(arr[mid]);

        // Methode aufrufen, um linken und rechten Teilbaum vom aktuellen Knoten aufzubauen
        node.left = BuildHelper(arr, start, mid - 1);
        node.right = BuildHelper(arr, mid + 1, end);

        return node;
    }
}

// Hauptprogramm
class Program
{
    static void Main(string[] args)
    {
        int[] arr = { 8, 3, 10, 1, 6, 14, 4, 7, 13 , 4}; // 
        BinaryTree tree = new BinaryTree(); // neuer leerer baum

        // Fügt jedes Element des Arrays zum Baum hinzu
        foreach (int val in arr)
        {
            tree.Add(val);
        }

        Console.WriteLine("Unsorted array:");
        PrintArray(arr); // Gibt unsortiertes Array aus

        tree.Rebuild(); // Baut Baum neu auf

        Console.WriteLine("Sorted array:");
        PrintArray(tree.ToArray()); // Gibt das sortierte Array aus
    }

// Methode, die ein Array von ganzen Zahlen ausgibt
    static void PrintArray(int[] arr)
    {
        foreach (int val in arr)
        {
            Console.Write(val + " ");
        }

        Console.WriteLine();
    }
}
