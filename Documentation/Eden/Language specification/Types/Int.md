The `Int` type represents a 32-bit integer value, which uses two's complement to handle both positive and negative numbers. It is a signed integer type, meaning it can store both positive and negative values within a specified range.
### Example:
```
Var Int variable = 10i;  // Declares an Int variable and initializes it with the value 10.
```
## Type Indicator
The **Lexer** distinguishes `Int` literals by the `i` suffix. This suffix is **REQUIRED** and tells the interpreter that the literal should be treated as an `Int` type, not as another type such as `Float` or `String`.
### Example:
```
Var Int number = -5i;  // Declares an Int variable and initializes it with the value -5.
```

---
## Two's Complement Representation
The `Int` type uses **two's complement** to represent both positive and negative numbers. This method ensures that both positive and negative integers can be represented in a consistent way, with the following range:

- **Range**: -2,147,483,648 to 2,147,483,647 (for a 32-bit signed integer)