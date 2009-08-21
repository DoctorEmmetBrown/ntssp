# -*- coding: utf8 -*-
from Jeu.script import *
from Jeu.monster import *
import pygame.sprite
import Application.App
class Stage:
    def __init__(self,name,background,son,monsters,formations,touchedsound):
        self.background=background
        self.son=son
        self.monsters=monsters
        self.formations=formations
        self.touchedsound=touchedsound
        self.__isFirstUpdate=True
        self.__index=0
        self.__form_scripts=[]
        app = Application.App.App.getSingleton()
        app.jeu.stage=self
        for f in self.formations:
            s = loadScript(f)
            fct = s["load"]
            fct(app)
            self.__form_scripts.append(s)
        self.__curFormInd = [0,0]
        self.__curForms=[self.__form_scripts[self.__curFormInd[0]]]
        f = (self.__curForms[0])["newScript"]
        app.jeu.stage=self
        f(app,app.jeu.persos[0])
        f(app,app.jeu.persos[1])
    def getNewMonster(self,size,color):
        for m in self.monsters:
            if(m.size == size):
                return m.clone(color)
        return None
    def processForm(self,form):
        f = form["update"]
        finished = f()
        if(len(form["scripts"])==0):
            self.__curForms.remove(form)
        for fin in finished:
            app = Application.App.App.getSingleton()
            app.jeu.stage=self
            i = app.jeu.persos.index(fin.perso)
            self.__curFormInd[i] = (self.__curFormInd[i] +1)%len(self.__form_scripts)
            f = self.__form_scripts[self.__curFormInd[i]]["newScript"]
            f(app,app.jeu.persos[i])
            if(self.__form_scripts[self.__curFormInd[i]] not in self.__curForms):
                self.__curForms.append(self.__form_scripts[self.__curFormInd[i]])
    def update(self):
        if(self.__isFirstUpdate):
            self.son.play()
            self.__isFirstUpdate=False
        self.background.update()
        for f in self.__curForms:
            self.processForm(f)
        isComboMax=False
        for m in Monster.explosion_group:
            m.update()
            if(m.explosion.combo is not None and m.explosion.combo.combo == combomax):
                isComboMax=True
        combomax=0
        for k in list(Explosion.explosions_collide.keys()):
            col = pygame.sprite.groupcollide(Explosion.explosions_collide[k],Monster.monsters_collide[k],False,False)
            for k1 in list(col.keys()):
                if(k1.container.combo is None):
                    combo = Combo();
                else:
                    combo = k1.container.combo
                for m in col[k1]:
                    m.container.mhit(k1.container.pow,True,combo)
