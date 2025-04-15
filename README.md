# Eden - programing language

<img src="./Eden/ResourceLibrary/Assets/Logo.png" alt="Eden Logo" style="width: 33%;">

`Eden` is an `interpreted` programming language with a built-in REPL and an interpreter for files with the `.eden` extension. It is built entirely from scratch without relying on grammar generators like Bison or tokenizers like Lex. This deliberate choice was made to deepen the understanding of language construction and parsing techniques.

The interpreter is written in `C#` and based on the `.NET` platform, chosen for its robust cross-platform capabilities, enabling easy migration across `Windows`, `macOS`, and `Linux`. Additionally, `C#` offers extensive tools for software testing, ensuring reliability and maintainability throughout development.

Performance optimization was never a primary concern for `Eden`, as the focus is on clarity, learning, and design flexibility. The language employs an `LL(1)` parser, a top-down approach that predicts the next token, making parsing more structured. The implementation uses `Pratt parsing`, which allows efficient expression evaluation while maintaining a simple and extensible syntax.

The parser generates an `Abstract Syntax Tree (AST)`, which serves as the foundation for evaluation. Eden’s evaluator follows a `tree-walking` approach, traversing the AST and executing expressions accordingly. At this stage, no AST optimization has been implemented, but future improvements may include enhancements to parsing efficiency and execution performance.

---

<h1 id="custom-sections" style="color: rgb(117, 198, 166);">Sections</h1>

- <a href="#custom-motivation" style="font-size: 1.2em; color: rgb(117, 198, 166);">**Motivation**</a>  
- <a href="#custom-resources" style="font-size: 1.2em; color: rgb(117, 198, 166);">**Resources**</a>  
- <a href="#custom-milestones" style="font-size: 1.2em; color: rgb(117, 198, 166);">**Milestones**</a>  
- <a href="#custom-local-build" style="font-size: 1.2em; color: rgb(117, 198, 166);">**Local build**</a>
- <a href="#custom-instalation" style="font-size: 1.2em; color: rgb(117, 198, 166);">**Instalation**</a>
- <a href="#custom-interaction-with-language" style="font-size: 1.2em; color: rgb(117, 198, 166);">**Interaction with Language**</a>  
- <a href="#custom-examples" style="font-size: 1.2em; color: rgb(117, 198, 166);">**Examples**</a>  
- <a href="#custom-console-output-example" style="font-size: 1.2em; color: rgb(117, 198, 166);">**Donut example**</a>  
- <a href="#custom-currently-implemented" style="font-size: 1.2em; color: rgb(117, 198, 166);">**Currently Implemented**</a>  
- <a href="#custom-what-the-language-journey-looks-like" style="font-size: 1.2em; color: rgb(117, 198, 166);">**What the Language Journey Looks Like**</a>  
- <a href="#custom-division-of-code" style="font-size: 1.2em; color: rgb(117, 198, 166);">**Division of Code**</a>  

---

## [⬅️ Sections](#custom-sections)
<h1 id="custom-motivation" style="color: rgb(117, 198, 166);">Motivation</h1>

`Eden` is a programming language that I created to deepen my understanding of how programming languages work, especially from a practical perspective. The theory I studied during my university years played a key role in this, and completing my specialization in `Computer Programming` would feel like a complete failure if I couldn’t even write a simple interpreter. his project was a way to apply the knowledge I gained during my studies and put into practice the concepts I had learned.

The name `"Eden"` comes from the Garden of Eden mentioned in the Bible, and it was intended to symbolize how enjoyable (at least, that’s what I hoped for) coding in this language could be. I wanted to create something as simple and pleasant to use as the `C` language, while also allowing for a higher level of abstraction through interpretative capabilities across various environments. 

When creating `Eden`, my main goal was to make everything as explicit as possible in order to understand it better. I wanted to make sure every part of the language, from the parser to the evaluator, was transparent and easy to follow. By doing so, I could learn more deeply about the inner workings of programming languages. This approach is why I was particularly guided by this quote:
> ***"An idiot admires complexity, a genius admires simplicity."***  
>  _— Terry A. Davis_  

I believe simplicity allows for clarity, and by keeping things straightforward, I could not only make the language easier to understand for others, but also deepen my own understanding of language design.

---

## [⬅️ Sections](#custom-sections)
<h1 id="custom-resources" style="color: rgb(117, 198, 166);">Resources</h1>

- **[Top Down Operator Precedence](https://tdop.github.io/)** by Vaughan R. Pratt  
- **[Writing an Interpreter in Go](https://interpreterbook.com/)** by Thorsten Ball  
- **[Let’s Build a Compiler!](http://compilers.iecc.com/crenshaw/)** by Jack W. Crenshaw  
- **[Top Down Operator Precedence](http://javascript.crockford.com/tdop/tdop.html)** by Douglas Crockford  
- **[Pratt Parsers: Expression Parsing Made Easy](http://journal.stuffwithstuff.com/2011/03/19/pratt-parsers-expression-parsing-made-easy/)** by Bob Nystrom  
- **[Programming Languages: Application and Interpretation](http://papl.cs.brown.edu/2015/)** by Shriram Krishnamurthi and Joe Gibbs Politz  
- **[How to Write a Pratt Parser | Writing a Custom Language Parser in Golang ](https://www.youtube.com/watch?v=1BanGrbOcjs&ab_channel=tylerlaceby)**  by Tyler Laceby
- **[Simple but Powerful Pratt Parsing](https://matklad.github.io/2020/04/13/simple-but-powerful-pratt-parsing.html)** by Alex Kladov

---

## [⬅️ Sections](#custom-sections)
<h1 id="custom-milestones" style="color: rgb(117, 198, 166);">Milestones</h1>

-  **Release 1.0.0**
    - Basic implementation of the language.
    - Windows installer for the interpreter.
    - Formal grammar definition for the language. When creating the parser, I didn't use grammar generators but wrote the grammar manually. The grammar definition would also help in creating syntax highlighting add-ons for development environments like VSCode.

- **Release 1.1.0**
    - Importing external files and evaluating them.
    - External libraries support.
    - Accepting input arguments.

---

## [⬅️ Sections](#custom-sections)
<h1 id="custom-local-build" style="color: rgb(117, 198, 166);">Local build</h1>

To build `Eden`, follow these steps:

### 1. Clone the Repository
Download the repository to your local machine.

### 2. Install Visual Studio
Ensure that you have **Visual Studio** installed on your system. The solution includes all necessary dependencies, which are contained inside solution (`.sln`).

You can download Visual Studio from [here](https://visualstudio.microsoft.com/).

### 3. Open the Solution
Open the solution file (`.sln`) located in the repository with Visual Studio.

### 4. Build the Installer Project
In Visual Studio, build the **Installer** project by selecting `Build` from the top menu, then choosing **Build Solution** (or press `Ctrl+Shift+B`).

### 5. Find the Generated `.msi` File
After the build process completes successfully, the generated `.msi` file will be placed in the **`Installs`** folder inside the **Installer** project directory.

---

## [⬅️ Sections](#custom-sections)
<h1 id="custom-instalation" style="color: rgb(117, 198, 166);">Instalation</h1>
Although `Eden` supports platforms such as `macOS` and `Linux`, the main development currently takes place on `Windows`. Because of this, a Windows installer is available for download and installation. After launching the `.msi` installer, it will handle everything and complete the setup automatically.

<img src="./Eden/ResourceLibrary/Assets/InstallEden.gif" alt="Eden Logo" style="width: 100%;">

---

This project includes **four key scripts** that together handle the installation and uninstallation of the Eden runtime. They are run during instalation process. Below is a detailed explanation of what each script does, how it works, and why it's needed.

All scripts are located in installation folder of `Eden`.

## `EdenInitialize.bat`

**Purpose:**  
This batch script sets up the Eden environment by:

- Validating admin privileges
- Registering file types and context menu entries in the Windows Registry
- Executing a PowerShell script to add Eden to the system `PATH` environment variable
- Refreshing Windows Explorer to apply changes immediately

#### What It Does

1. **Admin Check**  
   Uses `net session` to ensure it's being run as Administrator. If not, the script exits immediately.

2. **Setup Variables**  
   Locates `Eden.exe` and `Logo.ico` in the current directory (`%~dp0`). If they don’t exist, the script stops.

3. **Add Environment Variable**  
   Executes `CreateEnvVar.ps1` to append Eden’s installation directory to the system `PATH`.

4. **Register File Type `.eden`**  
   Adds entries under `HKEY_CLASSES_ROOT`:
   - Associates `.eden` extension with `EdenFile`
   - Adds default icon and Notepad as the default handler
   - Enables right-click "New Eden File" in:
     - Background of directories
     - Folder context menus

5. **Explorer Refresh**  
   Restarts Windows Explorer to apply environment and registry changes without requiring a reboot.

---

## `CreateEnvVar.ps1`

**Purpose:**  
Adds Eden’s installation directory to the system `PATH` environment variable, allowing `Eden.exe` to be run from any command prompt.

####  What It Does

1. **Current Path Detection**  
   Automatically determines the folder the script is running from using:
2. **Validation**
    Checks if Eden.exe exists in this directory. If not, the script exits with exit 1.
3. **Path Check & Update**
    - Retrieves the current system PATH
    - Splits it to verify Eden’s path isn’t already included
    - If not present, it appends Eden’s path and updates the system variable
    - Writes a success or error message to the console

4. **Exit Codes** 
    Returns 0 on success, 1 on error — used by the batch script for error handling.

---

## `EdenRemove.bat`

**Purpose:**  
This batch script handles the **complete uninstallation** of the Eden runtime configuration from the system.

#### What It Does

1. **Check for Admin Rights**
   - Uses `net session` to verify the script is running with Administrator privileges.
   - If not elevated, it prints an error and exits.

2. **Locate Removal Script**
   - Sets the current script directory as `EdenRuntimeDirectory`.
   - Locates `RemoveEnvVar.ps1` in the same directory.
   - Exits if the script is missing.

3. **Registry Cleanup**
   - Removes file extension association for `.eden` under:
     - `HKEY_CLASSES_ROOT\.eden`
     - `HKEY_CLASSES_ROOT\EdenFile`
   - Deletes context menu entries for:
     - Background of folders: `Directory\Background\Shell\Create Eden File`
     - Folders themselves: `Directory\Shell\Create Eden File`

4. **Remove PATH Entry**
   - Executes `RemoveEnvVar.ps1` to delete Eden’s directory from the system PATH environment variable.

5. **Explorer Refresh**
   - Silently restarts `explorer.exe` to apply registry and environment changes immediately.

6. **Final Output**
   - Prints confirmation that Eden has been removed from the machine.

---

## `RemoveEnvVar.ps1`

**Purpose:**  
This PowerShell script **removes Eden’s directory from the system PATH** environment variable.

#### What It Does

1. **Determine Script Directory**
   - Gets the folder the script is running from:
2. **Build Target Path**
   - Constructs the full path to remove: `"$scriptDir"`
3. **Get and Sanitize PATH**
   - Retrieves the system PATH using:
   - Splits the PATH string by `;`
   - Filters out the Eden path if it exists

4. **Update PATH**
   - Joins the filtered list and writes it back to the system:
5. **Console Output**
   - Informs whether the Eden path was found and removed or not found at all.

---

## Environment Variable Backup

Before modifying the system `PATH`, both `Add` and `Remove` scripts create a backup:

- `C:\PathBackup_Eden_Add.txt` – before adding Eden
- `C:\PathBackup_Eden_Remove.txt` – before removing Eden

### How to Restore

1. Open the relevant `.txt` file from `C:\`.
2. Copy its content.
3. Go to **System Properties** → **Advanced** → **Environment Variables**.
4. Under **System variables**, select `Path` → **Edit**.
5. Replace the current value with the backup content.
6. Apply changes and restart your terminal or computer.

---

## Important Notes

- **Administrator Privileges Required**  
  All Eden setup and removal scripts **must be run as Administrator** to properly modify system environment variables and Windows registry keys. If you don't run them with admin rights, they will exit without making changes.

- **Use At Your Own Risk**  
  These scripts interact with critical parts of your system (such as registry and environment variables). While they are designed to be safe and self-contained, misuse or modifications may lead to undesired behavior.  
  > ⚠️ **DISCLAIMER:** You run these scripts at your own risk. The author is not responsible for any damage caused.
- **Installation Folder Must Contain Eden.exe**  
  The PowerShell script that adds Eden to the system PATH checks whether `Eden.exe` is present in the same folder. If not found, the script will exit with an error code and make no changes.

- **Environment Variable Changes Are Immediate**  
  Environment variable updates are made at the **Machine level**, and changes will take effect system-wide. `explorer.exe` is restarted automatically to ensure context menus and file associations refresh correctly.
- **Uninstallation Cleans Up Everything**  
  The uninstaller script (`EdenRemove.bat`) will remove:
  - File associations for `.eden` files
  - Right-click context menu entries
  - Eden’s entry in the system PATH

---

## [⬅️ Sections](#custom-sections)
<h1 id="custom-interaction-with-language" style="color: rgb(117, 198, 166);">Interaction with interpreter</h1>

This section demonstrates how the Eden language interacts with the user, showcasing various features and functionalities. The examples below show how you can use Eden interactively through a REPL or script execution.

<table>
    <tr>
      <h2>Run Eden</h2>
      <img src="./Eden/ResourceLibrary/Assets/RunEdenRuntime.gif" alt="Gif 1" style="width: 100%;">
    </tr>
    <tr>
      <h2>Create new Eden script</h2>
      <img src="./Eden/ResourceLibrary/Assets/CreateNewScript.gif" alt="Gif 1" style="width: 100%;">
    </tr>
    <tr>
      <h2>Open new script</h2>
      <img src="./Eden/ResourceLibrary/Assets/OpenNewScript.gif" alt="Gif 1" style="width: 100%;">
    </tr>
    <tr>
      <h2>Run Eden script</h2>
      <img src="./Eden/ResourceLibrary/Assets/RunEdenFile.gif" alt="Gif 1" style="width: 100%;">
    </tr>
    <tr>
      <h2>Run Eden REPL</h2>
      <img src="./Eden/ResourceLibrary/Assets/BasicREPL.gif" alt="Gif 1" style="width: 100%;">
    </tr>
</table>

---

## [⬅️ Sections](#custom-sections)
<h1 id="custom-examples" style="color: rgb(117, 198, 166);">Examples</h1>

This section contains example programs written in the `Eden` language. Some of the function implementations may seem suboptimal, but this is because not all intended functionalities of the language have been fully implemented yet, and we must work with what is available.

```vb
// Fibonacci n-th number.
Function Int Fibonacci(Var Int index){
    Var Int A = 0i;
    Var Int B = 1i;
    Var Int tmp = A + B;
    
    Loop(Var Int i = 1i; i < index; i = i + 1i){
        tmp = A + B;
        A = B;
        B = tmp;
    };
    
    Return B;
};

Var Int fibonacciNumber = Fibonacci(10i);
PrintLine(fibonacciNumber);
```

``` vb
// Is word a palindrome.
Function Bool IsPalindrome(Var String input){

    Function String Reverse(Var String input){
        Var Int length = Length(input);
        Var String reversed = "";

        Loop(Var Int i = length - 1i; i >= 0i; i = i - 1i){
            reversed = reversed + input[i];
        };

        Return reversed;
    };

    Var String reversed = Reverse(input);
    Var Int length = Length(input);

    Loop(Var Int i = 0i; i < length; i = i + 1i){
        If(input[i] != reversed[i]){
            Return False;
        };
    };
    
    Return True;
};

Var Bool isPali = IsPalindrome("abracadabra"); 
PrintLine(isPali);
```

``` vb
// Is Number Prime?
Function Bool IsPrime(Var Int n){
    If(n <= 1i){
        Return False;
    };

    If(n <= 3i){
        Return True;
    };

    If(n % 2i == 0i || n % 3i == 0i){
        Return False;
    };

    Loop(Var Int i = 5i; i * i <= n; i = i + 6i){
        If(n % i == 0i || n % (i + 2i) == 0i){
            Return False;
        };
    };

    Return True;
};

Var Bool isPrime = IsPrime(5i);
PrintLine(isPrime);
```

---

## [⬅️ Sections](#custom-sections)
<h1 id="custom-console-output-example" style="color: rgb(117, 198, 166);">Donut example</h1>

To demonstrate how `Eden` handles the console and to showcase its capabilities, a simple program was created that displays a rotating donut. This example tests several core features of the language, including loops, mathematical operations, string manipulation, and floating-point arithmetic.

Note: The video has been sped up, as the actual execution is quite time-consuming. This is due to the lack of any implemented optimizations. Currently, generating a single frame with the specified parameters takes approximately 6–8 seconds on Eden. Performance improvements will be addressed in future development, but for now, the focus is on producing working examples.

### Output
<img src="./Eden/ResourceLibrary/Assets/DonutEden.gif" alt="Eden Logo" style="width: 100%;">

### Code
```vb
Literal 3.14f As #PI;
Literal 0.07f As #ThetaStep;
Literal 0.02f As #PhiStep;

Literal 30i As #WIDTH;
Literal 15i As #HEIGHT;
Literal #WIDTH * #HEIGHT As #SIZE;

Var Float A = 0.00f;
Var Float B = 0.00f;

List Char buffer = (#SIZE);
List Float zbuffer = (#SIZE);
Var String asciSymbols = ".,-~:;=!*#$@";

Var Int Ro = 3i;
Var Int Roo = 1i;
Var Int Ko = 4i;
Var Int Koo = #WIDTH * Ko * 3i / (8i * (Ro + Roo));

Function Int FillBuffer(Var Char symbol){
    Loop(Var Int i = 0i; i < #SIZE; i = i + 1i){
        buffer[i] = symbol;
    };
};

Function Int FillZBuffer(Var Float digit){
    Loop(Var Int i = 0i; i < #SIZE; i = i + 1i){
        zbuffer[i] = digit;
    };
};

Function Int ClearZBuffer(){
    FillZBuffer(0.0f);
};

Function Int ClearBuffer(){
    FillBuffer(' ');
};

Function Int ClearBuffers(){
    ClearBuffer();
    ClearZBuffer();
};

Function Int PrintBuffer(){
    ConsoleGoHome();
    
    Var String tmp = "";
    
    Loop (Var Int i = 0i; i < #SIZE; i = i + 1i) {
        If (i != 0i && i % #WIDTH == 0i) {
            PrintLine(tmp);
            tmp = "";
        };
        
        tmp = tmp + buffer[i];
    };
    
    If (tmp != "") {
        PrintLine(tmp);
    };
};

ConsoleClear();
Sisyphus{
    Var Float cosA = CosinusR(A);
    Var Float sinA = SinusR(A);
    Var Float cosB = CosinusR(B);
    Var Float sinB = SinusR(B);  

    ClearBuffers();

    Loop(Var Float theta = 0f; theta < 2i * #PI; theta = theta + #ThetaStep){
        Var Float cosTheta = CosinusR(theta);
        Var Float sinTheta = SinusR(theta); 

        Loop(Var Float phi = 0f; phi < 2i * #PI; phi = phi + #PhiStep){
            Var Float cosPhi = CosinusR(phi);
            Var Float sinPhi = SinusR(phi);   

            Var Float circleX = Ro + Roo * cosTheta;
            Var Float circleY = Roo * sinTheta;        

            Var Float x = circleX * (cosB * cosPhi + sinA * sinB * sinPhi) - circleY * cosA * sinB;
            Var Float y = circleX * (sinB * cosPhi - sinA * cosB * sinPhi) + circleY * cosA * cosB;
            Var Float z = 5i + cosA * circleX * sinPhi + circleY * sinA;
            Var Float ooz = 1i / z;

            Var Int xp = (#WIDTH / 2i + Koo * ooz * x);
            Var Int yp = (#HEIGHT / 2i - Koo / 2i * ooz * y);

            Var Int idx = xp + yp * #WIDTH;
            Var Float luminance = cosPhi * cosTheta * sinB - cosA * cosTheta * sinPhi - sinA * sinTheta + cosB * (cosA * sinTheta - cosTheta * sinA * sinPhi);
        
            If (idx >= 0i && idx < #SIZE && ooz > zbuffer[idx])
            {
                zbuffer[idx] = ooz;
                Var Int lumIdx = luminance * 8i;
                
                If (lumIdx < 0i){
                    lumIdx = 0i;
                }
                Else{
                    If (lumIdx > 11i){
                        lumIdx = 11i;
                    };
                };
                
                buffer[idx] = asciSymbols[lumIdx];
            };
        };
    };
    
    ConsoleGoHome();
    PrintBuffer();
    A = A + 0.04f;
    B = B + 0.08f;
};
```
### Resources:
- [**Donut Math**](https://www.a1k0n.net/2011/07/20/donut-math.html) by Aik0n
- [**3D Donut in C**](https://github.com/akhileshthite/3d-donut) by akhileshthite 
- [**Video: How to Create a Spinning Donut**](https://www.youtube.com/watch?v=LqQ-ezbyiW4&ab_channel=GiovanniCode) by GiovanniCode
- [**Video: Spinning Donut in Console**](https://www.youtube.com/watch?v=DEqXNfs_HhY&ab_channel=LexFridman) by Lex Fridman
- [**Video: 3D Donut Console Project**](https://www.youtube.com/watch?v=74FJ8TTMM5E&ab_channel=GreenCode) by GreenCode

---

## [⬅️ Sections](#custom-sections)
<h1 id="custom-currently-implemented" style="color: rgb(117, 198, 166);">What is currently implemented</h1>

### Data Types:
- `Char`
- `Int`
- `Float`
- `String`
- `List of (Char, Int, Float, String)`
### Features Implemented:
- Variable definition
- Logical and arithmetic operators
- Basic methods for displaying output on the screen
- Function definition and invocation
- Built-in functions
- Loops
- Conditional statements
- Block-scoped variables (variables defined within the execution block)

---

## [⬅️ Sections](#custom-sections)
<h1 id="custom-what-the-language-journey-looks-like" style="color: rgb(117, 198, 166);">The Language's Journey (Milestones)</h1>

- Create a `Visual Studio Code` extension that sets up a development environment, with the primary goal of enabling syntax highlighting.
- Implement the `If-Else` statement, as currently only the `If` statement with `{}` is implemented.
- Add a `None` type (similar to the `void` type in C++).
- Support for user-defined types (custom types).
- Basic standard libraries (functions like `sleep`, etc.).
- Significant improvements to semantic analysis.
- Abstract Syntax Tree (AST) optimizer.
- Generation of bytecode.
- Support for accepting external arguments from the program.
- Implement external file imports for the Eden language.

---

## [⬅️ Sections](#custom-sections)
<h1 id="custom-division-of-code" style="color: rgb(117, 198, 166);">Crutial classes description</h1>

- **Lexer**: or `lexical analyzer` processes the raw source code and converts it into `tokens`. Each token contains detailed information about its type (e.g., `identifier`, `operator`, `keyword`) and its position within the input file. This step is essential for breaking down the code into manageable pieces that can be further analyzed and processed by the parser.

- **Parser**: takes the tokens generated by the `lexer` and uses them to build the `Abstract Syntax Tree`. AST is a hierarchical structure representing the program's syntax. The tree is divided into expressions (which produce values) and statements (which perform actions but do not return values). Statements end with a semicolon (`;`), marking the completion of their execution. Parser makes sure that the syntax is valid and that the program follows the correct structure.

- **Evaluator**: traverses the AST and evaluates each statement or expression, based on the logic of the language. Through a technique called `tree-walking`, the evaluator computes the values produced by expressions and executes the side effects of statements (like assignments or function calls). The evaluator provides the final outcome or result of the program execution.

- **Environment**: manages the scope of variables and functions within the program. Each block of code (such as loops, functions, and conditionals) has its own isolated scope. This ensures that variables defined within a block do not interfere with other blocks. The environment is key for managing variable lookups and ensuring that the program can execute in an organized manner with proper access to variables and functions.

- **BuildIn**: This refers to a set of core, built-in functions and methods provided by the language. These functions are available by default without the need for the programmer to define them. Examples include functions or methods like `Print()` or `Length()`.

- **Runtime**: is the heart of the language's execution environment. It ties together all the components, including the `lexer`, `parser`, `evaluator`, `environment`, and `built-in` functions. The runtime allows the language to be executed in a structured way.

- **ReplRunner**: allows for interactive execution of `Eden` directly from the console. It enables users to write and evaluate expressions in real-time within a loop, providing immediate feedback.

- **ScriptRunner**: is responsible for executing `Eden` script files. It reads and processes `.eden` files, running them as standalone programs. The ScriptRunner comes with built-in configuration that makes it easy to execute scripts in a predefined environment, handling file input/output, error reporting, and more.
