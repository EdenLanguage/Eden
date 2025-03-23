## First release of `Eden` language interpreter.

#### Lexer  
- [x] Generate tokens from input file or input code.  
- [x] If a token cannot be determined, return `Illegal Token`.  
- [x] Ensure compatibility with both Windows and Linux (e.g., handling differences in line endings).  
- [x] Each token should include the following details:  
  - Line number  
  - Token name  
  - Literal value  
  - Position (start & end)  
- [x] Implement data type tokens:  
  - [x] String  
  - [x] Int  
  - [x] Float  
  - [x] Char  
  - [x] List  
---

#### Parser  
- [x] Consume `Tokens` returned from the `Lexer`.  
- [x] Generate an `Abstract Syntax Tree (AST)`.  
- [x] Validate statement and expression structures.  
- [x] Return an error if statements or expressions do not follow the language specification.  
- [x] Create mappings for expression evaluation.  
- [x] Use the `Pratt Parser` technique for parsing binary and unary operations.  
---

#### Evaluator  
- [x] Evaluate the `Abstract Syntax Tree (AST)`.  
- [x] Implement robust error handling during evaluation.  
---

#### Environment  
- [x] Each scope `{ ... }` should have its own variable environment.  
- [x] Creating a new scope should extend the current environment.  
- [x] If a `Function` or `Variable` is not found in the current environment, check the root environment.  
---

#### Language Features  
- [x] Conditionals:  
  - [x] `If`  
  - [ ] `If Else`  
  - [x] `Else`  
- [x] Functions.  
- [x] Operators:  
  - [x] Basic: `+`, `-`, `*`, `/`, `...`  
  - [x] Logical: `==`, `!=`, `<`, `>`, `...`  
  - [x] Modulo `%`  
- [x] Comments.  
- [x] List structure with methods:  
  - `Add()`  
  - `Delete()`  
  - `RemoveAt()`  
  - `Clear()`  
  - `Get(index)`  
  - `Set(index, value)`  
- [x] Loops:  
  - [x] `Loop` (For)  
  - [x] `Sisyphus` (Infinite While Loop)  
- [x] Built-in functions:  
  - [x] `Length`, `Min`, `Max`  
  - [x] `Sin`, `Cos`  
  - [x] `Print`, `PrintLine`  
- [x] Data Types:  
  - [x] `Int`  
  - [x] `Float`  
  - [x] `Bool`  
  - [x] `Null`  
  - [x] `String`  
  - [x] `Char`  
- [ ] REPL  
---

#### Show-off spinning doughnut
  - [Idea](https://www.youtube.com/watch?v=DEqXNfs_HhY&ab_channel=LexFridman)
  - [Example 1](https://www.youtube.com/watch?v=74FJ8TTMM5E&ab_channel=GreenCode)
  - [Example 2](https://www.youtube.com/watch?v=LqQ-ezbyiW4&ab_channel=GiovanniCode)
---

#### Windows installer 
  - [ ] Running REPL from the command line.
  - [ ] Interpreting files with **.eden** extension.
  - [ ] Recognizing **.eden** files in the system and adding a right-click context menu to 'Run with Eden'.
---

#### Visual Studio Code extension 
- [ ] Visual Studio Code extension for syntax highlighting.
---

#### CI/CD 
- [ ] Prepare CI/CD on GitHub.