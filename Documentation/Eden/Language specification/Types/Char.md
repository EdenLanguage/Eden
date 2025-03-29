The `Char` type in Eden represents a single character and can hold values ranging from **0** to **255**. It is typically used to store individual characters, such as letters, digits, and special symbols.
### Defining a Char
A `Char` can be defined in two ways:
1. **Using a numeric value**: You can define a `Char` by specifying its numeric value, which corresponds to its ASCII code (from 0 to 255).
```
Var Char x = 0c;  // Defines a Char variable with the ASCII value 0 (null character).
```
2. **Using a character literal**: Alternatively, you can define a `Char` by enclosing the character in single quotes (`'`), similar to how characters are defined in many other programming languages.
```
Var Char x = '1';  // Defines a Char variable with the character '1'.
```
### Value Range

The `Char` type can hold values from **0** to **255**. These values correspond to the ASCII or extended ASCII character set, meaning they can represent:
- **Standard ASCII characters**: Values from **0** to **127** (e.g., letters, digits, punctuation).
- **Extended ASCII characters**: Values from **128** to **255** (e.g., accented characters, special symbols).
### Example:
```
Var Char letter = 'A';    // Defines a Char variable with the value 'A' (ASCII value 65). 
Var Char symbol = 35c;    // Defines a Char variable with the value corresponding to ASCII 35 ('#').
```