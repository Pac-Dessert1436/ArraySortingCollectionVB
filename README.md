# ArraySortingCollectionVB

A comprehensive collection of array sorting algorithms implemented in VB.NET, providing efficient and well-documented sorting operations for arrays of comparable elements.

> **Note**: This library is fully compatible with other .NET languages (including C# and F#). Rest assured that you can use this library in any .NET project regardless of the programming language you prefer.

## 📦 Package Information

- **Version:** 1.0.0
- **Target Framework:** .NET 8.0
- **License:** BSD-3-Clause
- **Language:** VB.NET

## 📝 Requirements

- [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) or later
- Works with VB.NET, C#, F# and other .NET languages

## 🚀 Installation

Install the package via NuGet:

```powershell
Install-Package ArraySortingCollectionVB
```

Or via .NET CLI:

```bash
dotnet add package ArraySortingCollectionVB
```

## ✨ Features

This library provides a comprehensive set of sorting algorithms, each optimized for different use cases:

### Simple Sorting Algorithms
- **Selection Sort** - Simple comparison-based sorting
- **Bubble Sort** - Repeatedly swaps adjacent elements
- **Insertion Sort** - Builds sorted array one element at a time
- **Gnome Sort** - Simple sorting algorithm similar to insertion sort
- **Cocktail Sort** - Bidirectional bubble sort

### Efficient Sorting Algorithms
- **Quick Sort** - Divide-and-conquer algorithm with O(n log n) average complexity
- **Merge Sort** - Stable divide-and-conquer algorithm
- **Heap Sort** - Uses binary heap data structure
- **C++ Standard Sort** - Hybrid algorithm combining Quick Sort, Heap Sort, and Insertion Sort

### Specialized Sorting Algorithms
- **Pancake Sort** - Returns sorted array with flip count (useful for bioinformatics)
- **Radix Sort (LSD)** - Least Significant Digit first
- **Radix Sort (MSD)** - Most Significant Digit first

## 📖 Usage

All sorting methods accept arrays of any type that implements `IComparable(Of T)` and return a new sorted array without modifying the original.

### Basic Example

```vb
Imports ArraySortingCollectionVB

Module Program
    Sub Main()
        Dim numbers = {5, 2, 8, 1, 9, 3}
        
        ' Using Quick Sort
        Dim sortedNumbers = ArraySorting.QuickSort(numbers)
        
        Console.WriteLine(String.Join(", ", sortedNumbers))
        ' Output: 1, 2, 3, 5, 8, 9
    End Sub
End Module
```

### Sorting Strings

```vb
Dim names = {"Alice", "Bob", "Charlie", "David"}
Dim sortedNames = ArraySorting.MergeSort(names)
```

### Sorting Custom Objects

```vb
Public Class Person
    Implements IComparable(Of Person)
    
    Public Property Name As String
    Public Property Age As Integer
    
    Public Function CompareTo(other As Person) As Integer Implements IComparable(Of Person).CompareTo
        Return Me.Age.CompareTo(other.Age)
    End Function
End Class

Dim people = {
    New Person With {.Name = "Alice", .Age = 30},
    New Person With {.Name = "Bob", .Age = 25},
    New Person With {.Name = "Charlie", .Age = 35}
}

Dim sortedPeople = ArraySorting.QuickSort(people)
```

### Pancake Sort with Flip Count

```vb
Dim numbers = {3, 1, 4, 2, 5}
Dim result = ArraySorting.PancakeSort(numbers)

Console.WriteLine($"Sorted: {String.Join(", ", result.resultArr)}")
Console.WriteLine($"Flips: {result.numFlips}")
```

## 📊 Algorithm Comparison

| Algorithm | Time Complexity (Average) | Time Complexity (Worst) | Space Complexity | Stable |
|-----------|---------------------------|-------------------------|------------------|--------|
| Selection Sort | O(n²) | O(n²) | O(1) | No |
| Bubble Sort | O(n²) | O(n²) | O(1) | Yes |
| Insertion Sort | O(n²) | O(n²) | O(1) | Yes |
| Gnome Sort | O(n²) | O(n²) | O(1) | Yes |
| Cocktail Sort | O(n²) | O(n²) | O(1) | Yes |
| Quick Sort | O(n log n) | O(n²) | O(log n) | No |
| Merge Sort | O(n log n) | O(n log n) | O(n) | Yes |
| Heap Sort | O(n log n) | O(n log n) | O(1) | No |
| C++ Standard Sort | O(n log n) | O(n log n) | O(log n) | No |
| Pancake Sort | O(n²) | O(n²) | O(1) | No |
| Radix Sort (LSD/MSD) | O(nk) | O(nk) | O(n + k) | Yes |

## 🎯 When to Use Each Algorithm

### Use Selection Sort when:
- You need a simple, easy-to-understand algorithm
- Memory usage is a concern (in-place sorting)
- The array is small

### Use Bubble Sort when:
- The array is nearly sorted
- You need to detect if the array is already sorted
- Simplicity is more important than performance

### Use Insertion Sort when:
- The array is small or nearly sorted
- You're building a sorted array incrementally
- Memory usage is a concern

### Use Quick Sort when:
- You need average-case O(n log n) performance
- The array is large and randomly distributed
- Memory usage is a concern (in-place sorting)

### Use Merge Sort when:
- You need stable sorting
- You have enough memory for O(n) space
- You need guaranteed O(n log n) performance

### Use Heap Sort when:
- You need guaranteed O(n log n) performance
- Memory usage is a concern
- You don't need stable sorting

### Use Pancake Sort when:
- You need to track the number of operations
- Working with bioinformatics applications
- Educational purposes

### Use Radix Sort when:
- Sorting integers or strings with fixed-length keys
- You need linear time complexity for certain data types
- The range of values is limited

## 🔧 API Reference

### ArraySorting Module

All methods are static members of the `ArraySorting` module.

#### Methods

```vb
Public Function SelectionSort(Of T As IComparable)(ParamArray values As T()) As T()
```

Sorts an array using selection sort algorithm.

```vb
Public Function BubbleSort(Of T As IComparable)(ParamArray values As T()) As T()
```

Sorts an array using bubble sort algorithm.

```vb
Public Function InsertionSort(Of T As IComparable)(ParamArray values As T()) As T()
```

Sorts an array using insertion sort algorithm.

```vb
Public Function GnomeSort(Of T As IComparable)(ParamArray values As T()) As T()
```

Sorts an array using gnome sort algorithm.

```vb
Public Function CocktailSort(Of T As IComparable)(ParamArray values As T()) As T()
```

Sorts an array using cocktail sort algorithm.

```vb
Public Function QuickSort(Of T As IComparable)(ParamArray values As T()) As T()
```

Sorts an array using quick sort algorithm.

```vb
Public Function MergeSort(Of T As IComparable)(ParamArray values As T()) As T()
```

Sorts an array using merge sort algorithm.

```vb
Public Function HeapSort(Of T As IComparable)(ParamArray values As T()) As T()
```

Sorts an array using heap sort algorithm.

```vb
Public Function CppStandardSort(Of T As IComparable)(ParamArray values As T()) As T()
```

Sorts an array using C++ standard sort hybrid algorithm.

```vb
Public Function PancakeSort(Of T As IComparable)(ParamArray values As T()) As (resultArr As T(), numFlips As Integer)
```

Sorts an array using pancake sort algorithm and returns a tuple with the sorted array and number of flips.

```vb
Public Function RadixSortLSD(Of T As IComparable)(ParamArray values As T()) As T()
```

Sorts an array using radix sort algorithm (LSD variant).

```vb
Public Function RadixSortMSD(Of T As IComparable)(ParamArray values As T()) As T()
```

Sorts an array using radix sort algorithm (MSD variant).

## 🧪 Testing

The library is designed to work with any type that implements `IComparable(Of T)`. Here's a simple test:

```vb
Imports ArraySortingCollectionVB
Imports System

Module TestModule
    Sub TestSorting()
        Dim testArray = {5, 2, 8, 1, 9, 3, 7, 4, 6}
        
        Dim algorithms = {
            AddressOf ArraySorting.SelectionSort,
            AddressOf ArraySorting.BubbleSort,
            AddressOf ArraySorting.InsertionSort,
            AddressOf ArraySorting.GnomeSort,
            AddressOf ArraySorting.CocktailSort,
            AddressOf ArraySorting.QuickSort,
            AddressOf ArraySorting.MergeSort,
            AddressOf ArraySorting.HeapSort,
            AddressOf ArraySorting.CppStandardSort,
            AddressOf ArraySorting.RadixSortLSD,
            AddressOf ArraySorting.RadixSortMSD
        }
        
        For Each algorithm In algorithms
            Dim result = algorithm(testArray)
            Console.WriteLine($"{algorithm.Method.Name}: {String.Join(", ", result)}")
        Next
    End Sub
End Module
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📄 License

This project is licensed under the BSD-3-Clause License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- This library implements classic sorting algorithms with modern VB.NET best practices
- All algorithms are implemented with proper XML documentation comments
- Special attention paid to memory efficiency and performance optimization

## 📚 Additional Resources

- [Sorting Algorithm Visualizations](https://www.youtube.com/watch?v=kPRA0W1kECg)
- [Algorithm Complexity Analysis](https://www.bigocheatsheet.com/)
- [VB.NET Documentation](https://docs.microsoft.com/en-us/dotnet/visual-basic/)

## 📞 Support

For issues, questions, or contributions, please visit the [GitHub repository](https://github.com/Pac-Dessert1436/ArraySortingCollectionVB).