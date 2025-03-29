Eden provides a built-in **List** collection, implemented internally as a **linked list**. Lists can store elements of a specific type (e.g., [[Int]], [[String]], [[Bool]]) and come with simple utility methods for easy manipulation.
### Declaring Lists
You can declare a list using the following syntax:
```
List <Type> <Identifier> = [<Elements>];
```
#### Examples:
```
List Int indexes = [];               // Define an empty list of integers   
List Int indexes = [1i];             // Define a list with one integer element (1)   
List String names = ["Mark"];        // Define a list of strings   
List Bool flags = [True, False, True, False]; // List of booleans   
List Int lengths = (10);             // Create a list with 10 elements, all initialized to the default value for the type
```
### List Methods
The **List** type provides several built-in methods for list manipulation. Here are the main methods:
- **`Add(Item)`** – Adds an item to the list. The argument `Item` must be of the same type as the list. This method does not return anything.
- **`Clear()`** – Removes all elements from the list. This method does not return anything.
- **`RemoveAt(Index)`** – Removes the element at the specified index. The argument `Index` is the position of the item to remove. This method does not return anything.
- **`Length()`** – Returns the number of elements in the list. It returns the length as an [[Int]].
#### Example Usage:
```
List Int numbers = [10i, 20i, 30i];   
numbers.Add(40i);                    // Adds 40 to the list   
numbers.RemoveAt(1i);                // Removes the element at index 1 (the second element)  
Var Int size = numbers.Length();     // Gets the number of elements in the list  
numbers.Clear();                     // Removes all elements from the list
```