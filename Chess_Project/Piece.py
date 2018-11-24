from Enumerators import PlayerType


#############################################################
# BASE CLASS for any chess piece
class Piece: 
    isDecommissioned : bool
    currentLocation : tuple # x,y
    color : PlayerType

    def __init__(self, location : tuple, pieceColor : PlayerType):
        self.isDecommissioned   = False
        self.currentLocation    = location
        self.color              = pieceColor

    def getAllMoves(self): # virtual method
        pass
