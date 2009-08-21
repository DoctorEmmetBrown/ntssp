# -*- coding: utf8 -*-
from xml.sax import ContentHandler, make_parser
from Affichage.background import *
from Affichage.sprite import *
import pygame.mixer
from Jeu.stage import *
from Jeu.personnage import *
from Jeu.script import *
import os.path
from Affichage.resourcecache import *
from Jeu.monster import *
from Jeu.Spline.parser import *
from Constantes.DisplayConstantes import *
class StageHandler(ContentHandler):
    def __init__(self,screen,fichier):
        self.__screen=screen
        self.__fichier=fichier 
        self.__back=False
        self.__music=False
        self.__monsterType=False
        self.__formation=False
        self.__formations=[]
        pass
    def startElement(self, name, attrs):
        if(name == 'Stage'):
            self.__name=attrs.get('name',"")
        elif(name == 'Background'):
            self.__back=True
            self.__image=''
        elif(name == 'Music'):
            self.__son=''
            self.__music=True
        elif(name == 'MonsterType'):
            self.__touchedSound=get_sound(os.path.abspath(os.path.join(os.path.dirname(self.__fichier),attrs.get('TouchedSound',""))))
            self.__monsterXML=''
            self.__monsterType=True
        elif(name == 'Formation'):
            self.__formation=True
            self.__formationPath=''
    def endElement(self, name):
        if(name == 'Background'):
            self.__back=False
            self.__image = os.path.abspath(os.path.join(os.path.dirname(self.__fichier),self.__image))
            self.__background=Background(self.__image,self.__screen)
        elif(name == 'Music'):
            self.__music=False
            self.__son = os.path.abspath(os.path.join(os.path.dirname(self.__fichier),self.__son))
            self.__sound=get_sound(self.__son)
        elif(name == 'MonsterType'):
            self.__monsterType=False
            self.__monsterXML = os.path.abspath(os.path.join(os.path.dirname(self.__fichier),self.__monsterXML))
            self.__monsters = getMonstersFromXML(self.__monsterXML,self.__screen)
        elif(name == 'Formation'):
            self.__formation=False
            self.__formations.append(os.path.abspath(os.path.join(os.path.dirname(self.__fichier),self.__formationPath)))
    def characters(self, chrs):
        if(self.__back):
            self.__image+=chrs
        elif(self.__music):
            self.__son+=chrs
        elif(self.__monsterType):
            self.__monsterXML+=chrs
        elif(self.__formation):
            self.__formationPath+=chrs
    def endDocument(self):
        self.stage = Stage(self.__name,self.__background,self.__sound,self.__monsters,self.__formations,self.__touchedSound)
          
class PersonnageHandler(ContentHandler):
    def __init__(self,screen,fichier):
        self.__screen=screen
        self.__fichier=fichier
        self.__frame=False
        self.__isscript=False
        self.__scripts={}
    def startElement(self, name, attrs):
        if(name == 'Perso'):
            self.__name=attrs.get('name',"")
            self.__speed=int(attrs.get('speed',""))
            self.__power=int(attrs.get('power',""))
        elif(name == 'Sprite'):
            self.__currentType = attrs.get('type',"")
            self.__currentSpriteSpeed = attrs.get('speed',str(FPS))
            self.__currentSprite=[]
        elif(name == 'Frame'):
            self.__image=''
            self.__frame=True
        elif(name == "Script"):
            self.__scriptpath=''
            self.__scripttype=attrs.get('type',"")
            self.__isscript=True
    def endElement(self, name):
        if(name == 'Frame'):
            self.__frame=False
            self.__image = os.path.abspath(os.path.join(os.path.dirname(self.__fichier),self.__image))
            print((self.__image))
            self.__currentSprite.append(self.__image)
        if(name == 'Sprite'):
            if(self.__currentType == 'IDLE'):
                self.__idle=Sprite(self.__currentSprite,self.__screen,self.__currentSpriteSpeed)
            elif(self.__currentType == 'RIGHT'):
                self.__right=Sprite(self.__currentSprite,self.__screen,self.__currentSpriteSpeed)
            elif(self.__currentType == 'LEFT'):
                self.__left=Sprite(self.__currentSprite,self.__screen,self.__currentSpriteSpeed)
        if(name == 'Script'):
            self.__scripts[self.__scripttype]=os.path.abspath(os.path.join(os.path.dirname(self.__fichier),self.__scriptpath))
            self.__isscript=False
    def characters(self, chrs):
        if(self.__frame):
            self.__image+=chrs
        if(self.__isscript):
            self.__scriptpath+=chrs
    def endDocument(self):
        self.perso = Personnage(self.__name,self.__idle,self.__right,self.__left,self.__scripts)
class ScriptHandler(ContentHandler):
    def __init__(self,screen,fichier):
        self.__screen=screen
        self.__fichier=fichier
        self.__sprites=[]
        self.__sons=[]
        self.__frame=False
        self.__son = False
    def startElement(self, name, attrs):
        if(name == 'Sprite'):
            self.__currentType = attrs.get('type',"")
            self.__currentSpriteSpeed = attrs.get('speed',str(FPS))
            self.__currentSprite=[]
        elif(name == 'Frame'):
            self.__image=''
            self.__frame=True
        elif(name == 'Son'):
            self.__son=True
            self.__sound=''
    def endElement(self, name):
        if(name == 'Frame'):
            self.__frame=False
            self.__image = os.path.abspath(os.path.join(os.path.dirname(self.__fichier),self.__image))
            self.__currentSprite.append(self.__image)
        if(name == 'Sprite'):
            self.__sprites.append(Sprite(self.__currentSprite,self.__screen,self.__currentSpriteSpeed))
        if(name == 'Son'):
            self.__son=False
            self.__sound = os.path.abspath(os.path.join(os.path.dirname(self.__fichier),self.__sound))
            self.__sons.append(get_sound(self.__sound))
    def characters(self, chrs):
        if(self.__frame):
            self.__image+=chrs
        elif(self.__son):
            self.__sound+=chrs
    def endDocument(self):
        self.script = (self.__sprites,self.__sons)

class MonsterHandler(ContentHandler):
    def __init__(self,screen,fichier):
        self.__screen=screen
        self.__fichier=fichier
        self.monsters=[]
        self.__normal=None
        self.__gonfle=None
        self.__chain=None
        self.__begin=None
        self.__explosion=None
        self.__explosSound=None
        self.__isExplosSound=False
        self.__frame=False
    def startElement(self, name, attrs):
        if(name == 'Monster'):
            self.__currentMonster = Monster(int(attrs.get('size',"")),1,None,None,None)
            self.__currentSprites=[None,None,None,None,None]
            self.__currentGonfles=[None,None,None,None,None]
        elif(name == 'ExplosionSound'):
            self.__isExplosSound=True
            self.__explosSound=''
        elif(name == 'Normal'):
            self.__currentSpriteSpeed = attrs.get('speed',str(FPS))
            self.__currentSprite=[]
        elif(name == 'Gonfle'):
            self.__currentSpriteSpeed = attrs.get('speed',str(FPS))
            self.__currentSprite=[]
        elif(name == 'Chain'):
            self.__currentSpriteSpeed = attrs.get('speed',str(FPS))
            self.__currentSprite=[]
        elif(name == 'Begin'):
            self.__currentSpriteSpeed = attrs.get('speed',str(FPS))
            self.__currentSprite=[]
        elif(name == 'Color'):
            self.__colorv=int(attrs.get('value',""))
        elif(name == 'Frame'):
            self.__image=''
            self.__frame=True
    def endElement(self, name):
        if(name == 'Frame'):
            self.__frame=False
            self.__image = os.path.abspath(os.path.join(os.path.dirname(self.__fichier),self.__image))
            self.__currentSprite.append(self.__image)
        if(name == 'Monster'):
            self.__currentMonster.sprites =  self.__currentSprites
            self.__currentMonster.gonfle = self.__currentGonfles
            self.__currentMonster.explosion = self.__explosion
            self.__currentMonster.setColor(1)
            self.monsters.append(self.__currentMonster)
        if(name == 'ExplosionSound'):
            self.__isExplosSound=False
        if(name == 'Explosion'):
            self.__explosSound = os.path.abspath(os.path.join(os.path.dirname(self.__fichier),self.__explosSound))
            self.__explosion = Explosion(self.__begin,self.__normal,self.__chain,get_sound(self.__explosSound))
        if(name == 'Gonfle'):
            self.__gonfle=Sprite(self.__currentSprite,self.__screen,self.__currentSpriteSpeed)
        if(name == 'Chain'):
            self.__chain=Sprite(self.__currentSprite,self.__screen,self.__currentSpriteSpeed)
        if(name == 'Begin'):
            self.__begin=Sprite(self.__currentSprite,self.__screen,self.__currentSpriteSpeed)
        if(name == 'Normal'):
            self.__normal=Sprite(self.__currentSprite,self.__screen,self.__currentSpriteSpeed)
        if(name == 'Color'):
            self.__currentSprites[self.__colorv-1] = self.__normal
            self.__currentGonfles[self.__colorv-1] = self.__gonfle
    def characters(self, chrs):
        if(self.__frame):
            self.__image+=chrs
        if(self.__isExplosSound):
            self.__explosSound+=chrs
class FormationHandler(ContentHandler):
    def __init__(self,stage,fichier):
        self.__stage=stage
        self.__fichier=fichier
        self.__monsters=[]
        self.__splines=[]
        self.__spline=False
    def startElement(self, name, attrs):
        if(name == 'Monster'):
            self.__currentMonster = self.__stage.getNewMonster(int(attrs.get('size',"")),int(attrs.get('color',"")))
        elif(name == 'Spline'):
            self.__splinePath=''
            self.__spline=True
    def endElement(self, name):
        if(name == 'Spline'):
            self.__spline=False
            self.__splinePath = os.path.abspath(os.path.join(os.path.dirname(self.__fichier),self.__splinePath))
            self.__splines = self.__splines + parse(self.__splinePath)
        if(name == 'Monster'):
            self.__monsters.append(self.__currentMonster)            
    def characters(self, chrs):
        if(self.__spline):
            self.__splinePath+=chrs
    def endDocument(self):
        self.formation = Formation(self.__monsters,self.__splines)
def getFormationFromXML(fichier,stage):
    handler = FormationHandler(stage,fichier)
    parser = make_parser()
    parser.setContentHandler(handler)
    parser.parse(open(fichier))
    return handler.formation
def getMonstersFromXML(fichier,screen):
    handler = MonsterHandler(screen,fichier)
    parser = make_parser()
    parser.setContentHandler(handler)
    parser.parse(open(fichier))
    return handler.monsters
def getScriptFromXML(fichier,screen):
    handler = ScriptHandler(screen,fichier)
    parser = make_parser()
    parser.setContentHandler(handler)
    parser.parse(open(fichier))
    return handler.script
def getPersoFromXML(fichier,screen):
    handler = PersonnageHandler(screen,fichier)
    parser = make_parser()
    parser.setContentHandler(handler)
    parser.parse(open(fichier))
    return handler.perso
def getStageFromXML(fichier,screen):
    handler = StageHandler(screen,fichier)
    parser = make_parser()
    parser.setContentHandler(handler)
    parser.parse(open(fichier))
    return handler.stage