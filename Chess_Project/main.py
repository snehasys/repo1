import sys
import pygame
import time
    # have the members
    # member will have category
    # each category shall have different kind of moves.
        # moves can be inherited (do we need this feature??)
    # prepare the nxn battlefield
    # Play
    # GUI management

class MatchController:
    boardSize = 8
    def setSize(self, n:int):
        self.boardSize = n
    
clock = pygame.time.Clock()
screen = pygame.display.set_mode((1024,768))

blue = 2, 3, 4

screen.fill(blue)

time.sleep(10)

## TODO : Call the controller engine from here
