One one the [[Loops]] statements. Allow for endless evaluation of code that is put inside body of this statement. Example:
```
Sisyphus {
	If(True){
		Quit;
	};
};
```
You can break outside of the loop my using [[Quit]] statement. If not used, code defined in this [[Block]] will be executed endlessly.