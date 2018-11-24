from Enumerators import BOARD_SIZE
from Enumerators import CellDiagonalMovements
import unittest
import Utils


#################################################################
class MovementBehavior:
    isJumpingAllowed: bool
    def __init__(self):
        isJumpingAllowed = True
    def getAllMoves(self, x : int, y : int):  #virtual
        pass
#################################################################
class AcrossMove(MovementBehavior):
    def __init__(self):
        self.isJumpingAllowed = False
        #super().isJumpingAllowed = False
    
    def getAllMoves(self, x : int, y : int):
        possibleMoveLocations : tuple = []
        count = 0
        while (count < BOARD_SIZE):
            if(count != x) : possibleMoveLocations.append((count,y))
            count += 1
            
        d = 0
        while (d < BOARD_SIZE):
            if(d != y) : possibleMoveLocations.append((x,d))
            d += 1

        return possibleMoveLocations
#################################################################
class DiagonalMove(MovementBehavior):
    def __init__(self):
        self.isJumpingAllowed = False
        #super().isJumpingAllowed = False
    
    def getAllMoves(self, x : int, y : int):
        possibleMoveLocations : tuple = []
        '''
        * There are 4 diagonal scenarios from any particular location.
        *  namely:  ++, --, +-, -+
        '''
        for strategy in CellDiagonalMovements :
            count = 1
            while (count < BOARD_SIZE):
                cell = Utils.getAdjacentDiagonalCell( False, strategy, (x,y), count) 
                if( Utils.isValidCell(cell)) :
                    possibleMoveLocations.append(cell)
                else : 
                    break
                count += 1

        return possibleMoveLocations            
#################################################################
class KnightlyMove(MovementBehavior):
    def __init__(self):
        self.isJumpingAllowed = True
    
    def getAllMoves(self, x : int, y : int):
        possibleMoveLocations : tuple = []
        '''
        * There are maximum 8 diagonal scenarios for all particular location.
        *  namely:  +2+1, -2-1, +2-1, -2+1   ,  +1+2, -1-2, +1-2, -1+2
        '''
        for strategy in CellDiagonalMovements :
            count = 1
            cells = Utils.getAdjacentDiagonalCell( True, strategy, (x,y), count) 
            if( Utils.isValidCell(cells[0])) :
                possibleMoveLocations.append(cells[0])
            if( Utils.isValidCell(cells[1])) :
                possibleMoveLocations.append(cells[1])

        return possibleMoveLocations 
#################################################################
class KingMove(MovementBehavior):
    def __init__(self):
        self.isJumpingAllowed = False
    
    def getAllMoves(self, x : int, y : int):
        possibleMoveLocations : tuple = []
        '''
        * There are maximum 8 total scenarios for a King.
        *  namely:  (-1,+1), (0,+1),  (+1,+1),  (-1,+0), (+1,+0), (-1,-1), (0,-1), (+1,-1) 
        '''
        cells = (
            (x-1,y+1), (x,y+1), (x+1,y+1), (x-1,y), (x+1,y), (x-1,y-1), (x,y-1), (x+1,y-1)
        )
        for cell in cells :
            if( Utils.isValidCell(cell)) :
                possibleMoveLocations.append(cell)
        
        # TODO : Implement Castling between Rooks & King. Maybe this location is not the right place to implement castling. Investigate
        return possibleMoveLocations
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
        bishop : MovementBehavior  = DiagonalMove()
        self.SortThenMatchTwoList(bishop.getAllMoves(3,5),
            [(0,2), (1,3), (2,4), (4,6), (5,7), (2,6), (1,7), (4,4), (5,3), (6,2), (7,1)]
        )
    def test_BishopMovement00(self):
        bishop : MovementBehavior  = DiagonalMove()
        self.SortThenMatchTwoList(bishop.getAllMoves(0,0),
            [(2,2), (3,3), (4,4), (6,6), (5,5), (1,1), (7,7)]
        )
        
    def test_KnightlyMovement00(self):
        knight : MovementBehavior  = KnightlyMove()
        self.SortThenMatchTwoList(knight.getAllMoves(0,0),
            [(2,1), (1,2)]
        )
    def test_KnightlyMovement32(self):
        knight : MovementBehavior  = KnightlyMove()
        self.SortThenMatchTwoList(knight.getAllMoves(3,2),
            [(5,1), (4,0), (4,4), (5,3), (2,4), (1,3), (2,0), (1,1)]
        )
        
    def test_KING_Movement30(self):
        king : MovementBehavior  = KingMove()
        self.SortThenMatchTwoList(king.getAllMoves(3,0),
            [(2,0), (2,1), (3,1), (4,1), (4,0)]
        )
#################################################################

if __name__ == '__main__': 
    unittest.main() 
