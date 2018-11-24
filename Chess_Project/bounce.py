import sys
import pygame
#pygame.init()

resolution = width, height = 1024, 768 #320, 240
speed = [2, 2]
black = 0, 0, 0

screen = pygame.display.set_mode(resolution)

def bounceMe(whatToBounce, howManyTimes:int):
    ballrect = whatToBounce.get_rect()
    while howManyTimes:
        for event in pygame.event.get():
            if event.type == pygame.QUIT: sys.exit()

        ballrect = ballrect.move(speed)
        if ballrect.left < 0 or ballrect.right > width:
            speed[0] = -speed[0]
        if ballrect.top < 0 or ballrect.bottom > height:
            speed[1] = -speed[1]

        screen.fill(black)
        screen.blit(ball, ballrect)
        pygame.display.flip()
        howManyTimes -= 1


ball = pygame.image.load(r'C:\00_GitHub\repo1\Chess_Project\images\pawn_white.jpg')
ball2 = pygame.image.load(r'C:\00_GitHub\repo1\Chess_Project\images\w_p.png')

#bounceMe(ball, 1000)
#bounceMe(ball2, 1000)


