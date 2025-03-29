The `None` type in Eden serves a similar purpose to the `void` type in languages like `C++`. It is used to indicate that a function does not return a meaningful value. In Eden, when a function returns `None`, it is treated as though it has a `void` return type in other languages.
### Usage in Functions

In Eden, a function that returns `None` effectively behaves like a `void` function in C++. For example, if a function doesn't return anything, it will implicitly return `None`. In the **REPL** (Read-Eval-Print Loop), the `None` value is omitted in output for simplicity.
### Example:
```
Function None PrintMessage() {     Print("Hello, World!");  // No return statement, so the function returns None }
```
In the example above, the function `PrintMessage` doesn't return any value, so it effectively returns `None`. This is similar to how a `void` function works in languages like C++.

### Limitations of `None` Type
Currently, the `None` type is limited in functionality:
- **No direct variable assignment**: At the moment, there is no direct way to define a variable to be of `None` type. This means you cannot assign `None` to a variable like you would with other types (e.g., [[Int]], [[Bool]], [[Bool]]).
- **Incomplete implementation**: The logic behind the `None` type is not yet fully implemented, and as such, it is not widely usable beyond indicating that a function does not return a value.