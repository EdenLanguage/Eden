Also called as [[Scope]], is a part of the language that is defined by `{...}` letters. Interpreter will not allow you to create block on your own. Blocks are used with keywords like: [[Loops]], [[Conditionals]], [[Program]], [[Functions]]. So for example:
```
Loop(Var Int i = 0i; i < 10i; i = i + 1i){
	If(i >= 4i){
		Var Float pi = 3.14f;
	};	
};
```
This code has 3 blocks, first block is a program a a hole, second one begins with [[Loop]] statement, and the third one begins with [[If]] conditional. 

This means that variable `pi` defined in `If` block is not accessible outside of the bound of the conditional. Calling `pi` outside of this block will return error. What is possible is to declare variable in outer block (either program or loop) and then calling it inside `If` conditional. 

[[Statements]] that have exclusive block/scope:
- [[Program]]
- [[Function]]
- Loops:
	- [[Loop]]
	- [[Sisyphus]]
- Conditionals:
	- [[If]]
	- [[If Else]]
	- [[Else]]