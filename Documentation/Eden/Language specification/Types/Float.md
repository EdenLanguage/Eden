The `Float` type represents floating-point numbers. When parsing literal values in code, the **Lexer** identifies a `Float` literal by the presence of the type indicator `f` at the end of the number.
### Example:
```
Var Float variable = 3.14f;  // Declares a Float variable and initializes it with the value 3.14.
```
## Type Indicator
The **Lexer** distinguishes `Float` literals by the `f` suffix. This ensures the value is treated as a floating-point number rather than an integer.
### Example:
```
Var Float pi = 3.14159f;  // The 'f' at the end indicates this is a Float literal.
```

---
## Platform Dependency

It's important to note that the evaluation of floating-point values is **platform-dependent**. The native `C` libraries used to determine the value of floating-point numbers can behave differently on various platforms. As a result, the precision and behavior of floating-point calculations may vary depending on the platform where the code is executed.
### Key Point:
- The actual evaluation and behavior of floating-point numbers may differ across different operating systems or hardware due to the underlying implementation in the platform's native `C` libraries.
