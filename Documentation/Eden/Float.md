Lexer should allow to parse [[Int]] and Float types. In case of Float lexer should recognize pattern like:
`number comma number`.
These tokens should be always present. Valid Float declarations:
`0,0`
`1,0`
`3,14`
`0,1`
Invalid Float declarations
`,0`
`1,`
`0,,1`
