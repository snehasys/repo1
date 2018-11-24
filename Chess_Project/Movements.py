from Enumerators import BOARD_SIZE
from Enumerators import CellDiagonalMovements
import unittest

def isValidCell(cell: tuple):
    if(cell[0] in range(0,BOARD_SIZE) and
       cell[1] in range(0,BOARD_SIZE) ) :
         return True
    return False

def getAdjacentDiagonalCell(strategy    : CellDiagonalMovements,
                            currentCell : tuple,
                            count       : int):
    if(strategy == CellDiagonalMovements.PP): return (currentCell[0] + count, currentCell[1] + count)
    if(strategy == CellDiagonalMovements.MM): return (currentCell[0] - count, currentCell[1] - count)
    if(strategy == CellDiagonalMovements.PM): return (currentCell[0] + count, currentCell[1] - count)
    if(strategy == CellDiagonalMovements.MP): return (currentCell[0] - count, currentCell[1] + count)


class MovementBehavior:
    isJumpingAllowed: bool
    def __init__(self):
        isJumpingAllowed = True
    def getAllMoves(self, x : int, y : int):  #virtual
        pass

class AcrossMove(MovementBehavior):
    def __init__(self):
        self.isJumpingAllowed = False
        #super().isJumpingAllowed = False
    
    def getAllMoves(self, x : int, y : int):
        moveLocations : tuple = []
        count = 0
        while (count < BOARD_SIZE):
            if(count != x) : moveLocations.append((count,y))
            count += 1
            
        d = 0
        while (d < BOARD_SIZE):
            if(d != y) : moveLocations.append((x,d))
            d += 1

        return moveLocations

class DiagonalMove(MovementBehavior):
    def __init__(self):
        self.isJumpingAllowed = False
        #super().isJumpingAllowed = False
    
    def getAllMoves(self, x : int, y : int):
        moveLocations : tuple = []
        '''
        * There are 4 diagonal scenarios from any particular location.
        *  namely:  ++, --, +-, -+
        '''
        for strategy in CellDiagonalMovements :
            count = 1
            while (count < BOARD_SIZE):
                cell = getAdjacentDiagonalCell( strategy, (x,y), count) 
                if( isValidCell(cell)) :
                    moveLocations.append(cell)
                else : 
                    break
                count += 1
        # TODO : Improve above logic, so that it requires less while loops
        return moveLocations            
'''        count = 1
        while (count < BOARD_SIZE):
            cell = (x-count, y-count)
            if( isValidCell(cell)) : moveLocations.append(cell)
            else : break
            count += 1
            
        count = 1
        while (count < BOARD_SIZE):
            cell = (x+count, y-count)
            if( isValidCell(cell)) : moveLocations.append(cell)
            else : break
            count += 1
            
        count = 1
        while (count < BOARD_SIZE):
            cell = (x-count, y+count)
            if( isValidCell(cell)) : moveLocations.append(cell)
            else : break
            count += 1
'''            
#################################################################
#################################################################
class UNIT_TESTS(unittest.TestCase): 
    def setUp(self):
        pass
  
    def SortThenMatchTwoList(self, actualMoves, expectedMoves):
        actualMoves.sort(key = lambda eachTuple : eachTuple[0] )
        actualMoves.sort(key = lambda eachTuple : eachTuple[1] )
        expectedMoves.sort(key = lambda eachTuple : eachTuple[0] )
        expectedMoves.sort(key = lambda eachTuple : eachTuple[1] )
        # print(actualMoves,expectedMoves)
        self.assertListEqual(expectedMoves, actualMoves, 'Assert ERROR: Unexpected Moves found!! ')
        
    def test_RookMovement00(self):
        rook : MovementBehavior = AcrossMove()
        self.SortThenMatchTwoList( rook.getAllMoves(0,0),
            [(1,0),(2,0), (3,0), (4,0), (5,0), (6,0), (7,0),  (0,1),(0,2),(0,3),(0,4),(0,5),(0,6),(0,7)] 
        )
    def test_RookMovement11(self):
        rook : MovementBehavior = AcrossMove()
        self.SortThenMatchTwoList( rook.getAllMoves(1,1),
            [(0,1),(2,1), (3,1), (4,1), (5,1), (6,1), (7,1),  (1,0),(1,2),(1,3),(1,4),(1,5),(1,6),(1,7)] 
        )
    def test_RookMovement77(self):
        rook : MovementBehavior = AcrossMove()
        self.SortThenMatchTwoList( rook.getAllMoves(7,7),
            [(0,7),(1,7),(2,7),(3,7),(4,7),(5,7),(6,7),(7,6),(7,5),(7,4),(7,3),(7,2),(7,1),(7,0)]
        )
    def test_RookMovement23(self):
        rook : MovementBehavior = AcrossMove()
        self.SortThenMatchTwoList( rook.getAllMoves(2,3),
            [(0,3),(1,3),(3,3),(4,3),(5,3),(6,3),(7,3), (2,0),(2,1),(2,2),(2,4),(2,5),(2,6),(2,7)]
        )

    def test_BishopMovement34(self):
        bishop = DiagonalMove()
        self.SortThenMatchTwoList(bishop.getAllMoves(3,5),
            [(0,2), (1,3), (2,4), (4,6), (5,7), (2,6), (1,7), (4,4), (5,3), (6,2), (7,1)]
        )

    def test_BishopMovement00(self):
        bishop = DiagonalMove()
        self.SortThenMatchTwoList(bishop.getAllMoves(0,0),
            [(2,2), (3,3), (4,4), (6,6), (5,5), (1,1), (7,7)]
        )

if __name__ == '__main__': 
    unittest.main() 
