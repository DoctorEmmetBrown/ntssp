# -*- coding: utf8 -*-
#simple Enemy class
class Enemy:
    EnemyID = 0;
    #life (int), size(int), position (2-tuple of int correspond to x,y coord)
    def __init__(self,life,size,position):
        self.id = Enemy.EnemyID;
        Enemy.EnemyID= Enemy.EnemyID +1;
        #Life from 1 to 5 correspond to certain color
        self.life = life;
        
        #Size from 1 to 5
        self.size = size;
        
        #Position
        self.x = position.x;
        self.y = position.y;
        self.groups = [];
        
    def toString(self):
        return "Enemy : " + str(self.life) +", "+str(self.size);
class EffectType:
    Zero = 0;
    Circle = 1;
    Switch = 2;
    Arc = 3;
    Rotate = 4;
    Fixed = 5;
    text = ["Normal","Circle","Switch","Arc","Rotate","Fixed"];
    
    @classmethod 
    def getText(cls,type):
        return cls.text[type];
    
class Association:
    def __init__(self,group,path,type,timeBefore):
        self.group = group;
        self.path = path;
        self.type = type;
        self.timeBefore = timeBefore;
class Group:
    GroupID = 0;
    WhenPlayerIsFound = 1;
    End = 0;
    def __init__(self,effect = EffectType.Zero,speed=1.0,timeBeetweenEnemies=1.0):
        self.type = effect;
        self.speed = speed;
        self.diffTime = timeBeetweenEnemies;
        self.id = Group.GroupID;
        Group.GroupID = Group.GroupID + 1;
        #table of enemies
        self.enemyPos = {};
        self.enemies = [];
        self.assoc = {};
        self.paths = [];
    def SetEnemyPos(self,enemy,x,y):
        if(enemy in self.enemyPos):
            self.enemyPos[enemy] = (x,y);
    def GetEnemyPos(self,enemy):
        if(enemy in self.enemyPos):
            return self.enemyPos[enemy];
        return (0,0);
    def ContainsEnemy(self,enemy):
        return enemy in self.enemies;
    def ContainsPath(self,path):
        return path in self.paths;
    def DeleteEnemy(self,enemy):
        self.enemies.remove(enemy);
        self.enemyPos.pop(enemy);
    def ClearEnemies(self):
        for e in self.enemies:
            self.DeleteEnemy(e);
    def DeletePath(self,path):
        self.paths.remove(path);
        self.assoc.pop(path);
    def AddEnemy(self,enemy):
        self.enemyPos[enemy] = (0,0);
        self.enemies.append(enemy);
    def AddPath(self,path,type,timeBefore):
        self.assoc[path] = Association(self,path,type,timeBefore);
        self.paths.append(path);
    def UpPath(self,path):
        index = self.paths.index(path);
        if(index > 0):
            tmp = self.paths[index-1];
            self.paths[index-1] = path;
            self.paths[index] = tmp;
    def clone(self):
        clone = Group(self.type,self.speed,self.diffTime);
        clone.id = self.id;
        Group.GroupID -= 1;
        clone.enemies = self.enemies[:];
        clone.paths = self.paths[:];
        clone.assoc = self.assoc.copy();
        clone.enemyPos = self.enemyPos.copy();
        return clone;
    def DownPath(self,path):
        index = self.paths.index(path);
        if(index < len(self.paths) - 1):
            tmp = self.paths[index+1];
            self.paths[index+1] = path;
            self.paths[index] = tmp;