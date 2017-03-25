# COSC4368-SimpleGeneticAlgoritm
COSC 4368: Artificial Intelligence Programming - Spring 2017 - HW1

Part 1 : Experiment 1:
Generate randomly 20 chromosomes with 10 bits containing 1’s and 0’s only.
Implement a simple genetic algorithm to find the string 1010101010. Let the fitness
function f(x) be the number of 1’s in odd bits and the number of 0’s at even bits. As
an example 1011010111:
 1 2 3 4 5 6 7 8 9 10
1 0 1 1 0 1 0 1 1 1
It has only one 0’s at even bits (2nd bit) and has three 1’s at odd bits (1st,3rd and 9th).
At each iteration assume the population size to be 20, the percentage of crossover
(Pco) = 0.7 and the crossover mask to be 1111100000 (5 ones followed by 5 zeros).
With regard to mutation, at every iteration choose randomly any 1 chromosome from
the population and change any one of its bits randomly.
Print the initial population. And at every iteration, print the fitness values for each
chromosome (after cross-over and mutation).
When you find the string of 1010101010 on any chromosome, stop the program.
Print the number of generations (that is the number of times you generated a new
population including the first one).
Homework 1
COSC4368 Artificial Intelligence Programming
University of Houston
Department of Computer Science
Sent on: Friday Jan 27, 2017
Due: Friday Feb 3, 2017 (midnight)
Bit # ->
Run the same genetic algorithm 20 times and print the average number of
generations at which the string of 1010101010 is discovered. Also plot the number
of generations for each run of the experiment.
Part 2:
Perform Experiment 1 with the same initial population for each run, but with the
following conditions:
(i) crossover percentage (i.e., pco )= 0.3
(ii) crossover percentage (i.e., pco )= 0.5
(iii) crossover percentage (i.e., pco) = 0.9
(iv) crossover turned off (i.e., pco = 0).
Plot the number of generations against the number of runs for each of the above
conditions. Find the average number of generations for each case.
Part 3:
Answer the following based on your findings:
(i) Which ‘pco’ value produced the best result, based on the average number
of generations?
(ii) Is mutation with crossover better than mutation alone? Why?
*************************************************************
What you need to deliver:
Each team needs to deliver one source code and one report.
The source code can be in any language but you are not allowed to use ready-to-use
toolboxes. The source code should have enough comments to clarify what has been
done in each section.
The report should contain:
1- A brief description of the code with the help of pseudocode.
2- Output of your code :
a. Plots showing the number of generations to reach the solution for each
pco (0.0, 0.3, 0.5, 0.7, 0.9)
b. The average number of generations to reach the solution for each pco.
c. The initial population, the population after cross over and mutation in
the first two and the last two iterations using pco=0.7 and pco=0.0, for
any one run.
3- Answers to Part 3.
- Please note that not following the proper instructions will result in loss of
marks.
- Please do not include any screen shots in your report.
- You can have up to 3 submissions on blackboard but TAs will only grade the
last one.
- Read the instructions of the homework carefully. Do not add anything that
was not asked in the instructions. If you are not sure about something, ask the
TAs by email (allow 24 hours before resending your questions) or attend
office hours.
- Start early and send your homework on-time. Submitting homework a few
minutes before the deadline is not a good idea. Some students found
Blackboard unresponsive in the last minutes before the deadline. To avoid that
scenario, send your homework at least a few hours before the deadline. If you
have other difficulties regarding Blackboard you should ask the UH
Blackboard Support: http://www.uh.edu/blackboard/support/. TAs cannot
view a student’s blackboard screen, and cannot help you in that way.
- Late submission penalty is 10 points per day after the deadline. 
