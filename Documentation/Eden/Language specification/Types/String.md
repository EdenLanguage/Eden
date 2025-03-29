A string is defined using the `Var` keyword, similar to other variable types. For example:
```
Var String name = "Jaroslaw";
```

## Accessing Elements in a String

You can access individual characters in a `String` using the **Indexer** operator. The index is zero-based, so the first character of the string is at index `0`. Here's an example:
```
Var String name = "Jaroslaw"; Var Char letter = name[0];   // Retrieves the first letter of the string, 'J'.
```

### Example:
```
Var String name = "Jaroslaw"; Var Char letter = name[3];   // Retrieves the fourth letter of the string, 'o'.
```

## String Methods
At the moment, the `String` type exposes the `Length()` method, which returns the number of characters in the string.

### Example:
```
Var String name = "Jaroslaw"; Length(name);  // Returns 8 (the length of the string "Jaroslaw").
```

## String Concatenation

You can append a `Char` to a `String` using the `+` operator. For example:
```
Var String name = "Jaroslaw"; name = name + '1';  // Appends '1' to the string, resulting in "Jaroslaw1".
```