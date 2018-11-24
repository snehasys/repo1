from Enumerators import PlayerType
import Movements

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

class Rook(Piece): # inheriting from ChessPiece Base class
    def __init__(self, location : tuple, pieceColor : PlayerType):
        self.isDecommissioned   = False
        self.currentLocation    = location
        self.color              = pieceColor
        self.movementBehavior   = Movements.AcrossMove()

    def getAllMoves(self): # virtual method
        return self.movementBehavior.getAllMoves(self.currentLocation[0],
                                                 self.currentLocation[1])

class Bishop(Piece): # inheriting from ChessPiece Base class
    def __init__(self, location : tuple, pieceColor : PlayerType):
        self.isDecommissioned   = False
        self.currentLocation    = location
        self.color              = pieceColor
        self.movementBehavior   = Movements.DiagonalMove()

    def getAllMoves(self): # virtual method
        return self.movementBehavior.getAllMoves(self.currentLocation[0],
                                                 self.currentLocation[1])

