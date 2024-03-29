# -*- coding: utf8 -*-
import pygame, sys,os
from Constantes.AppConstantes import *
from Constantes.DisplayConstantes import *
class Display:
    def __init__(self,fullscreen=False):
        if(fullscreen):
            self.window = pygame.display.set_mode((WIDTH, HEIGHT),pygame.FULLSCREEN,DEPTH_FULLSCREEN)
        else:
            self.window = pygame.display.set_mode((WIDTH, HEIGHT),0,DEPTH_WINDOWED)
        pygame.display.set_caption(APP_NAME) 
        self.screen = pygame.display.get_surface()
        self.backbuffer = pygame.Surface((WIDTH/2,HEIGHT/2)).convert()
        self.clock = pygame.time.Clock()
        pygame.font.init()
        self.FPSfont = pygame.font.SysFont('Times New Roman', 12)
    def getFPS(self):
        return self.clock.get_fps()
    def flip(self):
        self.clock.tick(FPS)
        #self.clock.tick()
        s = self.FPSfont.render(str(int(self.getFPS())),False,(255,255,255))
        self.backbuffer.blit(s,(0,0))
        self.screen.blit(pygame.transform.scale2x(self.backbuffer),(0,0))
        pygame.display.flip()
        
        
if __name__ == '__main__':
    # Import Psyco if available
    try:
        import psyco
        psyco.full()
    except ImportError:
        pass
    pygame.mixer.set_num_channels(50)
    d = Display()
    from Affichage.background import *
    back = Background('C:\\Twinkle Star Sprites\\Python\\Stage1.bmp',d.backbuffer)
    while True:
        back.update()
        d.flip()