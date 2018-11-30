import os, sys
import pygame
from pygame.locals import *  # MOUSEBUTTONDOWN

from pygame import font as pygame_font

from Enumerators import BOARD_SIZE
# Initialize the game engine
pygame.init()
 
# Define the colors we will use in RGB format
BLACK = (  0,   0,   0)
BROWN = (100,  73,  60)
GREY  = ( 50,  50,  50)
LGREY = (150, 150, 150)
WHITE = (255, 255, 255)
BLUE  = (  0,   0, 255)
GREEN = (  0, 255,   0)
RED   = (255,   0,   0)

screenResolution = width, height = 1280, 1024 #320, 240
speed = [5, 5]

screen = pygame.display.set_mode( screenResolution, 0, 32) # TODO: make it 32, for high quality images
pygame.display.set_caption('Offline Chess -created by snehasys')
pygame_font.init()
f = pygame_font.Font(None, 20)
s = f.render("foo", True, [0, 0, 0], [255, 255, 255])

#############################
class Rectangle:
    def __init__(self, pos, color, size):
        self.pos = pos
        self.color = color
        self.size = size
    def draw(self):
        pygame.draw.rect(screen, self.color, Rect(self.pos, self.size))
#############################

def drawChessBoard():
    box = (height-40)/BOARD_SIZE
    border = 3
    offset_r = offset_c = 20
    row = column = BOARD_SIZE
    printWhite = False
    screen.fill(GREY)  #// App background
    
    rectangles:Rectangle = []
    while row:        
        column = BOARD_SIZE
        offset_c = 20
        while column:
            if(printWhite): _color = WHITE 
            else:           _color = BROWN 
            _pos = (int(offset_r), int(offset_c)) #(randint(0,height), randint(0,width))
            _size = (box - border, box - border)#(639-randint(random_pos[0], 639), 479-randint(random_pos[1],479))

            rectangles.append(Rectangle(_pos, _color, _size))
            printWhite = not printWhite  # Toggle
            offset_c += box #760/BOARD_SIZE -10
            column -= 1

        printWhite = not printWhite
        offset_r += box #1000/BOARD_SIZE
        row -= 1

        pygame.draw.rect(screen, BLACK, Rect(15, 15, box*BOARD_SIZE+border, box*BOARD_SIZE+border), 10) #// Chessboard border
        # pygame.draw.rect(screen, WHITE, Rect(box*BOARD_SIZE+border+40, 5, (box*BOARD_SIZE)/4, (box*BOARD_SIZE)/2), 1) #// Scoreboard border
        screen.lock()
        for rectangle in rectangles:
            rectangle.draw()
        screen.unlock()


#############################
def bounceMe(chessPiece_img, howManyTimes:int):
    chessPiece_rect = chessPiece_img.get_rect()
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
                elif event.type == KEYDOWN and event.scancode == 1:
                    running = False #pygame.display.quit() # TODO show a exit warning on gamescreen
                elif event.type == KEYDOWN:
                    print (event)
                elif event.type == MOUSEBUTTONDOWN:
                    print (event)#.button, event.pos)
                    chessPiece_rect.left = event.pos[0]
                    chessPiece_rect.bottom = event.pos[1]
                    screen.blit(chessPiece_img, (event.pos))

                    pygame.display.flip()
                elif event.type == VIDEORESIZE:
                    screen = pygame.display.set_mode(
                        event.dict['size'], HWSURFACE | DOUBLEBUF | RESIZABLE)
                    #screen.blit(pygame.transform.scale(pic, event.dict['size']), (0, 0))
                    pygame.display.flip()
            except:
                pass #ignore the exception

        # chessPiece_rect = chessPiece_rect.move(speed)
        # if chessPiece_rect.left < 0 or chessPiece_rect.right > width:
        #     speed[0] = -speed[0]
        # if chessPiece_rect.top < 0 or chessPiece_rect.bottom > height:
        #     speed[1] = -speed[1]

        # screen.fill(BLACK)
        drawChessBoard()
        screen.blit(chessPiece_img, chessPiece_rect)
        f = pygame_font.Font(None, 50)
        s = f.render("\n" +' '*int(width/10) +"Score:", False, [200, 200, 200], None)
        font_surface = s.get_rect()#.move(speed)
        screen.blit(s, font_surface)
        pygame.display.flip()

    pygame.display.quit()

knight = pygame.image.load( os.path.join("Chess_Project","images", 'w_k.png')) #r'C:\\00_GitHub\\repo1\\Chess_Project\\images\\w_k.png'
smallKnight = pygame.transform.scale(knight, (75,120))

if __name__ == "__main__":
    #bounceMe(ball, 1000)
    bounceMe(smallKnight, 1000)


