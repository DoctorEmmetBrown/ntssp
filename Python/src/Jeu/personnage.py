# -*- coding: utf8 -*-
import pygame
from Jeu.script import *
import Application.App
from Jeu.monster import *
def getSide(numJoueur,nbPlayer,sizeEcran):
    if(nbPlayer == 2):
        if(numJoueur == 1):
            x = 0
            y = 0
            width = sizeEcran[0]/2
            height = sizeEcran[1]
        else:
            x = sizeEcran[0]/2
            y = 0
            width = sizeEcran[0]/2
            height = sizeEcran[1]
    r = pygame.Rect(x,y,width,height)
    print((r.right,r.left,r.top,r.bottom))
    return r
class Personnage:
    def __init__(self,name,spIdle,spDroite,spGauche,scripts):
        self.name=name
        self.spIdle=spIdle
        self.spDroite=spDroite
        self.spGauche=spGauche
        self.__lastSprite=self.spIdle
        self.__isRight=False
        self.__isLeft=False
        self.x=0
        self.y=0
        self.__scripts=scripts
        self.__tirescript=loadScript(self.__scripts["TIRE"])
        self.__bombscript=loadScript(self.__scripts["BOMB"])
        self.__persobombscript=loadScript(self.__scripts["PERSOBOMB"])
        self.__tire=False
        self.__lasttire=False
        self.__blocked=False
    def setCoord(self,x,y):
        self.x=x
        self.y=y
        print((x,y))
    def setNumPlayer(self,numPlayer,nbPlayer):
        self.side = getSide(numPlayer,nbPlayer,(self.spIdle.screen.get_width(),self.spIdle.screen.get_height()))
        f = self.__bombscript["load"]
        f(Application.App.App.getSingleton(),self)
        f = self.__tirescript["load"]
        f(Application.App.App.getSingleton(),self)
        f = self.__persobombscript["load"]
        f(Application.App.App.getSingleton(),self)
    def up(self):
        self.y-=1
    def down(self):
        self.y+=1
    def right(self):
        self.__isRight=True
        self.x+=1
    def left(self):
        self.__isLeft=True
        self.x-=1
    def tire(self):
        if(self.__lasttire):
            pass #charge
        else:
            f = self.__tirescript["newScript"]
            f(Application.App.App.getSingleton(),self)
        self.__tire=True
    def bomb(self):
        f = self.__bombscript["isActive"]
        active = f()
        if(not active):
            for m in Monster.getAllActiveMonsters(self):
                m.mhit(10,False,None)
            f = self.__bombscript["newScript"]
            f(Application.App.App.getSingleton(),self)
            f = self.__persobombscript["newScript"]
            f(Application.App.App.getSingleton(),self)
    def update(self):
        f = self.__bombscript["update"]
        f()
        f = self.__persobombscript["update"]
        f()
        if(not self.__blocked):
            self.spIdle.setCoord(self.x,self.y)
            r = self.spIdle.display_rect.clamp(self.side)
            self.x,self.y=r.x,r.y
            current = self.currentSprite()
            if(current is not self.__lastSprite):
                current.currentFrame = 0
            current.setCoord(self.x,self.y)
            current.update()
            self.__lastSprite=current
            self.__isRight=False
            self.__isLeft=False
        f = self.__tirescript["update"]
        f()

        d = pygame.sprite.groupcollide(self.__tirescript["tire_collide"],Monster.monsters_collide[(self)],False,False)
        app = Application.App.App.getSingleton()
        for l in list(d.values()):
            for m in l:
                    app.jeu.stage.touchedsound.play()
                    m.container.hit()
        f = self.__tirescript["collide"]
        f(list(d.keys()))
        if(self.__tire):
            self.__lasttire=True
        else:
            self.__lasttire=False
        self.__tire=False
    def block(self):
        self.__blocked=True
    def unblock(self):
        self.__blocked=False
    def currentSprite(self):
        if(self.__isLeft):
            current = self.spGauche
        elif(self.__isRight):
            current = self.spDroite
        else:
            current = self.spIdle
        return current