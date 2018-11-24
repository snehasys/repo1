from Enumerators import BOARD_SIZE
import unittest

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
        c = 0
        while (c < BOARD_SIZE):
            if(c != x) : moveLocations.append((c,y))
            c += 1
            
        d = 0
        while (d < BOARD_SIZE):
            if(d != y) : moveLocations.append((x,d))
            d += 1
        # TODO : Improve above logic, so that less while loop requires. Hint: Maybe de/increment c&d together
        return moveLocations


class TESTS(unittest.TestCase): 
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


if __name__ == '__main__': 
    unittest.main() 
