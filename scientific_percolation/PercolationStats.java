import java.lang.*;
import edu.princeton.cs.algs4.StdRandom;
import edu.princeton.cs.algs4.StdStats;

import edu.princeton.cs.algs4.Stopwatch;
/*
 * @author snehasys
 * @date   20170128
 * @brief  Monte Carlo simulation. To estimate the percolation threshold
 * 
 * */
public class PercolationStats 
{
  private int grid_size;
  private int total_trials;
  private double[] arr;
  public PercolationStats(int n, int trials)    // perform trials independent experiments on an n-by-n grid
  {  // constructor 
    if ( n <= 0 | trials <= 0)
    {
      throw new java.lang.IllegalArgumentException(); 
    }
    grid_size = n;
    total_trials = trials;
    arr = new double [ total_trials ];
    
    executeExperimentsNow();
    //ss = StdStats();
  }

////////////////////////////////////////
  private void executeExperimentsNow()
  {
    for(int i=0; i<total_trials; ++i)
    {
      Percolation p = new Percolation( grid_size );
//      StdRandom.setSeed(123456789);
      while( ! p.percolates())
      {
        int randRow = StdRandom.uniform(1, grid_size +1); // Returns a random integer uniformly in [a, b).
        int randCol = StdRandom.uniform(1, grid_size +1); // Returns a random integer uniformly in [a, b).
        p.open(randRow, randCol); // open a random cell
      }
      double percolation_threshold = (double) p.numberOfOpenSites() / (grid_size*grid_size);
      arr[i] = percolation_threshold;
    }
  }
  /////////////////////////////////////////////////
  public double mean()                          // sample mean of percolation threshold
  {
    double d = StdStats.mean( arr ) ; // <<-- calculate here
    return d;
  }
  
  /*
   * 
   * What should stddev() return if trials equals 1?
   *    The sample standard deviation is undefined. We recommend returning Double.NaN
   * 
   * */

  public double stddev()                        // sample standard deviation of percolation threshold
  {
    if( total_trials == 1)
      return Double.NaN;
    
    double d = StdStats.stddev( arr ); // <<-- calculate here
    return d;
  }
  
  
  private double variance()                        // sample standard deviation of percolation threshold
  {
    if( total_trials == 1)
      return Double.NaN;
    return StdStats.var( arr ); // <<-- calculate here
  }
  public double confidenceLo()                  // low  endpoint of 95% confidence interval
  {
    double d = (1.96 * stddev())/ Math.sqrt(total_trials); // <<-- (1.96 * S) / sqrt(T)    
    return mean() - d ;
  }
  public double confidenceHi()                  // high endpoint of 95% confidence interval
  {
    double d = (1.96 * stddev())/ Math.sqrt(total_trials); // <<-- (1.96 * S) / sqrt(T)    
    return mean() + d ;
  }
   //////////////////////////
  //////////////////////////
  public static void main(String[] args)        // test client (described below)
  {
    if(args.length < 2)
      throw new java.lang.IllegalArgumentException();
    
    int n = Integer.parseInt(args[0]);
    int T = Integer.parseInt(args[1]);
//    System.out.print(n +" |<  "+ T );
//    Stopwatch sw = new Stopwatch();
    
    PercolationStats ps = new PercolationStats(n, T);
//    System.out.print(sw.elapsedTime() +" seconds spent to run " + T + " tests on "+ n + " grid\n\n");
    
    System.out.print( "mean\t\t\t= "     + ps.mean()    + "\n" );    
    System.out.print( "stddev\t\t\t= "   + ps.stddev()  + "\n" );
//  System.out.print( "variance\t\t\t= " + ps.variance()+ "\n" );
    System.out.print("95% confidence interval\t= [" +ps.confidenceLo() + ", " + ps.confidenceHi() + "]\n");    
  }
};
