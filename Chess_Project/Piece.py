from Enumerators import PlayerType
import Movements
import unittest

#############################################################
# BASE CLASS for any chess piece
class Piece: 
    isDecommissioned : bool
    currentLocation : tuple # x,y
    color : PlayerType
    movementBehavior: Movements.MovementBehavior
    def __init__(self, location : tuple, pieceColor : PlayerType):
        self.isDecommissioned   = False
        self.currentLocation    = location
        self.color              = pieceColor
        # self.movementBehavior   = Movements.AcrossMove()

    def getAllMoves(self): # virtual method
        pass

# inheriting from ChessPiece Base class
class Rook(Piece): 
    def __init__(self, location : tuple, pieceColor : PlayerType):
        self.isDecommissioned   = False
        self.currentLocation    = location
        self.color              = pieceColor
        self.movementBehavior   = Movements.AcrossMove()

    def getAllMoves(self): 
        if(self.isDecommissioned): return []
        return self.movementBehavior.getAllMoves(self.currentLocation[0],
                                                 self.currentLocation[1])

# inheriting from ChessPiece Base class
class Bishop(Piece): 
    def __init__(self, location : tuple, pieceColor : PlayerType):
        self.isDecommissioned   = False
        self.currentLocation    = location
        self.color              = pieceColor
        self.movementBehavior   = Movements.DiagonalMove()

    def getAllMoves(self): 
        if(self.isDecommissioned): return []
        return self.movementBehavior.getAllMoves(self.currentLocation[0],
                                                 self.currentLocation[1])

# inheriting from ChessPiece Base class
class Knight(Piece): 
    def __init__(self, location : tuple, pieceColor : PlayerType):
        self.isDecommissioned   = False
        self.currentLocation    = location
        self.color              = pieceColor
        self.movementBehavior   = Movements.KnightlyMove()

    def getAllMoves(self): 
        if(self.isDecommissioned): return []
        return self.movementBehavior.getAllMoves(self.currentLocation[0],
                                                 self.currentLocation[1])



# if __name__ == '__main__': 
#     unittest.main() 
