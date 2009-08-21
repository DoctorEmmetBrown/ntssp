# -*- coding: utf8 -*-
from Affichage.sprite import *
import pygame.sprite


class flamme:
    def __init__(self,sprite,zoom):
        self.sprite = sprite
        self.__orientation = 0
        self.__zoom=zoom
        self.sprite = self.sprite.scale(zoom)
        self.currentSprite = self.sprite
        self.x=-500
        self.y=-500
    def setOrientation(self,orientation):
        self.__orientation = orientation % 360
        self.currentSprite = self.sprite.rotate(self.__orientation)
    def update(self):
        self.currentSprite.setCoord(self.x,self.y)
        self.currentSprite.update()
