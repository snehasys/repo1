import sys
import pygame
from pygame.locals import *  # MOUSEBUTTONDOWN
from Enumerators import BOARD_SIZE

# Initialize the game engine
pygame.init()
 
# Define the colors we will use in RGB format
BLACK = (  0,   0,   0)
BROWN = (100,  73,  60)
GREY  = (50, 50, 50)
WHITE = (255, 255, 255)
BLUE  = (  0,   0, 255)
GREEN = (  0, 255,   0)
RED   = (255,   0,   0)

screenResolution = width, height = 1280, 1024 #320, 240
speed = [5, 5]

screen = pygame.display.set_mode( screenResolution, 0, 32) # TODO: make it 32, for high quality images
pygame.display.set_caption('Offline Chess -created by snehasys')
pygame.FULLSCREEN
from random import *
# from pygame.locals import *
class Rectangle:
    def __init__(self, pos, color, size):
        self.pos = pos
        self.color = color
        self.size = size
    def draw(self):
        # print(self.pos)
        pygame.draw.rect(screen, self.color, Rect(self.pos, self.size))

#############################
def drawChessBoard():
    box = (height-50)/BOARD_SIZE
    border = 5
    offset_r = 20
    offset_c = 20
    row = BOARD_SIZE
    printWhite = False
    screen.fill(GREY)
    
    rectangles = []
    while row:        
        column = BOARD_SIZE
        offset_c = 20
        while column:
            if(printWhite):
                _color = WHITE #(randint(0,255), randint(0,255), randint(0,255))
            else:
                _color = BROWN #(randint(0,255), randint(0,255), randint(0,255))
            _pos = (int(offset_r), int(offset_c)) #(randint(0,height), randint(0,width))
            _size = (box - border, box - border)#(639-randint(random_pos[0], 639), 479-randint(random_pos[1],479))

            rectangles.append(Rectangle(_pos, _color, _size))
            printWhite = not printWhite  # Toggle
            offset_c += box #760/BOARD_SIZE -10
            column -= 1

        printWhite = not printWhite
        offset_r += box #1000/BOARD_SIZE
        row -= 1

        screen.lock()
        for rectangle in rectangles:
            rectangle.draw()
        screen.unlock()

    # need = True
    # while need:
    #     for event in pygame.event.get():
    #         if event.type == QUIT:
    #             exit()
    #     pygame.display.update()
    #     need = False

#############################
def bounceMe(whatToBounce, howManyTimes:int):
    # Draw a rectangle outline
    # pygame.draw.line(screen, GREEN, [0, 0], [50,30], 5)

    # pygame.display.flip()

    ballrect = whatToBounce.get_rect()
    running = True
    clock = pygame.time.Clock()
 
    while running: #howManyTimes:
        # This limits the while loop to a max of 10 times per second.
        clock.tick(60) #a.k.a 60fps
        pygame.mouse.set_visible(True)
        for event in pygame.event.get():
            try:
                #print (event)
                if event.type == pygame.QUIT:
                    pygame.quit(); sys.exit()
                if event.type == KEYDOWN and event.scancode == 1:
                    pygame.display.quit() # TODO show a exit warning on gamescreen
                if event.type == KEYDOWN:
                    print (event)

                if event.type == MOUSEBUTTONDOWN:
                    print (event)#.button, event.pos)
            except:
                pass #ignore the exception

        #pygame.transform.scale(ballrect, (width, height), DestSurface = bar)
        ballrect = ballrect.move(speed)
        if ballrect.left < 0 or ballrect.right > width:
            speed[0] = -speed[0]
        if ballrect.top < 0 or ballrect.bottom > height:
            speed[1] = -speed[1]

        # screen.fill(BLACK)
        drawChessBoard()
        screen.blit(whatToBounce, ballrect)
        pygame.display.flip()
        howManyTimes -= 1

    pygame.display.quit()


#ball = pygame.image.load(r'C:\00_GitHub\repo1\Chess_Project\images\pawn_white.jpg')  # TODO : find a way to remove the path hardcoding
knight = pygame.image.load(r'C:\\00_GitHub\\repo1\\Chess_Project\\images\\w_k.png')

if __name__ == "__main__":
    #bounceMe(ball, 1000)
    bounceMe(knight, 1000)


