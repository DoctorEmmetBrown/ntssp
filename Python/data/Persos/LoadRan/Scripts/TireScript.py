# -*- coding: utf8 -*-
from Jeu.script import *
from Xml.handlers import *
class TireScript(Script):
    def __init__(self,t,perso):
        super(TireScript,self).__init__(t[0],t[1])
        self.sprites[0].setContainer(self)
        tire_collide.add(self.sprites[0])
        cs = perso.currentSprite()
        self.x=perso.x+(cs.display_rect.width-self.sprites[0].display_rect.width)/2
        self.y=perso.y-self.sprites[0].display_rect.height
        self.perso = perso
        self.first=True
    def update(self):
       if(self.first):
           self.sons[0].play()
           self.first=False
       self.y-=5
       if(self.y+self.sprites[0].display_rect.height <= self.perso.side.top):
           return True
       self.sprites[0].setCoord(self.x,self.y)
       self.sprites[0].update()
       return False
import pygame.sprite
tire_collide=pygame.sprite.Group()
scripts=[]
def collide(sprites):
    collided=[]
    for s in sprites:
        collided.append(s.container)
    for c in collided:
        if(c in scripts):
            scripts.remove(c)
            tire_collide.remove(c.sprites[0])
def newScript(app,perso):
    script = getScriptFromXML("..\data\Persos\LoadRan\Scripts\TireScript.xml",app.display.backbuffer)
    ts = TireScript(script,perso)
    scripts.append(ts)
    
def update():
    finished=[]
    for s in scripts:
        if(s.update()):
            finished.append(s)
    for f in finished:
        scripts.remove(f)
        tire_collide.remove(f.sprites[0])
        
def load(app,perso):
    getScriptFromXML("..\data\Persos\LoadRan\Scripts\TireScript.xml",app.display.backbuffer)