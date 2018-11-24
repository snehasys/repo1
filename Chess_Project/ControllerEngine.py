from Enumerators import PlayerType
from Enumerators import BOARD_SIZE
from Piece import Piece


#############################################################
class ControllerEngine:
    lastPlayed : PlayerType
    chessBoardSize: int
    pieces : Piece = [] #: list(Piece) # 4* BOARD_SIZE


    def __init__(self):  # constructor
        self.chessBoardSize = BOARD_SIZE
        self.lastPlayed = PlayerType.Black # so that white can start the game



#############################################################

#############################################################
#############################################################
#############################################################
class Player:
    activePieces : Piece = []   
    playerType   : PlayerType


#############################################################