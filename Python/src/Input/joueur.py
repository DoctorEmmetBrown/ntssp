# -*- coding: utf8 -*-
import pygame
class Joueur:
    def __init__(self,perso):
        self.perso=perso
    def notify(self,events):
        pass

class JoueurClavier(Joueur):
    def __init__(self,perso):
        Joueur.__init__(self,perso)
        
    def notify(self):
        buff = pygame.key.get_pressed()
        if(buff[pygame.K_z]):
            self.perso.up()
        if(buff[pygame.K_s]):
            self.perso.down()
        if(buff[pygame.K_q]):
            self.perso.left()
        if(buff[pygame.K_d]):
            self.perso.right()
        if(buff[pygame.K_RETURN]):
            self.perso.tire()
        if(buff[pygame.K_SPACE]):
            self.perso.bomb()