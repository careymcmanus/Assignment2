# Assignment2
Repository for Assignment 2 of COS30019 : Introduction to AI

## Running the Program
To run the program type: <br>
InferenceEngine.exe <method> <testfile> <br>
Where the methods to choose from include:
- Truthtable Checking : TT
- Backwards Chaining : BC
- Forward Chaining : FC
- General Knowledge Base Checking : GTT 

## Student Details
Carey McManus - 7381247

## Features
- Truth Table Checking Working for horn clauses of the form a&b=>c; a; b
- Forward Checking working for horn clauses
- Backward Checking working for horn clause

- Truth Table Checking for expressions of the form a\/b=>c; ~a; b; d<=>c; 

## Test Cases
TELL<br>
p2=> p3; p3 => p1; c => e; b&e => f; f&g => h; p1=>d; p1&p3 => c; a; b; p2; <br>
ASK \\
d \\

ANSWER: True modelcount 3

TELL
p1&p2&p3=> p4; p5&p6 => p4; p1 => p2; p1&p2 => p3; p5&p7 => p6; p1; p4;
ASK
p7

## Acknowledgements/Resources

## Notes

## Summary Report
This project was a solo project and as such all the components were done by me.