In Eden, you can define a variable using the following syntax:
```
<Var> <Type> <Identifier> <=> <Expression> <;>
```
### Syntax Explanation:
- **`Var`**: Keyword used to declare a new variable.
- **`<Type>`**: Data type of the variable, such as [[Int]], [[Char]], [[String]], [[Bool]], etc. (as defined in the [[Types]] section).
- **`<Identifier>`**: Name of the variable you are declaring.
- **`<Expression>`**: Value or expression that is assigned to the variable at the time of declaration.
- **`<;>`**: A semicolon to terminate the statement.
### Example:
```
Var Int counter = 10i;  // Declares an 'Int' variable named 'counter' and initializes it with the value 10.
```

In this example:
- `Var` indicates that we are declaring a variable.
- `Int` is the type of the variable, meaning it will hold integer values.
- `counter` is the name (identifier) of the variable.
- `10i` is the value assigned to the variable, where `i` signifies an integer literal.
### Variable Types

The type of the variable determines what kind of data it can store. The available types are defined in the [[Types]] section and can include primitive types (such as [[Int]], [[Char]], [[String]], [[Bool]] etc.) or other user-defined types.