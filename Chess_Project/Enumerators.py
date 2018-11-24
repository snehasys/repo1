import enum

BOARD_SIZE = 8

PlayerType = enum.Enum('PlayerType', 'Black White', start = 3 , module=__name__)

class PieceType(enum.Enum):
    Rook   = enum.auto()
    Knight = enum.auto()
    Bishop = enum.auto()
    Queen  = enum.auto()
    King   = enum.auto()
    Pawn   = enum.auto()

MovementBehavior = enum.Enum('MovementBehavior', 'AcrossMove DiagonalMove KnightMove QueenMove KingMove PawnMove' , start = 1, module=__name__)

##############################################################
def UT1():
    print(list(PlayerType))
    print(list(PieceType))
    print(list(MovementBehavior))
##############################################################

if __name__ == '__main__': 
    UT1()