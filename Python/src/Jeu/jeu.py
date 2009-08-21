# -*- coding: utf8 -*-
import pygame, sys,os
from Affichage.background import *
from Affichage.sprite import *
from Xml.handlers import *
from Input.joueur import *
from Input.input_manager import *
import Application.App
from Jeu.monster import *
import pygame.sprite
class Jeu:
    def __init__(self,display):
        p = getPersoFromXML('..\data\Persos\LoadRan\LoadRan.xml',display.backbuffer)
        p1 = getPersoFromXML('..\data\Persos\LoadRan\LoadRan.xml',display.backbuffer)
        p.setNumPlayer(1,2)
        p.setCoord(50,50)
        p1.setNumPlayer(2,2)
        p1.setCoord(p1.side.left+50,p1.side.top+50)
        self.joueurs=[JoueurClavier(p),JoueurClavier(p1)]
        self.persos=[p,p1]
        Application.App.App.getSingleton().jeu=self
        self.stage = getStageFromXML('..\data\Stages\Stage 1\Stage1.xml',display.backbuffer)
    def update(self):
        scan()
        self.stage.update()
        for perso in self.persos:
            perso.update()
        for joueur in self.joueurs:
            joueur.notify()

        