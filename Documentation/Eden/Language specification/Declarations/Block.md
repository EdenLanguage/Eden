Also called [[Scope]], a block is a part of the language defined by `{...}` braces. The interpreter does not allow you to create blocks on your own. Blocks are used with keywords such as: [[Loops]], [[Conditionals]], [[Program]], and [[Functions]].

For example:
```
Loop(Var Int i = 0i; i < 10i; i = i + 1i){
	If(i >= 4i){
		Var Float pi = 3.14f;
	};	
};
```
This code contains 3 blocks: the first block is the program as a whole, the second begins with the [[Loop]] statement, and the third begins with the [[If]] conditional.

This means that the variable `pi`, defined in the [[If]] block, is not accessible outside the scope of the conditional. Calling `pi` outside of this block will result in an error. However, it is possible to declare a variable in an outer block (either the program or the loop) and then reference it inside the [[If]] conditional.

[[Statements]] that have exclusive blocks/scopes:
- [[Program]]
- [[Function]]
- Loops:
    - [[Loop]]
    - [[Sisyphus]]
- Conditionals:
    - [[If]]