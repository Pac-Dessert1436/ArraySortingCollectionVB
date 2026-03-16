Option Strict On
Option Infer On

''' <summary>
''' Contains methods for sorting arrays of comparable elements.
''' </summary>
Public Module ArraySorting
    ''' <summary>
    ''' Swaps the values of two variables.
    ''' </summary>
    ''' <typeparam name="T">The type of the variables.</typeparam>
    ''' <param name="left">The first variable.</param>
    ''' <param name="right">The second variable.</param>
    Private Sub Swap(Of T)(ByRef left As T, ByRef right As T)
        Dim temp As T = left
        left = right
        right = temp
    End Sub

    ''' <summary>
    ''' Partitions the array into two parts around a pivot element.
    ''' </summary>
    ''' <typeparam name="T">The type of the elements in the array (must implement IComparable).</typeparam>
    ''' <param name="arr">The array to be partitioned.</param>
    ''' <param name="left">The starting index of the partition.</param>
    ''' <param name="right">The ending index of the partition.</param>
    ''' <returns>The index of the pivot element after partitioning.</returns>
    Private Function Partition(Of T As IComparable) _
            (arr As T(), left As Integer, right As Integer) As Integer
        Dim pivot As T = arr(right)
        Dim i As Integer = left - 1
        For j As Integer = left To right - 1
            If arr(j).CompareTo(pivot) <= 0 Then
                i += 1
                Swap(arr(i), arr(j))
            End If
        Next j
        Swap(arr(i + 1), arr(right))
        Return i + 1
    End Function

    ''' <summary>
    ''' Maintains the max heap property for a subtree rooted at index i.
    ''' </summary>
    ''' <typeparam name="T">The type of the elements in the array (must implement IComparable).</typeparam>
    ''' <param name="arr">The array representing the heap.</param>
    ''' <param name="n">The size of the heap.</param>
    ''' <param name="i">The index of the root of the subtree.</param>
    ''' <param name="offset">The offset in the array where the heap starts.</param>
    Private Sub Heapify(Of T As IComparable) _
            (arr As T(), n As Integer, i As Integer, Optional offset As Integer = 0)
        Dim stack As New Stack(Of
            (arr As T(), n As Integer, i As Integer, offset As Integer)
        )
        stack.Push((arr, n, i, offset))
        Do Until stack.Count = 0
            With stack.Pop()
                Dim largest = .i
                Dim bestItem = .arr(.offset + largest)
                Dim leftChild = 2 * .i + 1
                Dim rightChild = 2 * .i + 2

                ' Compare with left child
                Dim leftComparison = .arr(.offset + leftChild).CompareTo(bestItem)
                If leftChild < n AndAlso leftComparison > 0 Then
                    largest = leftChild
                End If

                ' Compare with right child
                Dim rightComparison = .arr(.offset + rightChild).CompareTo(bestItem)
                If rightChild < .n AndAlso rightComparison > 0 Then
                    largest = rightChild
                End If

                ' If largest is not root
                If largest <> .i Then
                    Swap(.arr(offset + .i), .arr(offset + largest))
                    ' Recursively heapify affected subtree
                    stack.Push((.arr, .n, largest, offset))
                End If
            End With
        Loop
    End Sub

    ''' <summary>
    ''' Sorts an array of comparable elements using the selection sort algorithm.
    ''' </summary>
    ''' <remarks>
    ''' The selection sort algorithm works by repeatedly finding the minimum element
    ''' from the unsorted part of the array and putting it at the beginning.
    ''' </remarks>
    ''' <typeparam name="T">The type of the elements in the array (must implement IComparable).</typeparam>
    ''' <param name="values">The array of comparable elements to sort.</param>
    ''' <returns>The sorted array.</returns>
    Public Function SelectionSort(Of T As IComparable)(ParamArray values As T()) As T()
        Dim result As T() = CType(values.Clone(), T())
        Dim FindMinIndex = Function(arr As T(), first As Integer, last As Integer) As Integer
                               Dim minIdx = first
                               For i As Integer = first To last - 1 Step 1
                                   If arr(i).CompareTo(arr(minIdx)) < 0 Then minIdx = i
                               Next i
                               Return minIdx
                           End Function
        For i As Integer = 0 To values.Length - 2
            Dim j = FindMinIndex(result, i, result.Length)
            Swap(result(i), result(j))
        Next i
        Return result
    End Function

    ''' <summary>
    ''' Sorts an array of comparable elements using the pancake sort algorithm, and returns
    ''' a tuple containing the sorted array and the number of flips performed.
    ''' </summary>
    ''' <remarks>
    ''' The pancake sort algorithm works by repeatedly flipping the maximum element
    ''' to the end of the array and then flipping the entire array to put the maximum
    ''' element in its correct position, useful for bioinformatics applications.
    ''' </remarks>
    ''' <typeparam name="T">The type of the elements in the array (must implement IComparable).</typeparam>
    ''' <param name="values">The array of comparable elements to sort.</param>
    ''' <returns>The sorted array and the number of flips.</returns>
    Public Function PancakeSort(Of T As IComparable) _
            (ParamArray values As T()) As (resultArr As T(), numFlips As Integer)
        Dim result As T() = CType(values.Clone(), T())
        Dim numFlips As Integer = 0

        Dim FindMaxIndex = Function(arr As T(), n As Integer) As Integer
                               Dim maxIdx = 0
                               For i = 0 To n - 1 Step 1
                                   If arr(i).CompareTo(arr(maxIdx)) > 0 Then maxIdx = i
                               Next i
                               Return maxIdx
                           End Function
        Dim Flip = Sub(arr As T(), i As Integer)
                       Dim start As New Integer
                       While start < i
                           Swap(arr(start), arr(i))
                           start += 1
                           i -= 1
                       End While
                       numFlips += 1
                   End Sub
        For curr As Integer = values.Length To 2 Step -1
            Dim maxIdx = FindMaxIndex(values, curr)
            If maxIdx <> curr - 1 Then
                Flip(values, maxIdx)
                Flip(values, curr - 1)
            End If
        Next curr
        Return (result, numFlips)
    End Function

    ''' <summary>
    ''' Sorts an array of comparable elements using the bubble sort algorithm.
    ''' </summary>
    ''' <remarks>
    ''' The bubble sort algorithm works by repeatedly swapping adjacent elements
    ''' if they are in the wrong order.
    ''' </remarks>
    ''' <typeparam name="T">The type of the elements in the array (must implement IComparable).</typeparam>
    ''' <param name="values">The array of comparable elements to sort.</param>
    ''' <returns>The sorted array.</returns>
    Public Function BubbleSort(Of T As IComparable)(ParamArray values As T()) As T()
        Dim result As T() = CType(values.Clone(), T())
        Dim swapped As Boolean = True
        Dim n As Integer = values.Length

        While swapped
            swapped = False
            For i As Integer = 0 To n - 2
                If values(i).CompareTo(values(i + 1)) > 0 Then
                    Swap(values(i), values(i + 1))
                    swapped = True
                End If
            Next i
            n -= 1
        End While
        Return result
    End Function

    ''' <summary>
    ''' Sorts an array of comparable elements using the insertion sort algorithm.
    ''' </summary>
    ''' <remarks>
    ''' The insertion sort algorithm works by building a sorted array one element at a time.
    ''' </remarks>
    ''' <typeparam name="T">The type of the elements in the array (must implement IComparable).</typeparam>
    ''' <param name="values">The array of comparable elements to sort.</param>
    ''' <returns>The sorted array.</returns>
    Public Function InsertionSort(Of T As IComparable)(ParamArray values As T()) As T()
        Dim result As T() = CType(values.Clone(), T())
        For i As Integer = 1 To values.Length - 1
            Dim j As Integer = i
            While j > 0 AndAlso values(j).CompareTo(values(j - 1)) < 0
                Swap(values(j), values(j - 1))
                j -= 1
            End While
        Next i
        Return result
    End Function

    ''' <summary>
    ''' Sorts an array of comparable elements using the gnome sort algorithm.
    ''' </summary>
    ''' <remarks>
    ''' The gnome sort algorithm works by repeatedly swapping adjacent elements
    ''' if they are in the wrong order.
    ''' </remarks>
    ''' <typeparam name="T">The type of the elements in the array (must implement IComparable).</typeparam>
    ''' <param name="values">The array of comparable elements to sort.</param>
    ''' <returns>The sorted array.</returns>
    Public Function GnomeSort(Of T As IComparable)(ParamArray values As T()) As T()
        Dim result As T() = CType(values.Clone(), T())
        Dim i As Integer = 1
        While i < values.Length
            If values(i - 1).CompareTo(values(i)) <= 0 Then
                i += 1
            Else
                Swap(values(i - 1), values(i))
                If i > 1 Then i -= 1
            End If
        End While
        Return result
    End Function

    ''' <summary>
    ''' Sorts an array of comparable elements using the merge sort algorithm.
    ''' </summary>
    ''' <remarks>
    ''' The merge sort algorithm works by dividing the array into two halves,
    ''' sorting each half, and then merging the sorted halves.
    ''' </remarks>
    ''' <typeparam name="T">The type of the elements in the array (must implement IComparable).</typeparam>
    ''' <param name="values">The array of comparable elements to sort.</param>
    ''' <returns>The sorted array.</returns>
    Public Function MergeSort(Of T As IComparable)(ParamArray values As T()) As T()
        If values.Length <= 1 Then Return CType(values.Clone(), T())
        Dim mergeStack As New Stack(Of (arr As T(), left As Integer, right As Integer))
        Dim result As T() = CType(values.Clone(), T())

        ' Push the initial range to sort
        mergeStack.Push((result, 0, result.Length - 1))
        ' First, recursively divide into subarrays using stack
        Dim divideStack As New Stack(Of (arr As T(), left As Integer, right As Integer))
        divideStack.Push((result, 0, result.Length - 1))

        ' Step 1: Divide phase
        While divideStack.Count > 0
            With divideStack.Pop()
                If .left < .right Then
                    Dim mid = (.left + .right) \ 2

                    ' Push the ranges for later merging
                    mergeStack.Push((.arr, .left, .right))
                    mergeStack.Push((.arr, .left, mid))
                    mergeStack.Push((.arr, mid + 1, .right))
                    ' Continue dividing
                    divideStack.Push((.arr, .left, mid))
                    divideStack.Push((.arr, mid + 1, .right))
                End If
            End With
        End While

        ' Step 2: Merge phase
        While mergeStack.Count > 0
            With mergeStack.Pop()
                If .left < .right Then
                    Dim mid = (.left + .right) \ 2, temp(.right - .left + 1) As T
                    Dim i As Integer = .left, j As Integer = mid + 1, k As Integer = 0
                    While i <= mid AndAlso j <= .right
                        If .arr(i).CompareTo(.arr(j)) <= 0 Then
                            temp(k) = .arr(i)
                            i += 1
                        Else
                            temp(k) = .arr(j)
                            j += 1
                        End If
                        k += 1
                    End While
                    While i <= mid
                        temp(k) = .arr(i)
                        i += 1
                        k += 1
                    End While
                    While j <= .right
                        temp(k) = .arr(j)
                        j += 1
                        k += 1
                    End While
                    For m As Integer = 0 To temp.Length - 1
                        .arr(.left + m) = temp(m)
                    Next m
                End If
                result = .arr
            End With
        End While
        Return result
    End Function

    ''' <summary>
    ''' Sorts an array of comparable elements using the radix sort algorithm (LSD variant).
    ''' </summary>
    ''' <remarks>
    ''' The radix sort algorithm works by sorting the elements digit by digit,
    ''' starting from the least significant digit to the most significant digit.
    ''' </remarks>
    ''' <typeparam name="T">The type of the elements in the array (must implement IComparable).</typeparam>
    ''' <param name="values">The array of comparable elements to sort.</param>
    ''' <returns>The sorted array.</returns>
    Public Function RadixSortLSD(Of T As IComparable)(ParamArray values As T()) As T()
        Dim result As T() = CType(values.Clone(), T())
        If values.Length <= 1 Then Return result
        Dim max As T = values(0)
        For Each val As T In values
            If val.CompareTo(max) > 0 Then max = val
        Next val
        Dim maxDigits = max.ToString().Length
        For d As Integer = 0 To maxDigits - 1
            Dim buckets As New List(Of T)(Aggregate v In values Into ToList())
            For Each v As T In values
                Dim digit = CInt(v.ToString().Substring(d, 1))
                buckets(digit) = v
            Next v
            For i As Integer = 0 To values.Length - 1
                values(i) = buckets(i)
            Next i
        Next d
        Return result
    End Function

    ''' <summary>
    ''' Sorts an array of comparable elements using the radix sort algorithm (MSD variant).
    ''' </summary>
    ''' <remarks>
    ''' The radix sort algorithm works by sorting the elements digit by digit,
    ''' starting from the most significant digit to the least significant digit.
    ''' </remarks>
    ''' <typeparam name="T">The type of the elements in the array (must implement IComparable).</typeparam>
    ''' <param name="values">The array of comparable elements to sort.</param>
    ''' <returns>The sorted array.</returns>
    Public Function RadixSortMSD(Of T As IComparable)(ParamArray values As T()) As T()
        Dim result As T() = CType(values.Clone(), T())
        If values.Length <= 1 Then Return result
        Dim max As T = values(0)
        For Each val As T In values
            If val.CompareTo(max) > 0 Then max = val
        Next val
        Dim maxDigits = max.ToString().Length
        For d As Integer = maxDigits - 1 To 0 Step -1
            Dim buckets As New List(Of T)(Aggregate v In values Into ToList())
            For Each v As T In values
                Dim digit = CInt(v.ToString().Substring(d, 1))
                buckets(digit) = v
            Next v
            For i As Integer = 0 To values.Length - 1
                values(i) = buckets(i)
            Next i
        Next d
        Return result
    End Function

    ''' <summary>
    ''' Sorts an array of comparable elements using the quick sort algorithm.
    ''' </summary>
    ''' <remarks>
    ''' The quick sort algorithm works by partitioning the array into two halves,
    ''' sorting each half, and then merging the sorted halves.
    ''' </remarks>
    ''' <typeparam name="T">The type of the elements in the array (must implement IComparable).</typeparam>
    ''' <param name="values">The array of comparable elements to sort.</param>
    ''' <returns>The sorted array.</returns>
    Public Function QuickSort(Of T As IComparable)(ParamArray values As T()) As T()
        Dim result As T() = CType(values.Clone(), T())
        If values.Length <= 1 Then Return result
        Dim stack As New Stack(Of (left As Integer, right As Integer))
        stack.Push((0, values.Length - 1))
        Do Until stack.Count = 0
            With stack.Pop()
                If .left < .right Then
                    Dim pivotIdx = Partition(result, .left, .right)
                    stack.Push((.left, pivotIdx - 1))
                    stack.Push((pivotIdx + 1, .right))
                End If
            End With
        Loop
        Return result
    End Function

    ''' <summary>
    ''' Sorts an array of comparable elements using the cocktail sort algorithm.
    ''' </summary>
    ''' <remarks>
    ''' The cocktail sort algorithm works by sorting the elements bidirectionally,
    ''' starting from the least significant digit to the most significant digit.
    ''' </remarks>
    ''' <typeparam name="T">The type of the elements in the array (must implement IComparable).</typeparam>
    ''' <param name="values">The array of comparable elements to sort.</param>
    ''' <returns>The sorted array.</returns>
    Public Function CocktailSort(Of T As IComparable)(ParamArray values As T()) As T()
        Dim result As T() = CType(values.Clone(), T())
        Dim swapped As Boolean = True
        Dim start As Integer = 0, [end] As Integer = values.Length - 1

        While swapped
            swapped = False
            ' Forward pass
            For i As Integer = start To [end] - 1
                If result(i).CompareTo(result(i + 1)) > 0 Then
                    Swap(result(i), result(i + 1))
                    swapped = True
                End If
            Next i

            If Not swapped Then Exit While
            swapped = False
            [end] -= 1
            ' Backward pass
            For i As Integer = [end] - 1 To start Step -1
                If result(i).CompareTo(result(i + 1)) > 0 Then
                    Swap(result(i), result(i + 1))
                    swapped = True
                End If
            Next i
            start += 1
        End While
        Return result
    End Function

    ''' <summary>
    ''' Sorts an array of comparable elements using the C++ standard sort algorithm.
    ''' </summary>
    ''' <remarks>
    ''' The C++ standard sort algorithm (<c>std::sort</c>) typically uses a hybrid of 
    ''' Quick Sort, Heap Sort, and Insertion Sort.
    ''' </remarks>
    ''' <typeparam name="T">The type of the elements in the array (must implement IComparable).</typeparam>
    ''' <param name="values">The array of comparable elements to sort.</param>
    ''' <returns>The sorted array.</returns>
    Public Function CppStandardSort(Of T As IComparable)(ParamArray values As T()) As T()
        Dim result As T() = CType(values.Clone(), T())

        ' Step 1: Sort small subarrays using Insertion Sort
        Dim InsertionSort = Sub(ByRef arr As T(), left As Integer, right As Integer)
                                For i As Integer = left + 1 To right
                                    Dim key As T = arr(i)
                                    Dim j As Integer = i - 1
                                    While j >= left AndAlso arr(j).CompareTo(key) > 0
                                        arr(j + 1) = arr(j)
                                        j -= 1
                                    End While
                                    arr(j + 1) = key
                                Next i
                            End Sub

        ' Step 2: Sort medium-sized arrays using Heap Sort
        Dim HeapSort = Sub(ByRef arr As T(), left As Integer, right As Integer)
                           Dim n As Integer = right - left + 1

                           ' Build heap
                           For i As Integer = n \ 2 - 1 To 0 Step -1
                               Heapify(arr, n, i, left)
                           Next i
                           ' Extract elements from heap
                           For i As Integer = n - 1 To 0 Step -1
                               ' Move current root to end
                               Swap(arr(left), arr(left + i))
                               ' Heapify reduced heap
                               Heapify(arr, i, 0, left)
                           Next i
                       End Sub

        ' Step 3: Sort large arrays using IntroSort
        Dim IntroSort = Sub(arr As T(), left As Integer, right As Integer, depthLimit As Double)
                            Dim stack As New Stack(Of
                                (arr As T(), left As Integer, right As Integer, depthLimit As Double)
                            )
                            stack.Push((arr, left, right, depthLimit))
                            Do Until stack.Count = 0
                                With stack.Pop()
                                    ' If subarray is small, use Insertion Sort (optimization)
                                    If .right - .left <= 16 Then
                                        InsertionSort(.arr, .left, .right)
                                        Continue Do
                                    ElseIf .depthLimit = 0 Then
                                        HeapSort(.arr, .left, .right)
                                        Continue Do
                                    Else
                                        ' Otherwise, use QuickSort partition
                                        Dim pivotIndex = Partition(.arr, .left, .right)
                                        ' Recursively sort the two partitions with reduced depth limit
                                        stack.Push((.arr, .left, pivotIndex - 1, .depthLimit - 1))
                                        stack.Push((.arr, pivotIndex + 1, .right, .depthLimit - 1))
                                    End If
                                End With
                            Loop
                        End Sub

        ' Last Step: Sort the entire array using IntroSort
        IntroSort(result, 0, result.Length - 1, 2 * Math.Floor(Math.Log(result.Length, 2)))
        Return result
    End Function

    ''' <summary>
    ''' Sorts an array of comparable elements using the Shell Sort algorithm.
    ''' </summary>
    ''' <remarks>
    ''' Shell sort is an in-place comparison sort which is a generalization of insertion sort that
    ''' allows the exchange of items that are far apart. The method starts by sorting pairs of
    ''' elements far apart from each other, then progressively reducing the gap between elements
    ''' to be compared.
    ''' </remarks>
    ''' <typeparam name="T">The type of the elements in the array (must implement IComparable).</typeparam>
    ''' <param name="values">The array of comparable elements to sort.</param>
    ''' <returns>The sorted array.</returns>
    Public Function ShellSort(Of T As IComparable)(ParamArray values As T()) As T()
        Dim result As T() = CType(values.Clone(), T())
        Dim n = result.Length
        Dim gap = n \ 2

        ' Start with a big gap, then reduce the gap
        While gap > 0
            For i As Integer = gap To n - 1
                Dim temp = result(i)
                Dim j As Integer = i
                Do While j >= gap AndAlso result(j - gap).CompareTo(temp) > 0
                    result(j) = result(j - gap)
                    j -= gap
                Loop
                result(j) = temp
            Next i
            ' Reduce the gap for the next iteration
            gap \= 2
        End While
        Return result
    End Function

    ''' <summary>
    ''' Sorts an array of comparable elements using the heap sort algorithm.
    ''' </summary>
    ''' <remarks>
    ''' Heap sort is a comparison-based sorting algorithm that uses a binary heap data structure.
    ''' It first builds a max heap from the input array, then repeatedly extracts the maximum
    ''' element from the heap and places it at the end of the sorted array.
    ''' </remarks>
    ''' <typeparam name="T">The type of the elements in the array (must implement IComparable).</typeparam>
    ''' <param name="values">The array of comparable elements to sort.</param>
    ''' <returns>The sorted array.</returns>
    Public Function HeapSort(Of T As IComparable)(ParamArray values As T()) As T()
        Dim result As T() = CType(values.Clone(), T())
        Dim n = result.Length

        ' Step 1: Build a max heap
        For i As Integer = n \ 2 - 1 To 0 Step -1
            Heapify(result, n, i)
        Next i

        ' Step 2: Extract elements from heap one by one
        For i As Integer = n - 1 To 1 Step -1
            ' Move current root to end
            Swap(result(0), result(i))
            ' Call max heapify on the reduced heap
            Heapify(result, i, 0)
        Next i

        Return result
    End Function
End Module