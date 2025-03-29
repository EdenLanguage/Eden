Eden provides branching with the `If` statement. Currently, `ElseIf` is not implemented.
## Syntax
### If Statement

```
If (<Truthful expression>) {
    <Block>
};
```
### If-Else Statement

```
If (<Truthful expression>) {
    <Block>
}
Else {
    <Block>
};
```

## Condition Expression

A condition expression must be `Truthful`, meaning it evaluates to a logical type or a value that can be interpreted as logical. For example, an `Int` value of `0i` or `1i` can be evaluated as `False` or `True`, respectively.
### Example Usage

```
If (True) {
    ...
};
```
or
```
If (True) {
    ...
}
Else {
    ...
};
```

## Future Implementation
Currently, the `ElseIf` conditional statement is not implemented but will be added soon.