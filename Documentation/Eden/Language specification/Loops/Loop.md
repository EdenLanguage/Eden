Most basic form of [[Loops]]. Allows to execute multiple evaluations of block of this statement.
`Loop` statement takes three argument. First one is [[Variable]] definition. This variable will act as indexer of this loop. Second one is ending conditional. Third one is operation that will be executed each time successful evaluation of the loop has been achieved. Example:
```
Loop(Var Int i = 0i; i < 100i; i = i + 1) {
	Loop(Var Int j = 0i; j < 100i; j = j + 1) {
		If(i*j >= 10i){
			Quit;
		};	
	};
};
```