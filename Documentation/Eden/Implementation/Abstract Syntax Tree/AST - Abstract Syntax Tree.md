Abstract syntax tree is a final product of syntactical analysis. Tree represents relationship of [[Token]]s that are collectively creating whole tree. This part is crucial and we can't get to tokens evaluation before we don't get this to work correctly.
It should be possible to print AST to console or save it in file. Printable format should be easy to read, and it should contain only necessary information.
#### AST format
```json
{
  "type": "Root",
  "children": {
    "Statements": [
      {
        "type": "VariableDeclarationStatement",
        "Type": {
          "type": "VariableTypeExpression",
          "value": "int"
        },
        "Identifier": {
          "type": "IdentifierExpression",
          "name": "counter"
        },
        "Expression": {
          "type": "Expression",
          "Result": 5
        }
      }
    ]
  }
}

```