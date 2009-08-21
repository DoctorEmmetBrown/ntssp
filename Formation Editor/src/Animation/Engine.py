# -*- coding: utf8 -*-
from Data.Enemy import *
from Data.EnemyPath import *
from Data.Data import *
from Animation.Effects import *

class Engine:
    def __init__(self,groups):
        GroupEffect.types = {EffectType.Zero : NormalEffect, EffectType.Circle : CircleEffect, EffectType.Switch : SwitchEffect, EffectType.Arc : ArcEffect,EffectType.Fixed : FixedEffect};
        self.effects = {};
        self.groups = [];
        for g in groups:
            self.groups.append(g.clone());
        self.__change = True;
    def EndEvent(self,obj):
        self.__change = True;
        self.groups.remove(obj.group);
    def EndEnemyEvent(self,obj):
        self.__change = True;
    def getCurrentGroups(self):
        groups = self.groups[:];
        nb = len(self.groups);
        i = 0;
        while(i < len(groups)):
            j = i+1;
            while(j < len(groups)):
                if(self.isDependent(groups[i],groups[j])):
                   groups.pop(j);
                else:
                    j = j+1;
            i = i + 1;
        return groups;
    def isDependent(self,g,g2):
        for e in g.enemies:
            if(g2.ContainsEnemy(e)):
                return True;
        return False;
    def GlobalAnimation(self):
        if(self.__change):
            self.currentGroups = self.getCurrentGroups();
            self.__change = False;
        for g in self.currentGroups:
            self.__animateGroup(g);
        if(len(self.currentGroups) == 0):
            return False;
        return True;
    def __animateGroup(self,group):
        if(group not in self.effects):
            self.effects[group] = GroupEffect.getEffect(group);
            self.effects[group].AttachEvent(self);
        effect = self.effects[group];
        effect.animate();
        
        