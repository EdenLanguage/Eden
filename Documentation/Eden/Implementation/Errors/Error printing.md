Printing errors should look like this:

```
[Semantical error]
Variable 'false' not defined!
File: 'main.eden'; Line: '1'; Column: '15';

(4i > 2i) && (false || true);
			  ^--------------
```

```
[Lexical error]
Illegal token detected!
File: 'main.eden'; Line: '1'; Column: '5';

dssd0.0009dssd;
    ^----------
```

```
[Semantical error]
Binary operation 'Int' + 'Int' not defined!
File: 'main.eden'; Line: '1'; Column: '4';

If(5i && 5i);
   ^---------
```