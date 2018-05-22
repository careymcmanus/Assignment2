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
    - Truth Table Checking works by assigning all the atomic sentences in the knowledge base and query all the possible combinations of true and false. If all the clauses in a particular row of the kb are true then we check whether the query is also true. If every time the knowledge entails the model and the query is true in the model than the query is true.
- Forward Checking working for horn clauses
    - Forward Checking works by finding some known facts and then finds implications where those known facts are part of the premise. If all the facts of the premise are known then the conclusion is added to the known facts. This process continues until the query is determined or there are no more implications to explore.
- Backward Checking working for horn clause
    - Backward Checking works in reverse by finding the implications that have the query has the conclusion and then checks each of the items in the premise. It checks each item in the premise doing backward chaining on each premise until either an answer is found or their is nothing left to check.

- Truth Table Checking for expressions of the form a\/b=>c; ~a; b; d<=>c; 

## Test Cases
TELL<br>
p2=> p3; p3 => p1; c => e; b&e => f; f&g => h; p1=>d; p1&p3 => c; a; b; p2; <br>
ASK <br>
d <br>

ANSWER: True modelcount 3 <br>

TELL <br>
p1&p2&p3=> p4; p5&p6 => p4; p1 => p2; p1&p2 => p3; p5&p7 => p6; p1; p4; <br>
ASK <br>
p7 <br>

ANSWER: False model count: 3

## Acknowledgements/Resources
Artificial Intelligence - A Modern Approach, Stuart Russel and Peter Norvig <br>
_This assisted in providing the explanations of the various algorithms particularly 
the Truth Table Checker and the Forward Chaining algorithm <br>

Jordan <br>
help me solve a particular problem with my truth table checker<br>




## Notes

## Summary Report
This project was a solo project and as such all the components were done by me.