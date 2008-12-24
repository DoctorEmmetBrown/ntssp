import Enemy;
from EnemyPath import *;
import pickle;

class Data:
    #Singleton
    def __init__(self):
        #Data members
        self._groups = [];
        self._paths = [];
        self._currentFormation = None;
        self._monsters = [];
    def getMonster(self,i):
        if(i>=0 and i<len(self._monsters)):
           return self._monsters[i];
        return None;
    def newMonster(self,life,size,position):
        monster = Enemy.Enemy(life,size,position);
        self._monsters.append(monster);
    
    def deleteMonster(self,index):
        monster = self._monsters.pop(index);
        groups = self.getGroupsForEnemy(monster);
        for group in groups:
            groups.DeleteEnemy(monster);
    
    def getNbMonster(self):
        return len(self._monsters);
    def newGroup(self,enemies,effectType,speed,diffTime):
        group = Enemy.Group(effectType,speed,diffTime);
        for e in enemies:
            group.AddEnemy(e);
        self._groups.append(group);
        return group;
    def deleteGroup(self,i):
        self._groups.pop(i);
    #TODO change this
    def getGroups(self):
        return self._groups;
    def getGroup(self,i):
        if(i >= 0 and i < len(self._groups)):
           return self._groups[i];
        else:
            return None;
    
    def getNbGroups(self):
        return len(self._groups);
    
    def setCurrentFormation(self,i):
        self._currentFormation = self.getFormation(i);
        
    def getCurrentFormation(self):
        return self._currentFormation;
    
    def newEnemyPath(self):
        path = EnemyPath();
        self._paths.append(path);
        return path;
    def getGroupsForEnemy(self,enemy):
        groups=[];
        for group in self._groups:
            if(groups.ContainsEnemy(enemy)):
                groups.append(group);
        return groups;
    def getGroupsForPath(self,path):
        groups=[];
        for group in self._groups:
            if(group.ContainsPath(path)):
                groups.append(group);
        return groups;
    def GroupUp(self,group):
        index = self._groups.index(group);
        if(index > 0):
            tmp = self._groups[index-1];
            self._groups[index-1] = group;
            self._groups[index] = tmp;
            return index - 1;
        return index;
    def GroupDown(self,group):
        index = self._groups.index(group);
        if(index < self.getNbGroups()-1):
            tmp = self._groups[index+1];
            self._groups[index+1] = group;
            self._groups[index] = tmp;
            return index + 1;
        return index;
    def getEnemyPath(self,index):
        if(index >= 0 and index < len(self._paths)):
            return self._paths[index];
        return None;
    def getNbEnemyPath(self):
        return len(self._paths);
    def deleteEnemyPathByIndex(self,index):
        path = self._paths.pop(index);
        groups = self.getGroupsForPath(path);
        for group in groups:
            group.DeletePath(path);
    def deleteEnemyPath(self,path):
        self._paths.remove(path);
        groups = self.getGroupsForPath(path);
        for group in groups:
            group.DeletePath(path);
    def Save(self,file):
        sauve = pickle.dumps(self);
        fichier = open(file,'w');
        fichier.write(sauve);
    
    def Load(self,file):
        fichier = open(file,'r');
        readed = fichier.read();
        read = pickle.loads(readed);
        self._groups = read._groups;
        self._paths = read._paths;
        self._currentFormation = read._currentFormation;
        self._monsters = read._monsters;
    
class SData(Data,object):
    #Singleton
    def __init__(self):
        Data.__init__(self);
        
    @classmethod
    def __new__(cls,*args,**kwargs):
        if '_inst' not in vars(cls):
            cls._inst = object.__new__(cls,*args,**kwargs);
        return cls._inst;
    @classmethod
    def getInstance(cls):
        if '_inst' not in vars(cls):
            cls._inst = SData();
        return cls._inst;