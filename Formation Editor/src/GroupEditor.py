# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'G:\Twinkle Star Sprites\NTSSP\Formation Editor\src\GroupEditor.ui'
#
# Created: Sun Aug 09 18:15:20 2009
#      by: PyQt4 UI code generator 4.6-snapshot-20090808
#
# WARNING! All changes made in this file will be lost!

from PyQt4 import QtCore, QtGui

class Ui_GroupEditor(object):
    def setupUi(self, GroupEditor):
        GroupEditor.setObjectName("GroupEditor")
        GroupEditor.resize(704, 610)
        self.verticalLayout = QtGui.QVBoxLayout(GroupEditor)
        self.verticalLayout.setObjectName("verticalLayout")
        self.PreviewArea = QtGui.QScrollArea(GroupEditor)
        self.PreviewArea.setVerticalScrollBarPolicy(QtCore.Qt.ScrollBarAlwaysOff)
        self.PreviewArea.setHorizontalScrollBarPolicy(QtCore.Qt.ScrollBarAlwaysOff)
        self.PreviewArea.setWidgetResizable(True)
        self.PreviewArea.setObjectName("PreviewArea")
        self.scrollAreaWidgetContents = QtGui.QWidget(self.PreviewArea)
        self.scrollAreaWidgetContents.setGeometry(QtCore.QRect(0, 0, 684, 559))
        self.scrollAreaWidgetContents.setObjectName("scrollAreaWidgetContents")
        self.PreviewArea.setWidget(self.scrollAreaWidgetContents)
        self.verticalLayout.addWidget(self.PreviewArea)
        self.buttonBox = QtGui.QDialogButtonBox(GroupEditor)
        self.buttonBox.setOrientation(QtCore.Qt.Horizontal)
        self.buttonBox.setStandardButtons(QtGui.QDialogButtonBox.Cancel|QtGui.QDialogButtonBox.Ok)
        self.buttonBox.setObjectName("buttonBox")
        self.verticalLayout.addWidget(self.buttonBox)

        self.retranslateUi(GroupEditor)
        QtCore.QObject.connect(self.buttonBox, QtCore.SIGNAL("accepted()"), GroupEditor.accept)
        QtCore.QObject.connect(self.buttonBox, QtCore.SIGNAL("rejected()"), GroupEditor.reject)
        QtCore.QMetaObject.connectSlotsByName(GroupEditor)

    def retranslateUi(self, GroupEditor):
        GroupEditor.setWindowTitle(QtGui.QApplication.translate("GroupEditor", "Group Editor", None, QtGui.QApplication.UnicodeUTF8))

