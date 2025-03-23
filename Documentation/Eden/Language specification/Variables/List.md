### Lists in Eden Language

Eden provides a built-in **List** collection, implemented as a **linked list** internally. Lists can store elements of a specific [[Type]] and offer simple utility methods for manipulation.

##### Declaring Lists
You can declare a list using the following syntax:
```
List Int indexes = [];          // Define an empty list of integers  
List Int indexes = [1i];        // Define a list with one integer element (1)  
List String names = ["Mark"];   // Define a list of strings  
List Bool flags = [True, False, True, False]; // List of booleans  
List Int lengths = (10);        // Create list with 10 element with default list type value  
```
##### List Methods
The **List** type provides built-in methods for manipulation:
- **`Add(Item)`** – Adds an item to the list. It accepts one arguments that is object with the same type as type of a list. It doesn't return anything.
- **`Clear()`** – Removes all elements from the list. Doesn't return anything.
- **`RemoveAt(Index)`** – Removes the element at the specified index. Doesn't return anything.
- **`Length()`** – Returns the number of elements in the list. Returns length of [[List]] as [[Int]]. 

Example usage:
```
List Int numbers = [10i, 20i, 30i];  
numbers.Add(40i);  
numbers.RemoveAt(1i);              // Removes the second element (index 1)  
Var Int size = numbers.Length();   // Get the list size  
numbers.Clear();                   // Removes all elements  
```