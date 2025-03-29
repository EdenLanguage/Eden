A function in the language is defined using the following structure:
```
<Function> <Type> <Identifier> <(> <Argument Declaration> <)> <{>     
	<Statements>     
	<Return> <Expression> <;> 
<}> <;>
```

### Example of Function Definition:
```
Function Int Add(Var Int A, Var Int B) {     
	Return A + B; 
};
```

## Function Flow and Return Statements
Currently, the interpreter does **not** validate whether every logical branch within a function contains a `Return` statement. For example:
```
Function Int GetPi(Var Int i) {     
	If (i > 10) {         
		Return 10;     
	};     <------ // No return statement here, interpreted doesn't check for validity of this block. 
};
```

In the above example, the function `GetPi` may fail to return a value if the condition `(i > 10)` is false. Checking for completeness of return statements in all branches of a function will be implemented in the future, but for now, it is the programmer's responsibility to ensure that the function executes properly with branching logic.

### Future Enhancements
Function return validation and error checking will be addressed in a future release. Until then, ensure your function includes a valid return statement in all branches.

## Functions Without Return Values
If a function doesn't explicitly return a value, the interpreter will default to returning `[[None]]`. Currently, it is not possible to define a function that doesn't return anything. However, as a workaround, you can define the function to return a type and simply omit the return statement.

**Note**: This is a temporary workaround, and the full implementation of functions without return values will be available in future versions.

## Variable Scope
Each function creates its own variable context, meaning variables declared within the function are not accessible outside of it. The only way to return a value from a function is through the `Return` statement.

**Argument Types**: Common argument types such as `In`, `Out`, and `Ref`, as seen in languages like C#, will **not** be implemented at this time.