# -*- coding: utf8 -*-
'''
Created on 16 ao�t 2009

@author: Syph
'''

from Data.Enemy import *
from Data.EnemyPath import *
from Data.Data import *

class GroupEffect:
    types = {};
    def __init__(self,group):
        self.group = group;
        self.listener = [];
    def getName(self):
        return "Base";
    def animate(self):
        return False;
    def getGroup(self):
        return self.group;
    @classmethod
    def getEffect(cls,group):
        return cls.types[group.type](group);
    def AttachEvent(self,obj):
        self.listener.append(obj);
    def SendEndEvent(self):
        for l in self.listener:
            l.EndEvent(self);
    def SendEndEnemyEvent(self):
        for l in self.listener:
            l.EndEnemyEvent(self);
        

FPS = 30.0
class NormalEffect(GroupEffect):
    def __init__(self,group):
        GroupEffect.__init__(self, group);
        if(len(group.paths) > 0):
            for e in self.group.enemies:
                e.__currentPath = self.group.paths[0];
                e.__currentPath.BezierSpline.update();
                e.__currentPathPercent = 0;
                e.__currentPathIndex = 0;
            self.enemiesIndex = 0;
            
            #TODO change for the length of the path
            self.nbPoints = int(1000.0 / group.speed);
            self.nbPointsInter = int(group.diffTime * FPS);
            self.globalPoints = 0;
    def getName(self):
        return "Normal";
    def animate(self):
        if(len(self.group.paths)>0):
            if(len(self.group.enemies) ==0):
                #Group End
                self.SendEndEvent();
                return;
            
            #animation loop
            for i in range(self.enemiesIndex+1):
                if(i >= len(self.group.enemies)):
                    break;
                
                #Enemy infos
                e = self.group.enemies[i];
                path = e.__currentPath;
                percent = e.__currentPathPercent;
                
                if(percent == self.nbPoints):
                    e.__currentPathIndex = e.__currentPathIndex + 1;
                    if(e.__currentPathIndex >= len(self.group.paths)):
                        #Delete Enemy
                        self.group.enemies.remove(e);
                        i-=1;
                        
                        #clean enemy
                        del(e.__currentPath);
                        del(e.__currentPathIndex);
                        del(e.__currentPathPercent);
                        self.SendEndEnemyEvent()
                        continue;
                    else:
                        #Next Path
                        e.__currentPath = self.group.paths[e.__currentPathIndex];
                        e.__currentPath.BezierSpline.update();
                        e.__currentPathPercent = 0;
                    
                
                #Get New position
                coord = path.BezierSpline.getPoint(float(percent)/float(self.nbPoints));
                
                #Update Position
                e.x = coord[0];
                e.y = coord[1];
                
                e.__currentPathPercent += 1;
                
                
            #Update Enemies Index;
            self.globalPoints = self.globalPoints +1;
            if(self.globalPoints % self.nbPointsInter == 0):
                self.enemiesIndex = self.enemiesIndex + 1;
class SwitchEffect(GroupEffect):
    SWITCH_SPEED = 10;
    def __init__(self,group):
        GroupEffect.__init__(self, group);
        self.centerX = 0;
        self.centerY = 0;
        if(len(group.paths) > 0):
            self.__currentPath = self.group.paths[0];
            self.__currentPath.BezierSpline.update();
            self.__currentPathPercent = 0;
            self.__currentPathIndex = 0;
            
            for e in self.group.enemies:
                pos = self.group.GetEnemyPos(e);
                e.__line = Line(Point(pos[0],pos[1]),Point(-pos[0],pos[1]));
                e.__t = 0.0;
            
            #TODO change for the length of the path
            self.nbPoints = int(1000.0 / group.speed);
            self.nbPointsInter = int(group.diffTime * FPS);
            self.globalPoints = 0;
    def getName(self):
        return "Switch";
    def animate(self):
        if(len(self.group.paths) > 0):
                #Enemy infos
                path = self.__currentPath;
                percent = self.__currentPathPercent;
                
                if(percent == self.nbPoints):
                    self.__currentPathIndex = self.__currentPathIndex + 1;
                    if(self.__currentPathIndex >= len(self.group.paths)):
                        #Delete Enemy
                        self.group.enemies = [];
                        
                        #clean enemy
                        for e in self.group.enemies:
                            del e.__line;
                            del e.__t;
                        self.SendEndEvent();
                        
                    else:
                        #Next Path
                        self.__currentPath = self.group.paths[self.__currentPathIndex];
                        self.__currentPath.BezierSpline.update();
                        self.__currentPathPercent = 0;
                        
                #Get New position
                coord = path.BezierSpline.getPoint(float(percent) / float(self.nbPoints));
                
                #Update Position
                self.centerX = coord[0];
                self.centerY = coord[1];
                
                self.__currentPathPercent += 1;
                    
                #Update Enemies       
                for e in self.group.enemies:
                    pos = self.group.GetEnemyPos(e);

                    p = e.__line.pointAt(e.__t);
                    x = self.centerX + p.x;
                    y = self.centerY + p.y;
                    
                    e.__t += ArcEffect.ARC_SPEED;
                    
                    if(e.__t >= 1.0):
                        tmp = e.__line.p2;
                        e.__line.p2 = e.__line.p1;
                        e.__line.p1 = tmp;
                        e.__t = 0;
                            
                    e.x = x;
                    e.y = y;

class FixedEffect(GroupEffect):
    def __init__(self,group):
        GroupEffect.__init__(self, group);
        self.centerX = 0;
        self.centerY = 0;
        if(len(group.paths) > 0):
            self.__currentPath = self.group.paths[0];
            self.__currentPath.BezierSpline.update();
            self.__currentPathPercent = 0;
            self.__currentPathIndex = 0;
            
            #TODO change for the length of the path
            self.nbPoints = int(1000.0 / group.speed);
            self.nbPointsInter = int(group.diffTime * FPS);
            self.globalPoints = 0;
    def getName(self):
        return "Switch";
    def animate(self):
        if(len(self.group.paths) > 0):
                #Enemy infos
                path = self.__currentPath;
                percent = self.__currentPathPercent;
                
                if(percent == self.nbPoints):
                    self.__currentPathIndex = self.__currentPathIndex + 1;
                    if(self.__currentPathIndex >= len(self.group.paths)):
                        #Delete Enemy
                        self.group.enemies = [];
                        self.SendEndEvent();
                        
                    else:
                        #Next Path
                        self.__currentPath = self.group.paths[self.__currentPathIndex];
                        self.__currentPath.BezierSpline.update();
                        self.__currentPathPercent = 0;
                        
                #Get New position
                coord = path.BezierSpline.getPoint(float(percent) / float(self.nbPoints));
                
                #Update Position
                self.centerX = coord[0];
                self.centerY = coord[1];
                
                self.__currentPathPercent += 1;
                    
                #Update Enemies       
                for e in self.group.enemies:
                    pos = self.group.GetEnemyPos(e);
                    
                    x = self.centerX + pos[0];
                    y = self.centerY + pos[1];
                            
                    e.x = x;
                    e.y = y;
                    
class ArcEffect(GroupEffect):
    ARC_SPEED = 0.1;
    def __init__(self,group):
        GroupEffect.__init__(self, group);
        self.centerX = 0;
        self.centerY = 0;
        if(len(group.paths) > 0):
            self.__currentPath = self.group.paths[0];
            self.__currentPath.BezierSpline.update();
            self.__currentPathPercent = 0;
            self.__currentPathIndex = 0;
            
            for e in self.group.enemies:
                pos = self.group.GetEnemyPos(e);
                
                e.__line = Line(Point(pos[0],pos[1]),Point(-pos[0],-pos[1]));
                e.__t = 0.0;
                
            
            #TODO change for the length of the path
            self.nbPoints = int(1000.0 / group.speed);
            self.nbPointsInter = int(group.diffTime * FPS);
            self.globalPoints = 0;
    def getName(self):
        return "Arc";
    def animate(self):
        if(len(self.group.paths) > 0):
                #Enemy infos
                path = self.__currentPath;
                percent = self.__currentPathPercent;
                
                if(percent == self.nbPoints):
                    self.__currentPathIndex = self.__currentPathIndex + 1;
                    if(self.__currentPathIndex >= len(self.group.paths)):
                        #Delete Enemy
                        self.group.enemies = [];
                        
                        #clean enemy
                        for e in self.group.enemies:
                            del e.__line;
                            del e.__t;
                        self.SendEndEvent();
                        
                    else:
                        #Next Path
                        self.__currentPath = self.group.paths[self.__currentPathIndex];
                        self.__currentPath.BezierSpline.update();
                        self.__currentPathPercent = 0;
                        
                #Get New position
                coord = path.BezierSpline.getPoint(float(percent) / float(self.nbPoints));
                
                #Update Position
                self.centerX = coord[0];
                self.centerY = coord[1];
                
                self.__currentPathPercent += 1;
                    
                #Update Enemies       
                for e in self.group.enemies:
                    pos = self.group.GetEnemyPos(e);

                    p = e.__line.pointAt(e.__t);
                    x = self.centerX + p.x;
                    y = self.centerY + p.y;
                    
                    e.__t += ArcEffect.ARC_SPEED;
                    
                    if(e.__t >= 1.0):
                        tmp = e.__line.p2;
                        e.__line.p2 = e.__line.p1;
                        e.__line.p1 = tmp;
                        e.__t = 0;
                        
                    e.x = x;
                    e.y = y;
                                                        
class CircleEffect(GroupEffect):
    def __init__(self,group):
        GroupEffect.__init__(self, group);
        self.centerX = 0;
        self.centerY = 0;
        if(len(group.paths) > 0):
            self.__currentPath = self.group.paths[0];
            self.__currentPath.BezierSpline.update();
            self.__currentPathPercent = 0;
            self.__currentPathIndex = 0;
            
            for e in self.group.enemies:
                pos = self.group.GetEnemyPos(e);
                e.__currentR = math.sqrt(pos[0]*pos[0] + pos[1]*pos[1]);
                if(e.__currentR == 0):
                    e.__currentDeg = 0;
                else:
                    e.__currentDeg = math.acos(pos[0]/e.__currentR) % 360;
            
            #TODO change for the length of the path
            self.nbPoints = int(1000.0 / group.speed);
            self.nbPointsInter = int(group.diffTime * FPS);
            self.globalPoints = 0;
    def getName(self):
        return "Circle";
    def animate(self):
        if(len(self.group.paths) > 0):
                #Enemy infos
                path = self.__currentPath;
                percent = self.__currentPathPercent;
                
                if(percent == self.nbPoints):
                    self.__currentPathIndex = self.__currentPathIndex + 1;
                    if(self.__currentPathIndex >= len(self.group.paths)):
                        #Delete Enemy
                        self.group.enemies = [];
                        
                        #clean enemy
                        for e in self.group.enemies:
                            del e.__currentR;
                            del e.__currentDeg;
                        self.SendEndEvent();
                        
                    else:
                        #Next Path
                        self.__currentPath = self.group.paths[self.__currentPathIndex];
                        self.__currentPath.BezierSpline.update();
                        self.__currentPathPercent = 0;
                        
                #Get New position
                coord = path.BezierSpline.getPoint(float(percent) / float(self.nbPoints));
                
                #Update Position
                self.centerX = coord[0];
                self.centerY = coord[1];
                
                self.__currentPathPercent += 1;
                    
                #Update Enemies       
                for e in self.group.enemies:
                    x = self.centerX + e.__currentR * math.cos(e.__currentDeg);
                    y = self.centerY + e.__currentR * math.sin(e.__currentDeg);
                    e.x = x;
                    e.y = y;
                    e.__currentDeg = (e.__currentDeg +  0.1) % 360;
            
            
    