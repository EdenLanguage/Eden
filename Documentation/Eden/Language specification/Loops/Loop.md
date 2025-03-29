The most basic form of [[Loops]] allows the execution of multiple evaluations within a block of this statement.
The `Loop` statement takes three arguments:
1. The first is the [[Variable definition]], which acts as the indexer for the loop.
2. The second is the ending condition.
3. The third is the operation executed each time the loop successfully evaluates.
Example:
```
Loop(Var Int i = 0i; i < 100i; i = i + 1) {
	Loop(Var Int j = 0i; j < 100i; j = j + 1) {
		If(i*j >= 10i){
			Quit;
		};	
	};
};
```