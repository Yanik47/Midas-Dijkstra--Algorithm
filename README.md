Below is your text rewritten in a formal style and translated into English.

---

# Overview

This coursework, focusing on algorithms and data structures, addresses the solution to the “Przemitnicy” problem (see: [Przemitciny Task Description](https://www.mimuw.edu.pl/~kowalik/asd/2003/Zadanie3/z3_2003.pdf)). 

## Project Description

This coursework is dedicated to solving the “Przemitnicy” problem, which involves optimizing the transportation of materials across a border. The primary objective was to develop an algorithm that determines the most advantageous method of material conversion, thereby minimizing costs while maximizing profit.

The project comprises the following components:

- **Graph Implementation**: A representation of the relationships between various materials.
- **Dijkstra’s Algorithm**: Utilized for finding the optimal path through the graph.
- **Fibonacci Heap**: Employed as a data structure for managing the priority queue.

From an algorithmic perspective, the graph traversal is performed in O(E + V·log V) time, where E denotes the number of edges and V represents the number of vertices.

## Project Structure

- **Graph.cs, Node.cs, Edge.cs**: These files implement the graph, its nodes, and its edges.
- **FibonacciHeap.cs, FibonacciHeapNode.cs, CustomComparer.cs**: These files contain the implementation of the Fibonacci heap and the auxiliary classes necessary for its operation.
- **SortDirection.cs**: Defines the sorting order (ascending or descending).
- **App.xaml.cs, DataPage.xaml.cs, ResultsPage.xaml.cs**: Contain the UI logic for data loading, algorithm execution, and result display.
- **z3_2003.pdf**: Provides the problem description.

### Theoretical Foundations and Application of the Fibonacci Heap

The Fibonacci heap is a data structure used to implement a priority queue with amortized efficient operation times. Its design minimizes tree operations, making it particularly effective for algorithms that intensively use a priority queue, such as Dijkstra’s algorithm.

#### Key Features of the Fibonacci Heap

1. **Hybrid Structure**: It consists of a collection of trees in which the minimum element is always located at the root of one of the trees.
2. **Amortized Operation Times**:
   - **Insertion**: O(1)
   - **Heap Merge (Union)**: O(1)
   - **Decrease-Key**: O(1)
   - **Extract-Minimum**: O(log n)
3. **Lazy Consolidation**: Nodes with the same degree are merged only during the extraction of the minimum element, thereby reducing the number of operations and increasing efficiency.

#### Implementation in the Project

The Fibonacci heap is implemented in the project using two primary classes:

- **FibonacciHeap.cs**: Contains the core logic for the heap, including operations for insertion, extraction of the minimum element, key updates, and heap merging.
- **FibonacciHeapNode.cs**: Implements the heap nodes, which include references to the parent and children, as well as properties to track the loss of child nodes (essential for balancing).

The use of the Fibonacci heap enhances the performance of Dijkstra’s algorithm, particularly when handling large graphs.

#### Key Implementation Aspects

1. **Trees**:
   - Nodes are organized into multiple trees, each characterized by its degree (i.e., the number of children).
   - The nodes are maintained in a doubly-linked list, which facilitates the rapid addition or removal of nodes.
2. **Balancing**:
   - During the extraction of the minimum element, trees with identical degrees are merged to minimize tree height and expedite subsequent operations.
3. **Key Updates**:
   - When a key is decreased, the node is relocated to the root list if the minimum heap property is violated.
   - If a node has already lost a child, it is also moved to the root list, thus preventing excessive imbalance.

#### Application of the Fibonacci Heap in the Project

The implementation of Dijkstra’s algorithm within the project leverages the Fibonacci heap to accelerate priority queue operations. This is particularly crucial when processing graphs with a large number of edges and vertices, where conventional binary heap implementations could become a performance bottleneck.

1. **Initialization**:
   - All graph nodes are inserted into the Fibonacci heap with an initial distance value (e.g., ∞ for all nodes except the source node).
2. **Minimum Node Extraction**:
   - The heap facilitates the extraction of the node with the minimum distance in O(log n) time, thereby expediting the selection of the next vertex for processing.
3. **Distance Updates**:
   - When a shorter path to a neighboring vertex is identified, the distance is updated and the corresponding node is repositioned within the heap.
4. **Heap Merging**:
   - Although heap merging is not directly used in the current implementation, this functionality could be extended to support divided graphs or multi-threaded algorithms.

#### Advantages of Using the Fibonacci Heap

- **Algorithmic Acceleration**: The Fibonacci heap reduces the number of operations involved in handling the priority queue, making it well-suited for graphs with a high number of edges.
- **Research Interest**: Beyond its practical applications, the Fibonacci heap represents a rich subject for theoretical study, enhancing both conceptual understanding and practical skills.
- **Versatility**: The implementation supports both min-heaps and max-heaps, thereby offering versatility for various applications.

#### Notable Implementation Details in the Code

- Use of enumerations (enum) to define the sorting direction (minimum or maximum heap).
- A specialized comparator (CustomComparer.cs) to configure the sorting order.
- A flexible node structure that supports references to parents, children, and sibling nodes.

### Modernization of the Standard Project

During the resolution of this problem, certain aspects of the initial approach were reconsidered and revised.

#### UWP (Universal Windows Platform)

Since there were no restrictions regarding the choice of programming language, C# was selected. The goal was to incorporate a graphical user interface and implement functionality within a desktop application. Although the input data and requirements limited extensive experimentation with the UI, valuable experience was gained in developing Windows desktop applications.

#### Economy Simulation

Upon reviewing the problem statement, it was hypothesized that the potential profit might vary for large versus small quantities of gold. This is due to the fact that the tax amount is dependent on the transported weight, increasing with the quantity, while the cost of alchemists’ services remains constant regardless of the amount (i.e., the cost for transporting 1000 tons is equivalent to that for 100 grams).

This consideration led to the development of a market simulation within the kingdom. The sale of each resource would decrease its market price, thereby preventing the importation of 1000 tons of gold into a city and the subsequent possibility of infinite wealth—since an oversupplied market would drive the price of gold to a minimum. The new parameters introduced were the **quantity of the resource available within the kingdom** and the **weight of the resource transported across the border**.

This approach required an adaptation of the algorithm to account for the new conditions. Instead of executing a standard Dijkstra’s traversal from a single node to all nodes, selecting the minimum, and then computing the reverse path, it became necessary to construct a priority list that incorporated conversion costs adjusted by the ratio of the transported weight to the available weight in the kingdom. Additionally, a secondary priority list was computed for each element of the primary list.

The graphical interface includes two output fields, each demonstrating the performance of the standard algorithm and the modified version, thus allowing a comparative analysis of the impact of the optimization.

Under standard problem conditions, there is a pronounced imbalance in the market price of gold, which minimizes noticeable differences in outcomes. However, certain modifications to the input parameters indicate that price normalization and the inclusion of a broader range of resources significantly complicate the problem, allowing the algorithm to perform substantially better under these enhanced conditions.

### Screenshots :)

### Main Page
![MainPage](https://drive.google.com/uc?export=view&id=1Ige5pyO1hZx1m-IwnKg83-o1o9TtEuXA)
### Page with data
!(SecondPage)[https://drive.google.com/uc?export=view&id=1_zwbI3cF3RfGHbCPfL77YPpSF4XbPJUN]

