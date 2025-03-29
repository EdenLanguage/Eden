One of the [[Loops]] statements. Allows for endless evaluation of the code inside the body of this statement.
Example:
```
Sisyphus {
	If(True){
		Quit;
	};
};
```
You can break out of the loop by using the [[Quit]] statement. If not used, the code defined in this [[Block]] will be executed endlessly.