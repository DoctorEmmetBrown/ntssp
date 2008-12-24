# -*- coding: utf8 -*-
from PyQt4 import QtCore, QtGui
from PyQt4.QtGui import *
from PyQt4.QtCore import *
from FormationEditor import *
from PathStroke import *
from Data.Data import *;
from Data.Enemy import *;
from Animation.Engine import *;
from GroupEditorWindow import *;
class FormationEditorWindow(QtGui.QMainWindow):

    def __init__(self):
        QtGui.QMainWindow.__init__(self);
        #Import UI from Designer
        self.ui = Ui_MainWindow();
        
        #SetupUI
        self.ui.setupUi(self);
        
        #PathStrokeRender
        self.m_renderer = PathStrokeRenderer(self);
        
        #Add to PreviewArea
        self.ui.PreviewArea.setWidget(self.m_renderer);
        
        #init
        self.monsterDialog = None;
        
        #Connections :
        #Path Group:
        QObject.connect(self.ui.Path_Add,SIGNAL("clicked(bool)"),self.AddPath);
        QObject.connect(self.ui.Path_Delete,SIGNAL("clicked(bool)"),self.DeletePath);
        QObject.connect(self.ui.Curve,SIGNAL("clicked(bool)"),self.Curve);
        QObject.connect(self.ui.Line,SIGNAL("clicked(bool)"),self.Line);
        
        #Pen Width
        QObject.connect(self.ui.PenWidthSlider, SIGNAL("valueChanged(int)"),self.m_renderer.setPenWidth);

        #Global Group:
        QObject.connect(self.ui.Animate,SIGNAL("clicked(bool)"),self.Animate);
        QObject.connect(self.ui.Save, SIGNAL("clicked(bool)"),self.Save);
        QObject.connect(self.ui.Load, SIGNAL("clicked(bool)"),self.Load);
        
        #Groups Group:
        QObject.connect(self.ui.Groups_Add,SIGNAL("clicked(bool)"),self.AddGroup);
        QObject.connect(self.ui.Groups_Change, SIGNAL("clicked(bool)"),self.ChangeGroup);
        QObject.connect(self.ui.Groups_Delete, SIGNAL("clicked(bool)"),self.DeleteGroup);
        QObject.connect(self.ui.Group_Edit, SIGNAL("clicked(bool)"),self.EditGroup);
        QObject.connect(self.ui.Formation_GroupDiffTime,SIGNAL("sliderMoved(int)"),self.GroupDiffTimeChange);
        QObject.connect(self.ui.Formation_GroupSpeed,SIGNAL("sliderMoved(int)"),self.GroupSpeedChange);
        
        #Monsters Group:
        QObject.connect(self.ui.Monsters_Add,SIGNAL("clicked(bool)"),self.AddMonster);
        QObject.connect(self.ui.Monsters_Change, SIGNAL("clicked(bool)"),self.ChangeMonster);
        QObject.connect(self.ui.Monsters_Delete,SIGNAL("clicked(bool)"),self.DeleteMonster);
        QObject.connect(self.ui.Monster_Color,SIGNAL("sliderMoved(int)"),self.MonsterColorChange);
        QObject.connect(self.ui.Monster_Size,SIGNAL("sliderMoved(int)"),self.MonsterSizeChange);
        
        #Association Group:
        QObject.connect(self.ui.Association_Associate,SIGNAL("clicked(bool)"),self.Associate);
        QObject.connect(self.ui.Association_TimeBefore,SIGNAL("sliderMoved(int)"),self.TimeBeforeChange);
        
        #Time Tab:
        QObject.connect(self.ui.Time_GroupsList,SIGNAL("itemSelectionChanged()"),self.TimeCurrentGroupChange);
        QObject.connect(self.ui.Time_OrderUp,SIGNAL("clicked(bool)"),self.TimeOrderCurrentUp);
        QObject.connect(self.ui.Time_OrderDown,SIGNAL("clicked(bool)"),self.TimeOrderCurrentDown);
        QObject.connect(self.ui.Time_GroupUp,SIGNAL("clicked(bool)"),self.TimeGroupUp);
        QObject.connect(self.ui.Time_GroupDown,SIGNAL("clicked(bool)"),self.TimeGroupDown);
        QObject.connect(self.ui.Time_Delete,SIGNAL("clicked(bool)"),self.TimeDelete);
        
    
    #TimeTab:
    def TimeCurrentGroupChange(self):
        group = self.GetGroupSelected(self.ui.Time_GroupsList);
        if(len(group) > 0):
            self.UpdateCurrentTimeOrder(group[0]);
    def TimeOrderCurrentUp(self):
        group = self.GetGroupSelected(self.ui.Time_GroupsList);
        path = self.TimeCurrentGetPathSelected();
        if(path is not None):
            group[0].UpPath(path);
            self.TimeCurrentGroupChange();
            self.ui.Time_GroupOrder.setCurrentItem(self.ui.Time_GroupOrder.topLevelItem(group[0].paths.index(path)));
        
    def TimeOrderCurrentDown(self):
        group = self.GetGroupSelected(self.ui.Time_GroupsList);
        path = self.TimeCurrentGetPathSelected();
        if(len(group) > 0 and path is not None):
            group[0].DownPath(path);
            self.TimeCurrentGroupChange();
            self.ui.Time_GroupOrder.setCurrentItem(self.ui.Time_GroupOrder.topLevelItem(group[0].paths.index(path)));
            
    def TimeGroupUp(self):
        group = self.GetGroupSelected(self.ui.Time_GroupsList);
        if(len(group) > 0):
            i = SData.getInstance().GroupUp(group[0]);
            self.UpdateGroups();
            self.ui.Time_GroupsList.setCurrentItem(self.ui.Time_GroupsList.topLevelItem(i));
    def TimeGroupDown(self):
        group = self.GetGroupSelected(self.ui.Time_GroupsList);
        if(len(group) > 0):
            i = SData.getInstance().GroupDown(group[0]);
            self.UpdateGroups();
            self.ui.Time_GroupsList.setCurrentItem(self.ui.Time_GroupsList.topLevelItem(i));    
    def TimeCurrentGetPathSelected(self):
        group = self.GetGroupSelected(self.ui.Time_GroupsList);
        if(len(group) > 0):
            inds = self.ui.Time_GroupOrder.selectedIndexes();
            if(len(inds) > 0):
                index = inds[0].row();
                return group[0].paths[index];
        return None;
    def TimeDelete(self):
        path = self.TimeCurrentGetPathSelected();
        if(path is not None):
            SData.getInstance().deleteEnemyPath(path);
            self.UpdateGroups();
    def UpdateCurrentTimeOrder(self,current):
        self.ui.Time_GroupOrder.clear();
        for i in current.paths:
            if(current.assoc[i].type == Group.WhenPlayerIsFound):
                continue;
            item = self.GetPathUI(i);
            self.ui.Time_GroupOrder.addTopLevelItem(item);
    
    #Association Group:
    def Associate(self):
        group = self.GetGroupSelected(self.ui.Association_GroupsList);
        path = self.GetPathSelected(self.ui.Association_PathsList);
        if(len(group) > 0 and len(path) > 0):
            group[0].AddPath(path[0],self.ui.Association_Type.currentIndex(),self.ui.Association_TimeBefore.value()/10.0);
        self.UpdateGroups();
        self.UpdatePaths();
    def TimeBeforeChange(self,value):
        self.ui.Association_TimeLabel.setText("Time Before: " + str(value/10.0)+"s");
    
    #Monsters Group
    def AddMonster(self):
        size = self.ui.Monster_Size.value();
        color = self.ui.Monster_Color.value();
        pos = Point(100,100);
        SData.getInstance().newMonster(color,size, pos);
        self.UpdateMonsters();
    def ChangeMonster(self):
        selected = self.GetMonsterSelectedIndexes();
        if(len(selected) == 1):
            monster = SData.getInstance().getMonster(selected[0]);
            size = self.ui.Monster_Size.value();
            color = self.ui.Monster_Color.value();
            monster.life = color;
            monster.size = size;
            self.UpdateMonsters();
    def DeleteMonster(self):
        selected = self.GetMonsterSelectedIndexes();
        for i in selected :
            SData.getInstance().deleteMonster(i);
        self.UpdateMonsters();
        self.UpdateGroups();
            
    def MonsterColorChange(self,value):
        self.ui.Monster_ColorLabel.setText("Color : " + str(value));
    def MonsterSizeChange(self,value):
        self.ui.Monster_SizeLabel.setText("Size : " + str(value));
    def GetMonsterSelected(self):
        selected = [];
        items = self.ui.Formation_MonstersList.selectedIndexes();
        for i in items:
            if(i.column() == 0):
                selected.append(SData.getInstance().getMonster(i.row()));
        return selected;
    def GetMonsterSelectedIndexes(self):
        selected = [];
        items = self.ui.Formation_MonstersList.selectedIndexes();
        for i in items:
            if(i.column() == 0):
                selected.append(i.row());
        return selected;
    def UpdateMonsters(self):
        self.ui.Formation_MonstersList.clear();
        for i in xrange(SData.getInstance().getNbMonster()):
            self.ui.Formation_MonstersList.addTopLevelItem(self.GetMonsterUI(SData.getInstance().getMonster(i)));
    def GetMonsterUI(self,monster):
        item = QTreeWidgetItem();
        item.setText(0,"Monster (" + str(monster.id) + ")");
        item.setText(1,"Life : "+str(monster.life) +", Size : "+str(monster.size))
        return item;    
    
    
    #Groups Group
    def EditGroup(self):
        selected = self.GetGroupSelected(self.ui.Formation_GroupsList);
        if(len(selected)>0):
            dialog = GroupEditorWindow(selected[0]);
            dialog.exec_();
    def GroupDiffTimeChange(self,i):
        self.ui.Formation_GroupDiffTimeLabel.setText("DiffTime: "+str(i/10.0) +"s");
    def GroupSpeedChange(self,i):
        self.ui.Formation_GroupSpeedLabel.setText("Speed: "+str(i/10.0));
    def AddGroup(self):
        enemies = self.GetMonsterSelected();
        if(len(enemies) > 0):
            speed = self.ui.Formation_GroupSpeed.value() / 10.0;
            diffTime = self.ui.Formation_GroupDiffTime.value() / 10.0;
            type = self.ui.Formation_GroupEffectStyle.currentIndex();
            SData.getInstance().newGroup(enemies,type,speed,diffTime);
            self.UpdateGroups()
    def GetGroupSelected(self,list):
        selected = [];
        items = list.selectedIndexes();
        for i in items:
            if(i.column() == 0):
                selected.append(SData.getInstance().getGroup(i.row()));
        return selected;
    def GetGroupSelectedIndexes(self,list):
        selected = [];
        items = list.selectedIndexes();
        for i in items:
            if(i.column() == 0):
                selected.append(i.row());
        return selected;
    def ChangeGroup(self):
        selected = self.GetGroupSelected(self.ui.Formation_GroupsList);
        if(len(selected) == 1):
            selected[0].ClearEnemies();
            monsters = self.GetMonsterSelected();
            for e in monsters:
                selected[0].AddEnemy(e);
            self.UpdateGroups();
            
    def DeleteGroup(self):
        selected = self.GetGroupSelectedIndexes(self.ui.Formation_GroupsList);
        for i in selected :
            SData.getInstance().deleteGroup(i);
        self.UpdateGroups();
        self.UpdatePaths();
    def UpdateGroups(self):
        formsel = self.GetGroupSelected(self.ui.Formation_GroupsList);
        assocsel = self.GetGroupSelected(self.ui.Association_GroupsList);
        timesel = self.GetGroupSelected(self.ui.Time_GroupsList);
        self.ui.Formation_GroupsList.clear();
        self.ui.Association_GroupsList.clear();
        self.ui.Time_GroupsList.clear();
        for i in xrange(SData.getInstance().getNbGroups()):
            group = SData.getInstance().getGroup(i);
            item = self.GetGroupUI(group);
            self.ui.Formation_GroupsList.addTopLevelItem(item);
            if(len(formsel)>0 and formsel[0] is group):
                self.ui.Formation_GroupsList.setCurrentItem(item);
                
            item = self.GetGroupUI(group);
            self.ui.Association_GroupsList.addTopLevelItem(item);
            if(len(assocsel)>0 and assocsel[0] is group):
                self.ui.Association_GroupsList.setCurrentItem(item);
                
            item = self.GetGroupUI(group);
            self.ui.Time_GroupsList.addTopLevelItem(item);
            if(len(timesel)>0 and timesel[0] is group):
                self.ui.Time_GroupsList.setCurrentItem(item);
    def GetGroupUI(self,group):
        item = QTreeWidgetItem();
        item.setText(0,"Group (" + str(group.id) +")");
        item.setText(1,"Type:" + EffectType.getText(group.type) + " Speed:" + str(group.speed) + " DiffTime:" + str(group.diffTime))
        for monster in group.enemies:
           uimonster = self.GetMonsterUI(monster);
           item.addChild(uimonster);
        for path in group.paths:
            g = QTreeWidgetItem();
            if(group.assoc[path].type == Group.End):
                text = "When End";
            else:
                text = "When Player is Found"
            g.setText(0,"Path (" + str(path.id) +")");
            g.setText(1,text);
            item.addChild(g);
        return item;   
    
    #Path Group
    def AddPath(self):
        path = SData.getInstance().newEnemyPath();   
        self.m_renderer.currentPath = path;
        self.m_renderer.update(); 
        self.UpdatePaths();
    def DeletePath(self):
        current = self.m_renderer.getCurrentPath();
        SData.getInstance().deleteEnemyPath(current);
        self.m_renderer.update();
        self.UpdatePaths();
        self.UpdateGroups();
    def Curve(self):
        self.ui.Line.setChecked(False);
        self.m_renderer.setCurveLine(True);
    def Line(self):
        self.ui.Curve.setChecked(False);
        self.m_renderer.setCurveLine(False);
    def UpdatePaths(self):
        assocSelected = self.GetPathSelected(self.ui.Association_PathsList);
        self.ui.Association_PathsList.clear();
        for i in xrange(SData.getInstance().getNbEnemyPath()):
            path = SData.getInstance().getEnemyPath(i);
            item = self.GetPathUI(path);
            self.ui.Association_PathsList.addTopLevelItem(item);
            if(len(assocSelected)>0 and assocSelected[0] is path):
                self.ui.Association_PathsList.setCurrentItem(item);
                
            
    def GetPathUI(self,path):
        item = QTreeWidgetItem();
        item.setText(0,"Path (" + str(path.id) +")");
        groups = SData.getInstance().getGroupsForPath(path);
        for group in groups:
            g = QTreeWidgetItem();
            g.setText(0,"Group (" + str(group.id) +")");
            item.addChild(g);
        return item;  
    def GetPathSelected(self,list):
        selected = [];
        items = list.selectedIndexes();
        for i in items:
            if(i.column() == 0):
                selected.append(SData.getInstance().getEnemyPath(i.row()));
        return selected;
    def GetPathSelectedIndexes(self,list):
        selected = [];
        items = list.selectedIndexes();
        for i in items:
            if(i.column() == 0):
                selected.append(i.row());
        return selected;
    #Pen Width
    def SetPenWidth(self,val):
        pass
    
    #GlobalGroup
    def Animate(self):
        self.m_renderer.Animation = True;
        self.m_renderer.AnimEngine = Engine(SData.getInstance().getGroups());
        self.timer = QTimer();
        QObject.connect(self.timer,SIGNAL("timeout()"),self.m_renderer.repaint);
        self.timer.start(200);
    def Save(self):
        file = QFileDialog.getSaveFileName(self,"Save File","","Formation File (*.fsz)");
        if file.length() > 0:
            SData.getInstance().Save(file);
    
    def RefreshUI(self):
        self.UpdatePaths();
        self.UpdateGroups();
        self.UpdateMonsters();
    def Load(self):
        file = QFileDialog.getOpenFileName(self,"Save File","","Formation File (*.fsz)");
        if file.length() > 0:
            SData.getInstance().Load(file);
            self.m_renderer.repaint();
            self.RefreshUI();
        