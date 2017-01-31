import java.lang.*;
import edu.princeton.cs.algs4.WeightedQuickUnionUF; //imported from algs4.jar, which is needed to be added in classpath
/*
 * @author snehasys
 * @date   20170129
 * @brief  The PERCOLATION dataStructure model
 * */

/*
 * What is Percolation? 
 * Given a composite systems comprised of randomly distributed insulating and metallic materials: 
 * what fraction of the materials need to be metallic so that the composite system is an electrical conductor?
 * Given a porous landscape with water on the surface (or oil below), 
 * under what conditions will the water be able to drain till the bottom (or the oil to rush to the surface)?
 * Scientists have defined an abstract process known as PERCOLATION to model such situations. 
 * */
public class Percolation 
{
  private int  grid_size;
  private boolean[] siteOpenStatuses;     // false is closed, true is opened..  index 0 will never be used
  private int openSitesCount;
  private WeightedQuickUnionUF ufObj;   // the row & column indices are between 1 & n, where (1,1) is the upper-left site
  private int virtualTopIdx;
  private int virtualBottomIdx;
  
  ////////////////////////////////////////////
  public Percolation(int n)                // create n-by-n grid, with all sites blocked
  { // *** Constructor ****
    if ( n < 1)
      throw new java.lang.IllegalArgumentException();
    
    grid_size = n;
    ufObj = new WeightedQuickUnionUF(grid_size * grid_size + 2); //Initialize them as close blocks
    siteOpenStatuses = new  boolean [grid_size * grid_size + 1]; // auto initialized false..  idx 0 will never be used
    openSitesCount   = 0;  // initially all sites are closed
    virtualTopIdx    = 0;  
    virtualBottomIdx = (grid_size * grid_size) + 1;
  }
  ////////////////////////////////////////////////////
  private void validate(int row, int col)
  {
    if(  row < 1 || row > grid_size )
      throw new java.lang.IndexOutOfBoundsException("row index " + row + " out of bounds");
    if(  col < 1 || col > grid_size )
      throw new java.lang.IndexOutOfBoundsException("col index " + col + " out of bounds");
  }
  ////////////////////////////////////////////////////
  private boolean isValid(int row, int col)
  {
    if(  row < 1 || row > grid_size )  return false;
    if(  col < 1 || col > grid_size )  return false;    
    return true;
  }  
  ////////////////////////////////////////////////////
  private int xyTo1D(int x, int y)
  {
    validate(x, y);
    return ((x - 1) * grid_size) + y; // flattening the coordinates... 
  }
      
  ////////////////////////////////////////////
  public    void open(int row, int col)    // open site (row, col) if it is not open already
  {
    validate(row, col);
    
    int idx = xyTo1D(row, col);
    if ( false == siteOpenStatuses[idx]) //it seems to be closed, so open it
    {
      siteOpenStatuses[idx] = true;
      openSitesCount++; // open done
      /*
       * now establish connectivity with neighbour preopened cells
       */
      
      // UP adjacent cell 
      
      if (1 == row ) //when we already are in top row, connect this cell to virtualTop
      {
        ufObj.union(idx, virtualTopIdx);
      }
      else if(   isValid(row-1, col) 
         && isOpen (row-1, col) ) //check if up adjacent cell exist AND it is already opened 
      {
        int upIdx = xyTo1D(row-1, col);
        ufObj.union(idx, upIdx); //connect up adjacent cell to current cell
      }
      // DOWN adjacent cell 
      if (grid_size == row ) //when we already are in bottom row, connect this cell to virtualBottom
      {
        ufObj.union(idx, virtualBottomIdx );
      }
      else if(   isValid(row+1, col) 
         && isOpen (row+1, col) ) //check if down adjacent cell exist AND it is already opened 
      {
        int downIdx = xyTo1D(row+1, col);
        ufObj.union(idx, downIdx); //connect down adjacent cell to current cell
      }
      
      
      // left  adjacent cell 
      if(   isValid(row, col-1) 
         && isOpen (row, col-1) ) //check if left adjacent cell exist AND it is already opened 
      {
        int leftIdx = xyTo1D(row, col-1);
        ufObj.union(idx, leftIdx); //connect left adjacent cell to current cell
      }
      
      
      // right adjacent cell 
      if(   isValid(row, col+1) 
         && isOpen (row, col+1) ) //check if right adjacent cell exist AND it is already opened 
      {
        int rightIdx = xyTo1D(row, col+1);
        ufObj.union(idx, rightIdx); //connect right adjacent cell to current cell
      }      
    }    
  }
  ////////////////////////////////////////////
  public boolean isOpen(int row, int col)  // is site (row, col) open?
  {
    validate(row, col);
    int idx = xyTo1D(row, col);
    return siteOpenStatuses[idx ] ; // since item(1,1) will be at idx 1, idx 0 will never be used
  }
  ////////////////////////////////////////////
  public boolean isFull(int row, int col)  // is site (row, col) full?
  {
    validate(row, col);
    int idx = xyTo1D(row, col);
    return ufObj.connected( virtualTopIdx, idx ) ; // if current cell is connected to the top, then it can be filled
  }
  ////////////////////////////////////////////
  public     int numberOfOpenSites()       // number of open sites
  {    
    return openSitesCount;
  }
  ////////////////////////////////////////////
  public boolean percolates()              // does the system percolate?    
  {
    return ufObj.connected(virtualTopIdx, virtualBottomIdx);
  }
  
   ////////////////////////////////////////////
  ////////////////  UT ///////////////////////
  public static void main(String[] args)   // test client (optional)
  {
    //System.out.print(args[0]);
    Percolation p = new Percolation(2);
    p.open(1, 1);
    p.open(1, 2);
    p.open(2, 2);
    
    
    System.out.print( p.percolates() );
  }
}
