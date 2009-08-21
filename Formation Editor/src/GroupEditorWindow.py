# -*- coding: utf8 -*-
from PyQt4 import QtCore, QtGui
from PyQt4.QtGui import *
from PyQt4.QtCore import *
from GroupEditor import *
from Data.Enemy import *
class GroupEditorWidget(QWidget):
    ENEMY_BASE_SIZE = 30;
    ENEMY_COLOR = [Qt.red,Qt.yellow,Qt.green,Qt.blue,QColor(255.0,0.0,255.0)];
    def __init__(self,parent,group):
        QWidget.__init__(self,parent)
        self.group = group;
        self.currentMonster = None;
    def paintEvent(self,e):
        static_image = 0;
        painter = QPainter();
        static_image = QImage(self.size(), QImage.Format_RGB32);
        painter.begin(static_image);
        
        painter.setRenderHint(QPainter.Antialiasing);
        # client painting
    
        self.paint(painter);
    
        painter.end();
        painter.begin(self);
        painter.drawImage(e.rect(), static_image, e.rect());
    
    def paint(self,painter):
        
        #draw background
        brush = QBrush(QColor(255, 255, 255, 255));
        painter.fillRect(self.rect(),brush);

        #get center
        centerX = (self.rect().x() + self.rect().width())/2;
        centerY = (self.rect().y() + self.rect().height())/2;
        
        #draw monsters
        for e in self.group.enemies:
            pos = self.group.GetEnemyPos(e);
            painter.setPen(GroupEditorWidget.ENEMY_COLOR[e.life]);
            painter.setBrush(GroupEditorWidget.ENEMY_COLOR[e.life]);
            painter.drawEllipse(QRect(centerX + pos[0],centerY + pos[1],GroupEditorWidget.ENEMY_BASE_SIZE * e.size,GroupEditorWidget.ENEMY_BASE_SIZE * e.size));
        
        #Draw Center
        painter.setPen(QColor(255, 0, 0, 200));
        painter.setBrush(QColor(255, 0, 0, 120));
        painter.drawEllipse(QRectF(centerX,centerY,10,10));
    def mousePressEvent(self,event):
        
        centerX = (self.rect().x() + self.rect().width())/2;
        centerY = (self.rect().y() + self.rect().height())/2;
        
        distance = -1;
        currentMonster = None;
        for e in self.group.enemies:
            pos = self.group.GetEnemyPos(e);
            d = QLineF(event.pos().x(),event.pos().y(),centerX + pos[0], centerY + pos[1]).length();
            if ((distance < 0 and d < GroupEditorWidget.ENEMY_BASE_SIZE* e.size ) or d < distance):
                currentMonster = e
                distance = d;
        
        if(distance != -1):
            self.currentMonster=currentMonster;
            self.mouseMoveEvent(event);
    def mouseReleaseEvent(self,e):
        self.currentMonster = None;
        
    def mouseMoveEvent(self,e):
        centerX = (self.rect().x() + self.rect().width())/2;
        centerY = (self.rect().y() + self.rect().height())/2;
        if (self.currentMonster is not None):
            size = GroupEditorWidget.ENEMY_BASE_SIZE* self.currentMonster.size / 2.0
            self.group.SetEnemyPos(self.currentMonster,(e.pos().x() - size) - centerX, (e.pos().y()-size)-centerY);
            self.update();
            
class GroupEditorWindow(QtGui.QDialog):
    def __init__(self,group):
        QtGui.QDialog.__init__(self);
        #Import UI from Designer
        self.ui = Ui_GroupEditor();
        
        #SetupUI
        self.ui.setupUi(self);
        
        self.ui.PreviewArea.setWidget(GroupEditorWidget(self,group));
        
        
        