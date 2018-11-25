from Enumerators import BOARD_SIZE
from Enumerators import CellDiagonalMovements


def isValidCell(cell: tuple):
    if(cell[0] in range(0,BOARD_SIZE) and
       cell[1] in range(0,BOARD_SIZE) ) :
         return True
    return False

def getAdjacentDiagonalCell(isKnight    : bool,
                            strategy    : CellDiagonalMovements,
                            currentCell : tuple,
                            count       : int):
    if(strategy == CellDiagonalMovements.PP):
      if(not isKnight):
        return (currentCell[0] + count, currentCell[1] + count)
      else:
        return ((currentCell[0] + 2*count, currentCell[1] + count),
                (currentCell[0] + count, currentCell[1] + 2*count))

    if(strategy == CellDiagonalMovements.MM):
      if(not isKnight):
        return (currentCell[0] - count, currentCell[1] - count)
      else:
        return ((currentCell[0] - 2*count, currentCell[1] - count),
                (currentCell[0] - count, currentCell[1] - 2*count))

    if(strategy == CellDiagonalMovements.PM): 
      if(not isKnight):
        return (currentCell[0] + count, currentCell[1] - count)
      else:
        return ((currentCell[0] + 2*count, currentCell[1] - count),
                (currentCell[0] + count, currentCell[1] - 2*count))
                
    if(strategy == CellDiagonalMovements.MP): 
      if(not isKnight):
        return (currentCell[0] - count, currentCell[1] + count)
      else:
        return ((currentCell[0] - 2*count, currentCell[1] + count),
                (currentCell[0] - count, currentCell[1] + 2*count))

