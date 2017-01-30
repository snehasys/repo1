# A percolation test gives a model to find a consistent crack in soil/metal/object.
It can also be tweaked to suggest, in how many more steps a suitable crack can be made with least cost.
#How to run
1. compile all java files in current directory. Note: they are not part of any packages.
2. do not compile (references directory)
3. execute the following in console>>  
      $ java PercolationVisualizer "percolation_test/input20.txt"
4. check the visual process for correctness

TODO: Yet to fix the backwash issue (when virtualHead and VirtualTail are linked, bleeding happens from the bottom of matrix). 
       //Guess I need to fix isFull() & open() both, when I have time.
       
       
#Dependency: 
The module is dependent on the algs4.jar which can be found at http://algs4.cs.princeton.edu/code/algs4.jar
#Disclaimer
Assignment mentored by Professor Robert Sedgewick, Princeton University
The test suit was provided, (refer percolation_test/credits )
