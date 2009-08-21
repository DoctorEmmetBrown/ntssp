# -*- coding: utf8 -*-
from Affichage.sprite import *
import pygame.sprite
combomax = 0
class Combo:
    def __init__(self):
        self.combo=1
class Explosion:
    explosions_collide={}
    def __init__(self,begin,normal,chain,sound):
        self.begin=begin
        self.normal=normal
        self.chain=chain
        self.sound=sound
        self.begin.setContainer(self)
        self.x=0
        self.y=0
        self.isBegin=True
        self.isChain=False
        self.First=True
        self.combo=None
    def setChain(self,chain,combo):
        global combomax
        self.isChain=chain
        if(self.combo is None and combo is not None):
            self.combo = combo
            self.combo.combo+=1
            if(self.combo.combo > combomax):
                combomax = self.combo.combo
            print(combomax)
    def setNormal(self):
        self.isChaine=False
    def setPower(self,pow):
        self.pow=pow
        self.isBegin=True
    def load_collide(self):
        if((self.perso) not in Explosion.explosions_collide):
            Explosion.explosions_collide[(self.perso)]=pygame.sprite.Group()
        (Explosion.explosions_collide[(self.perso)]).add(self.begin)
    def unload_collide(self):
        (Explosion.explosions_collide[(self.perso)]).remove(self.begin)
    def clone(self):
        return Explosion(self.begin.clone(),self.normal.clone(),self.chain.clone(),self.sound)
    def update(self):
        if(self.isBegin):
            if(self.First):
                self.sound.play()
                self.load_collide()
                self.First=False
            elif(self.begin.currentFrame>0):
                self.unload_collide()
                self.combo=None
            self.begin.setCoordCenter(self.x,self.y)
            self.begin.update()
            self.isBegin=not self.begin.end()
            return False
        else:
            self.First=True
            if(self.isChain):
                self.chain.setCoordCenter(self.x,self.y)
                self.chain.update()
                return self.chain.end()
            else:
                self.normal.setCoordCenter(self.x,self.y)
                self.normal.update()
                return self.normal.end()
    def setPerso(self,perso):
        self.perso=perso
        
class Monster:
    monsters_collide={}
    explosion_group=[]
    def __init__(self,size,color,sprites,gonfle,explosion):
        self.size=size
        self.color=color
        self.__initialColor=color    
        self.sprites=sprites
        self.gonfle=gonfle
        self.explosion=explosion
        self.curSprite=None
        if(sprites is not None):
            for s in sprites:
                s.setContainer(self)
            self.curSprite=self.sprites[color-1]
        self.x=-500
        self.y=-500
        self.perso=None
        self.destroyed=False
        self.explosed=False
    @classmethod
    def getAllActiveMonsters(cls,perso):
        monsters=[]
        for s in cls.monsters_collide[(perso)]:
            monsters.append(s.container)
        return monsters
    def setPerso(self,perso):
        self.perso=perso
        self.explosion.setPerso(perso)
    def unload_collide(self):
        if(self.perso is not None):
            (Monster.monsters_collide[(self.perso)]).remove(self.curSprite)
    def load_collide(self):
        if(self.perso is not None):
            if((self.perso) not in Monster.monsters_collide):
                Monster.monsters_collide[(self.perso)]=pygame.sprite.Group()
            (Monster.monsters_collide[(self.perso)]).add(self.curSprite)
    def destroy(self):
        self.unload_collide()
        self.color = self.__initialColor
        self.curSprite=self.sprites[self.color-1]
        self.curSprite.setCoord(-500,-500)
        self.x=-500
        self.y=-500
        self.destroyed=True
    def kill(self,chain,combo):
        self.explosion.setChain(chain,combo)
        self.explosion.setPower(self.__initialColor)
        self.explosion.x=self.x
        self.explosion.y=self.y
        self.gonfle[self.colorGonfle-1].setCoordCenter(self.x,self.y)
        Monster.explosion_group.append(self)
        self.isGonfle=True
        self.destroy()
    def mhit(self,nb,chain,combo):
        if(not self.destroyed):
            self.colorGonfle=max(1,self.color)
            self.color-=nb
            self.color=max(0,self.color)
            if(self.color == 0):
                self.kill(chain,combo)
            else:
                self.setColor(self.color)
    def hit(self):
        self.mhit(1,False,None)
    def setColor(self,color):
        if(self.curSprite is not None):
            curframe = self.curSprite.currentFrame
        else:
            curframe = 0
        self.unload_collide()
        self.color=color     
        self.curSprite=self.sprites[color-1]
        self.curSprite.currentFrame=curframe
        self.load_collide()
    def clone(self,color):
        sprites=[]
        for s in self.sprites:
            sprites.append(s.clone())
        gonfle=[]
        for g in self.gonfle:
            gonfle.append(g.clone())
        return Monster(self.size,color,sprites,gonfle,self.explosion.clone())
    def start(self,splines,speed,side):
        self.destroyed=False
        self.__speed=speed
        self.__splines=splines
        self.__index=0
        self.__indexSpline=0
        self.__points=splines[0].getpoints(speed)
        self.__side=side
        for s in self.sprites:
            s.setClippingArea(self.__side)
        for g in self.gonfle:
            g.setClippingArea(self.__side)
        self.explosion.begin.setClippingArea(self.__side)
        self.explosion.normal.setClippingArea(self.__side)
        self.explosion.chain.setClippingArea(self.__side)
    def followPoints(self):
        self.x = (self.__points[self.__index][0])/2+self.__side.left
        self.y = (self.__points[self.__index][1])/2+self.__side.top
        self.__index+=1
        if(self.__index == len(self.__points)):
            self.__indexSpline+=1
            self.__index=0
            if(self.__indexSpline == len(self.__splines)):
                self.destroy()
                return True
            self.__points=self.__splines[self.__indexSpline].getpoints(self.__speed)
        return False
    def update(self):
        if(not self.destroyed):
            self.curSprite.setCoord(self.x-(self.curSprite.display_rect.width/2),self.y-(self.curSprite.display_rect.height/2))
            self.curSprite.update()
        else:
            if(self.isGonfle):
                self.gonfle[self.colorGonfle-1].update()
                self.isGonfle=not self.gonfle[self.colorGonfle-1].end();
            else:
                if(self.explosion.update()):
                    Monster.explosion_group.remove(self)
#0.03
class Formation:
    def __init__(self,monsters,splines):
        self.monsters=monsters
        self.splines=splines
    def clone(self):
        monsters=[]
        for m in self.monsters:
            monsters.append(m.clone(m.color))
        return Formation(monsters,self.splines)
    def start(self,speed,inter,perso):
        self.__speed=speed
        self.__inter=inter
        self.__interInd=inter
        self.__curMonsters=[]
        self.__indexMonsters=0
        self.__side=perso.side
        self.__perso=perso
        for m in self.monsters:
            m.setPerso(self.__perso)
        for s in self.splines:
            s.getpoints(speed)
    def followSplines(self):
        finished=[]
        if(self.__interInd == self.__inter and self.__indexMonsters < len(self.monsters)):
            self.__curMonsters.append(self.monsters[self.__indexMonsters])
            (self.monsters[self.__indexMonsters]).load_collide()
            self.monsters[self.__indexMonsters].start(self.splines,self.__speed,self.__side)
            self.__interInd=0
            self.__indexMonsters+=1
        for m in self.__curMonsters:  
            if(m.destroyed):
                finished.append(m)
            elif(m.followPoints()):
                m.destroy()
                finished.append(m)
        for f in finished:
            self.__curMonsters.remove(f)
        if(len(self.__curMonsters) == 0 and self.__indexMonsters == len(self.monsters)):
            return True
        self.__interInd+=1
        return False
    def update(self):
        for m in self.__curMonsters:
            m.update()